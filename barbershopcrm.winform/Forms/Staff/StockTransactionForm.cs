using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class StockTransactionForm : Form
{
    private readonly User _currentUser;

    public StockTransactionForm(User currentUser)
    {
        InitializeComponent();
        _currentUser = currentUser;
        ThemeHelper.ApplyModernGrid(dgvCurrentStock);
        ThemeHelper.ApplyModernGrid(dgvStockHistory);
        PopulateDropdowns();
        LoadStockData();
    }

    private void PopulateDropdowns()
    {
        var items = SqlDataRepository.Instance.GetInventoryItems();
        cmbItem.DataSource = items;
        cmbItem.DisplayMember = "ItemName";
        cmbItem.ValueMember = "Id";

        cmbTxnType.Items.Clear();
        cmbTxnType.Items.AddRange(new object[] { "USED SUPPLY", "STOCK IN", "STOCK OUT", "RESTOCK" });
        cmbTxnType.SelectedIndex = 0;
    }

    private void LoadStockData()
    {
        var items = SqlDataRepository.Instance.GetInventoryItems();
        dgvCurrentStock.DataSource = items.Select(i => new
        {
            i.Id,
            i.ItemName,
            i.Category,
            Available = $"{i.Quantity} {i.Unit}",
            MinAlert = i.MinimumStockLevel,
            Status = i.Status
        }).ToList();

        var history = SqlDataRepository.Instance.GetInventoryTransactions();
        dgvStockHistory.DataSource = history.Select(h => new
        {
            h.Id,
            h.ItemName,
            Type = h.TransactionType,
            Qty = h.Quantity,
            h.RecordedBy,
            Date = h.DateCreated.ToString("yyyy-MM-dd HH:mm"),
            h.Notes
        }).ToList();
    }

    private void btnRecordTransaction_Click(object sender, EventArgs e)
    {
        if (cmbItem.SelectedItem is not InventoryItem selectedItem)
        {
            MessageBox.Show("Please select an inventory item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int qty = (int)numQuantity.Value;
        if (qty <= 0)
        {
            MessageBox.Show("Quantity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string txnType = cmbTxnType.SelectedItem?.ToString() ?? "USED SUPPLY";

        // Prevent negative stock for STOCK OUT / USED SUPPLY
        if ((txnType == "STOCK OUT" || txnType == "USED SUPPLY") && qty > selectedItem.Quantity)
        {
            MessageBox.Show($"Cannot record usage of {qty} {selectedItem.Unit}. Current stock level is only {selectedItem.Quantity} {selectedItem.Unit}.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var stockTxn = new InventoryTransaction
        {
            InventoryItemId = selectedItem.Id,
            ItemName = selectedItem.ItemName,
            TransactionType = txnType,
            Quantity = qty,
            RecordedBy = _currentUser.FullName,
            Notes = txtNotes.Text.Trim()
        };

        SqlDataRepository.Instance.RecordStockTransaction(stockTxn);
        SqlDataRepository.Instance.AddSystemLog("INFO", "InventoryTasks", $"Recorded '{txnType}' for '{selectedItem.ItemName}' (Qty: {qty})", _currentUser.Username);

        MessageBox.Show($"Recorded {txnType} of {qty} {selectedItem.Unit} for '{selectedItem.ItemName}'. Inventory updated automatically.", "Stock Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

        txtNotes.Clear();
        numQuantity.Value = 1;
        LoadStockData();
    }
}

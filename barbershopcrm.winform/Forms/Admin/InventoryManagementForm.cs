using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class InventoryManagementForm : Form
{
    private int _selectedItemId = 0;

    public InventoryManagementForm()
    {
        InitializeComponent();
        ThemeHelper.ApplyModernGrid(dgvInventory);
        PopulateSuppliers();
        LoadInventory();
    }

    private void PopulateSuppliers()
    {
        var suppliers = SqlDataRepository.Instance.GetSuppliers();
        cmbSupplier.DataSource = suppliers;
        cmbSupplier.DisplayMember = "SupplierName";
        cmbSupplier.ValueMember = "Id";
    }

    private void LoadInventory()
    {
        var items = SqlDataRepository.Instance.GetInventoryItems();
        dgvInventory.DataSource = items.Select(i => new
        {
            i.Id,
            i.ItemName,
            i.Category,
            Qty = $"{i.Quantity} {i.Unit}",
            MinStock = i.MinimumStockLevel,
            Cost = $"₱{i.Cost:N2}",
            i.SupplierName,
            i.Status
        }).ToList();

        ClearForm();
    }

    private void dgvInventory_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvInventory.CurrentRow != null && dgvInventory.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvInventory.CurrentRow.DataBoundItem;
            _selectedItemId = item.Id;

            var invItem = SqlDataRepository.Instance.GetInventoryItems().FirstOrDefault(i => i.Id == _selectedItemId);
            if (invItem != null)
            {
                txtItemName.Text = invItem.ItemName;
                txtCategory.Text = invItem.Category;
                numQuantity.Value = Math.Max(0, invItem.Quantity);
                txtUnit.Text = invItem.Unit;
                numMinStock.Value = Math.Max(0, invItem.MinimumStockLevel);
                numCost.Value = Math.Max(0, invItem.Cost);
                if (invItem.SupplierId.HasValue)
                {
                    cmbSupplier.SelectedValue = invItem.SupplierId.Value;
                }
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtItemName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Please enter an item name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int? supplierId = (cmbSupplier.SelectedItem is Supplier s) ? s.Id : null;

        var item = new InventoryItem
        {
            ItemName = name,
            Category = txtCategory.Text.Trim(),
            Quantity = (int)numQuantity.Value,
            Unit = string.IsNullOrWhiteSpace(txtUnit.Text) ? "pcs" : txtUnit.Text.Trim(),
            MinimumStockLevel = (int)numMinStock.Value,
            SupplierId = supplierId,
            Cost = numCost.Value
        };

        SqlDataRepository.Instance.AddInventoryItem(item);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Inventory", $"Added inventory item '{item.ItemName}' (Qty: {item.Quantity})", "Admin");
        MessageBox.Show($"Item '{item.ItemName}' added to inventory.", "Item Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadInventory();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedItemId == 0)
        {
            MessageBox.Show("Please select an item to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string name = txtItemName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Item name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int? supplierId = (cmbSupplier.SelectedItem is Supplier s) ? s.Id : null;

        var item = new InventoryItem
        {
            Id = _selectedItemId,
            ItemName = name,
            Category = txtCategory.Text.Trim(),
            Quantity = (int)numQuantity.Value,
            Unit = string.IsNullOrWhiteSpace(txtUnit.Text) ? "pcs" : txtUnit.Text.Trim(),
            MinimumStockLevel = (int)numMinStock.Value,
            SupplierId = supplierId,
            Cost = numCost.Value
        };

        SqlDataRepository.Instance.UpdateInventoryItem(item);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Inventory", $"Updated inventory item '{item.ItemName}'", "Admin");
        MessageBox.Show($"Item '{item.ItemName}' updated successfully.", "Item Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadInventory();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedItemId == 0)
        {
            MessageBox.Show("Please select an item to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show("Are you sure you want to remove this item from inventory?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteInventoryItem(_selectedItemId);
            SqlDataRepository.Instance.AddSystemLog("WARNING", "Inventory", $"Deleted item ID {_selectedItemId}", "Admin");
            MessageBox.Show("Item removed from inventory.", "Item Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadInventory();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedItemId = 0;
        txtItemName.Clear();
        txtCategory.Text = "General";
        numQuantity.Value = 0;
        txtUnit.Text = "pcs";
        numMinStock.Value = 5;
        numCost.Value = 0;
    }
}

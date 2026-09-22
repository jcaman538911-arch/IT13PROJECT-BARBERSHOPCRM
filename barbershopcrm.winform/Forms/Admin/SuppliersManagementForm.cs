using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class SuppliersManagementForm : Form
{
    private int _selectedSupplierId = 0;

    public SuppliersManagementForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvSuppliers);
        LoadSuppliers();
    }

    private void LoadSuppliers()
    {
        var suppliers = SqlDataRepository.Instance.GetSuppliers();
        dgvSuppliers.DataSource = suppliers.Select(s => new
        {
            s.Id,
            s.SupplierName,
            s.ContactInformation,
            s.Status,
            Created = s.CreatedAt.ToString("yyyy-MM-dd")
        }).ToList();

        ClearForm();
    }

    private void dgvSuppliers_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvSuppliers.CurrentRow != null && dgvSuppliers.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvSuppliers.CurrentRow.DataBoundItem;
            _selectedSupplierId = item.Id;

            var supp = SqlDataRepository.Instance.GetSuppliers().FirstOrDefault(s => s.Id == _selectedSupplierId);
            if (supp != null)
            {
                txtSupplierName.Text = supp.SupplierName;
                txtContactInfo.Text = supp.ContactInformation;
                chkIsActive.Checked = supp.Status.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtSupplierName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Please enter a valid supplier name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var supp = new Supplier
        {
            SupplierName = name,
            ContactInformation = txtContactInfo.Text.Trim(),
            Status = chkIsActive.Checked ? "ACTIVE" : "INACTIVE"
        };

        SqlDataRepository.Instance.AddSupplier(supp);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Suppliers", $"Added new supplier '{supp.SupplierName}'", "Admin");
        MessageBox.Show($"Supplier '{supp.SupplierName}' created successfully.", "Supplier Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadSuppliers();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedSupplierId == 0)
        {
            MessageBox.Show("Please select a supplier to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string name = txtSupplierName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Supplier name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var supp = new Supplier
        {
            Id = _selectedSupplierId,
            SupplierName = name,
            ContactInformation = txtContactInfo.Text.Trim(),
            Status = chkIsActive.Checked ? "ACTIVE" : "INACTIVE"
        };

        SqlDataRepository.Instance.UpdateSupplier(supp);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Suppliers", $"Updated supplier '{supp.SupplierName}'", "Admin");
        MessageBox.Show($"Supplier '{supp.SupplierName}' updated successfully.", "Supplier Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadSuppliers();
    }

    private void btnDeactivate_Click(object sender, EventArgs e)
    {
        if (_selectedSupplierId == 0)
        {
            MessageBox.Show("Please select a supplier to deactivate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show("Are you sure you want to deactivate this supplier?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteSupplier(_selectedSupplierId);
            SqlDataRepository.Instance.AddSystemLog("WARNING", "Suppliers", $"Deactivated supplier ID {_selectedSupplierId}", "Admin");
            MessageBox.Show("Supplier deactivated.", "Supplier Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadSuppliers();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedSupplierId = 0;
        txtSupplierName.Clear();
        txtContactInfo.Clear();
        chkIsActive.Checked = true;
    }
}

using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class PromotionsForm : Form
{
    private readonly UserRole _userRole;
    private int _selectedPromoId = 0;

    public PromotionsForm(UserRole userRole = UserRole.Admin)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _userRole = userRole;
        ThemeHelper.ApplyModernGrid(dgvPromotions);
        ConfigureRoleAccess();
        LoadPromotions();
    }

    private void ConfigureRoleAccess()
    {
        if (_userRole == UserRole.Staff)
        {
            // Staff can only view active promotions, cannot create or edit
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClear.Enabled = false;
            txtTitle.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtEligibility.ReadOnly = true;
            cmbDiscountType.Enabled = false;
            numDiscountValue.Enabled = false;
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            chkActive.Enabled = false;
        }
        else
        {
            cmbDiscountType.SelectedIndex = 0;
        }
    }

    private void LoadPromotions()
    {
        var promos = SqlDataRepository.Instance.GetPromotions();
        if (_userRole == UserRole.Staff)
        {
            promos = promos.Where(p => p.IsActive).ToList();
        }

        dgvPromotions.DataSource = promos.Select(p => new
        {
            p.Id,
            p.Title,
            p.Description,
            p.DiscountType,
            Discount = p.DiscountType == "Percentage" ? $"{p.DiscountValue}% OFF" : $"₱{p.DiscountValue:N2} OFF",
            p.EligibilityRule,
            ValidUntil = p.EndDate.ToString("yyyy-MM-dd"),
            Status = p.IsActive ? "Active" : "Disabled"
        }).ToList();
    }

    private void dgvPromotions_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvPromotions.CurrentRow != null && dgvPromotions.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvPromotions.CurrentRow.DataBoundItem;
            _selectedPromoId = item.Id;

            var promo = SqlDataRepository.Instance.GetPromotions().FirstOrDefault(p => p.Id == _selectedPromoId);
            if (promo != null)
            {
                txtTitle.Text = promo.Title;
                txtDescription.Text = promo.Description;
                cmbDiscountType.SelectedItem = promo.DiscountType;
                numDiscountValue.Value = promo.DiscountValue;
                txtEligibility.Text = promo.EligibilityRule;
                dtpStartDate.Value = promo.StartDate;
                dtpEndDate.Value = promo.EndDate;
                chkActive.Checked = promo.IsActive;
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (_userRole == UserRole.Staff) return;

        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            MessageBox.Show("Promotion Title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var promo = new Promotion
        {
            Title = txtTitle.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            DiscountType = cmbDiscountType.SelectedItem?.ToString() ?? "Percentage",
            DiscountValue = numDiscountValue.Value,
            EligibilityRule = txtEligibility.Text.Trim(),
            StartDate = dtpStartDate.Value,
            EndDate = dtpEndDate.Value,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.AddPromotion(promo);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Promotions", $"Created promotion '{promo.Title}'", "admin");
        MessageBox.Show("Promotion created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadPromotions();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_userRole == UserRole.Staff || _selectedPromoId == 0) return;

        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            MessageBox.Show("Promotion Title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var promo = new Promotion
        {
            Id = _selectedPromoId,
            Title = txtTitle.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            DiscountType = cmbDiscountType.SelectedItem?.ToString() ?? "Percentage",
            DiscountValue = numDiscountValue.Value,
            EligibilityRule = txtEligibility.Text.Trim(),
            StartDate = dtpStartDate.Value,
            EndDate = dtpEndDate.Value,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.UpdatePromotion(promo);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Promotions", $"Updated promotion ID {_selectedPromoId} '{promo.Title}'", "admin");
        MessageBox.Show("Promotion updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadPromotions();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_userRole == UserRole.Staff || _selectedPromoId == 0) return;

        var result = MessageBox.Show($"Are you sure you want to delete promotion ID {_selectedPromoId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeletePromotion(_selectedPromoId);
            SqlDataRepository.Instance.AddSystemLog("WARN", "Promotions", $"Deleted promotion ID {_selectedPromoId}", "admin");
            MessageBox.Show("Promotion deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadPromotions();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedPromoId = 0;
        txtTitle.Clear();
        txtDescription.Clear();
        txtEligibility.Clear();
        cmbDiscountType.SelectedIndex = 0;
        numDiscountValue.Value = 0;
        dtpStartDate.Value = DateTime.Today;
        dtpEndDate.Value = DateTime.Today.AddDays(30);
        chkActive.Checked = true;
    }
}

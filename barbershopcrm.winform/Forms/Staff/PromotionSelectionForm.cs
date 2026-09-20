using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class PromotionSelectionForm : Form
{
    public Promotion? SelectedPromotion { get; private set; }

    public PromotionSelectionForm()
    {
        InitializeComponent();
        ThemeHelper.ApplyModernGrid(dgvPromotions);
        LoadActivePromotions();
    }

    private void LoadActivePromotions()
    {
        var promos = SqlDataRepository.Instance.GetActivePromotions();
        dgvPromotions.DataSource = promos.Select(p => new
        {
            p.Id,
            p.Title,
            p.Description,
            Discount = p.DiscountType == "Percentage" ? $"{p.DiscountValue}% OFF" : $"₱{p.DiscountValue:N2} OFF",
            p.EligibilityRule
        }).ToList();
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
        if (dgvPromotions.CurrentRow != null && dgvPromotions.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvPromotions.CurrentRow.DataBoundItem;
            int id = item.Id;
            SelectedPromotion = SqlDataRepository.Instance.GetPromotions().FirstOrDefault(p => p.Id == id);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}

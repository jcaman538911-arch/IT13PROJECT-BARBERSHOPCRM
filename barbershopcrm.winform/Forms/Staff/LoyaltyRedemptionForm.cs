using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class LoyaltyRedemptionForm : Form
{
    private readonly Customer? _customer;
    public LoyaltyReward? SelectedReward { get; private set; }

    public LoyaltyRedemptionForm(Customer? customer)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _customer = customer;
        ThemeHelper.ApplyModernGrid(dgvRewards);
        LoadCustomerRewards();
    }

    private void LoadCustomerRewards()
    {
        if (_customer == null)
        {
            lblCustomerPointsInfo.Text = "Walk-in Customer (Not enrolled in Loyalty Program)";
            btnRedeem.Enabled = false;
            return;
        }

        lblCustomerPointsInfo.Text = $"Customer: {_customer.FullName} | Current Balance: {_customer.LoyaltyPoints} Points";

        var rewards = SqlDataRepository.Instance.GetLoyaltyRewards().Where(r => r.IsActive).ToList();
        dgvRewards.DataSource = rewards.Select(r => new
        {
            r.Id,
            r.RewardName,
            PointsRequired = r.PointsRequired,
            Discount = $"₱{r.DiscountAmount:N2} OFF",
            Eligible = _customer.LoyaltyPoints >= r.PointsRequired ? "YES" : "INSUFFICIENT POINTS"
        }).ToList();
    }

    private void btnRedeem_Click(object sender, EventArgs e)
    {
        if (_customer == null) return;

        if (dgvRewards.CurrentRow != null && dgvRewards.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvRewards.CurrentRow.DataBoundItem;
            int id = item.Id;

            var rwd = SqlDataRepository.Instance.GetLoyaltyRewards().FirstOrDefault(r => r.Id == id);
            if (rwd != null)
            {
                if (_customer.LoyaltyPoints < rwd.PointsRequired)
                {
                    MessageBox.Show($"Customer has {_customer.LoyaltyPoints} points, but {rwd.PointsRequired} points are required for this reward.", "Insufficient Points", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SelectedReward = rwd;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}

using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class LoyaltyRewardsForm : Form
{
    private readonly UserRole _userRole;
    private int _selectedRewardId = 0;

    public LoyaltyRewardsForm(UserRole userRole = UserRole.Admin)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _userRole = userRole;
        ThemeHelper.ApplyModernGrid(dgvLoyaltyRewards);
        ThemeHelper.ApplyModernGrid(dgvLoyaltyHistory);
        ConfigureRoleAccess();
        LoadRewards();
        LoadLoyaltyHistory();
    }

    private void ConfigureRoleAccess()
    {
        if (_userRole == UserRole.Staff)
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClear.Enabled = false;
            txtRewardName.ReadOnly = true;
            numPointsRequired.Enabled = false;
            numDiscountAmount.Enabled = false;
            chkActive.Enabled = false;
        }
    }

    private void LoadRewards()
    {
        var rewards = SqlDataRepository.Instance.GetLoyaltyRewards();
        dgvLoyaltyRewards.DataSource = rewards.Select(r => new
        {
            r.Id,
            r.RewardName,
            Points = r.PointsRequired,
            Discount = $"₱{r.DiscountAmount:N2} OFF",
            Status = r.IsActive ? "Active" : "Disabled"
        }).ToList();
    }

    private void LoadLoyaltyHistory()
    {
        try
        {
            var history = SqlDataRepository.Instance.GetAllLoyaltyHistory();
            dgvLoyaltyHistory.DataSource = history.Select(h => new
            {
                Date = h.DateCreated.ToString("yyyy-MM-dd HH:mm"),
                Customer = h.CustomerName,
                Activity = h.PointsEarned > 0 && h.PointsRedeemed > 0 ? "EARNED+REDEEMED" : h.PointsEarned > 0 ? "EARNED" : "REDEEMED",
                Points = h.PointsEarned > 0 ? $"+{h.PointsEarned}" : $"-{h.PointsRedeemed}",
                h.Description,
                h.RecordedBy
            }).ToList();
        }
        catch
        {
            dgvLoyaltyHistory.DataSource = null;
        }
    }

    private void dgvLoyaltyRewards_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvLoyaltyRewards.CurrentRow != null && dgvLoyaltyRewards.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvLoyaltyRewards.CurrentRow.DataBoundItem;
            _selectedRewardId = item.Id;

            var rwd = SqlDataRepository.Instance.GetLoyaltyRewards().FirstOrDefault(r => r.Id == _selectedRewardId);
            if (rwd != null)
            {
                txtRewardName.Text = rwd.RewardName;
                numPointsRequired.Value = rwd.PointsRequired;
                numDiscountAmount.Value = rwd.DiscountAmount;
                chkActive.Checked = rwd.IsActive;
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (_userRole == UserRole.Staff) return;

        if (string.IsNullOrWhiteSpace(txtRewardName.Text))
        {
            MessageBox.Show("Reward Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var reward = new LoyaltyReward
        {
            RewardName = txtRewardName.Text.Trim(),
            PointsRequired = (int)numPointsRequired.Value,
            DiscountAmount = numDiscountAmount.Value,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.AddLoyaltyReward(reward);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Loyalty", $"Added reward tier '{reward.RewardName}'", "admin");
        LoadLoyaltyHistory();
        MessageBox.Show("Loyalty Reward created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadRewards();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_userRole == UserRole.Staff || _selectedRewardId == 0) return;

        if (string.IsNullOrWhiteSpace(txtRewardName.Text))
        {
            MessageBox.Show("Reward Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var reward = new LoyaltyReward
        {
            Id = _selectedRewardId,
            RewardName = txtRewardName.Text.Trim(),
            PointsRequired = (int)numPointsRequired.Value,
            DiscountAmount = numDiscountAmount.Value,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.UpdateLoyaltyReward(reward);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Loyalty", $"Updated reward ID {_selectedRewardId} '{reward.RewardName}'", "admin");
        LoadLoyaltyHistory();
        MessageBox.Show("Loyalty Reward updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadRewards();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_userRole == UserRole.Staff || _selectedRewardId == 0) return;

        var result = MessageBox.Show($"Are you sure you want to delete reward ID {_selectedRewardId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteLoyaltyReward(_selectedRewardId);
            SqlDataRepository.Instance.AddSystemLog("WARN", "Loyalty", $"Deleted reward ID {_selectedRewardId}", "admin");
            MessageBox.Show("Loyalty Reward deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadRewards();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedRewardId = 0;
        txtRewardName.Clear();
        numPointsRequired.Value = 50;
        numDiscountAmount.Value = 30.00m;
        chkActive.Checked = true;
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class ServiceTransactionForm : Form
{
    private readonly User _currentUser;
    private Customer? _selectedCustomer = null;
    private int _selectedTxnId = 0;

    private decimal _subtotal = 200.00m;
    private decimal _discountAmount = 0.00m;
    private decimal _finalAmount = 200.00m;

    public ServiceTransactionForm(User currentUser)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _currentUser = currentUser;
        ThemeHelper.ApplyModernGrid(dgvActiveTransactions);
        PopulateDropdowns();
        LoadTodayTransactions();
    }

    private void PopulateDropdowns()
    {
        // 1. Services
        var services = SqlDataRepository.Instance.GetServices().Where(s => s.IsActive).ToList();
        cmbService.DataSource = services;
        cmbService.DisplayMember = "ServiceName";
        cmbService.ValueMember = "Id";

        // 2. Barbers
        var barbers = SqlDataRepository.Instance.GetBarbers();
        cmbBarber.DataSource = barbers;
        cmbBarber.DisplayMember = "Name";
        cmbBarber.ValueMember = "Id";

        // 3. Promotions
        var promos = SqlDataRepository.Instance.GetActivePromotions();
        promos.Insert(0, new Promotion { Id = 0, Title = "None (No Promotion)" });
        cmbPromotion.DataSource = promos;
        cmbPromotion.DisplayMember = "Title";
        cmbPromotion.ValueMember = "Id";

        // 4. Loyalty Rewards
        var rewards = SqlDataRepository.Instance.GetLoyaltyRewards().Where(r => r.IsActive).ToList();
        rewards.Insert(0, new LoyaltyReward { Id = 0, RewardName = "None (No Loyalty Reward)" });
        cmbLoyaltyReward.DataSource = rewards;
        cmbLoyaltyReward.DisplayMember = "RewardName";
        cmbLoyaltyReward.ValueMember = "Id";
    }

    private void LoadTodayTransactions()
    {
        var txns = SqlDataRepository.Instance.GetTodayTransactions();
        dgvActiveTransactions.DataSource = txns.Select(t => new
        {
            t.Id,
            t.TransactionNumber,
            t.CustomerName,
            t.ServiceName,
            t.BarberName,
            Total = $"₱{t.FinalAmount:N2}",
            t.Status,
            Time = t.TransactionDate.ToString("HH:mm")
        }).ToList();
    }

    private void btnSelectCustomer_Click(object sender, EventArgs e)
    {
        using (var searchModal = new CustomerSearchForm())
        {
            if (searchModal.ShowDialog() == DialogResult.OK)
            {
                if (searchModal.IsWalkIn || searchModal.SelectedCustomer == null)
                {
                    _selectedCustomer = null;
                    lblCustomerName.Text = "Walk-in Customer";
                }
                else
                {
                    _selectedCustomer = searchModal.SelectedCustomer;
                    lblCustomerName.Text = $"{_selectedCustomer.FullName} ({(_selectedCustomer.IsLoyaltyMember ? "Member" : "Regular")})";
                }
                UpdateLoyaltyDisplay();
                RecalculateTotals();
            }
        }
    }

    private void cmbService_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbService.SelectedItem is ServiceItem svc)
        {
            _subtotal = svc.BasePrice;
            lblBasePriceValue.Text = $"₱{_subtotal:N2}";
            RecalculateTotals();
        }
    }

    private void cmbPromotion_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecalculateTotals();
    }

    private void cmbLoyaltyReward_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbLoyaltyReward.SelectedItem is LoyaltyReward reward && reward.Id > 0)
        {
            if (_selectedCustomer == null || !_selectedCustomer.IsLoyaltyMember)
            {
                MessageBox.Show("Loyalty rewards can only be redeemed by registered loyalty members.", "Loyalty Reward", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLoyaltyReward.SelectedIndex = 0;
                return;
            }
            var fresh = SqlDataRepository.Instance.GetCustomerById(_selectedCustomer.Id);
            int balance = fresh?.LoyaltyPoints ?? _selectedCustomer.LoyaltyPoints;
            if (fresh != null) _selectedCustomer.LoyaltyPoints = balance;
            if (balance < reward.PointsRequired)
            {
                MessageBox.Show($"Insufficient loyalty points. '{reward.RewardName}' requires {reward.PointsRequired} points; current balance is {balance}.", "Loyalty Reward", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLoyaltyReward.SelectedIndex = 0;
                return;
            }
        }
        RecalculateTotals();
        UpdateLoyaltyDisplay();
    }

    private void UpdateLoyaltyDisplay()
    {
        if (_selectedCustomer == null)
        {
            lblLoyaltyInfo.Text = "Walk-in Customer\nRegister customer to start earning loyalty points.";
            lblLoyaltyInfo.ForeColor = ThemeHelper.TextSecondary;
            return;
        }

        var fresh = SqlDataRepository.Instance.GetCustomerById(_selectedCustomer.Id);
        if (fresh != null) _selectedCustomer.LoyaltyPoints = fresh.LoyaltyPoints;

        if (!_selectedCustomer.IsLoyaltyMember)
        {
            lblLoyaltyInfo.Text = $"{_selectedCustomer.FullName}\nNot a loyalty member. Enroll to start earning points.";
            lblLoyaltyInfo.ForeColor = ThemeHelper.TextSecondary;
            return;
        }

        string extra = cmbLoyaltyReward.SelectedItem is LoyaltyReward r && r.Id > 0
            ? $"Redeeming '{r.RewardName}' (-{r.PointsRequired} pts)"
            : "Possible Earned Points: +10 after successful payment";
        lblLoyaltyInfo.Text = $"{_selectedCustomer.FullName} - Loyalty Member\nCurrent Points: {_selectedCustomer.LoyaltyPoints}  |  {extra}";
        lblLoyaltyInfo.ForeColor = ThemeHelper.PrimaryNavy;
    }

    private void RecalculateTotals()
    {
        _discountAmount = 0.00m;

        // Apply Promotion Discount
        if (cmbPromotion.SelectedItem is Promotion promo && promo.Id > 0)
        {
            if (promo.DiscountType == "Percentage")
            {
                _discountAmount += _subtotal * (promo.DiscountValue / 100.0m);
            }
            else
            {
                _discountAmount += promo.DiscountValue;
            }
        }

        // Apply Loyalty Reward Discount
        if (cmbLoyaltyReward.SelectedItem is LoyaltyReward reward && reward.Id > 0)
        {
            _discountAmount += reward.DiscountAmount;
        }

        _discountAmount = Math.Min(_subtotal, _discountAmount);
        _finalAmount = Math.Max(0.00m, _subtotal - _discountAmount);

        lblSubtotalValue.Text = $"₱{_subtotal:N2}";
        lblDiscountValue.Text = $"- ₱{_discountAmount:N2}";
        lblFinalValue.Text = $"₱{_finalAmount:N2}";
    }

    private void btnSaveDraft_Click(object sender, EventArgs e)
    {
        if (cmbBarber.SelectedItem is not Employee barber)
        {
            MessageBox.Show("Please select a barber.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cmbService.SelectedItem is not ServiceItem service)
        {
            MessageBox.Show("Please select a haircut service.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var txn = new Transaction
        {
            TransactionNumber = SqlDataRepository.Instance.GenerateTransactionNumber(),
            CustomerId = _selectedCustomer?.Id,
            CustomerName = _selectedCustomer?.FullName ?? "Walk-in Customer",
            ServiceId = service.Id,
            ServiceName = service.ServiceName,
            BarberId = barber.Id,
            BarberName = barber.Name,
            StaffId = _currentUser.Id,
            StaffName = _currentUser.FullName,
            Subtotal = _subtotal,
            DiscountAmount = _discountAmount,
            FinalAmount = _finalAmount,
            Status = TransactionStatus.InService,
            TransactionDate = DateTime.Now
        };

        SqlDataRepository.Instance.SaveTransaction(txn);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Transactions", $"Booked service '{txn.TransactionNumber}' for customer '{txn.CustomerName}' with Barber {barber.Name}", _currentUser.Username);
        MessageBox.Show($"Service booked for {txn.CustomerName} with Barber {barber.Name}. Status: In Service.", "Booked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadTodayTransactions();
    }

    private void btnProcessPayment_Click(object sender, EventArgs e)
    {
        if (cmbBarber.SelectedItem is not Employee barber)
        {
            MessageBox.Show("Please select a barber before processing payment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cmbService.SelectedItem is not ServiceItem service)
        {
            MessageBox.Show("Please select a haircut service.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int? promoId = (cmbPromotion.SelectedItem is Promotion p && p.Id > 0) ? p.Id : null;
        string? promoName = promoId.HasValue ? (cmbPromotion.SelectedItem as Promotion)?.Title : null;

        int? rewardId = (cmbLoyaltyReward.SelectedItem is LoyaltyReward r && r.Id > 0) ? r.Id : null;
        string? rewardName = rewardId.HasValue ? (cmbLoyaltyReward.SelectedItem as LoyaltyReward)?.RewardName : null;
        int pointsRedeemed = rewardId.HasValue ? (cmbLoyaltyReward.SelectedItem as LoyaltyReward)?.PointsRequired ?? 0 : 0;

        int pointsEarned = (_selectedCustomer != null && _selectedCustomer.IsLoyaltyMember) ? 10 : 0;

        var txn = new Transaction
        {
            Id = _selectedTxnId,
            TransactionNumber = _selectedTxnId > 0 ? (SqlDataRepository.Instance.GetTransactions().FirstOrDefault(t => t.Id == _selectedTxnId)?.TransactionNumber ?? SqlDataRepository.Instance.GenerateTransactionNumber()) : SqlDataRepository.Instance.GenerateTransactionNumber(),
            CustomerId = _selectedCustomer?.Id,
            CustomerName = _selectedCustomer?.FullName ?? "Walk-in Customer",
            ServiceId = service.Id,
            ServiceName = service.ServiceName,
            BarberId = barber.Id,
            BarberName = barber.Name,
            StaffId = _currentUser.Id,
            StaffName = _currentUser.FullName,
            Subtotal = _subtotal,
            DiscountAmount = _discountAmount,
            FinalAmount = _finalAmount,
            PromotionId = promoId,
            AppliedPromotionName = promoName,
            LoyaltyRewardId = rewardId,
            AppliedRewardName = rewardName,
            PointsEarned = pointsEarned,
            PointsRedeemed = pointsRedeemed,
            Status = TransactionStatus.Completed
        };

        using (var payModal = new PaymentForm(txn))
        {
            if (payModal.ShowDialog() == DialogResult.OK)
            {
                SqlDataRepository.Instance.SaveTransaction(payModal.CompletedTransaction);
                SqlDataRepository.Instance.AddSystemLog("INFO", "Payments", $"Processed payment for '{payModal.CompletedTransaction.TransactionNumber}' (₱{payModal.CompletedTransaction.FinalAmount:N2})", _currentUser.Username);
                MessageBox.Show(BuildPaymentConfirmation(payModal.CompletedTransaction), "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                LoadTodayTransactions();
            }
        }
    }

    private void dgvActiveTransactions_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvActiveTransactions.CurrentRow != null && dgvActiveTransactions.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvActiveTransactions.CurrentRow.DataBoundItem;
            _selectedTxnId = item.Id;
        }
    }

    private string BuildPaymentConfirmation(Transaction txn)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Transaction: {txn.TransactionNumber}");
        sb.AppendLine($"Customer: {txn.CustomerName}");
        sb.AppendLine($"Final Amount: ₱{txn.FinalAmount:N2}");

        if (txn.CustomerId.HasValue && txn.PointsEarned > 0)
        {
            var customer = SqlDataRepository.Instance.GetCustomerById(txn.CustomerId.Value);
            sb.AppendLine();
            sb.AppendLine($"Loyalty Points Earned: +{txn.PointsEarned}");
            if (txn.PointsRedeemed > 0)
                sb.AppendLine($"Loyalty Points Redeemed: -{txn.PointsRedeemed}");
            if (customer != null)
                sb.AppendLine($"New Loyalty Balance: {customer.LoyaltyPoints} points");
        }

        return sb.ToString();
    }

    private void ResetForm()
    {
        _selectedTxnId = 0;
        _selectedCustomer = null;
        lblCustomerName.Text = "Walk-in Customer";
        UpdateLoyaltyDisplay();
        cmbService.SelectedIndex = 0;
        cmbBarber.SelectedIndex = 0;
        cmbPromotion.SelectedIndex = 0;
        cmbLoyaltyReward.SelectedIndex = 0;
        RecalculateTotals();
    }
}

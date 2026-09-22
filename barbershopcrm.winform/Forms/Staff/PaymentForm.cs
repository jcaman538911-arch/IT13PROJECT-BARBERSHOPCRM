using BarberShopCRM.Helpers;
using System;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Forms.Staff;

public partial class PaymentForm : Form
{
    public Transaction CompletedTransaction { get; }
    private Customer? _customer;
    private const int POINTS_PER_SERVICE = 10;

    public PaymentForm(Transaction transaction)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        CompletedTransaction = transaction;
        PopulatePaymentMethods();
        LoadCustomerData();
        LoadSummary();
        LoadLoyaltyPreview();
    }

    private void PopulatePaymentMethods()
    {
        cmbPaymentMethod.DataSource = Enum.GetValues(typeof(PaymentMethod));
    }

    private void LoadCustomerData()
    {
        if (CompletedTransaction.CustomerId.HasValue)
        {
            _customer = SqlDataRepository.Instance.GetCustomerById(CompletedTransaction.CustomerId.Value);
        }
    }

    private void LoadSummary()
    {
        lblSubtotalValue.Text = $"₱{CompletedTransaction.Subtotal:N2}";
        lblDiscountValue.Text = $"- ₱{CompletedTransaction.DiscountAmount:N2}";
        lblFinalValue.Text = $"₱{CompletedTransaction.FinalAmount:N2}";
        numAmountReceived.Value = CompletedTransaction.FinalAmount;
        CalculateChange();
    }

    private void LoadLoyaltyPreview()
    {
        if (_customer != null && _customer.IsLoyaltyMember)
        {
            pnlLoyaltySection.Visible = true;
            lblLoyaltyHeader.Text = "LOYALTY MEMBER";
            lblCurrentPointsValue.Text = _customer.LoyaltyPoints.ToString();
            
            // Calculate points to earn (automatic: 10 points per completed service)
            int pointsToEarn = POINTS_PER_SERVICE;
            int pointsRedeemed = CompletedTransaction.PointsRedeemed;
            
            // Set the numeric control to automatic value
            numPointsToEarn.Value = pointsToEarn;
            numPointsToEarn.Enabled = true; // Allow staff to adjust if needed
            
            // Calculate new balance: current - redeemed + earned
            int newBalance = _customer.LoyaltyPoints - pointsRedeemed + pointsToEarn;
            lblNewBalanceValue.Text = newBalance.ToString();
            
            // Automatically set the points to earn in the transaction
            CompletedTransaction.PointsEarned = pointsToEarn;
        }
        else if (_customer != null && !_customer.IsLoyaltyMember)
        {
            pnlLoyaltySection.Visible = true;
            lblLoyaltyHeader.Text = "NOT A LOYALTY MEMBER";
            lblCurrentPointsValue.Text = _customer.LoyaltyPoints.ToString();
            numPointsToEarn.Value = 0;
            numPointsToEarn.Enabled = false; // Cannot earn points if not a member
            lblNewBalanceLabel.Text = "New Balance After:";
            lblNewBalanceValue.Text = _customer.LoyaltyPoints.ToString();
            CompletedTransaction.PointsEarned = 0;
        }
        else
        {
            // Walk-in customer
            pnlLoyaltySection.Visible = true;
            lblLoyaltyHeader.Text = "WALK-IN CUSTOMER";
            lblCurrentPointsLabel.Text = "Registration:";
            lblCurrentPointsValue.Text = "Not Registered";
            numPointsToEarn.Value = 0;
            numPointsToEarn.Enabled = false; // Cannot earn points if not registered
            lblNewBalanceLabel.Text = "New Balance After:";
            lblNewBalanceValue.Text = "N/A";
            CompletedTransaction.PointsEarned = 0;
        }
    }

    private void numPointsToEarn_ValueChanged(object sender, EventArgs e)
    {
        // Update the transaction with the manually entered points
        if (_customer != null && _customer.IsLoyaltyMember)
        {
            int pointsToEarn = (int)numPointsToEarn.Value;
            int pointsRedeemed = CompletedTransaction.PointsRedeemed;
            
            // Recalculate new balance
            int newBalance = _customer.LoyaltyPoints - pointsRedeemed + pointsToEarn;
            lblNewBalanceValue.Text = newBalance.ToString();
            
            // Update the transaction
            CompletedTransaction.PointsEarned = pointsToEarn;
        }
    }

    private void numAmountReceived_ValueChanged(object sender, EventArgs e)
    {
        CalculateChange();
    }

    private void CalculateChange()
    {
        decimal received = numAmountReceived.Value;
        decimal change = Math.Max(0.00m, received - CompletedTransaction.FinalAmount);
        lblChangeValue.Text = $"₱{change:N2}";
    }

    private void btnConfirmPayment_Click(object sender, EventArgs e)
    {
        if (numAmountReceived.Value < CompletedTransaction.FinalAmount)
        {
            MessageBox.Show("Amount received is less than the final price.", "Payment Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Use the manually entered points from the numeric control
        if (_customer != null && _customer.IsLoyaltyMember)
        {
            CompletedTransaction.PointsEarned = (int)numPointsToEarn.Value;
        }

        CompletedTransaction.PaymentMethod = (PaymentMethod)cmbPaymentMethod.SelectedItem!;
        CompletedTransaction.AmountReceived = numAmountReceived.Value;
        CompletedTransaction.ChangeAmount = numAmountReceived.Value - CompletedTransaction.FinalAmount;
        CompletedTransaction.Status = TransactionStatus.Completed;

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}

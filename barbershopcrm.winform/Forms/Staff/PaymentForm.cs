using System;
using System.Windows.Forms;
using barbershop.domain;

namespace BarberShopCRM.Forms.Staff;

public partial class PaymentForm : Form
{
    public Transaction CompletedTransaction { get; }

    public PaymentForm(Transaction transaction)
    {
        InitializeComponent();
        CompletedTransaction = transaction;
        PopulatePaymentMethods();
        LoadSummary();
    }

    private void PopulatePaymentMethods()
    {
        cmbPaymentMethod.DataSource = Enum.GetValues(typeof(PaymentMethod));
    }

    private void LoadSummary()
    {
        lblSubtotalValue.Text = $"₱{CompletedTransaction.Subtotal:N2}";
        lblDiscountValue.Text = $"- ₱{CompletedTransaction.DiscountAmount:N2}";
        lblFinalValue.Text = $"₱{CompletedTransaction.FinalAmount:N2}";
        numAmountReceived.Value = CompletedTransaction.FinalAmount;
        CalculateChange();
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

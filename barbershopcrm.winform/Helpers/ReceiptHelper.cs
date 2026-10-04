using System;
using System.Drawing;
using System.Windows.Forms;
using barbershop.domain;

namespace BarberShopCRM.Helpers;

public static class ReceiptHelper
{
    public static void PrintOrPreviewReceipt(Transaction txn, IWin32Window? owner = null)
    {
        var receiptText = $@"
==========================================
        UPPERCUT BARBER SHOP
        OFFICIAL SERVICE RECEIPT
==========================================
Receipt #:     {txn.TransactionNumber}
Date/Time:     {txn.TransactionDate:yyyy-MM-dd HH:mm}
Customer:      {txn.CustomerName}
Barber:        {txn.BarberName}
Cashier:       {txn.StaffName}
------------------------------------------
Service:       {txn.ServiceName}
Subtotal:      ₱{txn.Subtotal:N2}
Discount:      - ₱{txn.DiscountAmount:N2}
------------------------------------------
TOTAL FINAL:   ₱{txn.FinalAmount:N2}
Payment:       {txn.PaymentMethod}
Amount Paid:   ₱{txn.AmountReceived:N2}
Change:        ₱{txn.ChangeAmount:N2}
------------------------------------------
Points Earned: {txn.PointsEarned} pts
Points Used:   {txn.PointsRedeemed} pts
==========================================
    THANK YOU FOR VISITING UPPERCUT!
==========================================";

        try
        {
            using var printDoc = new System.Drawing.Printing.PrintDocument();
            printDoc.PrintPage += (s, e) =>
            {
                e.Graphics?.DrawString(receiptText, new Font("Consolas", 10, FontStyle.Regular), Brushes.Black, new RectangleF(50, 50, e.PageBounds.Width - 100, e.PageBounds.Height - 100));
            };

            using var printDialog = new PrintDialog();
            printDialog.Document = printDoc;
            
            if (printDialog.ShowDialog(owner) == DialogResult.OK)
            {
                printDoc.Print();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to communicate with printer. Details:\n{ex.Message}", "Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

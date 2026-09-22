using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Digital Uppercut loyalty membership card: member name, member ID, current points
/// and the next reward within reach. Laid out so a QR/barcode panel can be dropped in later.
/// </summary>
public class LoyaltyCardForm : Form
{
    public LoyaltyCardForm(Customer customer)
    {
        Text = $"Loyalty Card - {customer.FullName}";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(420, 300);
        BackColor = ThemeHelper.WarmIvory;

        var card = new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(20),
            Padding = new Padding(24),
            BackColor = ThemeHelper.DeepCharcoal
        };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 6,
            BackColor = ThemeHelper.DeepCharcoal
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        layout.Controls.Add(Line("✂ UPPERCUT LOYALTY", new Font("Georgia", 12F, FontStyle.Bold), ThemeHelper.MutedGold), 0, 0);
        layout.Controls.Add(Line(customer.FullName.ToUpperInvariant(), new Font("Segoe UI", 15F, FontStyle.Bold), ThemeHelper.WarmIvory), 0, 1);
        layout.Controls.Add(Line($"MEMBER ID: {FormatMemberId(customer.Id)}", new Font("Consolas", 11F, FontStyle.Regular), ThemeHelper.WarmGray), 0, 2);
        layout.Controls.Add(Line("POINTS", ThemeHelper.SmallFont, ThemeHelper.WarmGray), 0, 3);
        layout.Controls.Add(Line(customer.LoyaltyPoints.ToString(), new Font("Segoe UI", 26F, FontStyle.Bold), ThemeHelper.MutedGold), 0, 4);
        layout.Controls.Add(Line(NextRewardText(customer.LoyaltyPoints), ThemeHelper.BodyFont, ThemeHelper.WarmIvory), 0, 5);

        card.Controls.Add(layout);

        var host = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), BackColor = ThemeHelper.WarmIvory };
        host.Controls.Add(card);
        Controls.Add(host);
    }

    /// <summary>Stable, human-readable membership number derived from the customer record.</summary>
    public static string FormatMemberId(int customerId) => $"UC-{customerId:D6}";

    private static string NextRewardText(int points)
    {
        try
        {
            var rewards = SqlDataRepository.Instance.GetLoyaltyRewards().Where(r => r.IsActive).ToList();
            var affordable = rewards.Where(r => r.PointsRequired <= points).OrderByDescending(r => r.PointsRequired).FirstOrDefault();
            if (affordable != null)
                return $"Available now: {affordable.RewardName} (₱{affordable.DiscountAmount:N0} OFF)";

            var next = rewards.Where(r => r.PointsRequired > points).OrderBy(r => r.PointsRequired).FirstOrDefault();
            return next != null
                ? $"Next Reward: {next.RewardName} - {next.PointsRequired - points} points to go"
                : "No rewards configured yet.";
        }
        catch
        {
            return string.Empty;
        }
    }

    private static Label Line(string text, Font font, Color color) => new()
    {
        Text = text,
        Dock = DockStyle.Fill,
        Font = font,
        ForeColor = color,
        BackColor = ThemeHelper.DeepCharcoal,
        TextAlign = ContentAlignment.MiddleLeft,
        AutoEllipsis = true
    };
}

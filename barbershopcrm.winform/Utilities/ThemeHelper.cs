using System.Drawing;
using System.Windows.Forms;

namespace BarberShopCRM.Helpers;

public static class ThemeHelper
{
    // Official Uppercut Barber Shop Color Palette
    public static readonly Color DeepCharcoal = Color.FromArgb(23, 23, 23);       // #171717 - Deep Charcoal Sidebar/Header
    public static readonly Color WarmIvory = Color.FromArgb(243, 235, 221);       // #F3EBDD - Warm Ivory Canvas / Cards
    public static readonly Color MutedGold = Color.FromArgb(198, 161, 91);        // #C6A15B - Muted Gold Accent / Selected Item / Primary Button
    public static readonly Color DeepBurgundy = Color.FromArgb(110, 36, 36);      // #6E2424 - Deep Burgundy Logout / Delete / Sales Highlight
    public static readonly Color WarmGray = Color.FromArgb(138, 131, 120);        // #8A8378 - Warm Gray Subtitles & Muted Borders

    // Structural Color Aliases
    public static readonly Color SidebarBackground = DeepCharcoal;               // #171717
    public static readonly Color HeaderBackground = DeepCharcoal;                // #171717
    public static readonly Color WarmCanvas = WarmIvory;                          // #F3EBDD
    public static readonly Color CardBackground = Color.FromArgb(255, 255, 255);  // Crisp White Cards
    public static readonly Color CardHeaderBg = Color.FromArgb(243, 235, 221);    // #F3EBDD Header Accent

    // Text Colors
    public static readonly Color TextPrimary = DeepCharcoal;                       // #171717
    public static readonly Color TextSecondary = WarmGray;                        // #8A8378
    public static readonly Color TextMuted = WarmGray;                            // #8A8378
    public static readonly Color TextLight = WarmIvory;                           // #F3EBDD
    public static readonly Color TextWhite = Color.FromArgb(255, 255, 255);

    // Brand Accent Aliases
    public static readonly Color BarberRed = DeepBurgundy;                        // #6E2424
    public static readonly Color AccentBeige = MutedGold;                         // #C6A15B
    public static readonly Color AccentBlue = MutedGold;                          // #C6A15B
    public static readonly Color AccentTeal = DeepCharcoal;                        // #171717
    public static readonly Color AccentWarning = DeepBurgundy;                     // #6E2424

    // Compatibility Alias Properties
    public static Color PrimaryNavy => DeepCharcoal;
    public static Color SecondaryNavy => DeepCharcoal;
    public static Color DarkNavy => DeepCharcoal;
    public static Color VintageBlack => DeepCharcoal;
    public static Color PrimaryGold => MutedGold;
    public static Color SecondaryGold => MutedGold;
    public static Color DarkBackground => WarmIvory;
    public static Color AccentRed => DeepBurgundy;

    // Typography
    public static readonly Font HeaderFont = new Font("Segoe UI", 16F, FontStyle.Bold);
    public static readonly Font TitleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
    public static readonly Font SubtitleFont = new Font("Segoe UI", 10F, FontStyle.Bold);
    public static readonly Font BodyFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
    public static readonly Font SmallFont = new Font("Segoe UI", 8.5F, FontStyle.Regular);

    public static void ApplyPrimaryButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = MutedGold;
        btn.ForeColor = DeepCharcoal;
        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btn.Cursor = Cursors.Hand;
    }

    public static void ApplySecondaryButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = DeepCharcoal;
        btn.ForeColor = WarmIvory;
        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btn.Cursor = Cursors.Hand;
    }

    public static void ApplyDestructiveButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = DeepBurgundy;
        btn.ForeColor = WarmIvory;
        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btn.Cursor = Cursors.Hand;
    }

    public static void ApplyModernButton(Button btn, Color bg, Color text)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = bg;
        btn.ForeColor = text;
        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btn.Cursor = Cursors.Hand;
    }

    public static void ApplyModernGrid(DataGridView dgv)
    {
        dgv.BackgroundColor = WarmIvory;
        dgv.BorderStyle = BorderStyle.FixedSingle;
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.GridColor = WarmGray; // #8A8378

        dgv.EnableHeadersVisualStyles = false;
        
        // Column Header Styling (Deep Charcoal #171717 background, Warm Ivory #F3EBDD text)
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgv.ColumnHeadersDefaultCellStyle.BackColor = DeepCharcoal;
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = WarmIvory;
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = DeepCharcoal;
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = MutedGold;
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dgv.ColumnHeadersHeight = 40;

        // Row Header / Selector Styling (Completely eliminates default Windows Forms blue selector)
        dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgv.RowHeadersDefaultCellStyle.BackColor = DeepCharcoal;
        dgv.RowHeadersDefaultCellStyle.ForeColor = MutedGold;
        dgv.RowHeadersDefaultCellStyle.SelectionBackColor = DeepCharcoal;
        dgv.RowHeadersDefaultCellStyle.SelectionForeColor = MutedGold;
        dgv.RowHeadersWidth = 25;
        dgv.RowHeadersVisible = false; // Hide left row selector box to prevent default blue indicator

        // Data Cell Styling (Warm Ivory background, Deep Charcoal text, Muted Gold selection)
        dgv.DefaultCellStyle.BackColor = Color.FromArgb(250, 246, 238);
        dgv.DefaultCellStyle.ForeColor = DeepCharcoal;
        dgv.DefaultCellStyle.SelectionBackColor = MutedGold;
        dgv.DefaultCellStyle.SelectionForeColor = DeepCharcoal;
        dgv.DefaultCellStyle.Font = BodyFont;

        // Alternating row styling
        dgv.AlternatingRowsDefaultCellStyle.BackColor = WarmIvory;
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = DeepCharcoal;
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = MutedGold;
        dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = DeepCharcoal;

        dgv.RowTemplate.Height = 35;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.ReadOnly = true;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }
}


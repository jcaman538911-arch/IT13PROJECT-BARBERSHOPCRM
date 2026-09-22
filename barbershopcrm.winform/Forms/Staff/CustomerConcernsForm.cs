using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Logging and tracking of customer complaints, inquiries and feedback.
/// Backed by the existing SupportRequests store.
/// </summary>
public class CustomerConcernsForm : Form
{
    private readonly User _currentUser;
    private readonly DataGridView dgvConcerns = new();
    private readonly ComboBox cmbFilter = new();
    private readonly TextBox txtCustomer = new();
    private readonly ComboBox cmbType = new();
    private readonly TextBox txtDetails = new() { Multiline = true };
    private readonly Button btnLog = new();
    private readonly Button btnResolve = new();
    private readonly Button btnRefresh = new();
    private List<SupportRequest> _concerns = new();

    public CustomerConcernsForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = "Customer Concerns";
        BackColor = ThemeHelper.WarmIvory;

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(16)
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        root.Controls.Add(BuildEntryPanel(), 0, 0);
        root.Controls.Add(BuildListPanel(), 1, 0);

        Controls.Add(root);
        ThemeHelper.ApplyModernGrid(dgvConcerns);
        LoadConcerns();
        ResponsiveLayoutHelper.Apply(this);
    }

    private Control BuildEntryPanel()
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 7,
            Padding = new Padding(0, 0, 16, 0),
            BackColor = ThemeHelper.WarmIvory
        };
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        panel.Controls.Add(new Label
        {
            Text = "Log a Concern",
            Dock = DockStyle.Fill,
            Font = ThemeHelper.TitleFont,
            ForeColor = ThemeHelper.TextPrimary
        }, 0, 0);

        panel.Controls.Add(Caption("Customer / Reported By"), 0, 1);
        txtCustomer.Dock = DockStyle.Fill;
        panel.Controls.Add(txtCustomer, 0, 2);

        panel.Controls.Add(Caption("Type"), 0, 3);
        cmbType.Dock = DockStyle.Fill;
        cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbType.Items.AddRange(new object[] { "Complaint", "Inquiry", "Feedback" });
        cmbType.SelectedIndex = 0;
        panel.Controls.Add(cmbType, 0, 4);

        panel.Controls.Add(Caption("Details"), 0, 5);

        var lower = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        lower.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        lower.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        txtDetails.Dock = DockStyle.Fill;
        lower.Controls.Add(txtDetails, 0, 0);

        btnLog.Text = "Log Concern";
        btnLog.Dock = DockStyle.Fill;
        ThemeHelper.ApplyPrimaryButton(btnLog);
        btnLog.Click += btnLog_Click;
        lower.Controls.Add(btnLog, 0, 1);

        panel.Controls.Add(lower, 0, 6);
        return panel;
    }

    private Control BuildListPanel()
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            BackColor = ThemeHelper.WarmIvory
        };
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        cmbFilter.Width = 150;
        cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFilter.Items.AddRange(new object[] { "Unresolved", "All", "Resolved" });
        cmbFilter.SelectedIndex = 0;
        cmbFilter.Margin = new Padding(0, 4, 8, 0);
        cmbFilter.SelectedIndexChanged += (s, e) => RefreshGrid();

        btnResolve.Text = "Mark Resolved";
        btnResolve.AutoSize = true;
        btnResolve.MinimumSize = new Size(130, 34);
        ThemeHelper.ApplyPrimaryButton(btnResolve);
        btnResolve.Click += btnResolve_Click;

        btnRefresh.Text = "Refresh";
        btnRefresh.AutoSize = true;
        btnRefresh.MinimumSize = new Size(100, 34);
        btnRefresh.Margin = new Padding(8, 0, 0, 0);
        ThemeHelper.ApplyPrimaryButton(btnRefresh);
        btnRefresh.Click += (s, e) => LoadConcerns();

        toolbar.Controls.AddRange(new Control[] { cmbFilter, btnResolve, btnRefresh });
        panel.Controls.Add(toolbar, 0, 0);

        dgvConcerns.Dock = DockStyle.Fill;
        dgvConcerns.ReadOnly = true;
        dgvConcerns.AllowUserToAddRows = false;
        dgvConcerns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvConcerns.MultiSelect = false;
        dgvConcerns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvConcerns.BackgroundColor = ThemeHelper.WarmIvory;
        panel.Controls.Add(dgvConcerns, 0, 1);
        return panel;
    }

    private static Label Caption(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Fill,
        Font = ThemeHelper.SmallFont,
        ForeColor = ThemeHelper.TextSecondary,
        TextAlign = ContentAlignment.BottomLeft
    };

    private void LoadConcerns()
    {
        try
        {
            _concerns = SqlDataRepository.Instance.GetSupportRequests();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not load concerns: {ex.Message}", "Customer Concerns", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _concerns = new List<SupportRequest>();
        }
        RefreshGrid();
    }

    private void RefreshGrid()
    {
        string filter = cmbFilter.SelectedItem?.ToString() ?? "Unresolved";
        IEnumerable<SupportRequest> query = _concerns;
        if (filter == "Unresolved")
            query = query.Where(c => !c.Status.Equals("Resolved", StringComparison.OrdinalIgnoreCase));
        else if (filter == "Resolved")
            query = query.Where(c => c.Status.Equals("Resolved", StringComparison.OrdinalIgnoreCase));

        dgvConcerns.DataSource = query
            .OrderByDescending(c => c.CreatedDate)
            .Select(c => new
            {
                c.Id,
                Ticket = c.TicketNumber,
                Customer = c.RequestedBy,
                Type = c.Subject,
                c.Details,
                c.Status,
                Date = c.CreatedDate.ToString("MMM d, h:mm tt")
            }).ToList();
    }

    private void btnLog_Click(object? sender, EventArgs e)
    {
        if (txtCustomer.Text.Trim().Length == 0 || txtDetails.Text.Trim().Length == 0)
        {
            MessageBox.Show("Please enter the customer and the details of the concern.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var req = new SupportRequest
        {
            RequestedBy = txtCustomer.Text.Trim(),
            Subject = cmbType.SelectedItem?.ToString() ?? "Complaint",
            Details = txtDetails.Text.Trim(),
            Priority = cmbType.SelectedItem?.ToString() == "Complaint" ? "High" : "Medium"
        };
        SqlDataRepository.Instance.AddSupportRequest(req);
        SqlDataRepository.Instance.AddSystemLog("INFO", "CustomerConcerns",
            $"Logged {req.Subject.ToLowerInvariant()} '{req.TicketNumber}' from '{req.RequestedBy}'", _currentUser.Username);

        txtCustomer.Clear();
        txtDetails.Clear();
        LoadConcerns();
    }

    private void btnResolve_Click(object? sender, EventArgs e)
    {
        if (dgvConcerns.CurrentRow?.DataBoundItem == null) return;
        int id = (int)((dynamic)dgvConcerns.CurrentRow.DataBoundItem).Id;
        SqlDataRepository.Instance.UpdateSupportRequestStatus(id, "Resolved");
        LoadConcerns();
    }
}

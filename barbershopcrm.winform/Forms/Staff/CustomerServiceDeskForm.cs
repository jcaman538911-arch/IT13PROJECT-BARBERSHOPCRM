using System;
using System.Drawing;
using System.Windows.Forms;
using barbershop.domain;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Staff's main daily workspace: New Service, Appointments and Queue in one place.
/// Uses a segmented navigation bar instead of default WinForms tabs.
/// </summary>
public class CustomerServiceDeskForm : Form
{
    private readonly User _currentUser;
    private readonly Panel navigationPanel = new();
    private readonly Panel contentPanel = new();
    private readonly Button btnNewService = new();
    private readonly Button btnAppointments = new();
    private readonly Button btnQueue = new();
    private readonly Label lblSubtitle = new();

    private ServiceTransactionForm? _newService;
    private AppointmentsForm? _appointments;
    private QueueForm? _queue;

    public CustomerServiceDeskForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = "Customer Service Desk";
        BackColor = ThemeHelper.WarmIvory;
        Size = new Size(1200, 700);

        // Create segmented navigation
        CreateNavigationPanel();

        // Create content panel
        contentPanel.Dock = DockStyle.Fill;
        contentPanel.BackColor = ThemeHelper.WarmIvory;
        contentPanel.Padding = new Padding(16);

        Controls.Add(contentPanel);
        Controls.Add(navigationPanel);

        // Create embedded forms
        _newService = new ServiceTransactionForm(_currentUser);
        _appointments = new AppointmentsForm(_currentUser);
        _queue = new QueueForm(_currentUser);

        // Wire up navigation events from Queue form
        _queue.OnSwitchToNewService += () => ShowNewService();
        _queue.OnSwitchToAppointments += () => ShowAppointments();

        // Wire up navigation events from Appointments form
        _appointments.OnSwitchToQueue += () => ShowQueue();

        // Initially show New Service
        ShowNewService();
    }

    private void CreateNavigationPanel()
    {
        navigationPanel.Dock = DockStyle.Top;
        navigationPanel.Height = 108;
        navigationPanel.BackColor = ThemeHelper.WarmIvory;
        navigationPanel.Padding = new Padding(20, 10, 20, 6);

        // Use TableLayoutPanel for proper layout
        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1,
            BackColor = ThemeHelper.WarmIvory
        };

        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));

        // Title/subtitle panel
        var titlePanel = new Panel
        {
            Dock = DockStyle.Fill,
            Height = 45,
            BackColor = ThemeHelper.WarmIvory
        };

        var titleLabel = new Label
        {
            Text = "Customer Service Desk",
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            ForeColor = ThemeHelper.TextPrimary,
            Location = new Point(0, 0),
            AutoSize = true
        };

        lblSubtitle.Text = "Manage walk-ins, appointments, queue, and payments.";
        lblSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        lblSubtitle.ForeColor = ThemeHelper.TextSecondary;
        lblSubtitle.Location = new Point(0, 25);
        lblSubtitle.AutoSize = true;

        titlePanel.Controls.AddRange(new Control[] { titleLabel, lblSubtitle });

        // Navigation button panel with TableLayoutPanel
        var navButtonPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Height = 44,
            ColumnCount = 3,
            RowCount = 1,
            BackColor = ThemeHelper.WarmIvory,
            Margin = new Padding(0, 2, 0, 0)
        };

        navButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
        navButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
        navButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

        ConfigureNavigationButton(btnNewService, "✂ NEW SERVICE", ShowNewService);
        ConfigureNavigationButton(btnAppointments, "📅 APPOINTMENTS", ShowAppointments);
        ConfigureNavigationButton(btnQueue, "≡ QUEUE", ShowQueue);

        navButtonPanel.Controls.Add(btnNewService, 0, 0);
        navButtonPanel.Controls.Add(btnAppointments, 1, 0);
        navButtonPanel.Controls.Add(btnQueue, 2, 0);

        mainLayout.Controls.Add(titlePanel, 0, 0);
        mainLayout.Controls.Add(navButtonPanel, 0, 1);

        navigationPanel.Controls.Add(mainLayout);

        // Set initial selected state
        UpdateNavigationButtons(btnNewService);
    }

    private void ConfigureNavigationButton(Button btn, string text, Action onClick)
    {
        btn.Text = text;
        btn.Height = 36;
        btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 1;
        btn.FlatAppearance.BorderColor = ThemeHelper.WarmGray;
        btn.Cursor = Cursors.Hand;
        btn.Margin = new Padding(2, 2, 2, 2);
        btn.Dock = DockStyle.Fill;
        btn.TextAlign = ContentAlignment.MiddleCenter;
        btn.Click += (s, e) => onClick();

        // Add hover effect
        btn.MouseEnter += (s, e) =>
        {
            if (btn != GetActiveButton())
            {
                btn.BackColor = Color.FromArgb(235, 225, 215); // Slightly darker beige for hover
            }
        };

        btn.MouseLeave += (s, e) =>
        {
            if (btn != GetActiveButton())
            {
                btn.BackColor = ThemeHelper.LightBeige; // Return to inactive state
            }
        };
    }

    private Button? GetActiveButton()
    {
        if (btnNewService.BackColor == ThemeHelper.MutedGold) return btnNewService;
        if (btnAppointments.BackColor == ThemeHelper.MutedGold) return btnAppointments;
        if (btnQueue.BackColor == ThemeHelper.MutedGold) return btnQueue;
        return null;
    }

    private void UpdateNavigationButtons(Button selectedButton)
    {
        // Reset all buttons to inactive state
        foreach (var btn in new[] { btnNewService, btnAppointments, btnQueue })
        {
            btn.BackColor = ThemeHelper.LightBeige;
            btn.ForeColor = ThemeHelper.DeepCharcoal;
            btn.Font = new Font("Segoe UI", 10);
            btn.FlatAppearance.BorderColor = ThemeHelper.WarmGray;
        }

        // Set selected button to active state
        selectedButton.BackColor = ThemeHelper.MutedGold;
        selectedButton.ForeColor = ThemeHelper.DeepCharcoal;
        selectedButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        selectedButton.FlatAppearance.BorderColor = ThemeHelper.DeepCharcoal;
    }

    private void ShowNewService()
    {
        UpdateNavigationButtons(btnNewService);
        contentPanel.Controls.Clear();

        if (_newService != null)
        {
            _newService.TopLevel = false;
            _newService.FormBorderStyle = FormBorderStyle.None;
            _newService.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_newService);
            _newService.Show();
        }
    }

    private void ShowAppointments()
    {
        UpdateNavigationButtons(btnAppointments);
        contentPanel.Controls.Clear();

        if (_appointments != null)
        {
            _appointments.TopLevel = false;
            _appointments.FormBorderStyle = FormBorderStyle.None;
            _appointments.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_appointments);
            _appointments.Show();
            _appointments.Reload(); // Reload when switching to this tab
        }
    }

    private void ShowQueue()
    {
        UpdateNavigationButtons(btnQueue);
        contentPanel.Controls.Clear();

        if (_queue != null)
        {
            _queue.TopLevel = false;
            _queue.FormBorderStyle = FormBorderStyle.None;
            _queue.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_queue);
            _queue.Show();
            _queue.Reload(); // Reload when switching to this tab
        }
    }

    /// <summary>Number of customers currently waiting or called, for the sidebar badge.</summary>
    public static int WaitingCount()
    {
        try
        {
            return barbershop.infrastructure.SqlDataRepository.Instance.GetTodayTransactions()
                .FindAll(t => t.Status is TransactionStatus.Waiting or TransactionStatus.Called).Count;
        }
        catch
        {
            return 0;
        }
    }
}

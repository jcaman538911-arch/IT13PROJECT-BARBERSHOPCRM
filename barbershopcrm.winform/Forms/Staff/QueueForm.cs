using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Live service queue: today's transactions that are Waiting, Called, or In Service.
/// Actions depend on the selected entry's status.
/// </summary>
public class QueueForm : Form
{
    private readonly User _currentUser;
    private readonly DataGridView dgvQueue = new();
    private readonly Button btnCall = new();
    private readonly Button btnStartService = new();
    private readonly Button btnProcessPayment = new();
    private readonly Button btnCancel = new();
    private readonly Button btnRefresh = new();
    private readonly Label lblSummary = new();
    private List<Transaction> _queue = new();

    public QueueForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = "Service Queue";
        BackColor = ThemeHelper.WarmIvory;

        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 56,
            Padding = new Padding(16, 12, 16, 8),
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            BackColor = ThemeHelper.WarmIvory
        };

        ConfigureActionButton(btnCall, "Call Next / Call", btnCall_Click);
        ConfigureActionButton(btnStartService, "Start Service", btnStartService_Click);
        ConfigureActionButton(btnProcessPayment, "Process Payment", btnProcessPayment_Click);
        ConfigureActionButton(btnCancel, "Cancel", btnCancel_Click, danger: true);
        ConfigureActionButton(btnRefresh, "Refresh", (s, e) => LoadQueue());

        lblSummary.AutoSize = false;
        lblSummary.Size = new Size(360, 32);
        lblSummary.TextAlign = ContentAlignment.MiddleLeft;
        lblSummary.Font = ThemeHelper.BodyFont;
        lblSummary.ForeColor = ThemeHelper.TextSecondary;
        lblSummary.Margin = new Padding(16, 4, 0, 0);

        toolbar.Controls.AddRange(new Control[] { btnCall, btnStartService, btnProcessPayment, btnCancel, btnRefresh, lblSummary });

        dgvQueue.Dock = DockStyle.Fill;
        dgvQueue.ReadOnly = true;
        dgvQueue.AllowUserToAddRows = false;
        dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvQueue.MultiSelect = false;
        dgvQueue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvQueue.BackgroundColor = ThemeHelper.WarmIvory;
        dgvQueue.SelectionChanged += (s, e) => UpdateActionState();

        Controls.Add(dgvQueue);
        Controls.Add(toolbar);
        ThemeHelper.ApplyModernGrid(dgvQueue);
        LoadQueue();
        ResponsiveLayoutHelper.Apply(this);
    }

    private void ConfigureActionButton(Button btn, string text, EventHandler onClick, bool danger = false)
    {
        btn.Text = text;
        btn.AutoSize = true;
        btn.MinimumSize = new Size(120, 34);
        btn.Margin = new Padding(0, 0, 8, 0);
        ThemeHelper.ApplyPrimaryButton(btn);
        if (danger)
        {
            btn.BackColor = ThemeHelper.DeepBurgundy;
            btn.ForeColor = ThemeHelper.WarmIvory;
        }
        btn.Click += onClick;
    }

    /// <summary>Re-reads the queue (called when the Service Desk tab regains focus).</summary>
    public void Reload() => LoadQueue();

    private void LoadQueue()
    {
        _queue = SqlDataRepository.Instance.GetTodayTransactions()
            .Where(t => t.Status is TransactionStatus.Waiting or TransactionStatus.Called or TransactionStatus.InService)
            .OrderBy(t => t.TransactionDate)
            .ToList();

        dgvQueue.DataSource = _queue.Select(t => new
        {
            t.Id,
            QueueNo = t.TransactionNumber,
            t.CustomerName,
            t.ServiceName,
            t.BarberName,
            Waiting = FormatWaiting(t.TransactionDate),
            Status = t.Status == TransactionStatus.InService ? "In Service" : t.Status.ToString()
        }).ToList();

        int waiting = _queue.Count(t => t.Status == TransactionStatus.Waiting || t.Status == TransactionStatus.Called);
        lblSummary.Text = $"{_queue.Count} in queue ({waiting} waiting, {_queue.Count - waiting} in service)";
        UpdateActionState();
    }

    private static string FormatWaiting(DateTime since)
    {
        var span = DateTime.Now - since;
        return span.TotalMinutes < 1 ? "just now" : $"{(int)span.TotalMinutes} min";
    }

    private Transaction? SelectedEntry()
    {
        if (dgvQueue.CurrentRow?.DataBoundItem == null) return null;
        int id = (int)((dynamic)dgvQueue.CurrentRow.DataBoundItem).Id;
        return _queue.FirstOrDefault(t => t.Id == id);
    }

    private void UpdateActionState()
    {
        var t = SelectedEntry();
        btnCall.Enabled = t?.Status == TransactionStatus.Waiting;
        btnStartService.Enabled = t?.Status is TransactionStatus.Waiting or TransactionStatus.Called;
        btnProcessPayment.Enabled = t?.Status == TransactionStatus.InService;
        btnCancel.Enabled = t != null;
    }

    private void SetStatus(Transaction txn, TransactionStatus status)
    {
        txn.Status = status;
        SqlDataRepository.Instance.SaveTransaction(txn);
        LoadQueue();
    }

    private void btnCall_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is { } t) SetStatus(t, TransactionStatus.Called);
    }

    private void btnStartService_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is { } t) SetStatus(t, TransactionStatus.InService);
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is { } t &&
            MessageBox.Show($"Cancel queue entry {t.TransactionNumber} for {t.CustomerName}?", "Cancel",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            SetStatus(t, TransactionStatus.Cancelled);
        }
    }

    private void btnProcessPayment_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is not { } t) return;
        if (t.CustomerId.HasValue && SqlDataRepository.Instance.GetCustomerById(t.CustomerId.Value) is { IsLoyaltyMember: true })
            t.PointsEarned = 10;
        using var payModal = new PaymentForm(t);
        if (payModal.ShowDialog(this) == DialogResult.OK)
        {
            SqlDataRepository.Instance.SaveTransaction(payModal.CompletedTransaction);
            SqlDataRepository.Instance.AddSystemLog("INFO", "Payments",
                $"Processed payment for '{payModal.CompletedTransaction.TransactionNumber}' (₱{payModal.CompletedTransaction.FinalAmount:N2})",
                _currentUser.Username);
            LoadQueue();
        }
    }
}

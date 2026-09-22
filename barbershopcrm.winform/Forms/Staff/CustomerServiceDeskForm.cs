using System;
using System.Drawing;
using System.Windows.Forms;
using barbershop.domain;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Staff's main daily workspace: New Service, Appointments and Queue in one place.
/// Hosts the existing forms as embedded tab content rather than duplicating them.
/// </summary>
public class CustomerServiceDeskForm : Form
{
    private readonly User _currentUser;
    private readonly TabControl tabs = new();
    private readonly TabPage tabNewService = new("New Service");
    private readonly TabPage tabAppointments = new("Appointments");
    private readonly TabPage tabQueue = new("Queue");

    private ServiceTransactionForm? _newService;
    private AppointmentsForm? _appointments;
    private QueueForm? _queue;

    public CustomerServiceDeskForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = "Customer Service Desk";
        BackColor = ThemeHelper.WarmIvory;

        tabs.Dock = DockStyle.Fill;
        tabs.Font = ThemeHelper.SubtitleFont;
        tabs.ItemSize = new Size(160, 34);
        tabs.SizeMode = TabSizeMode.Fixed;
        tabs.TabPages.AddRange(new[] { tabNewService, tabAppointments, tabQueue });
        tabs.SelectedIndexChanged += (s, e) => ReloadSelectedTab();

        foreach (TabPage page in tabs.TabPages)
        {
            page.BackColor = ThemeHelper.WarmIvory;
            page.Padding = new Padding(0);
        }

        Controls.Add(tabs);

        _newService = Embed(tabNewService, new ServiceTransactionForm(_currentUser));
        _appointments = Embed(tabAppointments, new AppointmentsForm(_currentUser));
        _queue = Embed(tabQueue, new QueueForm(_currentUser));
    }

    private static T Embed<T>(TabPage page, T child) where T : Form
    {
        child.TopLevel = false;
        child.FormBorderStyle = FormBorderStyle.None;
        child.Dock = DockStyle.Fill;
        page.Controls.Add(child);
        child.Show();
        return child;
    }

    /// <summary>Queue and appointment lists must reflect actions taken on the other tabs.</summary>
    private void ReloadSelectedTab()
    {
        if (tabs.SelectedTab == tabQueue) _queue?.Reload();
        else if (tabs.SelectedTab == tabAppointments) _appointments?.Reload();
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

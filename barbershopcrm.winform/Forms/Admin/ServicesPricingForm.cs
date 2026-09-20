using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class ServicesPricingForm : Form
{
    private int _selectedServiceId = 0;

    public ServicesPricingForm()
    {
        InitializeComponent();
        ThemeHelper.ApplyModernGrid(dgvServices);
        LoadServices();
    }

    private void LoadServices()
    {
        var services = SqlDataRepository.Instance.GetServices();
        dgvServices.DataSource = services.Select(s => new
        {
            s.Id,
            s.ServiceName,
            s.Description,
            BasePrice = $"₱{s.BasePrice:N2}",
            Status = s.IsActive ? "Active" : "Inactive"
        }).ToList();
    }

    private void dgvServices_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvServices.CurrentRow != null && dgvServices.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvServices.CurrentRow.DataBoundItem;
            _selectedServiceId = item.Id;

            var service = SqlDataRepository.Instance.GetServices().FirstOrDefault(s => s.Id == _selectedServiceId);
            if (service != null)
            {
                txtServiceName.Text = service.ServiceName;
                txtDescription.Text = service.Description;
                numBasePrice.Value = service.BasePrice;
                chkActive.Checked = service.IsActive;
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtServiceName.Text))
        {
            MessageBox.Show("Service Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var svc = new ServiceItem
        {
            ServiceName = txtServiceName.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            BasePrice = numBasePrice.Value,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.AddService(svc);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Services", $"Added service '{svc.ServiceName}' price ₱{svc.BasePrice:N2}", "admin");
        MessageBox.Show("Service added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadServices();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedServiceId == 0)
        {
            MessageBox.Show("Please select a service to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtServiceName.Text))
        {
            MessageBox.Show("Service Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var svc = new ServiceItem
        {
            Id = _selectedServiceId,
            ServiceName = txtServiceName.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            BasePrice = numBasePrice.Value,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.UpdateService(svc);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Services", $"Updated service ID {_selectedServiceId} '{svc.ServiceName}' price ₱{svc.BasePrice:N2}", "admin");
        MessageBox.Show("Service & Base Price updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadServices();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedServiceId == 0)
        {
            MessageBox.Show("Please select a service to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete service ID {_selectedServiceId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteService(_selectedServiceId);
            SqlDataRepository.Instance.AddSystemLog("WARN", "Services", $"Deleted service ID {_selectedServiceId}", "admin");
            MessageBox.Show("Service deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadServices();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedServiceId = 0;
        txtServiceName.Clear();
        txtDescription.Clear();
        numBasePrice.Value = 200.00m;
        chkActive.Checked = true;
    }
}

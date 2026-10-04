using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class EmployeesForm : Form
{
    private int _selectedEmployeeId = 0;

    public EmployeesForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvEmployees);
        PopulatePositions();
        LoadEmployees();
    }

    private void PopulatePositions()
    {
        cmbPosition.DataSource = Enum.GetValues(typeof(EmployeePosition));
    }

    private async void LoadEmployees(string query = "")
    {
        var employees = await Task.Run(() => 
        {
            var data = SqlDataRepository.Instance.GetEmployees(includeInactive: true);
            if (!string.IsNullOrWhiteSpace(query))
            {
                var q = query.ToLower();
                data = data.Where(e => e.Name.StartsWith(q, StringComparison.OrdinalIgnoreCase) || e.ContactNumber.StartsWith(q)).ToList();
            }
            return data;
        });

        dgvEmployees.DataSource = employees.Select(e => new
        {
            e.Id,
            e.Name,
            e.ContactNumber,
            Position = e.Position.ToString(),
            Status = e.IsActive ? "Active" : "Inactive"
        }).ToList();
    }

    private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvEmployees.CurrentRow?.DataBoundItem == null) return;

        dynamic item = dgvEmployees.CurrentRow.DataBoundItem;
        _selectedEmployeeId = item.Id;

        var emp = SqlDataRepository.Instance.GetEmployees(includeInactive: true).FirstOrDefault(e => e.Id == _selectedEmployeeId);
        if (emp != null)
        {
            txtName.Text = emp.Name;
            txtContact.Text = emp.ContactNumber;
            cmbPosition.SelectedItem = emp.Position;
            chkActive.Checked = emp.IsActive;
        }
    }

    // Reads the selected row's ID directly from the grid — more reliable than _selectedEmployeeId alone.
    private int GetCurrentRowId()
    {
        if (dgvEmployees.CurrentRow?.DataBoundItem == null) return _selectedEmployeeId;
        dynamic item = dgvEmployees.CurrentRow.DataBoundItem;
        return item.Id;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Employee Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var emp = new Employee
        {
            Name = txtName.Text.Trim(),
            ContactNumber = txtContact.Text.Trim(),
            Position = (EmployeePosition)cmbPosition.SelectedItem!,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.AddEmployee(emp);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Employees", $"Added employee '{emp.Name}' ({emp.Position})", "admin");
        MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadEmployees();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedEmployeeId == 0)
        {
            MessageBox.Show("Please select an employee from the table to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Employee Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var emp = new Employee
        {
            Id = _selectedEmployeeId,
            Name = txtName.Text.Trim(),
            ContactNumber = txtContact.Text.Trim(),
            Position = (EmployeePosition)cmbPosition.SelectedItem!,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.UpdateEmployee(emp);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Employees", $"Updated employee ID {_selectedEmployeeId} '{emp.Name}'", "admin");
        MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadEmployees();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        int id = GetCurrentRowId();
        if (id == 0)
        {
            MessageBox.Show("Please select an employee to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete employee ID {id}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteEmployee(id);
            SqlDataRepository.Instance.AddSystemLog("WARN", "Employees", $"Deleted employee ID {id}", "admin");
            MessageBox.Show("Employee deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadEmployees();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedEmployeeId = 0;
        txtName.Clear();
        txtContact.Clear();
        cmbPosition.SelectedIndex = 0;
        chkActive.Checked = true;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        LoadEmployees(txtSearch.Text.Trim());
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadEmployees();
    }
}

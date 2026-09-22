using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class SystemUsersForm : Form
{
    private int _selectedUserId = 0;

    public SystemUsersForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvUsers);
        PopulateRoles();
        LoadUsers();
    }

    private void PopulateRoles()
    {
        cmbRole.DataSource = Enum.GetValues(typeof(UserRole));
    }

    private void LoadUsers(string query = "")
    {
        var users = SqlDataRepository.Instance.GetUsers();
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.ToLower();
            users = users.Where(u => u.Username.ToLower().Contains(query) || u.FullName.ToLower().Contains(query)).ToList();
        }

        dgvUsers.DataSource = users.Select(u => new
        {
            u.Id,
            u.Username,
            u.FullName,
            Role = u.Role.ToString(),
            u.IsActive,
            u.CreatedDate
        }).ToList();
    }

    private void dgvUsers_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvUsers.CurrentRow.DataBoundItem;
            _selectedUserId = item.Id;

            var user = SqlDataRepository.Instance.GetUsers().FirstOrDefault(u => u.Id == _selectedUserId);
            if (user != null)
            {
                txtUsername.Text = user.Username;
                txtPassword.Text = user.Password;
                txtFullName.Text = user.FullName;
                cmbRole.SelectedItem = user.Role;
                chkActive.Checked = user.IsActive;
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!ValidateForm()) return;

        var newUser = new User
        {
            Username = txtUsername.Text.Trim(),
            Password = txtPassword.Text,
            FullName = txtFullName.Text.Trim(),
            Role = (UserRole)cmbRole.SelectedItem!,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.AddUser(newUser);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Users", $"Added new user '{newUser.Username}' ({newUser.Role})", "superadmin");
        MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadUsers();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedUserId == 0)
        {
            MessageBox.Show("Please select a user from the list to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!ValidateForm()) return;

        var updatedUser = new User
        {
            Id = _selectedUserId,
            Username = txtUsername.Text.Trim(),
            Password = txtPassword.Text,
            FullName = txtFullName.Text.Trim(),
            Role = (UserRole)cmbRole.SelectedItem!,
            IsActive = chkActive.Checked
        };

        SqlDataRepository.Instance.UpdateUser(updatedUser);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Users", $"Updated user ID {_selectedUserId} '{updatedUser.Username}'", "superadmin");
        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadUsers();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedUserId == 0)
        {
            MessageBox.Show("Please select a user from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete user ID {_selectedUserId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteUser(_selectedUserId);
            SqlDataRepository.Instance.AddSystemLog("WARN", "Users", $"Deleted user ID {_selectedUserId}", "superadmin");
            MessageBox.Show("User deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadUsers();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedUserId = 0;
        txtUsername.Clear();
        txtPassword.Clear();
        txtFullName.Clear();
        cmbRole.SelectedIndex = 0;
        chkActive.Checked = true;
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        LoadUsers(txtSearch.Text.Trim());
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadUsers();
    }
}

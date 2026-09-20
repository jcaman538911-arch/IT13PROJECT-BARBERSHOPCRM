using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class BranchesManagementForm : Form
{
    private int _selectedBranchId = 0;

    public BranchesManagementForm()
    {
        InitializeComponent();
        ThemeHelper.ApplyModernGrid(dgvBranches);
        LoadBranches();
    }

    private void LoadBranches()
    {
        var branches = SqlDataRepository.Instance.GetBranches();
        dgvBranches.DataSource = branches.Select(b => new
        {
            b.Id,
            b.BranchName,
            b.Address,
            b.ContactInformation,
            b.Status,
            Created = b.CreatedAt.ToString("yyyy-MM-dd")
        }).ToList();

        ClearForm();
    }

    private void dgvBranches_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvBranches.CurrentRow != null && dgvBranches.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvBranches.CurrentRow.DataBoundItem;
            _selectedBranchId = item.Id;

            var branch = SqlDataRepository.Instance.GetBranches().FirstOrDefault(b => b.Id == _selectedBranchId);
            if (branch != null)
            {
                txtBranchName.Text = branch.BranchName;
                txtAddress.Text = branch.Address;
                txtContactInfo.Text = branch.ContactInformation;
                chkIsActive.Checked = branch.Status.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtBranchName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Please enter a valid branch name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var branch = new Branch
        {
            BranchName = name,
            Address = txtAddress.Text.Trim(),
            ContactInformation = txtContactInfo.Text.Trim(),
            Status = chkIsActive.Checked ? "ACTIVE" : "INACTIVE"
        };

        SqlDataRepository.Instance.AddBranch(branch);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Branches", $"Added new branch '{branch.BranchName}'", "Admin");
        MessageBox.Show($"Branch '{branch.BranchName}' created successfully.", "Branch Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadBranches();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedBranchId == 0)
        {
            MessageBox.Show("Please select a branch to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string name = txtBranchName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Branch name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var branch = new Branch
        {
            Id = _selectedBranchId,
            BranchName = name,
            Address = txtAddress.Text.Trim(),
            ContactInformation = txtContactInfo.Text.Trim(),
            Status = chkIsActive.Checked ? "ACTIVE" : "INACTIVE"
        };

        SqlDataRepository.Instance.UpdateBranch(branch);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Branches", $"Updated branch '{branch.BranchName}'", "Admin");
        MessageBox.Show($"Branch '{branch.BranchName}' updated successfully.", "Branch Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadBranches();
    }

    private void btnDeactivate_Click(object sender, EventArgs e)
    {
        if (_selectedBranchId == 0)
        {
            MessageBox.Show("Please select a branch to deactivate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show("Are you sure you want to deactivate this branch?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteBranch(_selectedBranchId);
            SqlDataRepository.Instance.AddSystemLog("WARNING", "Branches", $"Deactivated branch ID {_selectedBranchId}", "Admin");
            MessageBox.Show("Branch deactivated.", "Branch Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadBranches();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedBranchId = 0;
        txtBranchName.Clear();
        txtAddress.Clear();
        txtContactInfo.Clear();
        chkIsActive.Checked = true;
    }
}

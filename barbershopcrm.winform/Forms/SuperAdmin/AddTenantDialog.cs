using System;
using System.Drawing;
using System.Windows.Forms;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

public class AddTenantDialog : Form
{
    private TextBox txtCompany = null!;
    private TextBox txtDbName = null!;
    private TextBox txtDbPassword = null!;
    private ComboBox cmbPlan = null!;
    private TextBox txtFee = null!;
    private Button btnCreate = null!;
    private Button btnCancel = null!;

    public AddTenantDialog()
    {
        Text = "Provision New Tenant";
        Size = new Size(500, 650);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        AutoScroll = true;
        BackColor = ThemeHelper.WarmCanvas;

        var lblTitle = new Label
        {
            Text = "Create New Tenant Database",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = ThemeHelper.PrimaryGold,
            AutoSize = true,
            Location = new Point(20, 20)
        };

        var lblNote = new Label
        {
            Text = "Important: You must first create the blank database on MonsterASP.\nThis tool will run the setup scripts and register the tenant.",
            Font = new Font("Segoe UI", 9F, FontStyle.Italic),
            ForeColor = Color.Red,
            AutoSize = true,
            Location = new Point(20, 60)
        };

        this.Controls.Add(lblTitle);
        this.Controls.Add(lblNote);

        int y = 110;

        txtCompany = AddInputRow("Company Name:", y, out y);
        txtDbName = AddInputRow("Database Name (e.g. db99999):", y, out y);
        txtDbPassword = AddInputRow("Database Password:", y, out y, true);

        // Plan Dropdown
        var lblPlan = new Label { Text = "Subscription Plan:", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        cmbPlan = new ComboBox { Location = new Point(20, y + 25), Width = 440, Font = new Font("Segoe UI", 11F), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbPlan.Items.AddRange(new[] { "Standard", "Premium", "Enterprise" });
        cmbPlan.SelectedIndex = 1;
        this.Controls.Add(lblPlan);
        this.Controls.Add(cmbPlan);
        y += 70;

        txtFee = AddInputRow("Monthly Fee (₱):", y, out y);
        txtFee.Text = "1999.00";

        btnCreate = new Button
        {
            Text = "🚀 Provision Tenant",
            Location = new Point(20, y + 20),
            Size = new Size(200, 40),
            BackColor = ThemeHelper.PrimaryGold,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        };
        btnCreate.FlatAppearance.BorderSize = 0;
        btnCreate.Click += BtnCreate_Click;

        btnCancel = new Button
        {
            Text = "Cancel",
            Location = new Point(230, y + 20),
            Size = new Size(120, 40),
            BackColor = ThemeHelper.SecondaryNavy,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10F)
        };
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.Click += (s, e) => this.Close();

        this.Controls.Add(btnCreate);
        this.Controls.Add(btnCancel);
    }

    private TextBox AddInputRow(string label, int startY, out int nextY, bool isPassword = false)
    {
        var lbl = new Label { Text = label, Location = new Point(20, startY), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        var txt = new TextBox { Location = new Point(20, startY + 25), Width = 440, Font = new Font("Segoe UI", 11F) };
        if (isPassword) txt.PasswordChar = '●';
        this.Controls.Add(lbl);
        this.Controls.Add(txt);
        nextY = startY + 70;
        return txt;
    }

    private void BtnCreate_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCompany.Text) || string.IsNullOrWhiteSpace(txtDbName.Text) || string.IsNullOrWhiteSpace(txtDbPassword.Text))
        {
            MessageBox.Show("Please fill out all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtFee.Text, out decimal fee))
        {
            MessageBox.Show("Invalid monthly fee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        btnCreate.Enabled = false;
        btnCreate.Text = "Deploying Scripts...";

        try
        {
            Cursor = Cursors.WaitCursor;
            int newId = SqlDataRepository.Instance.AddNewTenant(
                txtCompany.Text.Trim(),
                txtDbName.Text.Trim(),
                txtDbPassword.Text,
                cmbPlan.SelectedItem?.ToString() ?? "Standard",
                fee
            );

            MessageBox.Show($"Tenant provisioned successfully!\nTenant ID: {newId}\nDatabase schema has been initialized.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to provision tenant.\n{ex.Message}\n\nMake sure the database exists on MonsterASP first!", "Deployment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
            btnCreate.Enabled = true;
            btnCreate.Text = "🚀 Provision Tenant";
        }
    }
}

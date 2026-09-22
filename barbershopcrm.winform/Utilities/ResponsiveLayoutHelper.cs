using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BarberShopCRM.Helpers;

/// <summary>
/// Reusable full-window layout pass applied to every module form.
/// Converts the fixed-size designer layouts into responsive ones:
/// controls sitting at the window edges stretch with the form,
/// grids fill their area and get uniform row height/empty states,
/// and checkbox grids support true multi-selection.
/// </summary>
public static class ResponsiveLayoutHelper
{
    private const int EdgeMargin = 80;

    /// <summary>
    /// Applies the full-window layout to a borderless module form that is
    /// hosted Dock=Fill inside MainForm's content panel.
    /// </summary>
    public static void Apply(Form form)
    {
        form.AutoScroll = true;
        StretchEdgeControls(form);
        FixGrids(form.Controls);
    }

    /// <summary>
    /// Top-level controls that already reach (within <see cref="EdgeMargin"/> of)
    /// the bottom/right edge of the design-time client size are anchored to that
    /// edge so they stretch instead of leaving dead space.
    /// </summary>
    private static void StretchEdgeControls(Form form)
    {
        int clientW = form.ClientSize.Width;
        int clientH = form.ClientSize.Height;

        foreach (Control ctrl in form.Controls)
        {
            if (ctrl.Dock != DockStyle.None) continue;
            if (ctrl is Form) continue;

            var anchor = ctrl.Anchor;
            int bottomMargin = clientH - ctrl.Bottom;
            int rightMargin = clientW - ctrl.Right;

            // Tall control near the bottom edge -> stretch vertically.
            if (!anchor.HasFlag(AnchorStyles.Bottom)
                && bottomMargin <= EdgeMargin
                && ctrl.Height >= clientH * 0.3)
            {
                ctrl.Anchor = anchor | AnchorStyles.Bottom;
                anchor = ctrl.Anchor;
            }

            // Wide control near the right edge -> stretch horizontally.
            if (!anchor.HasFlag(AnchorStyles.Right)
                && rightMargin <= EdgeMargin
                && ctrl.Width >= clientW * 0.4)
            {
                ctrl.Anchor = anchor | AnchorStyles.Right;
            }
        }
    }

    private static void FixGrids(Control.ControlCollection controls)
    {
        foreach (Control ctrl in controls)
        {
            if (ctrl is DataGridView dgv)
            {
                ApplyGridRules(dgv);
            }
            if (ctrl.HasChildren)
            {
                FixGrids(ctrl.Controls);
            }
        }
    }

    private static void ApplyGridRules(DataGridView dgv)
    {
        dgv.AllowUserToResizeRows = false;
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        if (dgv.RowTemplate.Height < 30) dgv.RowTemplate.Height = 38;
        if (dgv.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.None)
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // Commit checkbox edits immediately so multi-select doesn't depend
        // on leaving the cell.
        dgv.CurrentCellDirtyStateChanged += (s, e) =>
        {
            if (dgv.IsCurrentCellDirty && dgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        };

        AttachEmptyState(dgv);
    }

    /// <summary>
    /// Shows a friendly "no records" message instead of a blank grid.
    /// </summary>
    private static void AttachEmptyState(DataGridView dgv)
    {
        var emptyLabel = new Label
        {
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            BackColor = dgv.BackgroundColor,
            Text = EmptyMessageFor(dgv),
            Visible = false
        };

        void Refresh()
        {
            try
            {
                emptyLabel.Visible = dgv.Rows.Count == 0;
            }
            catch { }
        }

        dgv.ParentChanged += (s, e) =>
        {
            if (dgv.Parent != null && emptyLabel.Parent == null)
            {
                dgv.Parent.Controls.Add(emptyLabel);
                emptyLabel.BringToFront();
                // Cover only the data area, below the header feel.
                emptyLabel.Bounds = dgv.Bounds;
                emptyLabel.Anchor = dgv.Anchor;
                emptyLabel.Dock = dgv.Dock;
                emptyLabel.Margin = dgv.Margin;
                emptyLabel.Text = EmptyMessageFor(dgv);
                Refresh();
            }
        };

        dgv.DataSourceChanged += (s, e) => Refresh();
        dgv.RowsAdded += (s, e) => Refresh();
        dgv.RowsRemoved += (s, e) => Refresh();
        dgv.HandleCreated += (s, e) => Refresh();

        if (dgv.Parent != null)
        {
            dgv.Parent.Controls.Add(emptyLabel);
            emptyLabel.BringToFront();
            emptyLabel.Bounds = dgv.Bounds;
            emptyLabel.Anchor = dgv.Anchor;
            emptyLabel.Dock = dgv.Dock;
            emptyLabel.Margin = dgv.Margin;
            Refresh();
        }
    }

    private static string EmptyMessageFor(DataGridView dgv)
    {
        string name = (dgv.Name ?? string.Empty).ToLowerInvariant();
        string parentText = dgv.Parent?.Text?.ToLowerInvariant() ?? string.Empty;
        string context = name + " " + parentText;

        if (context.Contains("appointment")) return "No appointments scheduled for this date.";
        if (context.Contains("queue")) return "The service queue is currently empty.";
        if (context.Contains("transaction") || context.Contains("receipt")) return "No completed transactions today.";
        if (context.Contains("barber") || context.Contains("employee")) return "No records match the current filter.";
        if (context.Contains("reward") || context.Contains("loyalty")) return "No rewards currently available for this customer.";
        if (context.Contains("promotion")) return "No eligible promotions available.";
        if (context.Contains("inventory") || context.Contains("stock")) return "No inventory items match the current filter.";
        if (context.Contains("customer")) return "No customers match the current search.";
        if (context.Contains("history")) return "No history records found.";
        return "No records to display.";
    }
}

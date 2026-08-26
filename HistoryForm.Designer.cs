namespace DbScoutBaby;

partial class HistoryForm
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlHeader;
    private Label lblTitle;
    private Label lblCount;

    private DataGridView dgvHistory;

    private Panel pnlBottom;
    private Button btnClose;
    private Button btnClear;

    protected override void Dispose(
        bool disposing)
    {
        if (disposing &&
            components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlHeader =
            new Panel();

        lblTitle =
            new Label();

        lblCount =
            new Label();

        dgvHistory =
            new DataGridView();

        pnlBottom =
            new Panel();

        btnClose =
            new Button();

        btnClear =
            new Button();

        ((System.ComponentModel.ISupportInitialize)dgvHistory)
            .BeginInit();

        SuspendLayout();

        ClientSize =
            new Size(900, 560);

        StartPosition =
            FormStartPosition.CenterParent;

        Text =
            "DbScoutBaby - Search History";

        BackColor =
            Color.FromArgb(255, 249, 246);

        Font =
            new Font(
                "Segoe UI",
                9.5F);

        pnlHeader.Dock =
            DockStyle.Top;

        pnlHeader.Height =
            82;

        pnlHeader.BackColor =
            Color.FromArgb(238, 228, 255);

        lblTitle.AutoSize =
            true;

        lblTitle.Location =
            new Point(22, 20);

        lblTitle.Font =
            new Font(
                "Segoe UI",
                17F,
                FontStyle.Bold);

        lblTitle.ForeColor =
            Color.FromArgb(100, 78, 142);

        lblTitle.Text =
            "Search History";

        lblCount.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        lblCount.Location =
            new Point(700, 25);

        lblCount.Size =
            new Size(170, 28);

        lblCount.TextAlign =
            ContentAlignment.MiddleRight;

        lblCount.ForeColor =
            Color.FromArgb(103, 111, 132);

        lblCount.Text =
            "0 searches";

        pnlHeader.Controls.Add(
            lblTitle);

        pnlHeader.Controls.Add(
            lblCount);

        dgvHistory.Dock =
            DockStyle.Fill;

        dgvHistory.ReadOnly =
            true;

        dgvHistory.AllowUserToAddRows =
            false;

        dgvHistory.AllowUserToDeleteRows =
            false;

        dgvHistory.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

        dgvHistory.BackgroundColor =
            Color.FromArgb(255, 253, 251);

        dgvHistory.BorderStyle =
            BorderStyle.None;

        dgvHistory.RowHeadersVisible =
            false;

        dgvHistory.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        pnlBottom.Dock =
            DockStyle.Bottom;

        pnlBottom.Height =
            65;

        pnlBottom.BackColor =
            Color.FromArgb(255, 252, 250);

        btnClose.Location =
            new Point(18, 14);

        btnClose.Size =
            new Size(100, 38);

        btnClose.FlatStyle =
            FlatStyle.Flat;

        btnClose.BackColor =
            Color.FromArgb(237, 247, 255);

        btnClose.Text =
            "Close";

        btnClose.Click +=
            BtnClose_Click;

        btnClear.Anchor =
            AnchorStyles.Right |
            AnchorStyles.Bottom;

        btnClear.Location =
            new Point(750, 14);

        btnClear.Size =
            new Size(125, 38);

        btnClear.FlatStyle =
            FlatStyle.Flat;

        btnClear.FlatAppearance.BorderSize =
            0;

        btnClear.BackColor =
            Color.FromArgb(255, 229, 237);

        btnClear.ForeColor =
            Color.FromArgb(123, 73, 91);

        btnClear.Text =
            "Clear History";

        btnClear.Click +=
            BtnClear_Click;

        pnlBottom.Controls.Add(
            btnClose);

        pnlBottom.Controls.Add(
            btnClear);

        Controls.Add(
            dgvHistory);

        Controls.Add(
            pnlBottom);

        Controls.Add(
            pnlHeader);

        ((System.ComponentModel.ISupportInitialize)dgvHistory)
            .EndInit();

        ResumeLayout(false);
    }
}

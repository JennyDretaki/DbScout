namespace DbScoutBaby;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlSidebar;
    private Label lblBrand;
    private Label lblTagline;
    private Label lblServerTitle;
    private Label lblServer;
    private Label lblDatabaseTitle;
    private RadioButton rbDev;
    private RadioButton rbCtCollect;
    private RadioButton rbBoth;
    private Label lblOptionsTitle;
    private CheckBox chkSearchCode;
    private Label lblSimilarity;
    private NumericUpDown numSimilarity;
    private Button btnHistory;
    private Button btnRefresh;

    private Panel pnlTop;
    private Label lblTitle;
    private Label lblSubtitle;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnCancel;

    private SplitContainer splitMain;

    private Panel pnlResultsHeader;
    private Label lblResults;
    private Label lblCount;

    private DataGridView dgvResults;
    private DataGridViewTextBoxColumn colDatabase;
    private DataGridViewTextBoxColumn colType;
    private DataGridViewTextBoxColumn colSchema;
    private DataGridViewTextBoxColumn colObject;
    private DataGridViewTextBoxColumn colParent;
    private DataGridViewTextBoxColumn colExtra;
    private DataGridViewTextBoxColumn colScore;

    private Panel pnlPreview;
    private Panel pnlPreviewHeader;
    private Label lblPreview;
    private Label lblSelected;
    private Button btnCopy;
    private RichTextBox txtPreview;

    private Panel pnlStatus;
    private Label lblStatus;
    private ProgressBar progressBar;

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
        components =
            new System.ComponentModel.Container();

        pnlSidebar = new Panel();
        lblBrand = new Label();
        lblTagline = new Label();
        lblServerTitle = new Label();
        lblServer = new Label();
        lblDatabaseTitle = new Label();

        rbDev = new RadioButton();
        rbCtCollect = new RadioButton();
        rbBoth = new RadioButton();

        lblOptionsTitle = new Label();
        chkSearchCode = new CheckBox();

        lblSimilarity = new Label();
        numSimilarity = new NumericUpDown();

        btnHistory = new Button();
        btnRefresh = new Button();

        pnlTop = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnCancel = new Button();

        splitMain = new SplitContainer();

        pnlResultsHeader = new Panel();
        lblResults = new Label();
        lblCount = new Label();

        dgvResults = new DataGridView();

        colDatabase = new DataGridViewTextBoxColumn();
        colType = new DataGridViewTextBoxColumn();
        colSchema = new DataGridViewTextBoxColumn();
        colObject = new DataGridViewTextBoxColumn();
        colParent = new DataGridViewTextBoxColumn();
        colExtra = new DataGridViewTextBoxColumn();
        colScore = new DataGridViewTextBoxColumn();

        pnlPreview = new Panel();
        pnlPreviewHeader = new Panel();
        lblPreview = new Label();
        lblSelected = new Label();
        btnCopy = new Button();
        txtPreview = new RichTextBox();

        pnlStatus = new Panel();
        lblStatus = new Label();
        progressBar = new ProgressBar();

        ((System.ComponentModel.ISupportInitialize)numSimilarity)
            .BeginInit();

        ((System.ComponentModel.ISupportInitialize)splitMain)
            .BeginInit();

        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)dgvResults)
            .BeginInit();

        SuspendLayout();

        // =====================================================
        // FORM
        // =====================================================

        AutoScaleDimensions =
            new SizeF(7F, 15F);

        AutoScaleMode =
            AutoScaleMode.Font;

        ClientSize =
            new Size(1600, 920);

        MinimumSize =
            new Size(1200, 720);

        StartPosition =
            FormStartPosition.CenterScreen;

        Text =
            "DbScoutBaby";

        BackColor =
            Color.FromArgb(255, 249, 246);

        Font =
            new Font(
                "Segoe UI",
                9.5F);

        // =====================================================
        // SIDEBAR
        // =====================================================

        pnlSidebar.Dock =
            DockStyle.Left;

        pnlSidebar.Width =
            260;

        pnlSidebar.BackColor =
            Color.FromArgb(238, 228, 255);

        // Brand
        lblBrand.AutoSize =
            true;

        lblBrand.Location =
            new Point(22, 22);

        lblBrand.Font =
            new Font(
                "Segoe UI",
                21F,
                FontStyle.Bold);

        lblBrand.ForeColor =
            Color.FromArgb(100, 78, 142);

        lblBrand.Text =
            "DbScoutBaby";

        // Tagline
        lblTagline.Location =
            new Point(24, 68);

        lblTagline.Size =
            new Size(210, 55);

        lblTagline.ForeColor =
            Color.FromArgb(112, 98, 130);

        lblTagline.Text =
            "A soft little explorer for your very big SQL Server.";

        // Server
        lblServerTitle.AutoSize =
            true;

        lblServerTitle.Location =
            new Point(24, 145);

        lblServerTitle.Font =
            new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);

        lblServerTitle.ForeColor =
            Color.FromArgb(117, 93, 157);

        lblServerTitle.Text =
            "SERVER";

        lblServer.Location =
            new Point(24, 173);

        lblServer.Size =
            new Size(210, 30);

        lblServer.ForeColor =
            Color.FromArgb(75, 72, 85);

        lblServer.Text =
            "LAWPRODSRVNEW";

        // Database title
        lblDatabaseTitle.AutoSize =
            true;

        lblDatabaseTitle.Location =
            new Point(24, 225);

        lblDatabaseTitle.Font =
            new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);

        lblDatabaseTitle.ForeColor =
            Color.FromArgb(117, 93, 157);

        lblDatabaseTitle.Text =
            "DATABASE";

        rbDev.AutoSize =
            true;

        rbDev.Location =
            new Point(26, 258);

        rbDev.Text =
            "DEV";

        rbDev.ForeColor =
            Color.FromArgb(75, 72, 85);

        rbCtCollect.AutoSize =
            true;

        rbCtCollect.Location =
            new Point(26, 291);

        rbCtCollect.Text =
            "CTCOLLECT";

        rbCtCollect.ForeColor =
            Color.FromArgb(75, 72, 85);

        rbBoth.AutoSize =
            true;

        rbBoth.Location =
            new Point(26, 324);

        rbBoth.Text =
            "BOTH";

        rbBoth.Checked =
            true;

        rbBoth.ForeColor =
            Color.FromArgb(75, 72, 85);

        // Options
        lblOptionsTitle.AutoSize =
            true;

        lblOptionsTitle.Location =
            new Point(24, 385);

        lblOptionsTitle.Font =
            new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);

        lblOptionsTitle.ForeColor =
            Color.FromArgb(117, 93, 157);

        lblOptionsTitle.Text =
            "OPTIONS";

        chkSearchCode.AutoSize =
            true;

        chkSearchCode.Location =
            new Point(26, 418);

        chkSearchCode.Text =
            "Search SQL source code";

        chkSearchCode.Checked =
            true;

        chkSearchCode.ForeColor =
            Color.FromArgb(75, 72, 85);

        // Similarity
        lblSimilarity.AutoSize =
            true;

        lblSimilarity.Location =
            new Point(24, 472);

        lblSimilarity.Font =
            new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);

        lblSimilarity.ForeColor =
            Color.FromArgb(117, 93, 157);

        lblSimilarity.Text =
            "MINIMUM SIMILARITY";

        numSimilarity.Location =
            new Point(26, 503);

        numSimilarity.Size =
            new Size(90, 30);

        numSimilarity.Minimum =
            0;

        numSimilarity.Maximum =
            100;

        numSimilarity.Value =
            45;

        numSimilarity.BackColor =
            Color.FromArgb(255, 253, 255);

        // Buttons
        btnHistory.Anchor =
            AnchorStyles.Left |
            AnchorStyles.Bottom;

        btnHistory.Location =
            new Point(24, 808);

        btnHistory.Size =
            new Size(212, 42);

        btnHistory.FlatStyle =
            FlatStyle.Flat;

        btnHistory.FlatAppearance.BorderSize =
            0;

        btnHistory.BackColor =
            Color.FromArgb(211, 225, 255);

        btnHistory.ForeColor =
            Color.FromArgb(68, 82, 120);

        btnHistory.Font =
            new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold);

        btnHistory.Text =
            "Search History";

        btnHistory.Click +=
            BtnHistory_Click;

        btnRefresh.Anchor =
            AnchorStyles.Left |
            AnchorStyles.Bottom;

        btnRefresh.Location =
            new Point(24, 858);

        btnRefresh.Size =
            new Size(212, 42);

        btnRefresh.FlatStyle =
            FlatStyle.Flat;

        btnRefresh.FlatAppearance.BorderSize =
            0;

        btnRefresh.BackColor =
            Color.FromArgb(202, 242, 228);

        btnRefresh.ForeColor =
            Color.FromArgb(51, 105, 89);

        btnRefresh.Font =
            new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold);

        btnRefresh.Text =
            "Refresh Metadata Cache";

        btnRefresh.Click +=
            BtnRefresh_Click;

        pnlSidebar.Controls.AddRange(
            new Control[]
            {
                lblBrand,
                lblTagline,
                lblServerTitle,
                lblServer,
                lblDatabaseTitle,
                rbDev,
                rbCtCollect,
                rbBoth,
                lblOptionsTitle,
                chkSearchCode,
                lblSimilarity,
                numSimilarity,
                btnHistory,
                btnRefresh
            });

        // =====================================================
        // TOP
        // =====================================================

        pnlTop.Dock =
            DockStyle.Top;

        pnlTop.Height =
            155;

        pnlTop.BackColor =
            Color.FromArgb(255, 252, 250);

        lblTitle.AutoSize =
            true;

        lblTitle.Location =
            new Point(34, 20);

        lblTitle.Font =
            new Font(
                "Segoe UI",
                19F,
                FontStyle.Bold);

        lblTitle.ForeColor =
            Color.FromArgb(91, 78, 113);

        lblTitle.Text =
            "Search Everything ✨";

        lblSubtitle.AutoSize =
            true;

        lblSubtitle.Location =
            new Point(36, 61);

        lblSubtitle.ForeColor =
            Color.FromArgb(124, 111, 133);

        lblSubtitle.Text =
            "Tables • columns • views • procedures • functions • triggers • indexes • jobs • agent objects • and more";

        txtSearch.Location =
            new Point(36, 103);

        txtSearch.Size =
            new Size(650, 31);

        txtSearch.PlaceholderText =
            "Type a keyword, partial name or even a typo...";

        txtSearch.BackColor =
            Color.FromArgb(255, 255, 255);

        txtSearch.KeyDown +=
            TxtSearch_KeyDown;

        btnSearch.Location =
            new Point(702, 98);

        btnSearch.Size =
            new Size(125, 40);

        btnSearch.FlatStyle =
            FlatStyle.Flat;

        btnSearch.FlatAppearance.BorderSize =
            0;

        btnSearch.BackColor =
            Color.FromArgb(211, 193, 255);

        btnSearch.ForeColor =
            Color.FromArgb(77, 58, 112);

        btnSearch.Font =
            new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold);

        btnSearch.Text =
            "Search";

        btnSearch.Click +=
            BtnSearch_Click;

        btnCancel.Location =
            new Point(839, 98);

        btnCancel.Size =
            new Size(105, 40);

        btnCancel.FlatStyle =
            FlatStyle.Flat;

        btnCancel.FlatAppearance.BorderColor =
            Color.FromArgb(236, 205, 216);

        btnCancel.BackColor =
            Color.FromArgb(255, 229, 237);

        btnCancel.ForeColor =
            Color.FromArgb(123, 73, 91);

        btnCancel.Text =
            "Cancel";

        btnCancel.Enabled =
            false;

        btnCancel.Click +=
            BtnCancel_Click;

        pnlTop.Controls.AddRange(
            new Control[]
            {
                lblTitle,
                lblSubtitle,
                txtSearch,
                btnSearch,
                btnCancel
            });

        // =====================================================
        // MAIN SPLITTER
        // =====================================================

        splitMain.Dock =
            DockStyle.Fill;

        splitMain.SplitterDistance =
            850;

        splitMain.BackColor =
            Color.FromArgb(238, 232, 242);

        // =====================================================
        // RESULTS HEADER
        // =====================================================

        pnlResultsHeader.Dock =
            DockStyle.Top;

        pnlResultsHeader.Height =
            55;

        pnlResultsHeader.BackColor =
            Color.FromArgb(248, 243, 255);

        lblResults.AutoSize =
            true;

        lblResults.Location =
            new Point(20, 17);

        lblResults.Font =
            new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);

        lblResults.ForeColor =
            Color.FromArgb(91, 78, 113);

        lblResults.Text =
            "Results";

        lblCount.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        lblCount.Location =
            new Point(690, 16);

        lblCount.Size =
            new Size(135, 25);

        lblCount.TextAlign =
            ContentAlignment.MiddleRight;

        lblCount.ForeColor =
            Color.FromArgb(128, 114, 138);

        lblCount.Text =
            "0 results";

        pnlResultsHeader.Controls.Add(
            lblResults);

        pnlResultsHeader.Controls.Add(
            lblCount);

        // =====================================================
        // RESULTS GRID
        // =====================================================

        dgvResults.Dock =
            DockStyle.Fill;

        dgvResults.AllowUserToAddRows =
            false;

        dgvResults.AllowUserToDeleteRows =
            false;

        dgvResults.AllowUserToResizeRows =
            false;

        dgvResults.AutoGenerateColumns =
            false;

        dgvResults.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

        dgvResults.BackgroundColor =
            Color.FromArgb(255, 253, 251);

        dgvResults.BorderStyle =
            BorderStyle.None;

        dgvResults.RowHeadersVisible =
            false;

        dgvResults.ReadOnly =
            true;

        dgvResults.MultiSelect =
            false;

        dgvResults.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        dgvResults.ColumnHeadersHeight =
            40;

        dgvResults.RowTemplate.Height =
            34;

        dgvResults.SelectionChanged +=
            DgvResults_SelectionChanged;

        colDatabase.HeaderText =
            "DB";

        colDatabase.DataPropertyName =
            "Database";

        colDatabase.FillWeight =
            9;

        colType.HeaderText =
            "Type";

        colType.DataPropertyName =
            "ResultType";

        colType.FillWeight =
            18;

        colSchema.HeaderText =
            "Schema";

        colSchema.DataPropertyName =
            "Schema";

        colSchema.FillWeight =
            12;

        colObject.HeaderText =
            "Object";

        colObject.DataPropertyName =
            "ObjectName";

        colObject.FillWeight =
            26;

        colParent.HeaderText =
            "Parent";

        colParent.DataPropertyName =
            "ParentObject";

        colParent.FillWeight =
            20;

        colExtra.HeaderText =
            "Extra";

        colExtra.DataPropertyName =
            "Extra";

        colExtra.FillWeight =
            23;

        colScore.HeaderText =
            "Score";

        colScore.DataPropertyName =
            "Similarity";

        colScore.FillWeight =
            9;

        dgvResults.Columns.AddRange(
            colDatabase,
            colType,
            colSchema,
            colObject,
            colParent,
            colExtra,
            colScore);

        splitMain.Panel1.Controls.Add(
            dgvResults);

        splitMain.Panel1.Controls.Add(
            pnlResultsHeader);

        // =====================================================
        // PREVIEW
        // =====================================================

        pnlPreview.Dock =
            DockStyle.Fill;

        pnlPreview.BackColor =
            Color.FromArgb(255, 252, 250);

        pnlPreviewHeader.Dock =
            DockStyle.Top;

        pnlPreviewHeader.Height =
            95;

        pnlPreviewHeader.BackColor =
            Color.FromArgb(237, 247, 255);

        lblPreview.AutoSize =
            true;

        lblPreview.Location =
            new Point(18, 15);

        lblPreview.Font =
            new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);

        lblPreview.ForeColor =
            Color.FromArgb(76, 94, 125);

        lblPreview.Text =
            "Object Preview";

        lblSelected.Location =
            new Point(20, 48);

        lblSelected.Size =
            new Size(455, 30);

        lblSelected.ForeColor =
            Color.FromArgb(103, 111, 132);

        lblSelected.Text =
            "Select a result to preview it";

        btnCopy.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        btnCopy.Location =
            new Point(470, 22);

        btnCopy.Size =
            new Size(125, 38);

        btnCopy.FlatStyle =
            FlatStyle.Flat;

        btnCopy.FlatAppearance.BorderSize =
            0;

        btnCopy.BackColor =
            Color.FromArgb(202, 242, 228);

        btnCopy.ForeColor =
            Color.FromArgb(51, 105, 89);

        btnCopy.Font =
            new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);

        btnCopy.Text =
            "Copy Preview";

        btnCopy.Click +=
            BtnCopy_Click;

        pnlPreviewHeader.Controls.AddRange(
            new Control[]
            {
                lblPreview,
                lblSelected,
                btnCopy
            });

        txtPreview.Dock =
            DockStyle.Fill;

        txtPreview.BorderStyle =
            BorderStyle.None;

        txtPreview.BackColor =
            Color.FromArgb(255, 250, 253);

        txtPreview.ForeColor =
            Color.FromArgb(76, 69, 83);

        txtPreview.Font =
            new Font(
                "Consolas",
                10F);

        txtPreview.ReadOnly =
            true;

        txtPreview.WordWrap =
            false;

        pnlPreview.Controls.Add(
            txtPreview);

        pnlPreview.Controls.Add(
            pnlPreviewHeader);

        splitMain.Panel2.Controls.Add(
            pnlPreview);

        // =====================================================
        // STATUS
        // =====================================================

        pnlStatus.Dock =
            DockStyle.Bottom;

        pnlStatus.Height =
            43;

        pnlStatus.BackColor =
            Color.FromArgb(255, 252, 250);

        lblStatus.Dock =
            DockStyle.Fill;

        lblStatus.Padding =
            new Padding(14, 0, 0, 0);

        lblStatus.TextAlign =
            ContentAlignment.MiddleLeft;

        lblStatus.ForeColor =
            Color.FromArgb(111, 100, 119);

        lblStatus.Text =
            "Ready";

        progressBar.Dock =
            DockStyle.Right;

        progressBar.Width =
            220;

        progressBar.Style =
            ProgressBarStyle.Marquee;

        progressBar.MarqueeAnimationSpeed =
            25;

        progressBar.Visible =
            false;

        pnlStatus.Controls.Add(
            lblStatus);

        pnlStatus.Controls.Add(
            progressBar);

        // =====================================================
        // ADD
        // =====================================================

        Controls.Add(
            splitMain);

        Controls.Add(
            pnlStatus);

        Controls.Add(
            pnlTop);

        Controls.Add(
            pnlSidebar);

        ((System.ComponentModel.ISupportInitialize)numSimilarity)
            .EndInit();

        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);

        ((System.ComponentModel.ISupportInitialize)splitMain)
            .EndInit();

        splitMain.ResumeLayout(false);

        ((System.ComponentModel.ISupportInitialize)dgvResults)
            .EndInit();

        ResumeLayout(false);
    }
}

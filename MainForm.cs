using DbScoutBaby.Models;
using DbScoutBaby.Services;

namespace DbScoutBaby;

public partial class MainForm : Form
{
    private readonly MetadataCacheService
        _cache = new();

    private readonly DatabaseSearchService
        _searchService;

    private readonly HistoryService
        _historyService = new();

    private CancellationTokenSource?
        _cancellationTokenSource;

    public MainForm()
    {
        InitializeComponent();

        _searchService =
            new DatabaseSearchService(
                _cache);

        ApplyGridStyle();
    }

    private async void BtnSearch_Click(
        object? sender,
        EventArgs e)
    {
        await RunSearchAsync();
    }

    private async void TxtSearch_KeyDown(
        object? sender,
        KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
            return;

        e.SuppressKeyPress = true;

        await RunSearchAsync();
    }

    private async Task RunSearchAsync()
    {
        string searchText =
            txtSearch.Text.Trim();

        if (string.IsNullOrWhiteSpace(
                searchText))
        {
            MessageBox.Show(
                "Γράψε μία λέξη ή μέρος του ονόματος που ψάχνεις.",
                "DbScoutBaby",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtSearch.Focus();

            return;
        }

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();

        _cancellationTokenSource =
            new CancellationTokenSource();

        SetBusy(true);

        try
        {
            var results =
                new List<SearchResult>();

            foreach (string database
                     in GetSelectedDatabases())
            {
                lblStatus.Text =
                    _cache.HasCache(database)
                        ? $"Searching cached {database} metadata..."
                        : $"Loading {database} metadata for the first time...";

                List<SearchResult> databaseResults =
                    await _searchService.SearchAsync(
                        database,
                        DatabaseSettings.GetConnectionString(database),
                        searchText,
                        chkSearchCode.Checked,
                        (int)numSimilarity.Value,
                        _cancellationTokenSource.Token);

                results.AddRange(
                    databaseResults);
            }

            List<SearchResult> ordered =
                results
                    .OrderByDescending(
                        x => x.Similarity)
                    .ThenBy(
                        x => x.ResultType)
                    .ThenBy(
                        x => x.FullName)
                    .ToList();

            dgvResults.DataSource =
                null;

            dgvResults.DataSource =
                ordered;

            lblCount.Text =
                $"{ordered.Count} results";

            lblStatus.Text =
                ordered.Count == 0
                    ? "No matching objects found."
                    : "Search completed. Metadata is cached for the next search.";

            txtPreview.Clear();

            lblSelected.Text =
                "Select a result to preview it";

            await _historyService.AddAsync(
                new SearchHistoryItem
                {
                    SearchText =
                        searchText,

                    Database =
                        rbBoth.Checked
                            ? "BOTH"
                            : rbDev.Checked
                                ? "DEV"
                                : "CTCOLLECT",

                    SearchCode =
                        chkSearchCode.Checked,

                    Date =
                        DateTime.Now
                });

            if (ordered.Count > 0 &&
                dgvResults.Rows.Count > 0)
            {
                dgvResults.ClearSelection();

                dgvResults.Rows[0]
                    .Selected = true;

                dgvResults.CurrentCell =
                    dgvResults.Rows[0]
                        .Cells[0];
            }
        }
        catch (OperationCanceledException)
        {
            lblStatus.Text =
                "Search cancelled.";
        }
        catch (Exception ex)
        {
            lblStatus.Text =
                "Search failed.";

            MessageBox.Show(
                ex.Message,
                "DbScoutBaby",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private List<string>
        GetSelectedDatabases()
    {
        if (rbDev.Checked)
        {
            return new List<string>
            {
                "DEV"
            };
        }

        if (rbCtCollect.Checked)
        {
            return new List<string>
            {
                "CTCOLLECT"
            };
        }

        return new List<string>
        {
            "DEV",
            "CTCOLLECT"
        };
    }

    private void SetBusy(
        bool busy)
    {
        btnSearch.Enabled =
            !busy;

        btnCancel.Enabled =
            busy;

        btnRefresh.Enabled =
            !busy;

        btnHistory.Enabled =
            !busy;

        txtSearch.Enabled =
            !busy;

        numSimilarity.Enabled =
            !busy;

        chkSearchCode.Enabled =
            !busy;

        rbDev.Enabled =
            !busy;

        rbCtCollect.Enabled =
            !busy;

        rbBoth.Enabled =
            !busy;

        progressBar.Visible =
            busy;

        UseWaitCursor =
            busy;
    }

    private void BtnCancel_Click(
        object? sender,
        EventArgs e)
    {
        _cancellationTokenSource?
            .Cancel();

        lblStatus.Text =
            "Cancelling...";
    }

    private async void BtnRefresh_Click(
        object? sender,
        EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();

        _cancellationTokenSource =
            new CancellationTokenSource();

        SetBusy(true);

        try
        {
            foreach (string database
                     in GetSelectedDatabases())
            {
                lblStatus.Text =
                    $"Refreshing {database} metadata...";

                await _searchService
                    .RefreshMetadataAsync(
                        database,
                        DatabaseSettings.GetConnectionString(database),
                        _cancellationTokenSource.Token);
            }

            lblStatus.Text =
                "Metadata cache refreshed.";
        }
        catch (OperationCanceledException)
        {
            lblStatus.Text =
                "Refresh cancelled.";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "DbScoutBaby",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void DgvResults_SelectionChanged(
        object? sender,
        EventArgs e)
    {
        if (dgvResults.CurrentRow
                ?.DataBoundItem
            is not SearchResult result)
        {
            return;
        }

        lblSelected.Text =
            $"{result.Database}  •  {result.ResultType}  •  {result.FullName}";

        txtPreview.Text =
            result.PreviewText;
    }

    private void BtnCopy_Click(
        object? sender,
        EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(
                txtPreview.Text))
        {
            return;
        }

        Clipboard.SetText(
            txtPreview.Text);

        lblStatus.Text =
            "Preview copied to clipboard.";
    }

    private void BtnHistory_Click(
        object? sender,
        EventArgs e)
    {
        using var historyForm =
            new HistoryForm();

        historyForm.ShowDialog(this);
    }

    private void ApplyGridStyle()
    {
        dgvResults.EnableHeadersVisualStyles =
            false;

        dgvResults.ColumnHeadersDefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor =
                    Color.FromArgb(205, 193, 255),

                ForeColor =
                    Color.FromArgb(73, 60, 105),

                Font =
                    new Font(
                        "Segoe UI",
                        9.5F,
                        FontStyle.Bold),

                SelectionBackColor =
                    Color.FromArgb(205, 193, 255),

                SelectionForeColor =
                    Color.FromArgb(73, 60, 105)
            };

        dgvResults.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor =
                    Color.FromArgb(255, 252, 250),

                ForeColor =
                    Color.FromArgb(74, 70, 85),

                SelectionBackColor =
                    Color.FromArgb(218, 242, 255),

                SelectionForeColor =
                    Color.FromArgb(74, 70, 85),

                Padding =
                    new Padding(4)
            };

        dgvResults.AlternatingRowsDefaultCellStyle =
            new DataGridViewCellStyle
            {
                BackColor =
                    Color.FromArgb(250, 244, 255)
            };

        dgvResults.GridColor =
            Color.FromArgb(232, 225, 240);
    }
}

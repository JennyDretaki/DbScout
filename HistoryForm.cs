using DbScoutBaby.Models;
using DbScoutBaby.Services;

namespace DbScoutBaby;

public partial class HistoryForm : Form
{
    private readonly HistoryService
        _historyService = new();

    private List<SearchHistoryItem>
        _history = new();

    public HistoryForm()
    {
        InitializeComponent();

        Shown +=
            HistoryForm_Shown;

        ApplyGridStyle();
    }

    private async void HistoryForm_Shown(
        object? sender,
        EventArgs e)
    {
        await ReloadAsync();
    }

    private async Task ReloadAsync()
    {
        _history =
            await _historyService
                .LoadAsync();

        dgvHistory.DataSource =
            null;

        dgvHistory.DataSource =
            _history;

        lblCount.Text =
            $"{_history.Count} searches";
    }

    private async void BtnClear_Click(
        object? sender,
        EventArgs e)
    {
        if (_history.Count == 0)
            return;

        DialogResult result =
            MessageBox.Show(
                "Clear the entire search history?",
                "DbScoutBaby",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
            return;

        await _historyService
            .ClearAsync();

        await ReloadAsync();
    }

    private void BtnClose_Click(
        object? sender,
        EventArgs e)
    {
        Close();
    }

    private void ApplyGridStyle()
    {
        dgvHistory.EnableHeadersVisualStyles =
            false;

        dgvHistory.ColumnHeadersDefaultCellStyle =
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
                        FontStyle.Bold)
            };

        dgvHistory.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                SelectionBackColor =
                    Color.FromArgb(218, 242, 255),

                SelectionForeColor =
                    Color.FromArgb(74, 70, 85)
            };
    }
}

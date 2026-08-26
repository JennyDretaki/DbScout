using System.Text.Json;
using DbScoutBaby.Models;

namespace DbScoutBaby.Services;

public class HistoryService
{
    private readonly string _filePath;

    public HistoryService()
    {
        string folder =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "DbScoutBaby");

        Directory.CreateDirectory(folder);

        _filePath =
            Path.Combine(
                folder,
                "history.json");
    }

    public async Task<List<SearchHistoryItem>> LoadAsync()
    {
        if (!File.Exists(_filePath))
            return new List<SearchHistoryItem>();

        try
        {
            string json =
                await File.ReadAllTextAsync(
                    _filePath);

            return JsonSerializer.Deserialize<
                       List<SearchHistoryItem>>(json)
                   ?? new List<SearchHistoryItem>();
        }
        catch
        {
            return new List<SearchHistoryItem>();
        }
    }

    public async Task AddAsync(
        SearchHistoryItem item)
    {
        List<SearchHistoryItem> history =
            await LoadAsync();

        history.Insert(0, item);

        if (history.Count > 250)
        {
            history =
                history
                    .Take(250)
                    .ToList();
        }

        await SaveAsync(history);
    }

    public async Task DeleteAsync(
        SearchHistoryItem item)
    {
        List<SearchHistoryItem> history =
            await LoadAsync();

        SearchHistoryItem? found =
            history.FirstOrDefault(
                x =>
                    x.Date == item.Date &&
                    x.SearchText == item.SearchText &&
                    x.Database == item.Database);

        if (found != null)
            history.Remove(found);

        await SaveAsync(history);
    }

    public Task ClearAsync()
    {
        return SaveAsync(
            new List<SearchHistoryItem>());
    }

    private Task SaveAsync(
        List<SearchHistoryItem> history)
    {
        string json =
            JsonSerializer.Serialize(
                history,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

        return File.WriteAllTextAsync(
            _filePath,
            json);
    }
}

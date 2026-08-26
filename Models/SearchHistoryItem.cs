namespace DbScoutBaby.Models;

public class SearchHistoryItem
{
    public string SearchText { get; set; } = "";
    public string Database { get; set; } = "";
    public bool SearchCode { get; set; } = true;
    public DateTime Date { get; set; } = DateTime.Now;
}

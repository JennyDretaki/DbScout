using System.Text;

namespace DbScoutBaby.Services;

public static class FuzzySearch
{
    public static int Similarity(string source, string target)
    {
        if (string.IsNullOrWhiteSpace(source) ||
            string.IsNullOrWhiteSpace(target))
        {
            return 0;
        }

        string a = Normalize(source);
        string b = Normalize(target);

        if (a == b)
            return 100;

        if (b.Contains(a))
            return 98;

        if (a.Contains(b))
            return 95;

        string[] keywords = SplitWords(source);

        if (keywords.Length > 1)
        {
            int matched =
                keywords.Count(k => b.Contains(Normalize(k)));

            if (matched == keywords.Length)
                return 97;

            if (matched > 0)
            {
                return (int)Math.Round(
                    (double)matched / keywords.Length * 90);
            }
        }

        int distance =
            Levenshtein(a, b);

        int max =
            Math.Max(a.Length, b.Length);

        return max == 0
            ? 100
            : Math.Max(
                0,
                (int)Math.Round(
                    (1d - (double)distance / max) * 100));
    }

    public static int KeywordCoverageScore(
        string text,
        string searchText)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        string[] words =
            SplitWords(searchText);

        if (words.Length == 0)
            return 0;

        int matched =
            words.Count(w =>
                text.Contains(
                    w,
                    StringComparison.OrdinalIgnoreCase));

        return matched == 0
            ? 0
            : (int)Math.Round(
                (double)matched / words.Length * 100);
    }

    public static string Normalize(string value)
    {
        var builder =
            new StringBuilder();

        foreach (char c in value.ToLowerInvariant())
        {
            if (char.IsLetterOrDigit(c))
                builder.Append(c);
        }

        return builder.ToString();
    }

    private static string[] SplitWords(string value) =>
        value.Split(
            new[] { ' ', '_', '-', '.', '/', '\\', ',', ';', ':', '[', ']' },
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries);

    private static int Levenshtein(
        string a,
        string b)
    {
        int[,] d =
            new int[a.Length + 1, b.Length + 1];

        for (int i = 0; i <= a.Length; i++)
            d[i, 0] = i;

        for (int j = 0; j <= b.Length; j++)
            d[0, j] = j;

        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int cost =
                    a[i - 1] == b[j - 1] ? 0 : 1;

                d[i, j] =
                    Math.Min(
                        Math.Min(
                            d[i - 1, j] + 1,
                            d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
            }
        }

        return d[a.Length, b.Length];
    }
}

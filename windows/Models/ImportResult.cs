namespace TVFongMi.Windows.Models;

public sealed record ImportResult(int ImportedCount, IReadOnlyList<string> Warnings);

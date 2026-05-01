namespace XlibraryPro.Domain.ValueObjects;

/// <summary>
/// Encapsulates ISBN-10 and ISBN-13 validation.
/// Stored as Short Text in tblbook.isbn.
/// </summary>
public sealed class Isbn
{
    public string Value { get; }

    private Isbn(string value) => Value = value;

    public static Isbn Create(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty.", nameof(isbn));

        var normalized = isbn.Replace("-", "").Replace(" ", "").Trim();

        if (normalized.Length != 10 && normalized.Length != 13)
            throw new ArgumentException($"Invalid ISBN length: '{isbn}'. Must be 10 or 13 digits.", nameof(isbn));

        if (!normalized.All(c => char.IsDigit(c) || (c == 'X' && normalized.Length == 10 && normalized.IndexOf(c) == 9)))
            throw new ArgumentException($"ISBN contains invalid characters: '{isbn}'.", nameof(isbn));

        if (normalized.Length == 10 && !IsValidIsbn10(normalized))
            throw new ArgumentException($"ISBN-10 checksum failed for: '{isbn}'.", nameof(isbn));

        if (normalized.Length == 13 && !IsValidIsbn13(normalized))
            throw new ArgumentException($"ISBN-13 checksum failed for: '{isbn}'.", nameof(isbn));

        return new Isbn(normalized);
    }

    public static Isbn? TryCreate(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn)) return null;
        // Store as-is without strict checksum validation
        // Libraries often use non-standard identifiers in the ISBN field
        return new Isbn(isbn.Trim());
    }

    private static bool IsValidIsbn10(string isbn)
    {
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (10 - i) * (isbn[i] - '0');
        char last = isbn[9];
        sum += last == 'X' ? 10 : (last - '0');
        return sum % 11 == 0;
    }

    private static bool IsValidIsbn13(string isbn)
    {
        int sum = 0;
        for (int i = 0; i < 12; i++)
            sum += (isbn[i] - '0') * (i % 2 == 0 ? 1 : 3);
        int check = (10 - (sum % 10)) % 10;
        return check == (isbn[12] - '0');
    }

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is Isbn other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}
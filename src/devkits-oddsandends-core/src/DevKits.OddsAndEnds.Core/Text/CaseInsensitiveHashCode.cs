namespace DevKits.OddsAndEnds.Core.Text;

/// <summary>
///     Provides utility methods for combining and working with case-insensitive hash codes in C#.
/// </summary>
public static class CaseInsensitiveHashCode
{
    /// <summary>
    ///     Combines multiple hash codes for strings in a case-insensitive manner.
    /// </summary>
    /// <param name="strings">An array of strings for which to combine hash codes.</param>
    /// <returns>The combined hash code for the specified strings.</returns>
    public static int Combine(params string[] strings)
    {
        var combinedHashCode = 0;

        foreach (var str in strings)
        {
            // Using StringComparer.OrdinalIgnoreCase to ensure case-insensitive comparison
            combinedHashCode = Combine(combinedHashCode, StringComparer.OrdinalIgnoreCase.GetHashCode(str));
        }

        return combinedHashCode;
    }


    /// <summary>
    ///     Combines multiple hash codes for strings in a case-insensitive manner.
    /// </summary>
    /// <param name="strings">An array of strings for which to combine hash codes.</param>
    /// <returns>The combined hash code for the specified strings.</returns>
    public static int Combine(IEnumerable<string>? strings)
    {
        var combinedHashCode = 0;

        if (strings == null) return combinedHashCode;

        foreach (var str in strings)
        {
            // Using StringComparer.OrdinalIgnoreCase to ensure case-insensitive comparison
            combinedHashCode = Combine(combinedHashCode, StringComparer.OrdinalIgnoreCase.GetHashCode(str));
        }

        return combinedHashCode;
    }

    /// <summary>
    ///     Combines two hash codes.
    /// </summary>
    /// <param name="hashCode1">The first hash code to combine.</param>
    /// <param name="hashCode2">The second hash code to combine.</param>
    /// <returns>The combined hash code.</returns>
    private static int Combine(int hashCode1, int hashCode2)
    {
        // This is a simple algorithm for combining hash codes
        unchecked
        {
            // Choose large prime numbers to help distribute the bits
            const int prime1 = 17;
            const int prime2 = 23;

            var combinedHashCode = prime1 * hashCode1 + prime2 * hashCode2;
            return combinedHashCode;
        }
    }
}

//class Program
//{
//    static void Main()
//    {
//        string str1 = "Hello";
//        string str2 = "world";
//        string str3 = "CASE";
//        string str4 = "insensitive";

//        // Combine four hash codes in a case-insensitive manner
//        int combinedHashCode = CaseInsensitiveHashCode.Combine(str1, str2, str3, str4);

//        Console.WriteLine($"Combined Hash Code: {combinedHashCode}");
//    }
//}
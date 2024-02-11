namespace DevKits.OddsAndEnds.Core.Text;

/// <summary>
/// A Text manipulation Tools
/// </summary>
public static class TextTools
{
    /// <summary>
    /// Concatenates the elements of a string array, using the specified delimiter between each element,
    /// excluding empty or null strings.
    /// </summary>
    /// <param name="delimiter">The string to use as a delimiter. Delimiter is included in the returned string only if values exist to be joined.</param>
    /// <param name="stringArgs">An array of strings to concatenate. Null or empty strings in the array are ignored.</param>
    /// <returns>
    /// A string that consists of the non-empty elements in <paramref name="stringArgs"/> delimited by the <paramref name="delimiter"/> string.
    /// Returns <see langword="null"/> if <paramref name="stringArgs"/> is null or all elements are empty or null.
    /// Returns an empty string if <paramref name="stringArgs"/> contains no elements.
    /// </returns>
    /// <remarks>
    /// This method is useful for creating a concatenated string from an array of values while ignoring any null or empty values.
    /// It provides a clean way to join strings with a delimiter without needing to filter the array beforehand.
    /// </remarks>
    public static string? JoinIfNotEmpty(string delimiter, params string?[] stringArgs)
    {
        return string.Join(delimiter, stringArgs.Where(s => !string.IsNullOrEmpty(s)));
    }

}

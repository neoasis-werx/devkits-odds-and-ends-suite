namespace DevKits.Data.Core;

/// <summary>
/// Provides extension methods for setting or clearing flags of <see cref="ColumnAttributes"/> and <see cref="TableAttributes"/>.
/// </summary>
public static class DbSchemaObjectAttributeExtensions
{
    /// <summary>
    /// Sets or clears a specified flag for a <see cref="ColumnAttributes"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ColumnAttributes"/> value to modify.</param>
    /// <param name="flag">The flag to set or clear within <paramref name="value"/>.</param>
    /// <param name="set">If true, the flag is set; if false, the flag is cleared.</param>
    /// <returns>The modified <see cref="ColumnAttributes"/> value with the specified flag set or cleared.</returns>
    /// <remarks>
    /// This method allows for fluent modification of <see cref="ColumnAttributes"/> values, enabling or disabling specific attributes dynamically.
    /// </remarks>
    public static ColumnAttributes Set(ref this ColumnAttributes value, ColumnAttributes flag, bool set)
    {
        if (set)
        {
            value |= flag; // Set the flag
        }
        else
        {
            value &= ~flag; // Clear the flag
        }
        return value;
    }

    /// <summary>
    /// Sets or clears a specified flag for a <see cref="TableAttributes"/> value.
    /// </summary>
    /// <param name="value">The <see cref="TableAttributes"/> value to modify.</param>
    /// <param name="flag">The flag to set or clear within <paramref name="value"/>.</param>
    /// <param name="set">If true, the flag is set; if false, the flag is cleared.</param>
    /// <returns>The modified <see cref="TableAttributes"/> value with the specified flag set or cleared.</returns>
    /// <remarks>
    /// Similar to the method for <see cref="ColumnAttributes"/>, this allows for dynamic adjustments of <see cref="TableAttributes"/>,
    /// facilitating the management of table-level attributes in code.
    /// </remarks>
    public static TableAttributes Set(ref this TableAttributes value, TableAttributes flag, bool set)
    {
        if (set)
        {
            value |= flag; // Set the flag
        }
        else
        {
            value &= ~flag; // Clear the flag
        }
        return value;
    }
}

#pragma warning disable S101
namespace DevKits.Data;

using System.Data;

/// <summary>
/// Represents a SQL stored procedure as a strongly-typed value object. This class extends <see cref="SQLQuery"/>
/// to specifically handle SQL stored procedures, setting the command type to <see cref="CommandType.StoredProcedure"/> by default.
/// </summary>
/// <remarks>
/// The class supports implicit conversion to and from <see cref="string"/>, allowing for intuitive work with stored procedure names.
/// </remarks>
public class SQLProc : SQLQuery
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SQLProc"/> class with the specified stored procedure name.
    /// The command type is set to <see cref="CommandType.StoredProcedure"/>, indicating that the command text is the name of a stored procedure.
    /// </summary>
    /// <param name="value">The name of the stored procedure.</param>
    public SQLProc(string value) : base(value, CommandType.StoredProcedure)
    {
    }

    /// <summary>
    /// Defines an implicit conversion of a <see cref="string"/> to an <see cref="SQLProc"/> instance.
    /// This allows a string representing a stored procedure name to be directly assigned to an <see cref="SQLProc"/> variable.
    /// </summary>
    /// <param name="source">The stored procedure name to convert.</param>
    /// <returns>An <see cref="SQLProc"/> instance representing the stored procedure.</returns>
    public static implicit operator SQLProc(string source)
    {
        return new SQLProc(source);
    }
}
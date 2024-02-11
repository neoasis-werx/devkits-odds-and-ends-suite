namespace DevKits.Data;


public static class TableConstantExtensions
{
    public static SQLQuery GetInsertSQL(this TableConstant source)
    {
        var columnList = source.Columns.Where(c => c.IsUpdatable);
        var insertList = string.Join(",", columnList.Select(c => c.QualifiedTableColumnName.QuotedColumnName).ToArray());
        var valueList = string.Join(",", columnList.Select(c => $"@{c.ColumnName}").ToArray());
        return $"INSERT INTO {source.TableName} ({insertList}) VALUES({valueList})";
    }
}

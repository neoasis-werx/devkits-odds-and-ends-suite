namespace DevKits.Data.Core;

public static class DbSchemaObjectAttributeExtensions
{
    public static ColumnAttributes Set(ref this ColumnAttributes value, ColumnAttributes flag, bool set)
    {
        if (set)
        {
            value |= flag;
        }
        else
        {
            value &= ~flag;
        }
        return value;
    }

    public static TableAttributes Set(ref this TableAttributes value, TableAttributes flag, bool set)
    {
        if (set)
        {
            value |= flag;
        }
        else
        {
            value &= ~flag;
        }
        return value;
    }
}
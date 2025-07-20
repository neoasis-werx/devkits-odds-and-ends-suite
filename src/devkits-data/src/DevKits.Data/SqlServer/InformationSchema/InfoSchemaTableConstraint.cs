namespace DevKits.Data.SqlServer.InformationSchema;

using Core;


public class InfoSchemaTableConstraint
{
    /// <summary>Gets or sets the Constraint catalog  [CONSTRAINT_CATALOG].</summary>
    public string? ConstraintCatalog { get; set; }

    /// <summary>Gets or sets the Constraint schema  [CONSTRAINT_SCHEMA].</summary>
    public string? ConstraintSchema { get; set; }

    /// <summary>Gets or sets the Constraint name  [CONSTRAINT_NAME].</summary>
    public string ConstraintName { get; set; } = null!;


    /// <summary>Gets or sets the table catalog  [TABLE_CATALOG].</summary>
    public string? TableCatalog { get; set; }

    /// <summary>Gets or sets the table schema  [TABLE_SCHEMA].</summary>
    public string? TableSchema { get; set; }

    /// <summary>Gets or sets the table name  [TABLE_NAME].</summary>
    public string TableName { get; set; } = null!;

    /// <summary>Gets or sets the constraint type  [CONSTRAINT_TYPE].</summary>
    public string? ConstraintType { get; set; }

    public InfoSchemaColumnCollection Columns { get; set; } = new();

    /// <summary>Gets or sets the qualified table name  [QUALIFIED_TABLE_NAME].</summary>
    public QualifiedTableName QualifiedTableName { get; set; } = null!;

    public SchemaObjectName QualifiedConstraintName { get; set; } = null!;
}



/// <summary>Represents a DTO object for the InfoSchemaTableConstraintColumn table.</summary>
public class InfoSchemaTableConstraintColumn
{
    public string ConstraintName { get; set; } = null!;

    /// <summary>Gets or sets the table catalog  [TABLE_CATALOG].</summary>
    public string? TableCatalog { get; set; }

    /// <summary>Gets or sets the table schema  [TABLE_SCHEMA].</summary>
    public string? TableSchema { get; set; }

    /// <summary>Gets or sets the table name  [TABLE_NAME].</summary>
    public string TableName { get; set; } = null!;

    /// <summary>Gets or sets the column name  [COLUMN_NAME].</summary>
    public string? ColumnName { get; set; }

    /// <summary>Gets or sets the ordinal position  [ORDINAL_POSITION].</summary>
    public int OrdinalPosition { get; set; }

    /// <summary>Gets or sets the constraint type  [CONSTRAINT_TYPE].</summary>
    public string? ConstraintType { get; set; }

    /// <summary>Gets or sets the qualified table name  [QUALIFIED_TABLE_NAME].</summary>
    public QualifiedTableName QualifiedTableName { get; set; } = null!;

    public SchemaObjectName QualifiedConstraintName { get; set; } = null!;
}
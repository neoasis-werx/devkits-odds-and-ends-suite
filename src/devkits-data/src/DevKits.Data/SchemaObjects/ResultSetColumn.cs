#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using DevKits.Data.Abstractions;

namespace DevKits.Data.SchemaObjects;

using OddsAndEnds.Core.Collections;

/// <summary>
/// Represents column metadata similar to the result of sp_describe_first_result_set.
/// </summary>
public class ResultSetColumn : ICommonMetadata
{
    /// <summary>
    /// Specifies that the column is an extra column for browsing and informational purposes,
    /// and does not appear in the actual result set.
    /// </summary>
    public bool? IsHidden { get; set; }

    /// <summary>
    /// Ordinal position of the column in the result set (1-based).
    /// </summary>
    public int? ColumnOrdinal { get; set; }

    /// <summary>
    /// Name of the column if it can be determined; otherwise, NULL.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Indicates if the column allows NULL (1) or not (0), or 1 if undetermined.
    /// </summary>
    public bool? IsNullable { get; set; }

    /// <summary>
    /// System type ID of the column data type from sys.types.
    /// CLR types have ID 240 even if system_type_name is NULL.
    /// </summary>
    public int? SystemTypeId { get; set; }

    /// <summary>
    /// Name and arguments (length, precision, scale) for the column's system data type.
    /// For user-defined alias types, the underlying system type is listed.
    /// For CLR user-defined types, NULL is returned.
    /// </summary>
    public string SystemTypeName { get; set; }

    /// <summary>
    /// Maximum length in bytes of the column.
    /// -1 = varchar(max), nvarchar(max), varbinary(max), or xml.
    /// </summary>
    public short? MaxLength { get; set; }

    /// <summary>
    /// Precision if the column is numeric-based; otherwise 0.
    /// </summary>
    public byte? Precision { get; set; }

    /// <summary>
    /// Scale if the column is numeric-based; otherwise 0.
    /// </summary>
    public byte? Scale { get; set; }

    /// <summary>
    /// Name of the collation if the column is character-based; otherwise NULL.
    /// </summary>
    public string CollationName { get; set; }

    /// <summary>
    /// For CLR and alias types, contains the user_type_id from sys.types; otherwise NULL.
    /// </summary>
    public int? UserTypeId { get; set; }

    /// <summary>
    /// For CLR and alias types, database where the type is defined; otherwise NULL.
    /// </summary>
    public string UserTypeDatabase { get; set; }

    /// <summary>
    /// For CLR and alias types, schema where the type is defined; otherwise NULL.
    /// </summary>
    public string UserTypeSchema { get; set; }

    /// <summary>
    /// For CLR and alias types, the type name; otherwise NULL.
    /// </summary>
    public string UserTypeName { get; set; }

    /// <summary>
    /// For CLR types, the assembly and class defining the type; otherwise NULL.
    /// </summary>
    public string AssemblyQualifiedTypeName { get; set; }

    /// <summary>
    /// xml_collection_id of the data type from sys.columns. NULL if not associated with an XML schema.
    /// </summary>
    public int? XmlCollectionId { get; set; }

    /// <summary>
    /// Database of the XML schema collection; NULL if not associated with an XML schema.
    /// </summary>
    public string XmlCollectionDatabase { get; set; }

    /// <summary>
    /// Schema of the XML schema collection; NULL if not associated with an XML schema.
    /// </summary>
    public string XmlCollectionSchema { get; set; }

    /// <summary>
    /// Name of the XML schema collection; NULL if not associated with an XML schema.
    /// </summary>
    public string XmlCollectionName { get; set; }

    /// <summary>
    /// 1 if the XML type is guaranteed to be a complete XML document (with root),
    /// 0 if it could be a fragment; otherwise NULL.
    /// </summary>
    public bool? IsXmlDocument { get; set; }

    /// <summary>
    /// 1 if the column is of a case-sensitive string type, 0 if not.
    /// </summary>
    public bool? IsCaseSensitive { get; set; }

    /// <summary>
    /// 1 if the column is a fixed-length CLR type, 0 if not.
    /// </summary>
    public bool? IsFixedLengthClrType { get; set; }

    /// <summary>
    /// Name of the originating server if remote; otherwise NULL.
    /// Only populated if browsing information is requested.
    /// </summary>
    public string SourceServer { get; set; }

    /// <summary>
    /// Name of the originating database, if known; otherwise NULL.
    /// Only populated if browsing info is requested.
    /// </summary>
    public string SourceDatabase { get; set; }

    /// <summary>
    /// Name of the originating schema, if known; otherwise NULL.
    /// Only populated if browsing info is requested.
    /// </summary>
    public string SourceSchema { get; set; }

    /// <summary>
    /// Name of the originating table, if known; otherwise NULL.
    /// Only populated if browsing info is requested.
    /// </summary>
    public string SourceTable { get; set; }

    /// <summary>
    /// Name of the originating column, if known; otherwise NULL.
    /// Only populated if browsing info is requested.
    /// </summary>
    public string SourceColumn { get; set; }

    /// <summary>
    /// 1 if the column is an identity column, 0 if not, NULL if undetermined.
    /// </summary>
    public bool? IsIdentityColumn { get; set; }

    /// <summary>
    /// 1 if the column is part of a unique index or constraint, 0 if not, NULL if undetermined.
    /// </summary>
    public bool? IsPartOfUniqueKey { get; set; }

    /// <summary>
    /// 1 if the column is updatable, 0 if not, NULL if undetermined.
    /// </summary>
    public bool? IsUpdateable { get; set; }

    /// <summary>
    /// 1 if the column is a computed column, 0 if not, NULL if undetermined.
    /// </summary>
    public bool? IsComputedColumn { get; set; }

    /// <summary>
    /// 1 if the column is part of a sparse column set, 0 if not, NULL if undetermined.
    /// </summary>
    public bool? IsSparseColumnSet { get; set; }

    /// <summary>
    /// Position of this column in the ORDER BY list if uniquely determined; otherwise NULL.
    /// </summary>
    public short? OrdinalInOrderByList { get; set; }

    /// <summary>
    /// Number of columns in the ORDER BY list. Same for all rows if known; otherwise NULL.
    /// </summary>
    public short? OrderByListLength { get; set; }

    /// <summary>
    /// If not NULL, indicates whether this column is sorted descending (1) or not (0).
    /// Otherwise NULL if not in the ORDER BY or undetermined.
    /// </summary>
    public short? OrderByIsDescending { get; set; }

    /// <summary>
    /// Contains the error number if the function call returned an error; otherwise NULL.
    /// </summary>
    public int? ErrorNumber { get; set; }

    /// <summary>
    /// Severity of the error if applicable; otherwise NULL.
    /// </summary>
    public int? ErrorSeverity { get; set; }

    /// <summary>
    /// State of the error if applicable; otherwise NULL.
    /// </summary>
    public int? ErrorState { get; set; }

    /// <summary>
    /// The error message text if an error occurred; otherwise NULL.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Integer representing the error type if applicable; otherwise NULL.
    /// </summary>
    public int? ErrorType { get; set; }

    /// <summary>
    /// Short uppercase string representing the error type if applicable; otherwise NULL.
    /// </summary>
    public string ErrorTypeDesc { get; set; }



    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.TableCatalog,
    /// mapped to SourceDatabase or some appropriate property. Return null if not applicable.
    /// </summary>
    string? ICommonMetadata.TableCatalog => SourceDatabase;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.TableSchema,
    /// mapped to SourceSchema.
    /// </summary>
    string? ICommonMetadata.TableSchema => SourceSchema;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.TableName,
    /// mapped to SourceTable.
    /// </summary>
    string? ICommonMetadata.TableName => SourceTable;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.ColumnName,
    /// mapped to Name.
    /// </summary>
    string? ICommonMetadata.ColumnName => Name;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.OrdinalPosition,
    /// mapped to ColumnOrdinal.
    /// </summary>
    int? ICommonMetadata.OrdinalPosition => ColumnOrdinal;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.ColumnDefault,
    /// not typically returned by sp_describe_first_result_set. Return null.
    /// </summary>
    string? ICommonMetadata.ColumnDefault => null;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.IsNullable,
    /// returns "YES" or "NO" based on IsNullableColumn. If unknown, could return "YES".
    /// </summary>
    bool? ICommonMetadata.IsNullable => IsNullable;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.DataType,
    /// mapped to SystemTypeName.
    /// </summary>
    string? ICommonMetadata.DataType => SystemTypeName;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.CharacterMaximumLength,
    /// mapped to MaxLength (bytes). We simply cast short? to int?.
    /// </summary>
    int? ICommonMetadata.CharacterMaximumLength => MaxLength;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.NumericPrecision,
    /// mapped to Precision.
    /// </summary>
    byte? ICommonMetadata.NumericPrecision => Precision;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.NumericScale,
    /// mapped to Scale. We convert from byte? to int?.
    /// </summary>
    int? ICommonMetadata.NumericScale => Scale;

    /// <summary>
    /// Explicit interface implementation for ICommonMetadata.DatetimePrecision,
    /// not returned by sp_describe_first_result_set. Return null.
    /// </summary>
    short? ICommonMetadata.DatetimePrecision => Precision;
}


    public static class ResultSetDescriptionMappings
    {
        /// <summary>
        /// Dictionary mapping C# property names from ResultSetDescription to SQL column names in sp_describe_first_result_set.
        /// Uses nameof() for compile-time safety.
        /// </summary>
        public static readonly BiDirectionalDictionary<string, string> PropertyMappings = new(StringComparer.OrdinalIgnoreCase,StringComparer.OrdinalIgnoreCase)
        {
            { nameof(ResultSetColumn.IsHidden), "is_hidden" },
            { nameof(ResultSetColumn.ColumnOrdinal), "column_ordinal" },
            { nameof(ResultSetColumn.Name), "name" },
            { nameof(ResultSetColumn.IsNullable), "is_nullable" },
            { nameof(ResultSetColumn.SystemTypeId), "system_type_id" },
            { nameof(ResultSetColumn.SystemTypeName), "system_type_name" },
            { nameof(ResultSetColumn.MaxLength), "max_length" },
            { nameof(ResultSetColumn.Precision), "precision" },
            { nameof(ResultSetColumn.Scale), "scale" },
            { nameof(ResultSetColumn.CollationName), "collation_name" },
            { nameof(ResultSetColumn.UserTypeId), "user_type_id" },
            { nameof(ResultSetColumn.UserTypeDatabase), "user_type_database" },
            { nameof(ResultSetColumn.UserTypeSchema), "user_type_schema" },
            { nameof(ResultSetColumn.UserTypeName), "user_type_name" },
            { nameof(ResultSetColumn.AssemblyQualifiedTypeName), "assembly_qualified_type_name" },
            { nameof(ResultSetColumn.XmlCollectionId), "xml_collection_id" },
            { nameof(ResultSetColumn.XmlCollectionDatabase), "xml_collection_database" },
            { nameof(ResultSetColumn.XmlCollectionSchema), "xml_collection_schema" },
            { nameof(ResultSetColumn.XmlCollectionName), "xml_collection_name" },
            { nameof(ResultSetColumn.IsXmlDocument), "is_xml_document" },
            { nameof(ResultSetColumn.IsCaseSensitive), "is_case_sensitive" },
            { nameof(ResultSetColumn.IsFixedLengthClrType), "is_fixed_length_clr_type" },
            { nameof(ResultSetColumn.SourceServer), "source_server" },
            { nameof(ResultSetColumn.SourceDatabase), "source_database" },
            { nameof(ResultSetColumn.SourceSchema), "source_schema" },
            { nameof(ResultSetColumn.SourceTable), "source_table" },
            { nameof(ResultSetColumn.SourceColumn), "source_column" },
            { nameof(ResultSetColumn.IsIdentityColumn), "is_identity_column" },
            { nameof(ResultSetColumn.IsPartOfUniqueKey), "is_part_of_unique_key" },
            { nameof(ResultSetColumn.IsUpdateable), "is_updateable" },
            { nameof(ResultSetColumn.IsComputedColumn), "is_computed_column" },
            { nameof(ResultSetColumn.IsSparseColumnSet), "is_sparse_column_set" },
            { nameof(ResultSetColumn.OrdinalInOrderByList), "ordinal_in_order_by_list" },
            { nameof(ResultSetColumn.OrderByListLength), "order_by_list_length" },
            { nameof(ResultSetColumn.OrderByIsDescending), "order_by_is_descending" },
            { nameof(ResultSetColumn.ErrorNumber), "error_number" },
            { nameof(ResultSetColumn.ErrorSeverity), "error_severity" },
            { nameof(ResultSetColumn.ErrorState), "error_state" },
            { nameof(ResultSetColumn.ErrorMessage), "error_message" },
            { nameof(ResultSetColumn.ErrorType), "error_type" },
            { nameof(ResultSetColumn.ErrorTypeDesc), "error_type_desc" }
        };
    }


﻿#pragma warning disable S101

namespace DevKits.Data.SqlServer.InformationSchema;

public static class SQLQueries
{
    public static SQLQuery SelectInfoSchemaTablesRsrc { get; } = ResourceUtilities.ReadTextFile<InfoSchemata>("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaTables.sql");
    public static SQLQuery SelectInfoSchemaColumnsRsrc { get; } = ResourceUtilities.ReadTextFile<InfoSchemata>("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaColumns.sql");

    public static SQLQuery SelectInfoSchemaTables => @" 
-- SOURCE: InfoSchemaTables
-- CLASS: InfoSchemaTable
-- PARAMETER: DECLARE @QualifiedTableName NVARCHAR(517);
-- PARAMETER: DECLARE @SCHEMA_NAME sysname;

SET @QualifiedTableName = REPLACE(REPLACE(@QualifiedTableName, '[', ''), ']', '');
DECLARE @TABLE_SCHEMA sysname = ISNULL(PARSENAME(@QualifiedTableName, 2), @SCHEMA_NAME);
DECLARE @TABLE_NAME sysname = IIF(@QualifiedTableName IS NOT NULL, PARSENAME(@QualifiedTableName, 1), NULL);

WITH cteInfoSchemaTables AS
    (
        SELECT
            ci.*
          , TABLE_OBJECT_ID = OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME)
        FROM INFORMATION_SCHEMA.TABLES AS ci
    )
SELECT
    ci.TABLE_CATALOG
  , ci.TABLE_SCHEMA
  , ci.TABLE_NAME
  , ci.TABLE_TYPE
  , TABLE_TEMPORAL_TYPE = OBJECTPROPERTY(ci.TABLE_OBJECT_ID, 'TableTemporalType')
  , IS_UPDATABLE = IIF(ci.TABLE_TYPE = 'BASE TABLE' AND OBJECTPROPERTY(ci.TABLE_OBJECT_ID, 'TableTemporalType') <> 1, 1, 0)
  , IS_USER_TABLE = OBJECTPROPERTY(ci.TABLE_OBJECT_ID, 'IsUserTable')
  , IS_TEMPORAL_TABLE_TYPE = IIF(OBJECTPROPERTY(ci.TABLE_OBJECT_ID, 'TableTemporalType') <> 0, 1, 0)
  , IS_HISTORY_TABLE_FOR_SYSTEM_VERSIONED_TABLE = IIF(OBJECTPROPERTY(ci.TABLE_OBJECT_ID, 'TableTemporalType') = 1, 1, 0)
  , IS_SYSTEM_VERSIONED_TEMPORAL_TABLE = IIF(OBJECTPROPERTY(ci.TABLE_OBJECT_ID, 'TableTemporalType') = 2, 1, 0)
  , QUALIFIED_TABLE_NAME = CONCAT_WS('.', ci.TABLE_SCHEMA, ci.TABLE_NAME)
  , QUOTED_TABLE_SCHEMA = QUOTENAME(ci.TABLE_SCHEMA)
  , QUOTED_TABLE_NAME = QUOTENAME(ci.TABLE_NAME)
  , QUOTED_QUALIFIED_TABLE_NAME = CONCAT_WS('.', QUOTENAME(ci.TABLE_SCHEMA), QUOTENAME(ci.TABLE_NAME))
FROM cteInfoSchemaTables AS ci
WHERE ci.TABLE_SCHEMA = ISNULL(@TABLE_SCHEMA, ci.TABLE_SCHEMA)
      AND ci.TABLE_NAME = ISNULL(@TABLE_NAME, ci.TABLE_NAME)
ORDER BY QUALIFIED_TABLE_NAME;
";


    public static SQLQuery SelectInfoSchemaColumns => @"
-- SOURCE: InfoSchemaColumns
-- CLASS: InfoSchemaColumn
-- PARAMETER: DECLARE @QualifiedTableName NVARCHAR(517);
-- PARAMETER: DECLARE @SCHEMA_NAME sysname;

SET @QualifiedTableName = REPLACE(REPLACE(@QualifiedTableName, '[', ''), ']', '');
DECLARE @TABLE_SCHEMA sysname = ISNULL(PARSENAME(@QualifiedTableName, 2), @SCHEMA_NAME);
DECLARE @TABLE_NAME sysname = IIF(@QualifiedTableName IS NOT NULL, PARSENAME(@QualifiedTableName, 1), NULL);

WITH cteInfoSchemaColumns AS
    (
        SELECT
            ci.*
          , TABLE_OBJECT_ID = OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME)
        FROM INFORMATION_SCHEMA.COLUMNS AS ci
    )
   , TableConstraintColumns AS
    (
        SELECT
            kcu.TABLE_CATALOG
          , kcu.TABLE_SCHEMA
          , kcu.TABLE_NAME
          , kcu.COLUMN_NAME
          , COLUMN_CONSTRAINTS = STRING_AGG(tc.CONSTRAINT_TYPE, '|')WITHIN GROUP(ORDER BY tc.CONSTRAINT_TYPE)
        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS kcu
            LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                   ON tc.CONSTRAINT_SCHEMA = kcu.CONSTRAINT_SCHEMA
                      AND tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
        GROUP BY
            kcu.TABLE_CATALOG
          , kcu.TABLE_SCHEMA
          , kcu.TABLE_NAME
          , kcu.COLUMN_NAME
    )
SELECT
    ci.TABLE_CATALOG
  , ci.TABLE_SCHEMA
  , ci.TABLE_NAME
  , ci.COLUMN_NAME
  , ci.ORDINAL_POSITION
  , ci.COLUMN_DEFAULT
 -- , ci.IS_NULLABLE
  , ci.DATA_TYPE
  , ci.CHARACTER_MAXIMUM_LENGTH
  , ci.CHARACTER_OCTET_LENGTH
  , ci.NUMERIC_PRECISION
  , ci.NUMERIC_PRECISION_RADIX
  , ci.NUMERIC_SCALE
  , ci.DATETIME_PRECISION
  , ci.CHARACTER_SET_CATALOG
  , ci.CHARACTER_SET_SCHEMA
  , ci.CHARACTER_SET_NAME
  , ci.COLLATION_CATALOG
  , ci.COLLATION_SCHEMA
  , ci.COLLATION_NAME
  , ci.DOMAIN_CATALOG
  , ci.DOMAIN_SCHEMA
  , ci.DOMAIN_NAME
  , ci.TABLE_OBJECT_ID
  , COMPUTED_COLUMN_DEFINITION = cc.definition
  , IS_UPDATABLE = IIF(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsComputed') = 1 OR COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsIdentity') = 1 OR COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'GeneratedAlwaysType') <> 0 OR ci.DATA_TYPE = 'timestamp', 0, 1)
  , IS_COMPUTED = COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsComputed')
  , IS_NULLABLE = COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'AllowsNull')
  , IS_ROWGUIDCOL = COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsRowGuidCol')
  , IS_IDENTITY = COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsIdentity')
  , IS_COLUMN_SET = COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsColumnSet')
  , IS_TIMESTAMP = IIF(ci.DATA_TYPE = 'timestamp', 1, 0)
  --------------------------------------------------------------------------------------
  -- BELOW TEMPORAL TABLES ONLY IF SQL SERVER 2016 OR HIGHER 
  , IS_GENERATED_ALWAYS = IIF(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'GeneratedAlwaysType') = 0, 0, 1)
  , GENERATED_ALWAYS_TYPE = COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'GeneratedAlwaysType')
  --------------------------------------------------------------------------------------
  -- CAN BE CALCULATED
  --------------------------------------------------------------------------------------
  , COLUMN_DECLARATION = ci.DATA_TYPE + CASE WHEN ci.DATA_TYPE IN ( N'time', N'datetime2', N'datetimeoffset' )
                                                  THEN CONCAT('(', ci.DATETIME_PRECISION, ')')
                                            WHEN ci.DATA_TYPE IN ( N'decimal', N'numeric' )
                                                 THEN CONCAT('(', ci.NUMERIC_PRECISION, ',', ci.NUMERIC_SCALE, ')')
                                            WHEN ci.DATA_TYPE IN ( N'varbinary', N'binary' )
                                                 THEN CONCAT('(', IIF(ci.CHARACTER_MAXIMUM_LENGTH = -1, 'MAX', TRY_CAST(ci.CHARACTER_MAXIMUM_LENGTH AS NVARCHAR(100))), ')')
                                            WHEN ci.DATA_TYPE IN ( N'char', N'nvarchar', N'nchar', N'sysname', N'varchar' ) --'STRING.VARIABLE'
                                                 THEN CONCAT('(', IIF(ci.CHARACTER_MAXIMUM_LENGTH = -1, 'MAX', TRY_CAST(ci.CHARACTER_MAXIMUM_LENGTH AS NVARCHAR(100))), ')')
                                            ELSE ''
                                        END
  --------------------------------------------------------------------------------------
  , QUALIFIED_TABLE_NAME = CONCAT_WS('.', ci.TABLE_SCHEMA, ci.TABLE_NAME)
  , QUOTED_TABLE_SCHEMA = QUOTENAME(ci.TABLE_SCHEMA)
  , QUOTED_TABLE_NAME = QUOTENAME(ci.TABLE_NAME)
  , QUOTED_COLUMN_NAME = QUOTENAME(ci.COLUMN_NAME)
  , QUOTED_QUALIFIED_TABLE_NAME = CONCAT_WS('.', QUOTENAME(ci.TABLE_SCHEMA), QUOTENAME(ci.TABLE_NAME))
  , QUOTED_QUALIFIED_TABLE_COLUMN_NAME = CONCAT_WS('.', QUOTENAME(ci.TABLE_SCHEMA), QUOTENAME(ci.TABLE_NAME), QUOTENAME(ci.COLUMN_NAME))
  , IS_PRIMARY_KEY = IIF(kcu.COLUMN_CONSTRAINTS LIKE '%PRIMARY KEY%', 1, 0)
  , IS_UNIQUE = IIF(kcu.COLUMN_CONSTRAINTS LIKE '%UNIQUE%', 1, 0)
  , IS_FOREIGN_KEY = IIF(kcu.COLUMN_CONSTRAINTS LIKE '%FOREIGN KEY%', 1, 0)
  , kcu.COLUMN_CONSTRAINTS
FROM cteInfoSchemaColumns AS ci
    LEFT JOIN sys.computed_columns AS cc
           ON cc.object_id = ci.TABLE_OBJECT_ID
              AND cc.column_id = ci.ORDINAL_POSITION
    LEFT JOIN TableConstraintColumns AS kcu
           ON kcu.TABLE_SCHEMA = ci.TABLE_SCHEMA
              AND kcu.TABLE_NAME = ci.TABLE_NAME
              AND kcu.COLUMN_NAME = ci.COLUMN_NAME
WHERE ci.TABLE_SCHEMA = ISNULL(@TABLE_SCHEMA, ci.TABLE_SCHEMA)
      AND ci.TABLE_NAME = ISNULL(@TABLE_NAME, ci.TABLE_NAME)
ORDER BY
    QUALIFIED_TABLE_NAME
  , ci.ORDINAL_POSITION;
";

    public static SQLQuery SelectInfoSchemaTableConstraints => @"
-- SOURCE: InfoSchemaTableConstraints
-- CLASS: InfoSchemaTableConstraint
-- PARAMETER: DECLARE @QualifiedTableName NVARCHAR(517);
-- PARAMETER: DECLARE @SCHEMA_NAME sysname;

SET @QualifiedTableName = REPLACE(REPLACE(@QualifiedTableName, '[', ''), ']', '');
DECLARE @TABLE_SCHEMA sysname = ISNULL(PARSENAME(@QualifiedTableName, 2), @SCHEMA_NAME);
DECLARE @TABLE_NAME sysname = IIF(@QualifiedTableName IS NOT NULL, PARSENAME(@QualifiedTableName, 1), NULL);

SELECT
    ci.TABLE_CATALOG
  , ci.TABLE_SCHEMA
  , ci.TABLE_NAME
  , ci.COLUMN_NAME
  , ci.ORDINAL_POSITION
  , tc.CONSTRAINT_TYPE
  , QUALIFIED_TABLE_NAME = CONCAT_WS('.', ci.TABLE_SCHEMA, ci.TABLE_NAME)
  , QUOTED_TABLE_SCHEMA = QUOTENAME(ci.TABLE_SCHEMA)
  , QUOTED_TABLE_NAME = QUOTENAME(ci.TABLE_NAME)
  , QUOTED_COLUMN_NAME = QUOTENAME(ci.COLUMN_NAME)
  , QUOTED_QUALIFIED_TABLE_NAME = CONCAT_WS('.', QUOTENAME(ci.TABLE_SCHEMA), QUOTENAME(ci.TABLE_NAME))
  , QUOTED_QUALIFIED_TABLE_COLUMN_NAME = CONCAT_WS('.', QUOTENAME(ci.TABLE_SCHEMA), QUOTENAME(ci.TABLE_NAME), QUOTENAME(ci.COLUMN_NAME))
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ci
    JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
      ON tc.CONSTRAINT_SCHEMA = ci.CONSTRAINT_SCHEMA
         AND tc.CONSTRAINT_NAME = ci.CONSTRAINT_NAME
WHERE ci.TABLE_SCHEMA = ISNULL(@TABLE_SCHEMA, ci.TABLE_SCHEMA)
      AND ci.TABLE_NAME = ISNULL(@TABLE_NAME, ci.TABLE_NAME)
ORDER BY
    ci.TABLE_CATALOG
  , ci.TABLE_SCHEMA
  , ci.TABLE_NAME
  , tc.CONSTRAINT_TYPE
  , ci.ORDINAL_POSITION;
";


}

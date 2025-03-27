--SELECT QualifiedObjectName = CONCAT_WS('.', NULLIF(dl.SchemaName, ''), dl.ObjectName)
--     , *
--FROM dbo.DatabaseLogs AS dl
--ORDER BY dl.Id DESC;


WITH cteInfoSchemaColumns AS
    (
        SELECT ci.*
             , TABLE_OBJECT_ID = OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME)
        FROM INFORMATION_SCHEMA.COLUMNS AS ci
    )
SELECT *
     --, COMPUTED_COLUMN_DEFINITION = cc.definition
     , IS_NULLABLE = TRY_CAST(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'AllowsNull') AS BIT)
     , IS_UPDATABLE = TRY_CAST(IIF(
                                   COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsComputed') = 1
                                   OR COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsIdentity') = 1
                                   OR COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'GeneratedAlwaysType') <> 0
                                   OR COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsRowGuidCol') = 1
                                   OR ci.DATA_TYPE = 'timestamp'
                                 , 0
                                 , 1) AS BIT)
     , IS_COMPUTED = TRY_CAST(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsComputed') AS BIT)
     , IS_ROWGUIDCOL = TRY_CAST(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsRowGuidCol') AS BIT)
     , IS_IDENTITY = TRY_CAST(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsIdentity') AS BIT)
     , IS_COLUMN_SET = TRY_CAST(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsColumnSet') AS BIT)
     , IS_GENERATED_ALWAYS_TYPE = TRY_CAST(IIF(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'GeneratedAlwaysType') <> 0, 1, 0) AS BIT)
     , IS_TIMESTAMP = TRY_CAST(IIF(ci.DATA_TYPE = 'timestamp', 1, 0) AS BIT)
	 , IS_HIDDEN = TRY_CAST(IIF(COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'IsHidden') <> 0, 1, 0) AS BIT)
     , GENERATED_ALWAYS_TYPE = COLUMNPROPERTY(ci.TABLE_OBJECT_ID, ci.COLUMN_NAME, 'GeneratedAlwaysType')
FROM cteInfoSchemaColumns AS ci
ORDER BY
    ci.TABLE_CATALOG
  , ci.TABLE_SCHEMA
  , ci.TABLE_NAME
  , ci.ORDINAL_POSITION;



SELECT ci.TABLE_CATALOG
     , QuotedQualifiedTableName = CONCAT_WS('.', QUOTENAME(ci.TABLE_SCHEMA), QUOTENAME(ci.TABLE_NAME))
     , QuotedQualifiedTableColumnName = CONCAT_WS('.', QUOTENAME(ci.TABLE_SCHEMA), QUOTENAME(ci.TABLE_NAME), QUOTENAME(ci.COLUMN_NAME))
     , ci.TABLE_SCHEMA
     , ci.TABLE_NAME
     , ci.COLUMN_NAME
     , ci.ORDINAL_POSITION
     , IS_NULLABLE = TRY_CAST(COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'AllowsNull') AS BIT)
     , IS_UPDATABLE = TRY_CAST(IIF(
                                   COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsComputed') = 1
                                   OR COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsIdentity') = 1
                                   OR COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'GeneratedAlwaysType') <> 0
                                   OR COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsRowGuidCol') = 1
                                   OR ci.DATA_TYPE = 'timestamp'
                                 , 0
                                 , 1) AS BIT)
     , IS_COMPUTED = TRY_CAST(COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsComputed') AS BIT)
     , IS_ROWGUIDCOL = TRY_CAST(COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsRowGuidCol') AS BIT)
     , IS_IDENTITY = TRY_CAST(COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsIdentity') AS BIT)
     , IS_COLUMN_SET = TRY_CAST(COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsColumnSet') AS BIT)
     , IS_GENERATED_ALWAYS_TYPE = TRY_CAST(IIF(COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'GeneratedAlwaysType') <> 0, 1, 0) AS BIT)
     , IS_TIMESTAMP = TRY_CAST(IIF(ci.DATA_TYPE = 'timestamp', 1, 0) AS BIT)
	 , IS_HIDDEN = TRY_CAST(IIF(COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'IsHidden') <> 0, 1, 0) AS BIT)
     , GENERATED_ALWAYS_TYPE = COLUMNPROPERTY(OBJECT_ID(ci.TABLE_SCHEMA + '.' + ci.TABLE_NAME), ci.COLUMN_NAME, 'GeneratedAlwaysType')
FROM INFORMATION_SCHEMA.COLUMNS AS ci
    JOIN INFORMATION_SCHEMA.TABLES AS t
      ON t.TABLE_SCHEMA = ci.TABLE_SCHEMA
         AND t.TABLE_NAME = ci.TABLE_NAME
ORDER BY
    ci.TABLE_CATALOG
  , ci.TABLE_SCHEMA
  , ci.TABLE_NAME
  , ci.ORDINAL_POSITION;


SELECT TABLE_CATALOG = DB_NAME()
     , QuotedQualifiedTableName = CONCAT_WS('.', QUOTENAME(s.name), QUOTENAME(t.name))
     , QuotedQualifiedTableColumnName = CONCAT_WS('.', QUOTENAME(s.name), QUOTENAME(t.name), QUOTENAME(c.name))
     , TABLE_SCHEMA = s.name
     , TABLE_NAME = t.name
     , COLUMN_NAME = c.name
     , ORDINAL_POSITION = c.column_id
     , IS_NULLABLE = c.is_nullable
     , IS_UPDATABLE = TRY_CAST(CASE WHEN c.is_computed = 1
                                         OR c.is_identity = 1
                                         OR c.is_rowguidcol = 1
                                         OR COLUMNPROPERTY(c.object_id, c.name, 'GeneratedAlwaysType') <> 0
                                         OR c.system_type_id = 189 --TIMESTAMP OR ROWVERSION
                                         THEN 0
                                   ELSE  1
                               END AS BIT)
     , IS_COMPUTED = c.is_computed
     , IS_ROWGUIDCOL = c.is_rowguidcol
     , IS_IDENTITY = c.is_identity
     , IS_COLUMN_SET = c.is_column_set
     , IS_GENERATED_ALWAYS_TYPE = TRY_CAST(IIF(COLUMNPROPERTY(c.object_id, c.name, 'GeneratedAlwaysType') <> 0, 1, 0) AS BIT)
     , IS_TIMESTAMP = TRY_CAST(CASE WHEN c.system_type_id = 189 THEN 1 ELSE 0 END AS BIT)
	 , IS_HIDDEN = TRY_CAST(IIF(COLUMNPROPERTY(c.object_id, c.name, 'IsHidden') <> 0, 1, 0) AS BIT)
     , GENERATED_ALWAYS_TYPE = COLUMNPROPERTY(c.object_id, c.name, 'GeneratedAlwaysType')
FROM sys.columns AS c
    JOIN sys.tables AS t
      ON t.object_id = c.object_id
    JOIN sys.schemas AS s
      ON s.schema_id = t.schema_id
WHERE t.type = 'U'
ORDER BY
    TABLE_CATALOG
  , TABLE_SCHEMA
  , TABLE_NAME
  , ORDINAL_POSITION;



SELECT rs.*
     , is_primary_key = CONVERT(BIT, CASE WHEN pkc.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END)
     , is_unique_key = CONVERT(BIT, CASE WHEN uk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END)
     , GeneratedAlwaysType = COLUMNPROPERTY(OBJECT_ID(rs.source_schema + '.' + rs.source_table), rs.source_column, 'GeneratedAlwaysType')
     , is_error = CONVERT(BIT, IIF(rs.error_number IS NOT NULL, 1, 0))
FROM sys.dm_exec_describe_first_result_set('SELECT * FROM cfg.DepartmentDetails', NULL, 1) AS rs
    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS pkc
           ON pkc.TABLE_SCHEMA = rs.source_schema
              AND pkc.TABLE_NAME = rs.source_table
              AND pkc.COLUMN_NAME = rs.source_column
              AND EXISTS
                  (
                      SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND CONSTRAINT_NAME = pkc.CONSTRAINT_NAME
                  )
    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS uk
           ON uk.TABLE_SCHEMA = rs.source_schema
              AND uk.TABLE_NAME = rs.source_table
              AND uk.COLUMN_NAME = rs.source_column
              AND EXISTS
                  (
                      SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE = 'UNIQUE' AND CONSTRAINT_NAME = uk.CONSTRAINT_NAME
                  );


SELECT *
FROM sys.extended_properties AS ep;
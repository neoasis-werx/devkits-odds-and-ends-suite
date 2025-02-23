﻿-- SOURCE: InfoSchemaTables
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
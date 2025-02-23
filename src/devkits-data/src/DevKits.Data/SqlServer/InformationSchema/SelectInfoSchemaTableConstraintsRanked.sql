DROP TABLE IF EXISTS #KeyConstraints;

--------------------------------------------------------------------------------------

WITH TableConstraints AS
    (
        SELECT
            tc.TABLE_SCHEMA
          , tc.TABLE_NAME
          , tc.CONSTRAINT_NAME
          , QualifiedTableName = CONCAT_WS('.', tc.TABLE_SCHEMA, tc.TABLE_NAME)
          , CONSTRAINT_TYPE = MAX(tc.CONSTRAINT_TYPE)
          , ColumnCount = COUNT(*)
        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
            JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ccu
              ON tc.TABLE_CATALOG = ccu.TABLE_CATALOG
                 AND tc.TABLE_SCHEMA = ccu.TABLE_SCHEMA
                 AND tc.TABLE_NAME = ccu.TABLE_NAME
                 AND tc.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
                 AND tc.CONSTRAINT_TYPE IN ( 'UNIQUE', 'PRIMARY KEY' )
        GROUP BY
            tc.TABLE_SCHEMA
          , tc.TABLE_NAME
          , tc.CONSTRAINT_NAME
    )
   , RankedConstraints AS
    (
        SELECT
            KeyRank = ROW_NUMBER() OVER (PARTITION BY tc.TABLE_SCHEMA, tc.TABLE_NAME ORDER BY tc.ColumnCount DESC, tc.CONSTRAINT_TYPE DESC)
          , *
          , TABLE_OBJECT_ID = OBJECT_ID(tc.QualifiedTableName)
        FROM TableConstraints AS tc
    )
SELECT
    ccu.*
  , rc.CONSTRAINT_TYPE
  , rc.QualifiedTableName
  , rc.KeyRank
  , rc.ColumnCount
  , IsUpdatable = CONVERT(   BIT
                           , IIF(
                                 COLUMNPROPERTY(OBJECT_ID(rc.TABLE_SCHEMA + '.' + rc.TABLE_NAME), ccu.COLUMN_NAME, 'GeneratedAlwaysType') <> 0
                                 OR COLUMNPROPERTY(OBJECT_ID(rc.TABLE_SCHEMA + '.' + rc.TABLE_NAME), ccu.COLUMN_NAME, 'IsIdentity') = 1
                                 OR COLUMNPROPERTY(OBJECT_ID(rc.TABLE_SCHEMA + '.' + rc.TABLE_NAME), ccu.COLUMN_NAME, 'IsComputed') = 1
                               , 0
                               , 1)
                         )
INTO #KeyConstraints
FROM RankedConstraints AS rc
    JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ccu
      ON ccu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
         AND ccu.TABLE_SCHEMA = rc.TABLE_SCHEMA
         AND ccu.TABLE_NAME = rc.TABLE_NAME
ORDER BY
    rc.TABLE_SCHEMA
  , rc.TABLE_NAME
  , rc.CONSTRAINT_NAME
  , ccu.ORDINAL_POSITION;


SELECT *
FROM #KeyConstraints AS rc
WHERE 1 = 1
      AND rc.KeyRank = 1
      AND rc.QualifiedTableName = 'cfg.Departments'
ORDER BY
    rc.TABLE_SCHEMA
  , rc.TABLE_NAME
  , rc.KeyRank
  , rc.CONSTRAINT_NAME
  , rc.ORDINAL_POSITION;


SELECT *
FROM #KeyConstraints AS rc
WHERE 1 = 1
      AND rc.KeyRank = 1
      AND rc.IsUpdatable = 1
ORDER BY
    rc.TABLE_SCHEMA
  , rc.TABLE_NAME
  , rc.KeyRank
  , rc.CONSTRAINT_NAME
  , rc.ORDINAL_POSITION;



SELECT
    c.TABLE_CATALOG
  , c.TABLE_SCHEMA
  , c.TABLE_NAME
  , c.COLUMN_NAME
  , c.ORDINAL_POSITION
  , c.COLUMN_DEFAULT
  , c.IS_NULLABLE
  , c.DATA_TYPE
  , c.CHARACTER_MAXIMUM_LENGTH
  --, c.CHARACTER_OCTET_LENGTH
  , c.NUMERIC_PRECISION
  , c.NUMERIC_PRECISION_RADIX
  , c.NUMERIC_SCALE
  , c.DATETIME_PRECISION
  --, c.CHARACTER_SET_CATALOG
  --, c.CHARACTER_SET_SCHEMA
  --, c.CHARACTER_SET_NAME
  --, c.COLLATION_CATALOG
  --, c.COLLATION_SCHEMA
  --, c.COLLATION_NAME
  --, c.DOMAIN_CATALOG
  --, c.DOMAIN_SCHEMA
  --, c.DOMAIN_NAME
  , IsUpdatable = IIF(
                      COLUMNPROPERTY(OBJECT_ID(c.TABLE_SCHEMA + '.' + c.TABLE_NAME), c.COLUMN_NAME, 'GeneratedAlwaysType') > 0
                      OR COLUMNPROPERTY(OBJECT_ID(c.TABLE_SCHEMA + '.' + c.TABLE_NAME), c.COLUMN_NAME, 'IsIdentity') = 1
                      OR COLUMNPROPERTY(OBJECT_ID(c.TABLE_SCHEMA + '.' + c.TABLE_NAME), c.COLUMN_NAME, 'IsComputed') = 1
                    , 0
                    , 1)
FROM INFORMATION_SCHEMA.COLUMNS AS c;

--CREATE OR ALTER FUNCTION utils.FormatVariant(@SourceValue AS SQL_VARIANT)
--RETURNS NVARCHAR(4000)
--WITH SCHEMABINDING
--AS
--BEGIN
--    DECLARE @BaseType NVARCHAR(20) = CONVERT(NVARCHAR(20), SQL_VARIANT_PROPERTY(@SourceValue, 'BaseType'));
--    RETURN CASE WHEN @BaseType IN
--                   ( N'date', N'datetime', N'datetime2', N'datetimeoffset', N'smalldatetime', N'time' )
--                     THEN FORMAT(TRY_CAST(@SourceValue AS DATETIME2(0)), 'yyyy-MM-dd HH:mm:ss')
--               WHEN @BaseType IN
--                  ( N'bigint', N'decimal', N'decimal', N'float', N'int', N'money', N'numeric', N'real', N'smallint', N'smallmoney', N'tinyint' )
--                    THEN FORMAT(TRY_CAST(@SourceValue AS DECIMAL(38, 8)), '0.########')
--               WHEN @BaseType IN ( N'bit' )
--                    THEN TRY_CAST(@SourceValue AS NCHAR(1))
--               WHEN @BaseType IN
--                  ( N'char', N'nchar', N'varchar', N'nvarchar', N'json', N'sysname', N'uniqueidentifier', N'timestamp', N'binary', N'varbinary' )
--                    THEN TRY_CAST(@SourceValue AS NVARCHAR(4000))
--               ELSE TRY_CAST(@SourceValue AS NVARCHAR(4000))
--           END;
--END;
--GO

--CREATE OR ALTER FUNCTION utils.FormatVariant(@SourceValue AS SQL_VARIANT)
--RETURNS NVARCHAR(4000)
--WITH SCHEMABINDING
--AS
--BEGIN
--    DECLARE @BaseType NVARCHAR(20) = CONVERT(NVARCHAR(20), SQL_VARIANT_PROPERTY(@SourceValue, 'BaseType'));

--    RETURN CASE WHEN @BaseType IN
--                   ( N'date', N'datetime', N'datetime2', N'datetimeoffset', N'smalldatetime', N'time' )
--                     THEN CONVERT(NVARCHAR(19), TRY_CAST(@SourceValue AS DATETIME2(0)), 120) -- Standard SQL format 'YYYY-MM-DD HH:MI:SS'

--               WHEN @BaseType IN
--                  ( N'bigint', N'decimal', N'float', N'int', N'money', N'numeric', N'real', N'smallint', N'smallmoney', N'tinyint' )
--                    THEN CONVERT(NVARCHAR(4000), TRY_CAST(@SourceValue AS DECIMAL(38, 8)))
--               WHEN @BaseType = N'bit'
--                    THEN CASE WHEN TRY_CAST(@SourceValue AS BIT) = 1 THEN N'1' ELSE N'0' END
--               WHEN @BaseType IN
--                  ( N'char', N'nchar', N'varchar', N'nvarchar', N'json', N'sysname', N'uniqueidentifier', N'timestamp', N'binary', N'varbinary' )
--                    THEN TRY_CAST(@SourceValue AS NVARCHAR(4000))
--               ELSE TRY_CAST(@SourceValue AS NVARCHAR(4000))
--           END;
--END;
--GO

CREATE OR ALTER FUNCTION utils.FormatVariant2(@SourceValue AS SQL_VARIANT)
RETURNS NVARCHAR(4000)
WITH SCHEMABINDING
AS
BEGIN
    -- DECLARE @BaseType NVARCHAR(20) = CONVERT(NVARCHAR(20), SQL_VARIANT_PROPERTY(@SourceValue, 'BaseType'));
    DECLARE @BaseType INT = TYPE_ID(CONVERT(NVARCHAR(20), SQL_VARIANT_PROPERTY(GETDATE(), 'BaseType')));
    RETURN CASE --       N'date', N'datetime', N'datetime2', N'datetimeoffset', N'smalldatetime', N'time'
               WHEN @BaseType IN ( 40, 61, 42, 43, 58, 41 )
                    THEN CONVERT(NVARCHAR(19), TRY_CAST(@SourceValue AS DATETIME2(0)), 120) -- Standard SQL format 'YYYY-MM-DD HH:MI:SS'

               WHEN @BaseType IN ( 127, 106, 62, 56, 60, 108, 59, 52, 122, 48 ) -- N'bigint', N'decimal', N'float', N'int', N'money', N'numeric', N'real', N'smallint', N'smallmoney', N'tinyint'
                    THEN CONVERT(NVARCHAR(4000), TRY_CAST(@SourceValue AS DECIMAL(38, 8)))
               WHEN @BaseType = 104
                    THEN CASE WHEN TRY_CAST(@SourceValue AS BIT) = 1 THEN N'1' ELSE N'0' END

                                                                                            --WHEN @BaseType IN ( 175, 239, 167, 231, 244, 36, 189, 173, 165 ) --  N'char', N'nchar', N'varchar', N'nvarchar', N'json', N'sysname', N'uniqueidentifier', N'timestamp', N'binary', N'varbinary'
                                                                                            --     THEN TRY_CAST(@SourceValue AS NVARCHAR(4000))
               ELSE TRY_CAST(@SourceValue AS NVARCHAR(4000))
           END;
END;
GO

SELECT TYPE_ID(CONVERT(NVARCHAR(20), SQL_VARIANT_PROPERTY(GETDATE(), 'BaseType')));
GO
ALTER FUNCTION rawAltKronosProc.CreateTimeAndAttendanceCompositeKey
(
    @TenantId          INT
  , @Employee_ID       NVARCHAR(200) --20
  , @Clock_In          DATETIME2(0)
  , @Clock_Out         DATETIME2(0)
  , @Worked_Department NVARCHAR(200) --20
  , @Worked_Job        NVARCHAR(200) --20
  , @Total_Hours       DECIMAL(18, 10)
  , @Total_Dollars     DECIMAL(18, 10)
  , @Pay_Type          NVARCHAR(200) --20
                                     --, @Worked_Department_Name NVARCHAR(200)
                                     --, @Worked_Job_Description VARCHAR(80)
)
RETURNS NVARCHAR(4000)
WITH SCHEMABINDING
AS
BEGIN
	--SQL Prompt Formatting Off

    RETURN CONCAT(
			 @TenantId,
		'|', @Employee_ID,
		'|', CONVERT(NVARCHAR(20), @Clock_In, 120), --FORMAT(@Clock_In, 'yyyy-MM-dd HH:mm:ss'), 
		'|', CONVERT(NVARCHAR(20), @Clock_Out, 120), -- FORMAT(@Clock_Out, 'yyyy-MM-dd HH:mm:ss'), 
		'|', @Worked_Department, 
		'|', @Worked_Job, 
		'|', TRY_CAST(@Total_Hours AS DECIMAL(38,8)), --FORMAT(@Total_Hours, '0.########'),
		'|', TRY_CAST(@Total_Dollars AS DECIMAL(38,8)), --FORMAT(@Total_Dollars, '0.########'), 
		'|', @Pay_Type);

	--SQL Prompt Formatting On 
END;
GO


GO
--DROP FUNCTION rawAltKronosProc.CreateTimeAndAttendanceHashedKey
CREATE FUNCTION rawAltKronosProc.CreateTimeAndAttendanceHashedKey
(
    @TenantId          INT
  , @Employee_ID       NVARCHAR(200) --20
  , @Clock_In          DATETIME2(0)
  , @Clock_Out         DATETIME2(0)
  , @Worked_Department NVARCHAR(200) --20
  , @Worked_Job        NVARCHAR(200) --20
  , @Total_Hours       DECIMAL(18, 10)
  , @Total_Dollars     DECIMAL(18, 10)
  , @Pay_Type          NVARCHAR(200) --20
  , @HashType          VARCHAR(10) = 'SHA2_256'
)
RETURNS VARBINARY(64)
WITH SCHEMABINDING
AS
BEGIN
    RETURN TRY_CAST(HASHBYTES(@HashType, rawAltKronosProc.CreateTimeAndAttendanceCompositeKey(@TenantId, @Employee_ID, @Clock_In, @Clock_Out, @Worked_Department, @Worked_Job, @Total_Hours, @Total_Dollars, @Pay_Type)) AS VARBINARY(32));
END;
GO

CREATE FUNCTION rawAltKronosProc.CreateTimeAndAttendanceCompositeKeyEx
(
    @TenantId          INT
  , @Employee_ID       NVARCHAR(200) --20
  , @Clock_In          DATETIME2(0)
  , @Clock_Out         DATETIME2(0)
  , @Worked_Department NVARCHAR(200) --20
  , @Worked_Job        NVARCHAR(200) --20
  , @Total_Hours       DECIMAL(18, 10)
  , @Total_Dollars     DECIMAL(18, 10)
  , @Pay_Type          NVARCHAR(200) --20
                                     --, @Worked_Department_Name NVARCHAR(200)
                                     --, @Worked_Job_Description VARCHAR(80)
)
RETURNS NVARCHAR(4000)
WITH SCHEMABINDING
AS
BEGIN
	--SQL Prompt Formatting Off

    RETURN CONCAT(
			 utils.FormatVariant2(@TenantId),
		'|', utils.FormatVariant2(@Employee_ID),
		'|', utils.FormatVariant2(@Clock_In), 
		'|', utils.FormatVariant2(@Clock_Out), 
		'|', utils.FormatVariant2(@Worked_Department), 
		'|', utils.FormatVariant2(@Worked_Job), 
		'|', utils.FormatVariant2(@Total_Hours),
		'|', utils.FormatVariant2(@Total_Dollars), 
		'|', utils.FormatVariant2(@Pay_Type));

	--SQL Prompt Formatting On
--'|', @Worked_Department_Name, 
--'|', @Worked_Job_Description, 
END;
GO



DROP TABLE IF EXISTS #rawAltKronosProcTimeAndAttendanceHashed;

SELECT TableName = 'CompositeKeys'
     , CompositeKey = rawAltKronosProc.CreateTimeAndAttendanceCompositeKey(taa.TenantId, taa.Employee_ID, taa.Clock_In, taa.Clock_Out, taa.Worked_Department, taa.Worked_Job, taa.Total_Hours, taa.Total_Dollars, taa.Pay_Type)
     , DeteterministicKey = HASHBYTES('SHA2_256', rawAltKronosProc.CreateTimeAndAttendanceCompositeKey(taa.TenantId, taa.Employee_ID, taa.Clock_In, taa.Clock_Out, taa.Worked_Department, taa.Worked_Job, taa.Total_Hours, taa.Total_Dollars, taa.Pay_Type))
     , taa.*
INTO #rawAltKronosProcTimeAndAttendanceHashed
FROM rawAltKronosProc.TimeAndAttendance  AS taa;

SELECT
    DISTINCT
    CompositeKey = rawAltKronosProc.CreateTimeAndAttendanceCompositeKey(taa.TenantId, taa.Employee_ID, taa.Clock_In, taa.Clock_Out, taa.Worked_Department, taa.Worked_Job, taa.Total_Hours, taa.Total_Dollars, taa.Pay_Type)
-- , DeteterministicKey = HASHBYTES('SHA2_256', rawAltKronosProc.CreateTimeAndAttendanceCompositeKey(taa.TenantId, taa.Employee_ID, taa.Clock_In, taa.Clock_Out, taa.Worked_Department, taa.Worked_Department_Name, taa.Worked_Job, taa.Worked_Job_Description, taa.Total_Hours, taa.Total_Dollars, taa.Pay_Type))
--  , taa.*
INTO #rawAltKronosProcTimeAndAttendanceHashedDistinct
FROM rawAltKronosProc.TimeAndAttendance AS taa;


SELECT rakptaah.DeteterministicKey
     , rakptaah.CompositeKey
     , Transaction_ID = MIN(rakptaah.Transaction_ID)
     , RecCount = COUNT(*)
FROM #rawAltKronosProcTimeAndAttendanceHashed AS rakptaah
GROUP BY
    rakptaah.DeteterministicKey
  , rakptaah.CompositeKey
HAVING COUNT(*) > 2
ORDER BY RecCount DESC;

SELECT *
FROM #rawAltKronosProcTimeAndAttendanceHashed AS rakptaah
WHERE rakptaah.DeteterministicKey = 0xCBAADF90907D2A3897044E52F540576ABC2FDDBBF22EE128099C816430D9DFB2
ORDER BY rakptaah.Transaction_ID;


--DROP FUNCTION rawAltKronosProc.CreateTimeAndAttendanceDeterministicKey

--GO
--CREATE OR ALTER FUNCTION rawAltKronosProc.CreateTimeAndAttendanceDeterministicKey
--(
--    @TenantId               INT
--  , @Employee_ID            NVARCHAR(20)
--  , @Clock_In               DATETIME2(0)
--  , @Clock_Out              DATETIME2(0)
--  , @Worked_Department      NVARCHAR(20)
--  , @Worked_Department_Name NVARCHAR(200)
--  , @Worked_Job             NVARCHAR(20)
--  , @Worked_Job_Description VARCHAR(80)
--  , @Total_Hours            DECIMAL(18, 10)
--  , @Total_Dollars          DECIMAL(18, 10)
--  , @Pay_Type               NVARCHAR(20)
--)
--RETURNS @return_variable TABLE
--(
--    DeterministicKey VARBINARY(32)  NOT NULL
--  , CompositeKey     NVARCHAR(4000) NOT NULL
--)
--WITH SCHEMABINDING
--AS
--BEGIN
--    DECLARE @CompositeKey NVARCHAR(4000) = CONCAT(
--			--SQL Prompt Formatting Off
--			         @TenantId,
--			    '|', @Employee_ID,
--				'|', FORMAT(@Clock_In, 'yyyy-MM-dd HH:mm:ss'), 
--				'|', FORMAT(@Clock_Out, 'yyyy-MM-dd HH:mm:ss'), 
--				'|', @Worked_Department, 
--				'|', @Worked_Department_Name, 
--				'|', @Worked_Job, 
--				'|', @Worked_Job_Description, 
--				'|', FORMAT(@Total_Hours, '0.########'), 
--				'|', FORMAT(@Total_Dollars, '0.########'), 
--				'|', @Pay_Type);
--				--SQL Prompt Formatting On
--    INSERT INTO @return_variable(DeterministicKey, CompositeKey)
--    VALUES
--         (
--             HASHBYTES('SHA2_256', @CompositeKey) -- DeterministicKey - varbinary(32)
--           , @CompositeKey                        -- CompositeKey - nvarchar(4000)
--         );
--    RETURN;
--END;
--GO


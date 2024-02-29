namespace DevKits.Data.Core;

/// <summary>
/// Values that represent SQL level zero types.
/// The type of level 0 object. @level0type is varchar(128), with a default of None (NULL).
/// </summary>
public enum SqlLevelZeroTypes
{
    None,
    Assembly,
    Contract,
    EventNotification,
    FileGroup,
    MessageType,
    PartitionFunction,
    PartitionScheme,
    RemoteServiceBinding,
    Route,
    Schema,
    Service,
    User,
    Trigger,
    Type,
    PlanGuide,
}

/// <summary>
/// Values that represent SQL level one types.
/// The type of level 1 object. @level1type is varchar(128), with a default of NULL.
/// </summary>
public enum SqlLevelOneTypes
{
    None,
    Aggregate,
    Default,
    Function,
    LogicalFileName,
    Procedure,
    Queue,
    Rule,
    Sequence,
    Synonym,
    Table,
    TableType,
    Type,
    View,
    XMLSchemaCollection,
}

/// <summary>
/// Values that represent SQL level two types.
/// The type of level 2 object. @level2type is varchar(128), with a default of NULL.
/// </summary>
public enum SqlLevelTwoTypes
{
    None,
    Column,
    Constraint,
    EventNotification,
    Index,
    Parameter,
    Trigger,
}
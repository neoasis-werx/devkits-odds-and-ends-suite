# Resource Utilities

## Method: ResolveFileName

### Summary:
Resolves the fully qualified name of the resource.

### Parameters:
- `resourceLocalType` (Type): The type of the class local to the resource files.
- `resourceName` (string): The name of the resource.

### Returns:
- `string`: The resolved fully qualified name of the resource.

### Remarks:
This method takes in the `resourceLocalType` and `resourceName` parameters and resolves the fully qualified name of the resource. It replaces any backslashes in the `resourceName` with dots and then checks if the resolved name starts with the namespace of the `resourceLocalType`. If it does, it returns the resolved name as is. Otherwise, it prepends the namespace of the `resourceLocalType` to the resolved name and returns it.

This method is useful when working with embedded resources in an assembly and you need to resolve the fully qualified name of a resource.


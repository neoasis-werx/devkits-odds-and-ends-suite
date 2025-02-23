using System.Reflection;

namespace DevKits.Data;


/// <summary>
/// Utility class for working with embedded resources in an assembly.
/// </summary>
public class ResourceUtility
{
    private readonly Type _resourceLocalType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    ///
    /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
    ///
    /// <param name="resourceLocalType">The type of the class local to the resource files.</param>
    public ResourceUtility(Type resourceLocalType)
    {
        _resourceLocalType = resourceLocalType ?? throw new ArgumentNullException(nameof(resourceLocalType));
    }

    /// <summary>
    /// Reads a text file embedded as a resource in the assembly and returns its content as a string.
    /// </summary>
    /// <param name="resourceName">The fully qualified name of the resource.</param>
    /// <returns>The content of the text file as a string.</returns>
    public string ReadTextFile(string resourceName)
    {

        return ResourceUtilities.ReadTextFile(_resourceLocalType, resourceName);
    }

    /// <summary>
    /// Gets the assembly that contains the specified type.
    /// </summary>
    /// <returns>The assembly.</returns>
    public Assembly GetAssembly()
    {
        return _resourceLocalType.Assembly;
    }

    /// <summary>
    /// Resolves the fully qualified name of the resource.
    /// </summary>
    /// <param name="resourceName">The resource name.</param>
    /// <returns>The resolved resource name.</returns>
    public string ResolveFileName(string resourceName)
    {
        return ResourceUtilities.ResolveFileName(_resourceLocalType, resourceName);
    }

    /// <summary>
    /// Gets the names of all the embedded resources in the assembly.
    /// </summary>
    /// <returns>An enumerable collection of resource names.</returns>
    public IEnumerable<string> GetResourceNames()
    {
        return ResourceUtilities.GetResourceNames(_resourceLocalType);
    }
}

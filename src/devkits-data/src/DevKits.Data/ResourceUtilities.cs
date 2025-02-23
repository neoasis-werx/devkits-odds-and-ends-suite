using System.Reflection;
using System.Text;

namespace DevKits.Data
{
    /// <summary>
    /// Utility class for working with embedded resources in an assembly.
    /// </summary>
    public static class ResourceUtilities
    {
        /// <summary>
        /// Creates a new instance of <see cref="ResourceUtility"/> using the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the class local to the resource files.</typeparam>
        /// <returns>A new instance of <see cref="ResourceUtility"/> configured to access resources in the same assembly and namespace as the specified type <typeparamref name="T"/>.</returns>
        /// <remarks>
        /// This method is useful for accessing embedded resources that are located in the same assembly and namespace as the given type <typeparamref name="T"/>.
        /// </remarks>
        public static ResourceUtility GetUtility<T>() where T : class
        {
            return new ResourceUtility(typeof(T));
        }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceUtility"/> using the specified type <paramref name="resourceLocalType"/>.
        /// </summary>
        /// <param name="resourceLocalType">The type of the class local to the resource files.</param>
        /// <returns>A new instance of <see cref="ResourceUtility"/> configured to access resources in the same assembly and namespace as the specified type <paramref name="resourceLocalType"/>.</returns>
        /// <remarks>
        /// This method is useful for accessing embedded resources that are located in the same assembly and namespace as the given type <paramref name="resourceLocalType"/>.
        /// </remarks>
        public static ResourceUtility GetUtility(Type resourceLocalType)
        {
            return new ResourceUtility(resourceLocalType);
        }


        /// <summary>
        /// Reads a text file embedded as a resource in the assembly and returns its content as a string.
        /// </summary>
        /// <param name="resourceName">The fully qualified name of the resource.</param>
        /// <returns>The content of the text file as a string.</returns>
        public static string ReadTextFile(string resourceName)
        {
            return ReadTextFile(typeof(ResourceUtilities), resourceName);
        }

        /// <summary>
        /// Reads a text file embedded as a resource in the assembly and returns its content as a string.
        /// </summary>
        /// <param name="resourceName">The fully qualified name of the resource.</param>
        /// <typeparam name="T">The type of the class local to the resource files.</typeparam>
        /// <returns>The content of the text file as a string.</returns>
        public static string ReadTextFile<T>(string resourceName)
        {
            return ReadTextFile(typeof(T), resourceName);
        }

        /// <summary>
        /// Reads a text file embedded as a resource in the assembly and returns its content as a string.
        /// </summary>
        /// <param name="resourceName">The fully qualified name of the resource.</param>
        /// <param name="resourceLocalType">The type of the class local to the resource files.</param>
        /// <returns>The content of the text file as a string.</returns>
        /// <exception cref="FileNotFoundException">If the resource is not found.</exception>
        public static string ReadTextFile(Type resourceLocalType, string resourceName)
        {
            resourceName = ResolveFileName(resourceLocalType, resourceName);

            // Access the assembly that calls this method
            var assembly = GetAssembly(resourceLocalType);

            // Use GetManifestResourceStream to access the embedded resource
            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new FileNotFoundException($"Resource '{resourceName}' not found. Make sure the resource exists and the name is correctly spelled and properly namespaced.");
            }

            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }


        /// <summary>
        /// Gets the names of all the embedded resources in the assembly.
        /// </summary>
        /// <returns>An enumerable collection of resource names.</returns>
        public static IEnumerable<string> GetResourceNames()
        {
            var assembly = GetAssembly();
            return assembly.GetManifestResourceNames();
        }

        /// <summary>
        /// Gets the names of all the embedded resources in the assembly.
        /// </summary>
        /// <typeparam name="T">The type of the class local to the resource files.</typeparam>
        /// <returns>An enumerable collection of resource names.</returns>
        public static IEnumerable<string> GetResourceNames<T>()
        {
            return GetResourceNames(typeof(T));
        }


        /// <summary>
        /// Gets the names of all the embedded resources in the assembly.
        /// </summary>
        /// <param name="resourceLocalType">The type of the class local to the resource files.</param>
        /// <returns>An enumerable collection of resource names.</returns>
        public static IEnumerable<string> GetResourceNames(Type resourceLocalType)
        {
            var assembly = resourceLocalType.Assembly;
            return assembly.GetManifestResourceNames();
        }


        /// <summary>
        /// Gets the assembly that contains the ResourceUtilities class.
        /// </summary>
        /// <returns>The assembly.</returns>
        public static Assembly GetAssembly()
        {
            return typeof(ResourceUtilities).Assembly;
        }

        /// <summary>
        /// Gets the assembly that contains the specified type.
        /// </summary>
        /// <param name="resourceLocalType">The type of the class local to the resource files.</param>
        /// <returns>The assembly.</returns>
        public static Assembly GetAssembly(Type resourceLocalType)
        {
            return resourceLocalType.Assembly;
        }

        /// <summary>
        /// Resolves the fully qualified name of the resource.
        /// </summary>
        ///
        /// <remarks>
        /// This method takes a resource name and resolves its fully qualified name by replacing backslashes with dots and
        /// pre-pending the namespace of the specified <paramref name="resourceLocalType" />. If the resource name already
        /// starts with the namespace, it is returned as is. This method is useful when working with embedded resources in an
        /// assembly.
        /// </remarks>
        ///
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        ///
        /// <param name="resourceLocalType">The type of the class local to the resource files.</param>
        /// <param name="resourceName">The resource name.</param>
        ///
        /// <returns>The resolved resource name.</returns>
        public static string ResolveFileName(Type resourceLocalType, string resourceName)
        {
            ArgumentNullException.ThrowIfNull(resourceName);

            var resourceCleanName = resourceName.Replace("\\", ".");

            var ns = resourceLocalType.Namespace ?? string.Empty;
            if (resourceCleanName.StartsWith(ns, StringComparison.OrdinalIgnoreCase))
            {
                return resourceCleanName;
            }

            return ns + "." + resourceCleanName;
        }

        /// <summary>
        /// Resolves the fully qualified name of the resource.
        /// </summary>
        ///
        /// <remarks>
        /// This method takes a resource name and resolves its fully qualified name by replacing backslashes with dots and
        /// pre-pending the namespace of the specified <typeparamref name="T"/>. If the resource name already
        /// starts with the namespace, it is returned as is. This method is useful when working with embedded resources in an
        /// assembly.
        /// </remarks>
        ///
        /// <typeparam name="T">The type of the class local to the resource files.</typeparam>
        /// <param name="resourceName">The resource name.</param>
        ///
        /// <returns>The resolved resource name.</returns>
        public static string ResolveFileName<T>(string resourceName)
        {
            return ResolveFileName(typeof(T), resourceName);
        }
    }
}
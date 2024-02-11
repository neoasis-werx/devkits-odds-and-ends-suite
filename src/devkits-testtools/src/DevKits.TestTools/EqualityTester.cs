namespace DevKits.TestTools;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Utility class for testing equality of objects.
/// </summary>
/// <remarks>
/// This class provides methods for testing equality, including GetHashCode, Equals, equality operator, and inequality operator.
/// It is designed to assist in unit testing scenarios for the implementation of equality-related methods in classes.
/// </remarks>
/// <seealso href="https://codinghelmet.com/articles/testing-equals-and-gethashcode">How to Implement Unit Tests for Equals and GetHashCode Methods</seealso>
public static class EqualityTester
{
    /// <summary>
    /// Tests equality of two equal objects.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    /// <param name="obj1">The first object for comparison.</param>
    /// <param name="obj2">The second object for comparison.</param>
    /// <remarks>
    /// This method tests GetHashCode, Equals, equality operator, and inequality operator for two equal objects.
    /// </remarks>
    public static void TestEqualObjects<T>(T obj1, T obj2)
    {
        ThrowIfAnyIsNull(obj1, obj2);

        IList<TestResult> testResults = new List<TestResult>
            {
                TestGetHashCodeOnEqualObjects(obj1, obj2),
                TestEquals(obj1, obj2, true),
                TestEqualsOfT(obj1, obj2, true),
                TestEqualityOperator(obj1, obj2, true),
                TestInequalityOperator(obj1, obj2, false)
            };

        AssertAllTestsHavePassed(testResults);
    }

    /// <summary>
    /// Tests equality of two unequal objects.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    /// <param name="obj1">The first object for comparison.</param>
    /// <param name="obj2">The second object for comparison.</param>
    /// <remarks>
    /// This method tests various aspects of equality for two unequal objects.
    /// </remarks>
    public static void TestUnequalObjects<T>(T obj1, T obj2)
    {
        ThrowIfAnyIsNull(obj1, obj2);

        IList<TestResult> testResults = new List<TestResult>
            {
                TestEqualsReceivingNonNullOfOtherType(obj1),
                TestEquals(obj1, obj2, false),
                TestEqualsOfT(obj1, obj2, false),
                TestEqualityOperator(obj1, obj2, false),
                TestInequalityOperator(obj1, obj2, true)
            };

        AssertAllTestsHavePassed(testResults);
    }

    /// <summary>
    /// Tests equality against null for the given object.
    /// </summary>
    /// <typeparam name="T">The type of object being tested.</typeparam>
    /// <param name="obj">The object for testing against null.</param>
    /// <remarks>
    /// This method tests various aspects of equality against null for the given object.
    /// </remarks>
    public static void TestAgainstNull<T>(T obj)
    {
        ThrowIfAnyIsNull(obj);

        IList<TestResult> testResults = new List<TestResult>
            {
                TestEqualsReceivingNull(obj),
                TestEqualsOfTReceivingNull(obj),
                TestEqualityOperatorReceivingNull(obj),
                TestInequalityOperatorReceivingNull(obj)
            };

        AssertAllTestsHavePassed(testResults);
    }

    // Other private and helper methods...

    private static TestResult TestGetHashCodeOnEqualObjects<T>(T obj1, T obj2)
    {
        return SafeCall("GetHashCode", () =>
        {
            if (obj1!.GetHashCode() != obj2!.GetHashCode())
                return TestResult.CreateFailure(
                    "GetHashCode of equal objects " +
                    "returned different values.");
            return TestResult.CreateSuccess();
        });
    }

    private static TestResult TestEqualsReceivingNonNullOfOtherType<T>(T obj)
    {
        return SafeCall("Equals", () =>
        {
            if (obj!.Equals(new object()))
                return TestResult.CreateFailure(
                    "Equals returned true when comparing " +
                    "with an object of a different type.");
            return TestResult.CreateSuccess();
        });
    }

    private static TestResult TestEqualsReceivingNull<T>(T obj)
    {
        if (typeof(T).IsClass)
            return TestEquals(obj, default, false);
        return TestResult.CreateSuccess();
    }

    private static TestResult TestEqualsOfTReceivingNull<T>(T obj)
    {
        if (typeof(T).IsClass)
            return TestEqualsOfT(obj, default, false);
        return TestResult.CreateSuccess();
    }

    private static TestResult TestEquals<T>(T? obj1, T? obj2,
        bool expectedEqual)
    {
        return SafeCall("Equals", () =>
        {
            if (obj1!.Equals(obj2) != expectedEqual)
            {
                var message =
                    string.Format("Equals returns {0} " +
                                  "on {1}equal objects.",
                        !expectedEqual,
                        expectedEqual ? "" : "non-");
                return TestResult.CreateFailure(message);
            }

            return TestResult.CreateSuccess();
        });
    }

    private static TestResult TestEqualsOfT<T>(T? obj1, T? obj2,
        bool expectedEqual)
    {
        if (obj1 is IEquatable<T> equatable)
            return TestEqualsOfTOnIEquatable(equatable, obj2, expectedEqual);
        return TestResult.CreateSuccess();
    }

    private static TestResult
        TestEqualsOfTOnIEquatable<T>(IEquatable<T>? obj1, T? obj2,
            bool expectedEqual)
    {
        return SafeCall("Strongly typed Equals", () =>
        {
            if (obj1!.Equals(obj2) != expectedEqual)
            {
                var message =
                    string.Format("Strongly typed Equals " +
                                  "returns {0} on {1}equal " +
                                  "objects.",
                        !expectedEqual,
                        expectedEqual ? "" : "non-");
                return TestResult.CreateFailure(message);
            }

            return TestResult.CreateSuccess();
        });
    }

    private static TestResult
        TestEqualityOperatorReceivingNull<T>(T obj)
    {
        if (typeof(T).IsClass)
            return TestEqualityOperator(obj, default, false);
        return TestResult.CreateSuccess();
    }

    private static TestResult TestEqualityOperator<T>(T? obj1, T? obj2, bool expectedEqual)
    {
        var equalityOperator = GetEqualityOperator<T>();
        if (equalityOperator == null)
            return TestResult.CreateFailure("Type does not override " +
                                            "the equality operator.");
        return TestEqualityOperator(obj1, obj2, expectedEqual,
            equalityOperator);
    }

    private static TestResult
        TestEqualityOperator<T>(T? obj1, T? obj2, bool expectedEqual,
            MethodInfo equalityOperator)
    {
        return SafeCall("Operator ==", () =>
        {
            var equal = (bool)equalityOperator.Invoke(null, new object[] { obj1!, obj2! })!;
            if (equal != expectedEqual)
            {
                var message =
                    string.Format("Equality operator returned " +
                                  "{0} on {1}equal objects.",
                        equal,
                        expectedEqual ? "" : "non-");
                return TestResult.CreateFailure(message);
            }

            return TestResult.CreateSuccess();
        });
    }

    private static TestResult TestInequalityOperatorReceivingNull<T>(T obj)
    {
        if (typeof(T).IsClass)
            return TestInequalityOperator(obj, default, true);
        return TestResult.CreateSuccess();
    }

    private static TestResult TestInequalityOperator<T>(T? obj1, T? obj2, bool expectedUnequal)
    {
        var inequalityOperator = GetInequalityOperator<T>();
        if (inequalityOperator == null)
            return TestResult.CreateFailure("Type does not override " +
                                            "the inequality operator.");
        return TestInequalityOperator(obj1, obj2, expectedUnequal,
            inequalityOperator);
    }


    private static TestResult TestInequalityOperator<T>(T? obj1, T? obj2, bool expectedUnequal, MethodInfo inequalityOperator)
    {
        return SafeCall("Operator !=", () =>
        {
            var unequal = (bool)inequalityOperator.Invoke(null, new object[] { obj1!, obj2! })!;
            if (unequal != expectedUnequal)
            {
                var message = $"Inequality operator returned {unequal} when comparing {(expectedUnequal ? "non-" : "")}equal objects.";
                return TestResult.CreateFailure(message);
            }

            return TestResult.CreateSuccess();
        });
    }

    private static void ThrowIfAnyIsNull(params object?[] objects)
    {
        if (Array.Exists(objects, o => o is null))
        {
            throw new ArgumentNullException(nameof(objects), "Objects had a null value");
        }
    }

    private static TestResult SafeCall(string functionName, Func<TestResult> test)
    {
        try
        {
            return test();
        }
        catch (Exception ex)
        {
            var message = $"{functionName} threw {ex.GetType().Name}: {ex.Message}";

            return TestResult.CreateFailure(message);
        }
    }

    private static MethodInfo? GetEqualityOperator<T>()
    {
        return GetOperator<T>("op_Equality");
    }

    private static MethodInfo? GetInequalityOperator<T>()
    {
        return GetOperator<T>("op_Inequality");
    }

    private static MethodInfo? GetOperator<T>(string methodName)
    {
        var bindingFlags = BindingFlags.Static | BindingFlags.Public;
        var equalityOperator = typeof(T).GetMethod(methodName, bindingFlags);
        return equalityOperator;
    }

    private static void AssertAllTestsHavePassed(IList<TestResult> testResults)
    {
        var allTestsPass = testResults.All(r => r.IsSuccess);
        var errors = testResults
                        .Where(r => !r.IsSuccess)
                        .Select(r => r.ErrorMessage)
                        .ToArray();
        var compoundMessage = string.Join(Environment.NewLine, errors);

        if (!allTestsPass)
        {
            throw new Exception("Some tests have failed:\n" + compoundMessage);
        }
    }

    /// <summary>
    /// Represents the result of a test.
    /// </summary>
    private readonly struct TestResult
    {
        /// <summary>
        /// Gets a value indicating whether the test was successful.
        /// </summary>
        public bool IsSuccess { get; private init; }

        /// <summary>
        /// Gets the error message if the test failed.
        /// </summary>
        public string ErrorMessage { get; private init; }

        /// <summary>
        /// Creates a successful test result.
        /// </summary>
        /// <returns>A successful test result.</returns>
        public static TestResult CreateSuccess()
        {
            return new TestResult
            {
                IsSuccess = true
            };
        }

        /// <summary>
        /// Creates a failed test result with the specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>A failed test result.</returns>
        public static TestResult CreateFailure(string message)
        {
            return new TestResult
            {
                IsSuccess = false,
                ErrorMessage = message
            };
        }
    }
}


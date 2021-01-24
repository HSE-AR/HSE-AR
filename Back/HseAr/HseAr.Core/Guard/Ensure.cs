

using System;
using System.Collections.Generic;
using System.Linq;

namespace HseAr.Core.Guard
{
    public static class Ensure
    {
        public static void IsNotNull<T>(T value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, "Value cannot be null.");
            }
        }

        public static void IsNotNull<T>(T? value, string paramName)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, "Value cannot be null.");
            }
        }

        public static void IsNotNullOrEmpty(string? value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (value.Length == 0)
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }
        }

        public static void IsNotNullOrWhiteSpace(string? value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be empty or white space.", paramName);
            }
        }

        public static void IsNotNullOrEmptySequence<T>(IEnumerable<T>? value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (!value.Any())
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }
        }

        public static void IsNotDefault<T>(T value, string paramName)
            where T : struct
        {
            if (value.Equals(default))
            {
                throw new ArgumentException(paramName);
            }
        }

        public static void IsGreaterThenZero(int value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException(paramName);
            }
        }

        public static void IsGreaterThenZero(ushort value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException(paramName);
            }
        }

        public static void IsGreaterThenZero(long value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException(paramName);
            }
        }

        public static void IsGreaterThenZero(decimal value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException(paramName);
            }
        }

        public static void IsGreaterThenZero(double value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException(paramName);
            }
        }

        public static void IsNonNegative(int value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative.");
            }
        }

        public static void IsNonNegative(int? value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative.");
            }
        }

        public static void IsNotEmptyGuid(Guid value, string paramName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException(paramName);
            }
        }

        public static void That(bool assertion, string message)
        {
            if (!assertion)
            {
                throw new ArgumentException(message);
            }
        }

        public static void Equals(string x, string y, string message)
        {
            if (x != y)
            {
                throw new ArgumentException(message);
            }
        }

        public static void Equals(Guid x, Guid y, string message)
        {
            if (x != y)
            {
                throw new ArgumentException(message);
            }
        }
    }
}

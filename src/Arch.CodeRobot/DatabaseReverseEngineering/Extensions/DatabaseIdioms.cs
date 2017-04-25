// Copyright (c) yingtingxu(徐应庭). All rights reserved.

using System.Linq;

namespace Arch.CodeRobot
{
    /// <summary>
    /// Some extension methods for <see cref="string"/> for handling database idioms.
    /// </summary>
    public static class DatabaseIdioms
    {
        /// <summary>
        /// Converts database column name to C# class property name.
        /// </summary>
        /// <param name="columnName">The database column name.</param>
        /// <returns>The C# class property name.</returns>
        public static string PropertyName(this string columnName)
        {
            // remove 'F'
            var feild = columnName.Remove(0, 1);

            var items = feild.Split('_');

            items = items.Select(i => i.FirstLetterToUpper()).ToArray();

            return string.Concat(items);
        }

        /// <summary>
        /// Converts database table name to C# class name.
        /// </summary>
        /// <param name="tableName">The database table name.</param>
        /// <returns></returns>
        public static string ClassName(this string tableName)
        {
            // remove 't_'
            var table = tableName.Remove(0, 2);

            var items = table.Split('_');

            items = items.Select(i => i.FirstLetterToUpper()).ToArray();

            return string.Concat(items);
        }

        /// <summary>
        /// Converts database name to C# conventions and idioms name.
        /// </summary>
        /// <param name="databaseName">The database name.</param>
        /// <returns>The C# conventions and idioms name.</returns>
        public static string CSharpName(this string databaseName)
        {
            var items = databaseName.Split('_');

            items = items.Select(i => i.FirstLetterToUpper()).ToArray();

            return string.Concat(items);
        }

        private static string FirstLetterToUpper(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}

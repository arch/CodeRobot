// Copyright (c) yingtingxu(徐应庭). All rights reserved.

using System.Reflection;

namespace Arch.CodeRobot
{
    /// <summary>
    /// Defines the interfaces for database reverse engineering code generator.
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Generates source code from <paramref name="databaseModel"/>, <seealso cref="DatabaseModel"/>
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <returns>The source code.</returns>
        string Generate(DatabaseModel databaseModel);
        /// <summary>
        /// Generates assembly form <paramref name="databaseModel"/> and save to specified file.
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="saveDirectory">The directory to save the generated assembly.</param>
        void Generate(DatabaseModel databaseModel, string assemblyName, string saveDirectory);
        /// <summary>
        /// Generates an in memory assembly from <paramref name="databaseModel"/>.
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="assemblyName">The assembly name.</param>
        /// <returns>The in memory assembly.</returns>
        Assembly Generate(DatabaseModel databaseModel, string assemblyName);
    }
}

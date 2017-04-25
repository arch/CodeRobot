// Copyright (c) yingtingxu(徐应庭). All rights reserved

using System.Data.Common;

namespace Arch.CodeRobot
{
    /// <summary>
    /// Defines the interfaces for <see cref="DatabaseModel"/> factory.
    /// </summary>
    public interface IDatabaseModelFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="DatabaseModel"/> by the specified <paramref name="connection"/>.
        /// </summary>
        /// <param name="connection">The db connection.</param>
        /// <returns>The instance of <see cref="DatabaseModel"/></returns>
        DatabaseModel Create(DbConnection connection);
    }
}

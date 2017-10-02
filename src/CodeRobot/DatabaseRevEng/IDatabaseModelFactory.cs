// Copyright (c) rigofunc (xuyingting). All rights reserved.

using System.Data.Common;

namespace CodeRobot.Database.RevEng
{
    /// <summary>
    /// Defines the interfaces for <see cref="DatabaseModel"/> factory.
    /// </summary>
    public interface IDatabaseModelFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="DatabaseModel"/> by the specified <paramref name="connection"/> and <paramref name="tableSelectionSet"/>.
        /// </summary>
        /// <param name="connection">The db connection.</param>
        /// <param name="tableSelectionSet">The table selection set.</param>
        /// <returns>The instance of <see cref="DatabaseModel"/></returns>
        DatabaseModel Create(DbConnection connection, TableSelectionSet tableSelectionSet);
    }
}

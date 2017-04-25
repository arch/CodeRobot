// Copyright (c) yingtingxu(徐应庭). All rights reserved.

using System.Collections.Generic;

namespace Arch.CodeRobot
{
    /// <summary>
    /// Represents the database model.
    /// </summary>
    public class DatabaseModel
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the default schema name.
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// Gets or sets the tables belong to this database.
        /// </summary>
        public ICollection<TableModel> Tables { get; set; } = new List<TableModel>();
    }
}

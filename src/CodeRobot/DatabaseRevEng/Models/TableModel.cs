// Copyright (c) rigofunc (xuyingting). All rights reserved.

using System.Collections.Generic;

namespace CodeRobot.Database.RevEng
{
    /// <summary>
    /// Represens the table model
    /// </summary>
    public class TableModel
    {
        /// <summary>
        /// Gets or sets the belong to database.
        /// </summary>
        public DatabaseModel Database { get; set; }

        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the table schema.
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public ICollection<ColumnModel> Columns { get; set; } = new List<ColumnModel>();

        /// <summary>
        /// Gets or sets the indexes.
        /// </summary>
        public virtual ICollection<IndexModel> Indexes { get; } = new List<IndexModel>();

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName => (!string.IsNullOrEmpty(Schema) ? Schema + "." : "") + Name;
    }
}

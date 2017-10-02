// Copyright (c) rigofunc (xuyingting). All rights reserved.

namespace CodeRobot.Database.RevEng
{
    /// <summary>
    /// Represents the column model.
    /// </summary>
    public class ColumnModel
    {
        /// <summary>
        /// Gets or sets the belong to table.
        /// </summary>
        public TableModel Table { get; set; }
        /// <summary>
        /// Gets or sets the column name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the column comment
        /// </summary>
        public string Comment { get; set; }

        public int? PrimaryKeyOrdinal { get; set; }

        public int Ordinal { get; set; }

        public bool IsNullable { get; set; }

        public string DataType { get; set; }

        public string DefaultValue { get; set; }

        public string ComputedValue { get; set; }

        public int? MaxLength { get; set; }

        public int? Precision { get; set; }

        public int? Scale { get; set; }
    }
}

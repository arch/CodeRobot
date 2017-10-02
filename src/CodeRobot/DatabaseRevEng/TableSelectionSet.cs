// Copyright (c) rigofunc (xuyingting). All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeRobot.Database.RevEng
{
    public class TableSelectionSet
    {
        public static readonly TableSelectionSet All = new TableSelectionSet();

        public TableSelectionSet()
            : this(Enumerable.Empty<string>(), Enumerable.Empty<string>())
        {
        }

        public TableSelectionSet(IEnumerable<string> tables)
            : this(tables, Enumerable.Empty<string>())
        {
        }

        public TableSelectionSet(IEnumerable<string> tables, IEnumerable<string> schemas)
        {
            if (tables == null)
            {
                throw new ArgumentNullException(nameof(tables));
            }

            if (schemas == null)
            {
                throw new ArgumentNullException(nameof(schemas));
            }

            Schemas = schemas.Select(schema => new Selection(schema)).ToList();
            Tables = tables.Select(table => new Selection(table)).ToList();
        }

        public virtual IReadOnlyList<Selection> Schemas { get; }
        public virtual IReadOnlyList<Selection> Tables { get; }

        public class Selection
        {
            public Selection(string selectionText)
            {
                if (string.IsNullOrEmpty(selectionText))
                {
                    throw new ArgumentException("The selection text can not be null or empty", nameof(selectionText));
                }

                Text = selectionText;
            }

            public string Text { get; }
            public bool IsMatched { get; set; }
        }
    }
}

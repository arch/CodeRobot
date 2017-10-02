// Copyright (c) rigofunc (xuyingting). All rights reserved

using System.Collections.Generic;

namespace Tools.Database.RevEng
{
    public class IndexModel
    {
        public TableModel Table { get; set; }
        public string Name { get; set; }
        public ICollection<IndexColumnModel> IndexColumns { get; set; } = new List<IndexColumnModel>();
        public bool IsUnique { get; set; }
        public string Filter { get; set; }
    }
}

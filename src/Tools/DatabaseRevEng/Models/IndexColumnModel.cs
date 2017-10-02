// Copyright (c) rigofunc (xuyingting). All rights reserved

namespace Tools.Database.RevEng
{
    public class IndexColumnModel
    {
        public int Ordinal { get; set; }
        public ColumnModel Column { get; set; }
        public IndexModel Index { get; set; }
    }
}

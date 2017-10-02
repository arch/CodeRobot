// Copyright (c) rigofunc (xuyingting). All rights reserved

namespace CodeRobot.Database.RevEng
{
    public class IndexColumnModel
    {
        public int Ordinal { get; set; }
        public ColumnModel Column { get; set; }
        public IndexModel Index { get; set; }
    }
}

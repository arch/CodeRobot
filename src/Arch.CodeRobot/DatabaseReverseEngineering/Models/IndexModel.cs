// Copyright (c) yingtingxu(徐应庭). All rights reserved

using System.Collections.Generic;

namespace Arch.CodeRobot
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

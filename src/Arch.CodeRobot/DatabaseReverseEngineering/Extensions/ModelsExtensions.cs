// Copyright (c) yingtingxu(徐应庭). All rights reserved

namespace Arch.CodeRobot
{
    internal static class ModelsExtensions
    {
        internal static string DbContextPrefix(this DatabaseModel model)
        {
            return model.Name.CSharpName();
        }

        internal static string PropertyName(this ColumnModel model)
        {
            return model.Name.PropertyName();
        }

        internal static string ClassName(this TableModel model)
        {
            return model.Name.ClassName();
        }
    }
}

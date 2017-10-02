// Copyright (c) rigofunc (xuyingting). All rights reserved.

namespace Tools.Database.RevEng
{
    public interface IDatabaseIdioms
    {
        string GetPropertyName(string columnName);

        string GetClassName(string tableName);

        string GetContextName(string databaseName);
    }
}

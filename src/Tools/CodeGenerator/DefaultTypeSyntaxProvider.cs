// Copyright (c) rigofunc (xuyingting). All rights reserved.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Simplification;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Tools.Generator
{
    /// <summary>
    /// Represents the default implementation of the <see cref="ITypeSyntaxProvider"/>, which work for MySQL.
    /// </summary>
    public class DefaultTypeSyntaxProvider : ITypeSyntaxProvider
    {
        private static Dictionary<string, TypeSyntax> _map = new Dictionary<string, TypeSyntax>
        {
            ["boolean"] = PredefinedType(Token(SyntaxKind.BoolKeyword)),
            ["bit"] = PredefinedType(Token(SyntaxKind.BoolKeyword)),
            ["tinyint"] = PredefinedType(Token(SyntaxKind.ByteKeyword)),
            ["binary"] = ParseTypeName(typeof(byte[]).Name),
            ["varbinary"] = ParseTypeName(typeof(byte[]).Name),
            ["blob"] = ParseTypeName(typeof(byte[]).Name),
            ["longblob"] = ParseTypeName(typeof(byte[]).Name),
            ["datetime"] = ParseTypeName(typeof(DateTime).Name),
            ["datetimeoffset"] = ParseTypeName(typeof(DateTimeOffset).Name),
            ["decimal"] = PredefinedType(Token(SyntaxKind.DecimalKeyword)),
            ["double"] = PredefinedType(Token(SyntaxKind.DoubleKeyword)),
            ["guid"] = ParseTypeName(typeof(Guid).Name),
            ["smallint"] = PredefinedType(Token(SyntaxKind.ShortKeyword)),
            ["int"] = PredefinedType(Token(SyntaxKind.IntKeyword)),
            ["bigint"] = PredefinedType(Token(SyntaxKind.LongKeyword)),
            ["sbyte"] = PredefinedType(Token(SyntaxKind.SByteKeyword)),
            ["float"] = PredefinedType(Token(SyntaxKind.FloatKeyword)),
            ["char"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["varchar"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["text"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["longtext"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["time"] = ParseTypeName(typeof(TimeSpan).Name),
        };

        private static List<string> _nullables = new List<string> {
            "boolean",
            "bit",
            "tinyint",
            "datetime",
            "datetimeoffset",
            "decimal",
            "double",
            "guid",
            "smallint",
            "int",
            "bigint",
            "sbyte",
            "float",
            "time"
        };

        public TypeSyntax CreateTypeSyntax(string sqlDataType, bool isNullable)
        {
            if (_map.TryGetValue(sqlDataType, out var value))
            {
                if (isNullable && _nullables.Contains(sqlDataType))
                {
                    return NullableType(value);
                }

                return value;
            }

            return PredefinedType(Token(SyntaxKind.StringKeyword));
        }
    }
}

// Copyright (c) yingtingxu(徐应庭). All rights reserved.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Simplification;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Arch.CodeRobot
{
    internal static class MySqlDataTypeExtensions
    {
        private static Dictionary<string, TypeSyntax> _map = new Dictionary<string, TypeSyntax>
        {
            ["boolean"] = PredefinedType(Token(SyntaxKind.BoolKeyword)),
            ["bit"] = PredefinedType(Token(SyntaxKind.BoolKeyword)),
            ["tinyint"] = PredefinedType(Token(SyntaxKind.ByteKeyword)),
            ["binary"] = ParseTypeName(typeof(byte[]).Name).WithAdditionalAnnotations(Simplifier.Annotation),
            ["varbinary"] = ParseTypeName(typeof(byte[]).Name).WithAdditionalAnnotations(Simplifier.Annotation),
            ["blob"] = ParseTypeName(typeof(byte[]).Name).WithAdditionalAnnotations(Simplifier.Annotation),
            ["longblob"] = ParseTypeName(typeof(byte[]).Name).WithAdditionalAnnotations(Simplifier.Annotation),
            ["datetime"] = ParseTypeName(typeof(DateTime).Name).WithAdditionalAnnotations(Simplifier.Annotation),
            ["datetimeoffset"] = ParseTypeName(typeof(DateTimeOffset).Name).WithAdditionalAnnotations(Simplifier.Annotation),
            ["decimal"] = PredefinedType(Token(SyntaxKind.DecimalKeyword)),
            ["double"] = PredefinedType(Token(SyntaxKind.DoubleKeyword)),
            ["guid"] = ParseTypeName(typeof(Guid).Name).WithAdditionalAnnotations(Simplifier.Annotation),
            ["smallint"] = PredefinedType(Token(SyntaxKind.ShortKeyword)),
            ["int"] = PredefinedType(Token(SyntaxKind.IntKeyword)),
            ["bigint"] = PredefinedType(Token(SyntaxKind.LongKeyword)),
            ["sbyte"] = PredefinedType(Token(SyntaxKind.SByteKeyword)),
            ["float"] = PredefinedType(Token(SyntaxKind.FloatKeyword)),
            ["char"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["varchar"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["text"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["longtext"] = PredefinedType(Token(SyntaxKind.StringKeyword)),
            ["time"] = ParseTypeName(typeof(TimeSpan).Name).WithAdditionalAnnotations(Simplifier.Annotation),
        };

        public static TypeSyntax ToTypeSyntax(this string MySqlDataType)
        {
            if (_map.TryGetValue(MySqlDataType, out var value))
            {
                return value;
            }

            return PredefinedType(Token(SyntaxKind.StringKeyword));
        }
    }
}

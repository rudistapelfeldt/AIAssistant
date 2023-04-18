using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AIAssistant.Utils
{
    public static class CodeFormatter
    {
        public static string Format(string code)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            var root = syntaxTree.GetRoot();

            var formattedRoot = root.NormalizeWhitespace();

            return formattedRoot.ToFullString();
        }
    }
}


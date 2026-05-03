using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_gui
{
    public class SemanticAnalyzer
    {
        private List<Token> tokens;
        private SymbolTable symbolTable;
        private List<SemanticError> semanticErrors;
        private ProgramNode ast;

        private const int MIN_INT_VALUE = -32768;
        private const int MAX_INT_VALUE = 32767;

        public SemanticAnalyzer()
        {
            symbolTable = new SymbolTable();
            semanticErrors = new List<SemanticError>();
            ast = new ProgramNode();
        }

        public ProgramNode Analyze(List<Token> tokenList)
        {
            tokens = tokenList;
            semanticErrors.Clear();
            symbolTable = new SymbolTable();
            ast = new ProgramNode();

            List<ConstDeclarationData> declarations = ExtractConstDeclarations();

            foreach (var decl in declarations)
            {
                ProcessDeclaration(decl);
            }

            return ast;
        }

        private class ConstDeclarationData
        {
            public string Identifier { get; set; }
            public int IdentifierLine { get; set; }
            public int IdentifierStart { get; set; }
            public int IdentifierEnd { get; set; }
            public string TypeName { get; set; }
            public List<Token> ExpressionTokens { get; set; }
            public int ExpressionLine { get; set; }
            public int ExpressionStart { get; set; }
            public int ExpressionEnd { get; set; }
        }

        private List<ConstDeclarationData> ExtractConstDeclarations()
        {
            var declarations = new List<ConstDeclarationData>();
            int i = 0;

            while (i < tokens.Count)
            {
                if (tokens[i].Type == TokenType.WhiteSpace)
                {
                    i++;
                    continue;
                }

                if (tokens[i].Type == TokenType.Const)
                {
                    i++;

                    while (i < tokens.Count && tokens[i].Type == TokenType.WhiteSpace)
                        i++;

                    if (i >= tokens.Count) break;

                    if (tokens[i].Type == TokenType.Id)
                    {
                        var decl = new ConstDeclarationData
                        {
                            Identifier = tokens[i].Value,
                            IdentifierLine = tokens[i].Line,
                            IdentifierStart = tokens[i].StartPos,
                            IdentifierEnd = tokens[i].EndPos
                        };
                        i++;

                        while (i < tokens.Count && tokens[i].Type == TokenType.WhiteSpace)
                            i++;

                        if (i < tokens.Count && tokens[i].Type == TokenType.Colon)
                        {
                            i++;

                            while (i < tokens.Count && tokens[i].Type == TokenType.WhiteSpace)
                                i++;

                            if (i < tokens.Count && (tokens[i].Type == TokenType.Integer || tokens[i].Type == TokenType.Id))
                            {
                                decl.TypeName = tokens[i].Value ?? "integer";
                                i++;

                                while (i < tokens.Count && tokens[i].Type == TokenType.WhiteSpace)
                                    i++;

                                if (i < tokens.Count && tokens[i].Type == TokenType.Equal)
                                {
                                    i++;

                                    while (i < tokens.Count && tokens[i].Type == TokenType.WhiteSpace)
                                        i++;

                                    var exprTokens = new List<Token>();
                                    int startIdx = i;

                                    while (i < tokens.Count && tokens[i].Type != TokenType.EndOperator)
                                    {
                                        if (tokens[i].Type != TokenType.WhiteSpace)
                                            exprTokens.Add(tokens[i]);
                                        i++;
                                    }

                                    if (exprTokens.Count > 0)
                                    {
                                        decl.ExpressionTokens = exprTokens;
                                        decl.ExpressionStart = exprTokens.First().StartPos;
                                        decl.ExpressionEnd = exprTokens.Last().EndPos;
                                        decl.ExpressionLine = exprTokens.First().Line;
                                    }

                                    if (i < tokens.Count && tokens[i].Type == TokenType.EndOperator)
                                    {
                                        i++;
                                    }
                                }
                            }
                        }

                        declarations.Add(decl);
                    }
                }
                else
                {
                    i++;
                }
            }

            return declarations;
        }

        private void ProcessDeclaration(ConstDeclarationData decl)
        {
            if (!symbolTable.Declare(decl.Identifier, decl.TypeName ?? "integer", null,
                decl.IdentifierLine, decl.IdentifierStart, decl.IdentifierEnd))
            {
                return;
            }

            int? value = EvaluateExpression(decl.ExpressionTokens, decl.ExpressionLine,
                decl.ExpressionStart, decl.ExpressionEnd);

            if (value.HasValue)
            {
                if (value < MIN_INT_VALUE || value > MAX_INT_VALUE)
                {
                    semanticErrors.Add(new SemanticError(
                        $"Значение {value} выходит за пределы {MIN_INT_VALUE}..{MAX_INT_VALUE}",
                        value.ToString(),
                        decl.ExpressionLine,
                        decl.ExpressionStart,
                        decl.ExpressionEnd
                    ));
                    return;
                }

                var symbol = symbolTable.Lookup(decl.Identifier);
                if (symbol != null)
                {
                    symbol.Value = value;
                }
                var typeNode = new IntNode { Name = decl.TypeName ?? "integer" };
                var valueNode = new IntLiteralNode { Value = value.Value };

                var constDeclNode = new ConstDeclNode
                {
                    Name = decl.Identifier,
                    Modifier = "const",
                    Type = typeNode,
                    Value = valueNode,
                    Line = decl.IdentifierLine,
                    StartPos = decl.IdentifierStart,
                    EndPos = decl.IdentifierEnd
                };

                ast.Constants.Add(constDeclNode);
            }
        }

        private int? EvaluateExpression(List<Token> exprTokens, int line, int startPos, int endPos)
        {
            if (exprTokens == null || exprTokens.Count == 0)
            {
                semanticErrors.Add(new SemanticError("Ожидается целое число", "", line, startPos, endPos));
                return null;
            }

            int sign = 1;
            int tokenIdx = 0;

            if (exprTokens[0].Type == TokenType.Minus)
            {
                sign = -1;
                tokenIdx = 1;
            }
            else if (exprTokens[0].Type == TokenType.Plus)
            {
                tokenIdx = 1;
            }

            if (tokenIdx >= exprTokens.Count)
            {
                semanticErrors.Add(new SemanticError("После знака ожидается число", "", line, startPos, endPos));
                return null;
            }

            if (exprTokens[tokenIdx].Type == TokenType.IntDigit)
            {
                if (int.TryParse(exprTokens[tokenIdx].Value, out int value))
                {

                    if (exprTokens[tokenIdx].Type == TokenType.Id)
                    {
                        if (!symbolTable.CheckIdentifierUsed(exprTokens[tokenIdx].Value,
                            exprTokens[tokenIdx].Line, exprTokens[tokenIdx].StartPos, exprTokens[tokenIdx].EndPos))
                        {
                            return null;
                        }

                        var symbol = symbolTable.Lookup(exprTokens[tokenIdx].Value);
                        if (symbol != null && symbol.Value.HasValue)
                        {
                            return sign * symbol.Value.Value;
                        }
                        return null;
                    }

                    return sign * value;
                }
            }
            else if (exprTokens[tokenIdx].Type == TokenType.Id)
            {
                if (!symbolTable.CheckIdentifierUsed(exprTokens[tokenIdx].Value,
                    exprTokens[tokenIdx].Line, exprTokens[tokenIdx].StartPos, exprTokens[tokenIdx].EndPos))
                {
                    return null;
                }

                var symbol = symbolTable.Lookup(exprTokens[tokenIdx].Value);
                if (symbol != null && symbol.Value.HasValue)
                {
                    return sign * symbol.Value.Value;
                }
                else
                {
                    semanticErrors.Add(new SemanticError(
                        $"Идентификатор '{exprTokens[tokenIdx].Value}' не имеет значения",
                        exprTokens[tokenIdx].Value,
                        exprTokens[tokenIdx].Line,
                        exprTokens[tokenIdx].StartPos,
                        exprTokens[tokenIdx].EndPos
                    ));
                    return null;
                }
            }

            semanticErrors.Add(new SemanticError("Ожидается целое число",
                exprTokens[tokenIdx].Value ?? "",
                exprTokens[tokenIdx].Line,
                exprTokens[tokenIdx].StartPos,
                exprTokens[tokenIdx].EndPos));

            return null;
        }

        public List<SemanticError> GetSemanticErrors()
        {
            var allErrors = new List<SemanticError>();
            allErrors.AddRange(semanticErrors);
            allErrors.AddRange(symbolTable.Errors);
            return allErrors;
        }

        public bool HasErrors()
        {
            return GetSemanticErrors().Count > 0;
        }
        public void DisplaySemanticErrors(List<SemanticError> errors, DataGridView data, System.Windows.Forms.Label label)
        {
            data.Columns.Clear();
            data.Rows.Clear();
            label.Text = "";
            data.Columns.Clear();
            data.Columns.Add("Fragment", "Фрагмент");
            data.Columns.Add("Location", "Позиция");
            data.Columns.Add("Message", "Описание");

            foreach (var error in errors)
            {
                data.Rows.Add(
                    error.Fragment,
                    $"строка {error.Line}, поз. {error.StartPos}-{error.EndPos}",
                    error.Message
                );
            }
            label.Text = $"Семантических ошибок: {errors.Count}";
        }
    }
}
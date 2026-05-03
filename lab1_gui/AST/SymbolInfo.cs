using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1_gui
{
    public class SymbolInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Value { get; set; }
        public int Line { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }
        public bool IsConstant { get; set; } = true;
    }

    public class SymbolTable
    {
        private Dictionary<string, SymbolInfo> symbols = new Dictionary<string, SymbolInfo>();
        private List<SemanticError> errors = new List<SemanticError>();

        public List<SemanticError> Errors => errors;

        public bool IsDeclared(string name)
        {
            return symbols.ContainsKey(name);
        }

        public SymbolInfo Lookup(string name)
        {
            return symbols.ContainsKey(name) ? symbols[name] : null;
        }

        public bool Declare(string name, string type, int? value, int line, int startPos, int endPos)
        {
            if (IsDeclared(name))
            {
                SymbolInfo existing = symbols[name];
                errors.Add(new SemanticError(
                    $"Повторное объявление '{name}'. Первое объявление: строка {existing.Line}",
                    name,
                    line,
                    startPos,
                    endPos
                ));
                return false;
            }

            symbols[name] = new SymbolInfo
            {
                Name = name,
                Type = type,
                Value = value,
                Line = line,
                StartPos = startPos,
                EndPos = endPos,
                IsConstant = true
            };
            return true;
        }

        public bool CheckIdentifierUsed(string name, int line, int startPos, int endPos)
        {
            if (!IsDeclared(name))
            {
                errors.Add(new SemanticError(
                    $"Использование необъявленного идентификатора '{name}'",
                    name,
                    line,
                    startPos,
                    endPos
                ));
                return false;
            }
            return true;
        }
    }

    public class SemanticError
    {
        public string Message { get; set; }
        public string Fragment { get; set; }
        public int Line { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }

        public SemanticError(string message, string fragment, int line, int startPos, int endPos)
        {
            Message = message;
            Fragment = fragment;
            Line = line;
            StartPos = startPos;
            EndPos = endPos;
        }
    }
}
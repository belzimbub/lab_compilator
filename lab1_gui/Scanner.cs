using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab1_gui.Token;

namespace lab1_gui
{
    public class Scanner
    {
        public List<Token> Analyze(string text)
        {
            var tokens = new List<Token>();
            int pos = 0, line = 1, col = 1, cond_colon = 0;
            bool cond_keyword = true, cond_value = false;

            while (pos < text.Length)
            {
                int startCol = col;
                int startPos = pos;

                if (text[pos] == '\n')
                {
                    line++;
                    col = 1;
                    pos++;
                    continue;
                }
                if (text[pos] == ' ' || text[pos] == '\b' || text[pos] == '\r')
                {
                    string spaces = "";
                    while (pos < text.Length && (text[pos] == ' ' || text[pos] == '\t' || text[pos] == '\r'))
                    {
                        spaces += text[pos];
                        pos++; 
                        col++;
                    }

                    tokens.Add(new Token
                    {
                        Type = TokenType.WhiteSpace,
                        Value = "(пробел)",
                        Line = line,
                        StartPos = startCol,
                        EndPos = col - 1,
                        AbsoluteIndex = startPos
                    });
                    continue;
                }
                if (char.IsLetter(text[pos]) || text[pos] == '_')
                {
                    string lexeme = "";
                    while (pos < text.Length && (char.IsLetterOrDigit(text[pos]) || text[pos] == '_'))
                    {
                        lexeme += text[pos];
                        col++; 
                        pos++;
                    }
                    TokenType type;
                    switch (lexeme)
                        {
                            case "const":
                                if (cond_colon == 0 && cond_value == false)
                                {
                                    type = TokenType.Const;
                                    cond_keyword = false;
                                }
                                else
                                {
                                    type = TokenType.Error;
                                }
                                break;
                            case "integer":
                                if (cond_colon > 1 && cond_keyword == true && cond_value == false)
                                {
                                    type = TokenType.Integer;
                                    cond_keyword = false;
                                }
                                else
                                {
                                    type = TokenType.Error;
                                }
                                break;
                            default:
                                if (cond_colon != 0 || cond_keyword == true || cond_value==true)
                                {
                                    type = TokenType.Error;
                                    cond_keyword = false;
                                }
                                else
                                {
                                    type = TokenType.Id;
                                    cond_colon = 1;
                                }
                                break;
                        }
                    tokens.Add(new Token
                    {
                        Type = type,
                        Value = lexeme,
                        Line = line,
                        StartPos = startCol,
                        EndPos = col - 1,
                        AbsoluteIndex = startPos
                    });
                    continue;
                }
                
                if (text[pos] == '=')
                {
                    TokenType type = TokenType.Error;
                    if (cond_colon == 2 && cond_keyword == false)
                    {
                        type = TokenType.Equal;
                        
                    }
                    else
                    {
                        type = TokenType.Error;
                    }
                    cond_value = true;
                    col++;
                    pos++;
                    tokens.Add(new Token
                    {
                        Type = type,
                        Value = "=",
                        Line = line,
                        StartPos = startCol,
                        EndPos = col - 1,
                        AbsoluteIndex = startPos
                    });
                    continue;
                }
                if (text[pos] == ':')
                {
                    if (cond_colon == 1)
                    {
                        cond_keyword = true;
                        cond_colon = 2;
                        tokens.Add(new Token
                        {
                            Type = TokenType.Colon,
                            Value = ":",
                            Line = line,
                            StartPos = startCol,
                            EndPos = col,
                            AbsoluteIndex = startPos
                        });
                        col++; 
                        pos++;
                        continue;
                    }
                }
                if (text[pos] == ';')
                {
                    cond_colon = 0;
                    cond_value = false;
                    cond_keyword = true;
                    tokens.Add(new Token
                    {
                        Type = TokenType.EndOperator,
                        Value = ";",
                        Line = line,
                        StartPos = startCol,
                        EndPos = col,
                        AbsoluteIndex = startPos
                    });
                    col++; 
                    pos++;
                    continue;
                }
                if (char.IsDigit(text[pos]) || text[pos]=='-')
                {
                    string lexeme = "";
                    lexeme += text[pos];
                    col++;
                    pos++;
                    while (pos < text.Length && char.IsDigit(text[pos]))
                    {
                        lexeme += text[pos];
                        col++; 
                        pos++;
                    }
                    TokenType tokenType = TokenType.Error;
                    if (lexeme == "-" || (lexeme.StartsWith("-") && lexeme.Length == 1))
                    {
                        tokens.Add(new Token
                        {
                            Type = tokenType,
                            Value = lexeme,
                            Line = line,
                            StartPos = startCol,
                            EndPos = col - 1,
                            AbsoluteIndex = startPos
                        });
                    }
                    else
                    {
                        if (cond_keyword == false && cond_colon == 2)
                        {
                            tokenType = TokenType.IntDigit;
                        }
                        tokens.Add(new Token
                        {
                            Type = tokenType,
                            Value = lexeme,
                            Line = line,
                            StartPos = startCol,
                            EndPos = col - 1,
                            AbsoluteIndex = startPos
                        });
                    }
                    continue;
                }
                tokens.Add(new Token
                {
                    Type = TokenType.Error,
                    Value = text[pos].ToString(),
                    Line = line,
                    StartPos = startCol,
                    EndPos = col,
                    AbsoluteIndex = startPos
                });
                pos++; 
                col++;
            }
            return tokens;
        }
        public void Run(DataGridView d, RichTextBox r)
        {
            d.DataSource = Analyze(r.Text);
            d.Columns["Code"].HeaderText = "Условный код";
            d.Columns["Value"].HeaderText = "Лексема";
            d.Columns["TypeName"].HeaderText = "Тип лексемы";
            d.Columns["Location"].HeaderText = "Местоположение";
            d.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace lab1_gui
{
    public class ParseError : Exception
    {
        private int _idx;
        public int Idx
        {
            get { return _idx; }
        }

        private string incorrStr;
        public string IncorrStr
        {
            get { return incorrStr; }
        }

        public int Line { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }

        public ParseError(string msg, string rem, int index, int line = 0, int startPos = 0, int endPos = 0) : base(msg)
        {
            incorrStr = rem;
            _idx = index;
            Line = line;
            StartPos = startPos;
            EndPos = endPos;
        }
    }

    public class Parser
    {
        private int state;
        private List<Token> tokens;
        private int currentTokenIndex;

        private List<ParseError> errors;

        public List<ParseError> GetErrors()
        {
            return errors;
        }

        public void Parse(List<Token> tokenList)
        {
            tokens = tokenList;
            state = 1;
            currentTokenIndex = 0;

            errors = new List<ParseError>();
            if (tokens.Count > 0)
            {
                while (state != 10 && currentTokenIndex < tokens.Count)
                {
                    SkipWhiteSpace();
                    switch (state)
                    {
                        case 1:
                            state1();
                            break;
                        case 2:
                            state2();
                            break;
                        case 3:
                            state3();
                            break;
                        case 4:
                            state4();
                            break;
                        case 5:
                            state5();
                            break;
                        case 6:
                            state6();
                            break;
                        case 7:
                            state7();
                            break;
                        case 8:
                            state8();
                            break;
                    }
                }
                if (errors.Count == 0)
                {
                    ValidateEndOfInput();
                }
            }
            else
            {
                MessageBox.Show("Введите текст.", "Предупреждение", MessageBoxButtons.OK);
            }
        }

        private Token GetCurrentToken()
        {
            if (currentTokenIndex < tokens.Count)
                return tokens[currentTokenIndex];
            return null;
        }

        private void SkipWhiteSpace()
        {
            while (currentTokenIndex < tokens.Count &&
                   tokens[currentTokenIndex].Type == TokenType.WhiteSpace)
            {
                currentTokenIndex++;
            }
        }

        private void handleError(string eMess, string removed, Token token)
        {
            int line = -1;
            int startPos = -1;
            int endPos = -1;
            int index = -1;

            if (token != null)
            {
                line = token.Line;
                startPos = token.StartPos;
                endPos = token.EndPos;
                index = token.AbsoluteIndex;
            }
            else
            {
                Token currentToken = GetCurrentToken();
                if (currentToken != null)
                {
                    line = currentToken.Line;
                    startPos = currentToken.StartPos;
                    endPos = currentToken.EndPos;
                    index = currentToken.AbsoluteIndex;
                }
                else if (tokens.Count > 0)
                {
                    Token lastToken = tokens[tokens.Count - 1];
                    line = lastToken.Line;
                    startPos = lastToken.EndPos + 1;
                    endPos = lastToken.EndPos + 1;
                    index = lastToken.EndPos + 1;
                }
            }

            errors.Add(new ParseError(eMess, removed, index, line, startPos, endPos));
        }

        private void state1()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();
            if (token != null && token.Type == TokenType.Const)
            {
                currentTokenIndex++;
                state = 2;
            }
            else
            {
                handleError("Ожидается ключевое слово 'const'.", token != null ? token.Value : null, token);
                while (currentTokenIndex < tokens.Count)
                {
                    if (token != null && token.Type == TokenType.EndOperator)
                    {
                        state = 8;
                        return;
                    }
                    currentTokenIndex++;
                    token = GetCurrentToken();
                    if (token != null && (token.Type == TokenType.Id || token.Type == TokenType.IntDigit))
                    {
                        state = 2;
                        return;
                    }
                }
            }
        }

        private void state2()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();
            if (token != null && token.Type == TokenType.Id)
            {
                currentTokenIndex++;
                state = 3;
            }
            else
            {
                handleError("Ожидается идентификатор.", token != null ? token.Value : null, token);
                while (currentTokenIndex < tokens.Count)
                {
                    if (token != null && token.Type == TokenType.Id)
                    {
                        state = 2;
                        return;
                    }
                    if (token != null && token.Type == TokenType.Colon)
                    {
                        state = 3;
                        return;
                    }
                    if (token != null && token.Type == TokenType.EndOperator)
                    {
                        state = 8;
                        return;
                    }
                    currentTokenIndex++;
                    token = GetCurrentToken();
                }
            }
        }

        private void state3()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();
            if (token != null && token.Type == TokenType.Colon)
            {
                currentTokenIndex++;
                state = 4;
            }
            else
            {
                handleError("Ожидается двоеточие ':'.", token != null ? token.Value : null, token);
                while (currentTokenIndex < tokens.Count)
                {
                    if (token != null && token.Type == TokenType.Integer)
                    {
                        state = 4;
                        return;
                    }
                    if (token != null && token.Type == TokenType.Id)
                    {
                        state = 4;
                        return;
                    }
                    if (token != null && token.Type == TokenType.EndOperator)
                    {
                        state = 8;
                        return;
                    }
                    currentTokenIndex++;
                    token = GetCurrentToken();
                }
            }
        }

        private void state4()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();

            if (token != null && token.Type == TokenType.Integer)
            {
                currentTokenIndex++;
                state = 5;
            }
            else
            {
                handleError("Ожидается ключевое слово 'integer'.", token != null ? token.Value : null, token);
                while (currentTokenIndex < tokens.Count)
                {
                    if (token != null && token.Type == TokenType.Equal)
                    {
                        state = 5;
                        return;
                    }
                    if (token != null && token.Type == TokenType.EndOperator)
                    {
                        state = 8;
                        return;
                    }
                    currentTokenIndex++;
                    token = GetCurrentToken();
                    if (token != null && token.Type == TokenType.Id)
                    {
                        state = 5;
                        return;
                    }
                }
            }
        }

        private void state5()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();
            if (token != null && token.Type == TokenType.Equal)
            {
                currentTokenIndex++;
                state = 6;
            }
            else
            {
                handleError("Ожидается знак '='.", token != null ? token.Value : null, token);
                while (currentTokenIndex < tokens.Count)
                {
                    if (token != null && (token.Type == TokenType.Minus || token.Type == TokenType.Plus))
                    {
                        state = 6;
                        return;
                    }
                    if (token != null && token.Type == TokenType.IntDigit)
                    {
                        state = 7;
                        return;
                    }
                    if (token != null && token.Type == TokenType.EndOperator)
                    {
                        state = 8;
                        return;
                    }
                    currentTokenIndex++;
                    token = GetCurrentToken();
                    if (token != null && token.Type == TokenType.Id)
                    {
                        state = 6;
                        return;
                    }
                }
            }
        }

        private void state6()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();
            if (token != null && (token.Type == TokenType.Minus || token.Type == TokenType.Plus))
            {
                currentTokenIndex++;
                state = 7;
            }
            else
            {
                state = 7;
            }
        }

        private void state7()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();
            if (token != null && (token.Type == TokenType.IntDigit))
            {
                currentTokenIndex++;
                state = 8;
            }
            else
            {
                handleError("Ожидается целое число.", token != null ? token.Value : null, token);
                while (currentTokenIndex < tokens.Count)
                {
                    if (token != null && (token.Type == TokenType.EndOperator))
                    {
                        state = 8;
                        return;
                    }
                    currentTokenIndex++;
                    token = GetCurrentToken();
                }
            }
        }

        private void state8()
        {
            SkipWhiteSpace();
            Token token = GetCurrentToken();
            if (token != null && token.Type == TokenType.EndOperator)
            {
                currentTokenIndex++;
                if (currentTokenIndex >= tokens.Count) state = 9;
                else state = 1;
            }
            else
            {
                Token prevToken = currentTokenIndex > 0 ? tokens[currentTokenIndex - 1] : null;
                handleError("Ожидается знак ';'.", prevToken != null ? prevToken.Value : null, prevToken);
                while (currentTokenIndex < tokens.Count)
                {
                    if (token != null && (token.Type == TokenType.Const || token.Type == TokenType.Id))
                    {
                        state = 1;
                        return;
                    }
                    currentTokenIndex++;
                    token = GetCurrentToken();
                }
            }
        }

        private void ValidateEndOfInput()
        {
            SkipWhiteSpace();
            if (state != 9)
            {
                Token lastToken = tokens.Count > 0 ? tokens[tokens.Count - 1] : null;
                handleError("Ожидается знак ';'.", lastToken != null ? lastToken.Value : null, lastToken);
            }
        }

        public void Display(DataGridView grid, Label label, RichTextBox editor)
        {
            grid.Columns.Clear();
            grid.Rows.Clear();
            label.Text = "";

            if (errors.Count > 0)
            {
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.ReadOnly = true;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.MultiSelect = false;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grid.RowHeadersVisible = false;

                grid.Columns.Add("ErrorFragment", "Неверный фрагмент");
                grid.Columns.Add("Location", "Местоположение");
                grid.Columns.Add("Description", "Описание ошибки");

                grid.Columns["ErrorFragment"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                grid.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                grid.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                grid.CellClick -= (s, e) => OnErrorCellClick(s, e, editor);
                grid.CellClick += (s, e) => OnErrorCellClick(s, e, editor);

                foreach (ParseError error in errors)
                {
                    string location;
                    if (error.StartPos == error.EndPos)
                        location = $"строка: {error.Line}, позиция: {error.StartPos}";
                    else
                        location = $"строка: {error.Line}, позиция: {error.StartPos}-{error.EndPos}";

                    grid.Rows.Add(error.IncorrStr ?? "", location, error.Message);
                }
                label.Text = "Всего ошибок: " + errors.Count.ToString();
            }
            else
            {
                MessageBox.Show("Ошибок нет!", "Результат", MessageBoxButtons.OK);
            }
        }

        private void OnErrorCellClick(object sender, DataGridViewCellEventArgs e, RichTextBox editor)
        {
            if (e.RowIndex < 0 || e.RowIndex >= errors.Count)
                return;

            ParseError error = errors[e.RowIndex];
            Token errorToken = GetTokenAtPosition(error.StartPos);
            if (errorToken != null)
            {
                editor.SelectionStart = errorToken.AbsoluteIndex;
                editor.SelectionLength = errorToken.Value?.Length ?? 0;
                editor.ScrollToCaret();
                editor.Focus();
                editor.SelectionBackColor = Color.LightPink;

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 2000;
                timer.Tick += (s, args) =>
                {
                    editor.SelectionStart = 0;
                    editor.SelectionLength = 0;
                    editor.SelectionBackColor = Color.White;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
            else if (error.Idx >= 0 && error.Idx < editor.Text.Length)
            {
                editor.SelectionStart = error.Idx;
                editor.SelectionLength = 1;
                editor.ScrollToCaret();
                editor.Focus();
                editor.SelectionBackColor = Color.LightPink;

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 2000;
                timer.Tick += (s, args) =>
                {
                    editor.SelectionStart = 0;
                    editor.SelectionLength = 0;
                    editor.SelectionBackColor = Color.White;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private Token GetTokenAtPosition(int position)
        {
            if (tokens == null || tokens.Count == 0)
                return null;
            foreach (Token token in tokens)
            {
                if (token.StartPos <= position && position <= token.EndPos)
                {
                    return token;
                }
            }
            Token nearestToken = null;
            int minDistance = int.MaxValue;

            foreach (Token token in tokens)
            {
                int distance = Math.Abs(token.StartPos - position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestToken = token;
                }
            }

            return nearestToken;
        }
    }
}
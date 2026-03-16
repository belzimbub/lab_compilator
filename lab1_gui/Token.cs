using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_gui
{
    public class Token
    {
        public int Code
        {
            get
            {
                return (int)Type;
            }
        }
        public string Value { get; set; }
        [Browsable(false)]
        public int Line { get; set; }
        [Browsable(false)]
        public int StartPos { get; set; }
        [Browsable(false)]
        public int EndPos { get; set; }
        [Browsable(false)]
        public int AbsoluteIndex { get; set; }
        [Browsable(false)]
        public TokenType Type { get; set; }
        public string TypeName => Type switch
        {
            TokenType.Id => "Идентификатор",
            TokenType.Colon => "Оператор объявления",
            TokenType.IntDigit => "Порядковое число",
            TokenType.WhiteSpace => "Разделитель (пробел)",
            TokenType.EndOperator => "Конец оператора",
            TokenType.Equal => "Оператор присваивания",
            TokenType.Error => "Лексическая ошибка",
            TokenType.Const => "Ключевое слово const",
            TokenType.Integer => "Ключевое слово integer",

        };

        public string Location => $"строка: {Line}, {StartPos}-{EndPos}";
    }
}

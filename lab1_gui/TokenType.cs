using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_gui
{
    public enum TokenType
    {
        Const = 1,
        Integer = 2,
        Id = 3,
        WhiteSpace = 4,
        Colon = 5,
        IntDigit = 6,
        EndOperator = 7,
        Equal = 8,
        Error = 0
    }
}

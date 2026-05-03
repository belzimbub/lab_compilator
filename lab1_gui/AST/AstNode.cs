using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_gui
{
    public abstract class AstNode
    {
        public abstract string GetNodeType();
        public virtual List<AstNode> GetChildren() => new List<AstNode>();
        public virtual List<(string key, string value)> GetAttributes() => new List<(string, string)>();
    }

    public class ProgramNode : AstNode
    {
        public List<ConstDeclNode> Constants { get; set; } = new List<ConstDeclNode>();

        public override string GetNodeType() => "ProgramNode";

        public override List<AstNode> GetChildren() => Constants.Cast<AstNode>().ToList();

        public override List<(string key, string value)> GetAttributes()
        {
            return new List<(string, string)> { ("constantCount", Constants.Count.ToString()) };
        }
    }

    public class ConstDeclNode : AstNode
    {
        public string Name { get; set; }
        public string Modifier { get; set; } = "const";
        public IntNode Type { get; set; }
        public IntLiteralNode Value { get; set; }
        public int Line { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }

        public override string GetNodeType() => "ConstDeclNode";

        public override List<AstNode> GetChildren() => new List<AstNode> { Type, Value }.Where(n => n != null).ToList();

        public override List<(string key, string value)> GetAttributes()
        {
            var attrs = new List<(string, string)>();
            attrs.Add(("name", $"\"{Name}\""));
            attrs.Add(("modifier", Modifier));
            return attrs;
        }
    }

    public class IntNode : AstNode
    {
        public string Name { get; set; } = "integer";

        public override string GetNodeType() => "IntNode";

        public override List<(string key, string value)> GetAttributes()
        {
            return new List<(string, string)> { ("name", $"\"{Name}\"") };
        }
    }

    public class IntLiteralNode : AstNode
    {
        public int Value { get; set; }

        public override string GetNodeType() => "IntLiteralNode";

        public override List<(string key, string value)> GetAttributes()
        {
            return new List<(string, string)> { ("value", Value.ToString()) };
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_gui
{
    public partial class AstForm : Form
    {
        public AstForm(ProgramNode ast)
        {
            InitializeComponent();
            BuildRawText(ast);
        }
        private void BuildRawText(ProgramNode ast)
        {
            StringBuilder sb = new StringBuilder();
            PrintNode(ast, sb, "", true);
            richTextBox1.Text = sb.ToString();
        }

        private void PrintNode(AstNode node, StringBuilder sb, string indent, bool isLast)
        {
            sb.Append(indent);

            if (isLast)
            {
                sb.Append("└── ");
                indent += "    ";
            }
            else
            {
                sb.Append("├── ");
                indent += "│   ";
            }

            sb.AppendLine($"[{node.GetNodeType()}]");

            var attributes = node.GetAttributes();
            for (int i = 0; i < attributes.Count; i++)
            {
                var attr = attributes[i];
                bool isLastAttr = (i == attributes.Count - 1) && node.GetChildren().Count == 0;

                sb.Append(indent);
                if (isLastAttr)
                    sb.Append("└── ");
                else
                    sb.Append("├── ");

                sb.AppendLine($"{attr.key}: {attr.value}");
            }

            var children = node.GetChildren();
            for (int i = 0; i < children.Count; i++)
            {
                bool isLastChild = (i == children.Count - 1);
                PrintNode(children[i], sb, indent, isLastChild);
            }
        }
    }
}

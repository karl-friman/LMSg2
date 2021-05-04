using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class HtmlElement
    {
        public string Tag, IdContent, ClassContent, Content;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            Tag = name;
            IdContent = IdContent;
            ClassContent = ClassContent;
            Content = text;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.Append($"{i}<{Tag}>\n");
            if (!string.IsNullOrWhiteSpace(Content))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append(Content);
                sb.Append("\n");
            }

            foreach (var e in Elements)
                sb.Append(e.ToStringImpl(indent + 1));

            sb.Append($"{i}</{Tag}>\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        public static HtmlBuilder Create(string tag, string idContent, string classContent) => new HtmlBuilder(tag, idContent, classContent);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class HtmlBuilder
    {
        private readonly string tag;
        protected HtmlElement root = new HtmlElement();

        public HtmlBuilder(string tag, string idContent, string classContent)
        {
            this.tag = tag;
            root.Tag = tag;
            root.IdContent = idContent;
            root.ClassContent = classContent;
        }

        // not fluent
        public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
        }

        public HtmlBuilder AddChildFluent(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString() => root.ToString();

        public void Clear()
        {
            root = new HtmlElement { Tag = tag };
        }

        public HtmlElement Build() => root;

        //public static implicit operator HtmlElement(HtmlBuilder builder)
        //{
        //    return builder.root;
        //}
    }
}

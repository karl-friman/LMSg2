using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
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

        //fluent
        public HtmlBuilder AddChildFluent(string tagName, string idContent, string classContent, string innerContent)
        {
            var e = new HtmlElement(tagName, idContent, classContent, innerContent);
            root.Elements.Add(e);
            return this;
        }

        public HtmlBuilder AddChildFluent(string tagName, string idContent, string classContent, IHtmlContent innerContent)
        {
            var e = new HtmlElement(tagName, idContent, classContent, GetString(innerContent));
            root.Elements.Add(e);
            return this;
        }

        //public HtmlBuilder AddChildActionLink(IHtmlContent asdf)
        //{
        //    var e = new HtmlElement(IHtmlContent asdf.WriteTo());
        //    root.Elements.Add(e);
        //    return this;
        //}

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
        public static string GetString(IHtmlContent content)
        {
            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}

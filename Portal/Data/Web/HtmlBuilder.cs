using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Portal.Data.Web {

    /// <summary>
    /// Wrapper around an HtmlTextWriter, which is easier to use, but not everything is implemented.
    /// </summary>
    public sealed class HtmlBuilder {

        /// <summary>
        /// Actual writer.
        /// </summary>
        internal HtmlTextWriter HTML { get; }

        /// <summary>
        /// Stores tags, so end tags do not need to be specified.
        /// </summary>
        internal Stack<string> TagStack { get; }

        /// <summary>
        /// Writer that the HtmlTextWriter writes to.
        /// </summary>
        private StringWriter StringWriter { get; }

        /// <summary>
        /// Create a new HtmlBuilder.
        /// </summary>
        public HtmlBuilder() {
            StringWriter = new StringWriter();
            HTML = new HtmlTextWriter(StringWriter, "    ");
            TagStack = new Stack<string>();
        }

        /// <summary>
        /// Start defining an HTML start tag.
        /// </summary>
        public HtmlTagBuilder Tag(string tag) {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException("Tag");
            TagStack.Push(tag);
            HTML.WriteLine();
            HTML.WriteBeginTag(tag);
            return new HtmlTagBuilder(this, tag);
        }

        /// <summary>
        /// Write an HTML end tag.
        /// </summary>
        public HtmlBuilder End() {
            HTML.Indent--;
            HTML.WriteLine();
            HTML.WriteEndTag(TagStack.Pop());
            return this;
        }

        /// <summary>
        /// Closes all unclosed tags, and returns the completed HTML string.
        /// </summary>
        public string Finish() {
            while (TagStack.Count > 0) {
                End();
            }
            return StringWriter.ToString();
        }

    }

}

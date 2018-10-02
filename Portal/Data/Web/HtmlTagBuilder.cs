using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Portal.Data.Web {

    /// <summary>
    /// Define an HTML start tag.
    /// </summary>
    public sealed class HtmlTagBuilder {

        /// <summary>
        /// HTML Tag name.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// HtmlBuilder.
        /// </summary>
        private HtmlBuilder Builder { get; }

        /// <summary>
        /// Create a new HtmlTagBuilder with a reference to the actual HtmlBuilder.
        /// </summary>
        internal HtmlTagBuilder(HtmlBuilder Builder, string Tag) {
            this.Builder = Builder;
            this.Tag = Tag;
        }

        /// <summary>
        /// Adds an attribute to the HTML start tag.
        /// </summary>
        public HtmlTagBuilder Attribute(string key, string value) {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("Key");
            if (value == null)
                throw new ArgumentNullException("Value");
            Builder.HTML.WriteAttribute(key, value);
            return this;
        }

        /// <summary>
        /// Finish the HTML start tag.
        /// </summary>
        public HtmlBuilder Start() {
            Builder.HTML.Write(HtmlTextWriter.TagRightChar);
            Builder.HTML.Indent++;
            return Builder;
        }

    }

}

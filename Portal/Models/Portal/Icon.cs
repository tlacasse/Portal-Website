using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Models.Portal {

    /// <summary>
    /// Model representing the clickable icon bringing you to the link.
    /// </summary>
    public class Icon {

        /// <summary>
        /// Database Record Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Short name describing the link.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Icon image extension.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Url.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// DateTime when the icon was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// DateTime when the icon was last changed.
        /// </summary>
        public DateTime DateChanged { get; set; }

    }

}

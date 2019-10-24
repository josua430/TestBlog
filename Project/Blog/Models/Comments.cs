using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    /// <summary>
    /// Comment class
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// id of comment
        /// </summary>
        public int IdCommnent { get; set; }

        /// <summary>
        /// Text of comment
        /// </summary>
        public string strText { get; set; }

        /// <summary>
        /// Author of comment
        /// </summary>
        public string strAuthor { get; set; }

        /// <summary>
        /// Email of author
        /// </summary>
        public string strEmail { get; set; }

        /// <summary>
        /// timestamp of comment
        /// </summary>
        public DateTime dtChange { get; set; }

        /// <summary>
        /// timestamp of comment in string
        /// </summary>
        public string strChange { get; set; }

        /// <summary>
        /// indicate the post id for comment
        /// </summary>
        public int intPostId { get; set; }
    }
}
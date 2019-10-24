using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    /// <summary>
    /// Post class
    /// </summary>
    public class Post
    {
        /// <summary>
        /// id of post
        /// </summary>
        public int IdPost { get; set; }

        /// <summary>
        /// Title of post
        /// </summary>
        public string strTitle { get; set; }

        /// <summary>
        /// Text of post
        /// </summary>
        public string strText { get; set; }

        /// <summary>
        /// Author of post
        /// </summary>
        public string strAuthor { get; set; }

        /// <summary>
        /// timestamp of post
        /// </summary>
        public DateTime dtChange { get; set; }

        /// <summary>
        /// timestamp of post in string
        /// </summary>
        public string strChange { get; set; }

        /// <summary>
        /// indicate if the post is published
        /// </summary>
        public Boolean blnPublished { get; set; }
    }
}
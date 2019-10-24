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
        /// indicate if the writer sended the post to approval
        /// </summary>
        public bool blnStatusToPublish { get; set; }

        /// <summary>
        /// indicate if the button for edit are showed
        /// </summary>
        public string strShowEditButton { get; set; }

        /// <summary>
        /// indicate if the button for delete are showed
        /// </summary>
        public string strShowDeleteButton { get; set; }

        /// <summary>
        /// indicate if the buttons for comments are showed, for status published
        /// </summary>
        public string strShowCommentButton { get; set; }

        /// <summary>
        /// indicate if the post is created (0), pending (1) or published (2)
        /// </summary>
        public string strStatusPublished { get; set; }

        /// <summary>
        /// timestamp of approval
        /// </summary>
        public DateTime dtApproval { get; set; }

        /// <summary>
        /// timestamp of approval in string
        /// </summary>
        public string strApproval { get; set; }

        /// <summary>
        /// timestamp of approval in string
        /// </summary>
        public string strApproverName { get; set; }
    }
}
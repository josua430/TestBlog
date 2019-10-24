using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Helpers
{
    /// <summary>
    /// Interface to manage data for a Post
    /// </summary>
    public interface IDataBaseComments
    {

        /// <summary>
        /// Return a list of comments for a post
        /// </summary>
        /// <param name="intIdPost">Id of post</param>
        /// <returns></returns>
        List<Models.Comments> ListComments(int intIdPost);

        /// <summary>
        /// Insert method
        /// </summary>
        /// <param name="objComment">Object with all data to save</param>
        void Insert(Models.Comments objComment);

    }
}

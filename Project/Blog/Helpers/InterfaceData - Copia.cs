using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Helpers
{
    /// <summary>
    /// Interface to manage data
    /// </summary>
    public interface IDataBase
    {

        /// <summary>
        /// Method to validate if element exists by Id
        /// </summary>
        /// <param name="strId">Element id</param>
        /// <returns></returns>
        bool ExistsId(string strId);

        /// <summary>
        /// Method to validate if element exists by title
        /// </summary>
        /// <param name="strTitle">Element title</param>
        /// <returns></returns>
        bool ExistsTitle(string strTitle);

        /// <summary>
        /// Method to validate if element exists by title and by Id for update purpose
        /// </summary>
        /// <param name="strId">Element id</param>
        /// <param name="strTitle">Element title</param>
        /// <returns></returns>
        bool ExistsTitleAndIdForUpdate(string strId, string strTitle);

        /// <summary>
        /// Return a list of posts
        /// </summary>
        /// <param name="blnAuthenticated">Param to identify if the user is authenticated</param>
        /// <returns></returns>
        List<Models.Post> ListPosts(bool blnAuthenticated);

        /// <summary>
        /// delete method
        /// </summary>
        /// <param name="strId">Element Id</param>
        void Delete(string strId);

        /// <summary>
        /// Insert method
        /// </summary>
        /// <param name="objPost">Object with all data to save</param>
        void Insert(Models.Post objPost);

        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="objPost">Object with all data to save</param>
        void Update(Models.Post objPost);

    }
}

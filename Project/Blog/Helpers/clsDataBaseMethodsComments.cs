using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Helpers
{

    /// <summary>
    /// class to execute data operations for comments
    /// </summary>
    public class clsDataBaseMethodsComments : IDataBaseComments
    {

        /// <summary>
        /// Insert method
        /// </summary>
        /// <param name="objComment">Object with all data to save</param>
        public List<Comments> ListComments(int intIdPost)
        {
            //Lista a retornar o mostrar en la grilla
            var Lista = new List<Models.Comments>();

            using (var context = new Entity.blog_dbEntities())
            {
                //Consulta a la tabla
                List<Entity.comments> lstComments = context.comments.OrderBy(d => d.comment_timestamp).ToList();

                foreach (var item in lstComments)
                {
                    //Se agregan a la lista
                    Lista.Add(new Models.Comments
                    {
                        strEmail = item.comment_email,
                        strText = item.comment_text,
                        strAuthor = item.comment_author,
                        strChange = ((DateTime)item.comment_timestamp).ToString("yyyy-MM-dd hh:mm"),
                        IdComment = (int)item.comment_id
                    });
                }
                return Lista;
            }
        }
        
        /// <summary>
        /// Return a list of comments for a post
        /// </summary>
        /// <param name="intIdPost">Id of post</param>
        /// <returns></returns>
        public void Insert(Comments objComment)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                //create model and save to data base
                var model = new Entity.comments();
                model.comment_email = objComment.strEmail;
                model.comment_text = objComment.strText;
                model.comment_author = objComment.strAuthor;
                model.comment_timestamp = DateTime.Now;
                model.comment_post_id = objComment.intPostId;
                context.comments.Add(model);
                context.SaveChanges();
            }
        }

    }
}
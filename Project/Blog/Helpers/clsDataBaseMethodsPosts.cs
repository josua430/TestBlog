using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Helpers
{

    /// <summary>
    /// class to execute data operations for posts
    /// </summary>
    public class clsDataBaseMethodsPosts : IDataBase
    {

        /// <summary>
        /// validate if the post exists by Id
        /// </summary>
        /// <param name="strId">Id of post</param>
        /// <returns>bool</returns>
        public bool ExistsId(string strId)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                int idElemento = int.Parse(strId);
                var objPost = context.posts.FirstOrDefault(p => p.post_id == idElemento);

                if (objPost == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }
        }

        /// <summary>
        /// validate if the post exists by title
        /// </summary>
        /// <param name="strTitle">title of post</param>
        /// <returns>bool</returns>
        public bool ExistsTitle(string strTitle)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                //Se valida si ya existe
                var objPost = context.posts.FirstOrDefault(d => d.post_title.ToUpper().Trim() == strTitle.ToUpper().Trim());
                if (objPost == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }
        }

        /// <summary>
        /// validate if the post exists by title
        /// </summary>
        /// <param name="strId">Id of post</param>
        /// <param name="strTitle">title of post</param>
        /// <returns>bool</returns>
        public bool ExistsTitleAndIdForUpdate(string strId, string strTitle)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                //Se valida si ya existe
                //validacion si ya existe un elemento con el mismo nombre
                var validate = context.posts.FirstOrDefault(t => t.post_title.ToUpper().Trim() == strTitle.ToUpper().Trim());
                if (validate != null)
                {
                    if (strId != validate.post_id.ToString())
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Return a list of posts
        /// </summary>
        /// <param name="blnAuthenticated">Param to identify if the user is authenticated</param>
        /// <returns></returns>
        public List<Post> ListPosts(bool blnAuthenticated)
        {
            //Lista a retornar o mostrar en la grilla
            var Lista = new List<Models.Post>();

            using (var context = new Entity.blog_dbEntities())
            {
                //Consulta a la tabla
                List<Entity.posts> lstPosts;
                if (blnAuthenticated)
                {
                    lstPosts = context.posts.Where(a => a.post_published == true).OrderBy(d => d.post_title).ToList();
                }
                else
                {
                    lstPosts = context.posts.OrderBy(d => d.post_title).ToList();
                }

                foreach (var item in lstPosts)
                {
                    //Se agregan a la lista
                    Lista.Add(new Models.Post
                    {
                        strTitle = item.post_title,
                        strText = item.post_text,
                        strAuthor = item.post_author,
                        strChange = ((DateTime)item.post_change).ToString("yyyy-MM-dd hh:mm"),
                        blnPublished = (bool)item.post_published,
                        IdPost = (int)item.post_id
                    });
                }
                return Lista;
            }
        }

        /// <summary>
        /// delete method for posts
        /// </summary>
        /// <param name="strId"></param>
        public void Delete(string strId)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                int idElemento = int.Parse(strId);
                var objPost = context.posts.FirstOrDefault(p => p.post_id == idElemento);

                if (objPost == null)
                {
                    return;
                }

                context.posts.Remove(objPost);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Method to insert new posts
        /// </summary>
        /// <param name="objPost">Element with all data to save</param>
        public void Insert(Models.Post objPost)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                //create model and save to data base
                var model = new Entity.posts();
                model.post_title = objPost.strTitle;
                model.post_text = objPost.strText;
                model.post_author = objPost.strAuthor;
                model.post_change = DateTime.Now;
                model.post_published = objPost.blnPublished;
                context.posts.Add(model);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method to update a post
        /// </summary>
        /// <param name="objPost">Element with all data to save</param>
        public void Update(Models.Post objPost)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                //elemento a actualizar
                var update = context.posts.FirstOrDefault(t => t.post_id == objPost.IdPost);

                //Se actualiza
                update.post_title = objPost.strTitle.Trim();
                update.post_text = objPost.strText.Trim();
                update.post_author = objPost.strAuthor.Trim();
                update.post_change = DateTime.Now;
                update.post_published = objPost.blnPublished;
                context.SaveChanges();
            }
        }
    }
}
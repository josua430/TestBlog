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
                var validate = context.posts.FirstOrDefault(t => t.post_title.ToUpper().Trim() == strTitle.ToUpper().Trim()
                && t.post_id.ToString() != strId);
                if (validate == null)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Return a list of posts
        /// </summary>
        /// <param name="blnAuthenticated">Param to identify if the user is authenticated</param>
        /// <returns></returns>
        public List<Post> ListPosts(bool blnAuthenticated, string strProfile)
        {
            //Lista a retornar o mostrar en la grilla
            var Lista = new List<Models.Post>();

            using (var context = new Entity.blog_dbEntities())
            {
                //Consulta a la tabla
                List<Entity.posts> lstPosts;
                if (!blnAuthenticated)
                {
                    lstPosts = context.posts.Where(a => a.post_status_published == 2).OrderBy(d => d.post_title).ToList();
                }
                else
                {
                    if (strProfile == "2")
                    {
                        lstPosts = context.posts.Where(a => a.post_status_published == 1).OrderBy(d => d.post_title).ToList();
                    }
                    else
                    {
                        lstPosts = context.posts.OrderBy(d => d.post_title).ToList();
                    }
                }

                foreach (Entity.posts item in lstPosts)
                {
                    string strStatusName;
                    switch ((int)item.post_status_published)
                    {
                        case 1:
                            strStatusName = "Pending";
                            break;
                        case 2:
                            strStatusName = "Published";
                            break;
                        default:
                            strStatusName = "Created";
                            break;
                    }

                    bool blnEditButton = false;
                    if ((item.post_status_published != 1 && strProfile == "1") || (item.post_status_published == 1 && strProfile == "2"))
                    {
                        blnEditButton = true;
                    }

                    //Se agregan a la lista
                    Lista.Add(new Models.Post
                    {
                        strTitle = item.post_title,
                        strText = item.post_text,
                        strAuthor = item.post_author,
                        strChange = ((DateTime)item.post_change).ToString("yyyy-MM-dd hh:mm"),
                        strApproval = item.post_approval == null ? "" : ((DateTime)item.post_approval).ToString("yyyy-MM-dd hh:mm"),
                        strApproverName = item.post_approval_name,
                        strStatusPublished = strStatusName,
                        strShowEditButton = blnEditButton==false ? "disabled" : "",
                        strShowDeleteButton = item.post_status_published != 2 ? "disabled" : "",
                        strShowCommentButton = item.post_status_published != 2 ? "disabled" : "",
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
                model.post_status_published = objPost.blnStatusToPublish == true ? 1 : 0;
                context.posts.Add(model);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method to update a post
        /// </summary>
        /// <param name="objPost">Element with all data to save</param>
        /// <param name="strProfile">Profile of user. 1= writer, 2=editor</param>
        /// <param name="strName">User name</param>
        public void Update(Models.Post objPost, string strProfile, string strName)
        {
            using (var context = new Entity.blog_dbEntities())
            {
                //elemento a actualizar
                var update = context.posts.FirstOrDefault(t => t.post_id == objPost.IdPost);

                //Se actualiza
                if (strProfile == "1")
                {
                    //writer profile
                    update.post_title = objPost.strTitle.Trim();
                    update.post_text = objPost.strText.Trim();
                    update.post_author = objPost.strAuthor.Trim();
                    update.post_change = DateTime.Now;
                    update.post_status_published = objPost.blnStatusToPublish == true ? 1 : 0;
                }else
                {
                    //editor profile
                    update.post_status_published = objPost.blnStatusToPublish == true ? 2 : 0;
                    if (objPost.blnStatusToPublish == true)
                    {
                        update.post_approval = DateTime.Now;
                        update.post_approval_name = strName;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
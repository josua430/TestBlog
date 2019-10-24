using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Helpers;

namespace UnitTestProject
{
    /// <summary>
    /// test classes for Posts
    /// </summary>
    [TestClass]
    public class UnitTestPosts
    {
        /// <summary>
        /// Not Exists post id
        /// </summary>
        [TestMethod]
        public void TestMethod_NotExistPost()
        {
            IDataBase objDataBase = new clsDataBaseMethodsPosts();

            Assert.IsFalse(objDataBase.ExistsId("-1"));
        }

        /// <summary>
        /// Exists post id
        /// </summary>
        [TestMethod]
        public void TestMethod_ExistPost()
        {
            IDataBase objDataBase = new clsDataBaseMethodsPosts();

            Assert.IsTrue(objDataBase.ExistsId("1"));
        }

        /// <summary>
        /// Not Exists post id
        /// </summary>
        [TestMethod]
        public void TestMethod_NotExistTitle()
        {
            IDataBase objDataBase = new clsDataBaseMethodsPosts();

            Assert.IsFalse(objDataBase.ExistsTitle("-abcdeft76876889980"));
        }


    }
}

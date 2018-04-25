using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_1.Controllers;

namespace Assignment_1.Tests.Controllers
{
    /// <summary>
    /// Summary description for choppersControllerTest
    /// </summary>
    [TestClass]
    public class choppersControllerTest
    {
        [TestMethod]
        public void IndexViewLoads()
        {
            // arrange
            choppersController controller = new choppersController();

            // act
            var actual = controller.Index();

            // assert
            Assert.IsNotNull(actual);

      
        }
    }
}

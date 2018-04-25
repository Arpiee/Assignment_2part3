using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_1.Controllers;
using Moq;
using Assignment_1.Models;
using System.Linq;
using System.Web.Mvc;

namespace Assignment_1.Tests.Controllers
{
    /// <summary>
    /// Summary description for choppersControllerTest
    /// </summary>
    [TestClass]
    public class choppersControllerTest
    {
        choppersController controller;
        Mock<IMockchoppersRepository> mock;
        List<chopper> choppers;

        [TestInitialize]
        public void TestInitialize()
        {
            // runs automatically before each unit test

            // instantiate the mock object
            mock = new Mock<IMockchoppersRepository>();

            // instantiate the mock choppers  data
            choppers = new List<chopper>
            {
                new chopper {chopperId = 1, Name = "chopper 1"},
                new chopper {chopperId = 2, Name = "chopper 2"},
                new chopper {chopperId = 3, Name = "chopper 3"}

            };
            // bind the date to the mock
            mock.Setup(m => m.choppers).Returns(choppers.AsQueryable());

            // initialize the controller and inject the dependency
            controller = new choppersController(mock.Object);

        }


        [TestMethod]
        public void IndexViewLoads()
        {


            // act
            var actual = controller.Index();

            // assert
            Assert.IsNotNull(actual);


        }
        [TestMethod]

        public void IndexLoadschoppers()
        {
            // act - cast ActionResult to ViewResult, then Model to list of choppers
            var actual = (List<chopper>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(choppers, actual);

        }
        [TestMethod]
        public void DetailsValidchopperId()
        {
            //act
            var actual = (chopper)((ViewResult)controller.Details(1)).Model;

            //assert
            Assert.AreEqual(choppers[0], actual);

        }
        [TestMethod]
        public void DetailsInvalidchopperId()
        {
            // act - expect the Index view to load if no matching chopper
            var actual = (ViewResult)controller.Details(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]

        public void DetailsNochopperId()
        {
            // act
            var actual = (ViewResult)controller.Details(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);

        }
    }
}

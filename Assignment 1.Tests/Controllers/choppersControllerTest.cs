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
        // Get Edit
        [TestMethod]
        public void EditGetValidId()
        {
            // act
            var actual = ((ViewResult)controller.Edit(1)).Model;

            // assert
            Assert.AreEqual(choppers[0], actual);
        }
        [TestMethod]
        public void EditGetInvalidId()
        {
            // act
            var actual = (ViewResult)controller.Edit(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }
        [TestMethod]
        public void EditGetNoId()
        {
            int? id = null;

            // act
            var actual = (ViewResult)controller.Edit(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }
        // post: Edit
        [TestMethod]

        public void EditPostValid()
        {
            // act
            var actual = (RedirectToRouteResult)controller.Edit(choppers[0]);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);

        }
        [TestMethod]
        public void EditPostInvalid()
        {
            // arrange -  manually set model state to invalid
            controller.ModelState.AddModelError("key", "update error");

            // act
            var actual = (ViewResult)controller.Edit(choppers[0]);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        // create
        [TestMethod]
        public void CreateViewLoads()
        {
            // act
            var actual = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }
        [TestMethod]
        public void CreateValid()
        {
            // arrange
            chopper a = new chopper
            {
                Name = "New chopper"
            };

            // act
            var actual = (RedirectToRouteResult)controller.Create(a);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }
        [TestMethod]
        public void CreateInvalid()
        {
            // arrange 
            chopper a = new chopper
            {
                Name = "New chopper"
            };

            controller.ModelState.AddModelError("key", "create error");

            // act
            var actual = (ViewResult)controller.Create(a);

            // assert
            Assert.AreEqual("create", actual.ViewName);
        }

        // Delete
        [TestMethod]
        public void DeleteGetValidId()
        {
            // act
            var actual = ((ViewResult)controller.Delete(1)).Model;

            // assert
            Assert.AreEqual(choppers[0], actual);

        }
        [TestMethod]
        public void DeleteGetInvalidId()
        {
            //act
            var actual = (ViewResult)controller.Delete(4);

            //assert
            Assert.AreEqual("Error", actual.ViewName);
        }
        [TestMethod]
        public void DeleteGetNoId()
        {
            // act
            var actual = (ViewResult)controller.Delete(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }
        [TestMethod]
        public void DeletePostValid()
        {
            // act
            var actual = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }
        
    }
}

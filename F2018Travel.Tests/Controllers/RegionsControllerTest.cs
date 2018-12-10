using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// refs
using F2018Travel.Controllers;
using F2018Travel.Models;
using System.Web.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace F2018Travel.Tests.Controllers
{
    [TestClass]
    public class RegionsControllerTest
    {
        RegionsController controller;
        Mock<IMockRegions> mock;
        List<Region> regions;
        Region region;

        [TestInitialize]
        public void TestInitalize()
        {
            mock = new Mock<IMockRegions>();
            regions = new List<Region>
            {
                new Region { RegionId = 78, Region1 = "Oceania" },
                new Region { RegionId = 349, Region1 = "Eurasia" },
                new Region { RegionId = 205, Region1 = "Eastasia" },
            };

            region = new Region
            {
                RegionId = 489, Region1 = "Disputed"
            };

            mock.Setup(m => m.Regions).Returns(regions.AsQueryable());
            controller = new RegionsController(mock.Object);
        }

        [TestMethod]
        //loads the index view
        public void IndexLoadsView()
        {
            //arrange

            //act
            ViewResult result = controller.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        //Returns a list of all Regions
        public void IndexValidLoadsRegions()
        {
            //act
            var result = (List<Region>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(regions, result);
        }

        [TestMethod]
        //Returns the selected Region
        public void DetailsValidId()
        {
            // act - cast the model as an Album object
            Region actual = (Region)((ViewResult)controller.Details(78)).Model;

            // assert - is this the first mock album in our array?
            Assert.AreEqual(regions[0], actual);
        }

        [TestMethod]
        //Loads the Error view
        public void DetailsInvalidId()
        {
            // act
            var result = (ViewResult)controller.Details(3456);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        //Loads the Error view
        public void DetailsNoId()
        {
            // act
            var result = (ViewResult)controller.Details(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Linq;

namespace RecipeTests
{
    [TestClass]
    public class UnitTest1
    {
        static IWebDriver driverFF;
        static string site_url = ConfigurationManager.AppSettings["site_url"];

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            driverFF = new FirefoxDriver();
        }

        [TestInitialize]
        public void Before()
        {
            driverFF.Navigate().GoToUrl(site_url);
        }

        [TestMethod]
        public void TestSiteUrlFirefox()
        {
            Assert.AreEqual(driverFF.Url, site_url);
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            driverFF.Close();
        }


        //HOMEPAGE
        //tests relating to the homepage
        [TestMethod]
        public void TestRecipeHomepageSections()
        {
            List<string> sections = new List<string>(driverFF.FindElements(By.ClassName("section")).Select(iw => iw.Text));
            List<string> expected_sections = new List<string> { "Recipes", "Recipe Collections" };
            CollectionAssert.AreEqual(sections, expected_sections);
        }

        [TestMethod]
        public void TestSelectRecipeFromHomepage()
        {
            // find the first recipe on the page (need to add some handling for if there are none).
            IWebElement first_recipe = driverFF.FindElement(By.CssSelector(".recipe-link[type='submit']"));
            string first_recipe_name = first_recipe.GetAttribute("value");
            first_recipe.Click();
            string recipe_name = driverFF.FindElement(By.Id("recipe-name")).Text;
            Assert.AreEqual(first_recipe_name, recipe_name);
        }

        [TestMethod]
        public void TestSelectRecipeCollectionFromHomepage()
        {
            // find the first recipe collection on the page (need to add some handling for if there are none).
            IWebElement first_collection = driverFF.FindElement(By.CssSelector(".recipe-collection-link[type='submit']"));
            string first_collection_name = first_collection.GetAttribute("value");
            first_collection.Click();
            string collection_name = driverFF.FindElement(By.Id("collection-name")).Text;
            Assert.AreEqual(first_collection_name, collection_name);
        }


        //RECIPES
        //tests relating to the individual recipe pages

        [TestMethod]
        public void TestRecipeSectionsVisible()
        {
            //navigate to recipe page
            driverFF.FindElement(By.CssSelector(".recipe-link[type='submit']")).Click();
            //test that we can see Ingredients and Recipes for the recipe.
            List<string> sections = new List<string>(driverFF.FindElements(By.ClassName("heading")).Select(iw => iw.Text));
            List<string> expected_sections = new List<string> { "Ingredients", "Method" };
            CollectionAssert.AreEqual(sections, expected_sections);
        }

        [TestMethod]
        public void TestRecipeIngredientsVisible()
        {
            //navigate to recipe page
            driverFF.FindElement(By.CssSelector(".recipe-link[type='submit']")).Click();
            //check that the ingredients table is displayed
            Assert.IsNotNull(driverFF.FindElement(By.Id("ingredients")));
        }

        [TestMethod]
        public void TestRecipeMethodVisible()
        {
            //navigate to recipe page
            driverFF.FindElement(By.CssSelector(".recipe-link[type='submit']")).Click();
            //check that the method is displayed
            Assert.IsNotNull(driverFF.FindElement(By.Id("method-heading")));
        }

        //RECIPE COLLECTIONS
        //tests relating to the recipe collections pages


    }
}

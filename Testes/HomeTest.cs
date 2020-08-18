using Atata;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes
{
    [TestFixture]
    public class SignInTests
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().
                UseChrome().
                UseBaseUrl("https://demo.atata.io/").
                UseNUnitTestName().
                AddNUnitTestContextLogging().
                AddScreenshotFileSaving().
                LogNUnitError().
                TakeScreenshotOnNUnitError().
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }

        [Test]
        public void FullCycle()
        {
            Go.To<HomePage>().
                CheckBox0.Set(true).
                Wait(1).
                BtnSearch.Click().
                Wait(60).
                BtnFavorite0.Click();
        }

        public void TwoTopics()
        {
            Go.To<HomePage>().
                CheckBox2.Set(true).
                CheckBox5.Set(true).
                Wait(1).
                BtnSearch.Click().
                Wait(120).
                BtnFavorite0.Click();
        }

        public void MoreThanFiveTopics()
        {
            Go.To<HomePage>().
                CheckBox9.Set(true).
                CheckBox10.Set(true).
                CheckBox11.Set(true).
                CheckBox12.Set(true).
                CheckBox13.Set(true).
                CheckBox14.Set(true).
                CheckBox15.Set(true).
                Wait(1).
                BtnSearch.Click().
                Wait(350).
                BtnFavorite0.Click();
        }

        public void GoToFavorites()
        {
            Go.To<HomePage>().
                FavoriteLink.Click();
        }
    }
}

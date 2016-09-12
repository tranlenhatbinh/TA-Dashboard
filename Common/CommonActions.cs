using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TADASHBOARRD.Common
{
    public class CommonActions
    {
        #region Methods

        /// <summary>
        /// Method to navigate to TA Dashboard site
        /// </summary>
        public static void NavigateTADashboard()
        {
            WebDriver.driver.Navigate().GoToUrl(TestData.dashBoardURL);
        }

        /// <summary>
        /// Method to check whether the displayed text matches expectation or not
        /// </summary>
        public static void CheckTextDisplays(string expectedText, string actualText)
        {
            Assert.AreEqual(expectedText, actualText);
        }

        /// <summary>
        /// Method to the the date time of the system
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("ddmmyyyyhhmmss");
        }
        #endregion
    }
}

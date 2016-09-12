using System;
using OpenQA.Selenium;
using TADASHBOARRD.Common;

namespace TADASHBOARRD.PageActions.DataProfilesPage
{
    public class DisplayFieldsPage : GeneralPage.GeneralPage
    {
        protected readonly By tableDisplayFields = By.XPath("//*[@id='profilesettings']/tbody/tr");

        #region Methods

        ///<summary>
        /// Method to click check all link
        ///</summary>
        public void ClickCheckAllLink()
        {
            WaitForControl("checkall link", 5);
            Click("checkall link");
        }

        ///<summary>
        /// Method to click uncheck all link
        ///</summary>
        public void ClickUnCheckAllLink()
        {
            WaitForControl("uncheckall link", 5);
            Click("uncheckall link");
        }

        ///<summary>
        /// Method to check whether all checkboxs of Display Fields are checked or not
        ///</summary>
        public bool AreAllCheckboxChecked()
        {
            bool check = true;
            int rownumber = WebDriver.driver.FindElements(tableDisplayFields).Count;
            for (int row = 3; row < rownumber - 1; row++)
            {
                string xpathcolumn = string.Format("//*[@id='profilesettings']/tbody/tr[{0}]/td", row);
                int columnnumber = WebDriver.driver.FindElements(By.XPath(xpathcolumn)).Count;
                for(int column = 1; column <= columnnumber; column++)
                {
                    string xpathCheckbox = string.Format("//*[@id='profilesettings']/tbody/tr[{0}]/td[{1}]//input[@type = 'checkbox']", row, column);
                    if (WebDriver.driver.FindElement(By.XPath(xpathCheckbox)).Selected == false)
                    {
                        check = false;
                        break;
                    }
                }
            }
            return check;
        }

        ///<summary>
        /// Method to check whether all checkboxs of Display Fields are unchecked or not
        ///</summary>
        public bool AreAllCheckboxUnChecked()
        {
            bool check = true;
            int rownumber = WebDriver.driver.FindElements(tableDisplayFields).Count;
            for (int row = 3; row < rownumber - 1; row++)
            {
                string xpathcolumn = string.Format("//*[@id='profilesettings']/tbody/tr[{0}]/td", row);
                int columnnumber = WebDriver.driver.FindElements(By.XPath(xpathcolumn)).Count;
                for (int column = 1; column <= columnnumber; column++)
                {
                    string xpathCheckbox = string.Format("//*[@id='profilesettings']/tbody/tr[{0}]/td[{1}]//input[@type = 'checkbox']", row, column);
                    if (WebDriver.driver.FindElement(By.XPath(xpathCheckbox)).Selected == true)
                    {
                        check = false;
                        break;
                    }
                }
            }
            return check;
        }

        #endregion
    }
}

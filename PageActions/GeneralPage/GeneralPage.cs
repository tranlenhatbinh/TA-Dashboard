using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using TADASHBOARRD.Common;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using OpenQA.Selenium.Interactions;


namespace TADASHBOARRD.PageActions.GeneralPage
{
    public class GeneralPage : CommonActions
    {
        #region Methods

        /// <summary>
        /// Method to wait for control by locator
        /// </summary>
        public void WaitForControl(By locator, int timeoutInSeconds)
        {
            IWebElement element;
            bool check = false;
            for (int i = 0; i < timeoutInSeconds; i++)
            {
                try
                {
                    element = WebDriver.driver.FindElement(locator);
                    if (element.Displayed!=check)
                    {
                        Sleep(1);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Sleep(1);
                    continue;
                }
            }
        }

        /// <summary>
        /// Method to wait for control by json
        /// </summary>

        public void WaitForControl(string locator, int timeoutInSeconds)
        {
            IWebElement element;
            bool check = false;
            for (int i = 0; i < timeoutInSeconds; i++)
            {
                try
                {
                    element = FindWebElement(locator);
                    if (element.Displayed!=check)
                    {
                        Sleep(1);
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Sleep(1);
                    continue;                   
                }              
            }
        }

        ///<summary>
        /// Method to wait for an alert
        ///</summary>
        public void WaitForAlert(IWebDriver driver)
        {
            int i = 0;
            // Wait in maximum 5 seconds
            while (i++ < 5)
            {
                try
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    break;
                }
                catch (NoAlertPresentException e)
                {
                    Console.WriteLine(e);
                    // If alert is not presented, sleep 1 second then continue
                    Sleep(1);
                    continue;

                }
            }
        }

        ///<summary>
        /// Method to accept alert
        ///</summary>
        public void AcceptAlert()
        {
            WaitForAlert(WebDriver.driver);
            WebDriver.driver.SwitchTo().Alert().Accept();
            // Sleep 1 second after accept the alert
            Sleep(1);
        }

        ///<summary>
        /// Method to get text of the alert
        ///</summary>
        public string GetTextAlert()
        {
            WaitForAlert(WebDriver.driver);
            return WebDriver.driver.SwitchTo().Alert().Text;

        }

        ///<summary>
        /// Method to get text of an element
        ///</summary>
        public string GetText(string locator)
        {
            return FindWebElement(locator).Text;
        }

        ///<summary>
        /// Method to get text of a dynamic element
        ///</summary>
        public string GetTextDynamicElement(string locator, string value)
        {
            return FindDynamicWebElement(locator, value).Text;
        }

        ///<summary>
        /// Method to get the class name from a method
        ///</summary>
        private static string GetClassCaller(int level = 4)
        {
            var m = new StackTrace().GetFrame(level).GetMethod();
            string className = m.DeclaringType.Name;
            return className;
        }
        public class control
        {
            public string controlName { get; set; }
            public string type { get; set; }
            public string value { get; set; }
        }

        ///<summary>
        /// Method to get value of a control from json
        ///</summary>
        public string[] GetControlValue(string nameControl)
        {
            string page = GetClassCaller();
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            path = path.Replace("\\bin\\Debug", "");
            string content = string.Empty;
            switch (page)
            {
                case "LoginPage":
                    content = File.ReadAllText(path + @"\Interfaces\LoginPage\" + page + ".json");
                    break;
                case "GeneralPage":
                case "NewPageDialog":
                case "EditPageDialog":
                case "PanelConfigurationDialog":
                    content = File.ReadAllText(path + @"\Interfaces\GeneralPage\" + page + ".json");
                    break;
                case "PanelsPage":
                case "NewPanelDialog":
                    content = File.ReadAllText(path + @"\Interfaces\PanelsPage\" + page + ".json");
                    break;
                case "DataProfilesPage":
                case "GeneralSettingsPage":
                case "DisplayFieldsPage":
                    content = File.ReadAllText(path + @"\Interfaces\DataProfilesPage\" + page + ".json");
                    break;
                default:
                    break;
            }
            var result = new JavaScriptSerializer().Deserialize<List<control>>(content);
            string[] control = new string[2];
            foreach (var item in result)
            {
                if (item.controlName.Equals(nameControl))
                {
                    control[0] = item.type;
                    control[1] = item.value;
                    return control;
                }
            }
            return null;
        }

        ///<summary>
        /// Method to find a web element
        ///</summary>
        public IWebElement FindWebElement(string locator)
        {
            string[] control = GetControlValue(locator);
            switch (control[0].ToUpper())
            {
                case "ID":
                    return WebDriver.driver.FindElement(By.Id(control[1]));
                case "NAME":
                    return WebDriver.driver.FindElement(By.Name(control[1]));
                case "CLASSNAME":
                    return WebDriver.driver.FindElement(By.ClassName(control[1]));
                default:
                    return WebDriver.driver.FindElement(By.XPath(control[1]));
            }
        }

        ///<summary>
        /// Method to find a dynamic web element
        ///</summary>
        public IWebElement FindDynamicWebElement(string name, string value)
        {
            string[] control = GetControlValue(name);
            string dynamicControl = string.Format(control[1].ToString(), value);
            return WebDriver.driver.FindElement(By.XPath(dynamicControl));
        }

        ///<summary>
        /// Method to enter value to a control
        ///</summary>
        public void EnterValue(string locator, string value)
        {
            FindWebElement(locator).Clear();
            FindWebElement(locator).SendKeys(value);
        }

        ///<summary>
        /// Method to enter value to a control
        ///</summary>
        public void SelectValueDropdownList(string locator, string value)
        {
            FindWebElement(locator).SendKeys(value);
        }

        ///<summary>
        /// Method to tick a checkbox
        ///</summary>
        public void TickCheckbox(string locator)
        {
            if (FindWebElement(locator).Selected == false)
            {
                FindWebElement(locator).Click();
            }
        }

        ///<summary>
        /// Method to untick a checkbox
        ///</summary>
        public void UntickCheckbox(string locator)
        {
            if (FindWebElement(locator).Selected)
            {
                FindWebElement(locator).Click();
            }
        }

        ///<summary>
        /// Method to log out TA Dashboard site
        ///</summary>
        public void Logout()
        {
            WaitForControl("user tab", 5);
            Click("user tab");
            Click("logout tab");
            // For edge
            Sleep(1);
        }

        ///<summary>
        /// Method to open data profiles page
        ///</summary>
        public void OpenDataProfilesPage()
        {
            WaitForControl("administer tab", 5);
            Click("administer tab");
            Click("data profiles tab");
        }

        ///<summary>
        /// Method to open panels page
        ///</summary>
        public void OpenPanelsPage()
        {
            WaitForControl("administer tab", 5);
            Click("administer tab");
            Click("panels tab");

        }

        ///<summary>
        /// Method to open new panel dialog from choose panels button
        ///</summary>
        public void OpenNewPanelDialogFromChoosePanels()
        {
            WaitForControl("choose panels button", 5);
            Click("choose panels button");
            // Sleep 1 second for create new panel button displays
            Sleep(1);
            Click("create new panel button");
        }

        ///<summary>
        /// Method to open random chart panel instance
        ///</summary>
        public void OpenRandomChartPanelInstance()
        {
            Click("choose panels button");
            // wait for Choose panels is loaded
            WaitForControl(By.XPath("//div[@class='ptit pchart']/../table"),5);
            int rowCount = WebDriver.driver.FindElements(By.XPath("//div[@class='ptit pchart']/../table//tr")).Count;
            int randomRow = new Random().Next(1, rowCount);
            int colunmCount = WebDriver.driver.FindElements(By.XPath("//div[@class='ptit pchart']/../table//tr[" + randomRow + "]/td")).Count;
            int randomColumn = new Random().Next(1, colunmCount);
            string a = string.Format("//div[@class='ptit pchart']/../table//tr[{0}]/td[{1}]//a", randomRow, randomColumn);
            IWebElement randomChartPanelInstance = WebDriver.driver.FindElement(By.XPath(a));
            randomChartPanelInstance.Click();
        }

        ///<summary>
        /// Method to open add page dialog
        ///</summary>
        public void OpenAddPageDialog()
        {
            WaitForControl("global setting tab", 5);
            Click("global setting tab");
            Click("add page tab");
        }

        ///<summary>
        /// Method to open edit page dialog
        ///</summary>
        public void OpenEditPageDialog()
        {
            WaitForControl("global setting tab", 5);
            Click("global setting tab");
            Click("edit page tab");
        }

        ///<summary>
        /// Method to perform the delete page action
        ///</summary>
        public void PerformDelete()
        {
            // Sleep 1 second for stable running
            Sleep(1);
            Click("global setting tab");
            Click("delete tab");
            AcceptAlert();
        }

        ///<summary>
        /// Method to delete all pages
        ///</summary>
        public void DeleteAllPages()
        {
            string xpath = string.Empty;
            string xpathNext = string.Empty;
            string locatorClass = string.Empty;
            int numTab = WebDriver.driver.FindElements(By.XPath("//div[@id='main-menu']/div/ul/li/a")).Count;
            int pageIndex = numTab - 2;
            while (pageIndex != 0)
            {
                for (int i = numTab - 2; i >= 1; i--)
                {
                    int numChildren = WebDriver.driver.FindElements(By.XPath("//div[@id='main-menu']/div/ul/li[" + pageIndex + "]/a//..//ul/li/a")).Count;
                    for (int j = 0; j <= numChildren; j++)
                    {
                        xpath = "//div[@id='main-menu']/div/ul/li[" + pageIndex + "]/a";
                        locatorClass = WebDriver.driver.FindElement(By.XPath(xpath)).GetAttribute("class").ToString();
                        while (locatorClass.Contains("haschild"))
                        {
                            if (TestData.browser == "ie" || TestData.browser == "chrome")
                            {
                                ClickItemXpathByJS(xpath);
                            }
                            else
                            {
                                Actions builder = new Actions(WebDriver.driver);
                                builder.MoveToElement(WebDriver.driver.FindElement(By.XPath(xpath))).Build().Perform();
                            }
                            xpathNext = "/following-sibling::ul/li/a";
                            xpath = xpath + xpathNext;
                            locatorClass = WebDriver.driver.FindElement(By.XPath(xpath)).GetAttribute("class").ToString();
                        }
                        string text = WebDriver.driver.FindElement(By.XPath(xpath)).Text;
                        if (text.Equals("Overview") || text.Equals("Execution Dashboard"))
                        {
                            break;
                        }
                        else
                        {
                            if (TestData.browser == "ie" || TestData.browser == "chrome")
                            {
                                ClickItemXpathByJS(xpath);
                            }
                            else
                            {
                                ClickItemXpath(xpath);
                            }
                            PerformDelete();
                        }
                    }
                    pageIndex = pageIndex - 1;
                }
            }
            // Sleep 1 second for the page to load again
            Sleep(1);
        }

        ///<summary>
        /// Method to go to a specific page
        ///</summary>
        public void goToPage(string way)
        {
            Sleep(1);
            string[] allpages = way.Split('/');
            By lastpage = By.XPath("");
            string currentpagexpath = "//ul/li/a[text()='" + allpages[0] + "']";

            if (allpages.Length == 1)
            {
                //There is only main page without child
                lastpage = By.XPath(currentpagexpath);
                WebDriver.driver.FindElement(lastpage).Click();
            }
            else
            {
                //There is main page with has own childrens 
                for (int b = 1; b < allpages.Length; b++)
                {
                    Actions builder = new Actions(WebDriver.driver);
                    Actions hoverClick = builder.MoveToElement(WebDriver.driver.FindElement(By.XPath(currentpagexpath)));
                    hoverClick.Build().Perform();
                    string next = "/following-sibling::ul/li/a[text()='" + allpages[b] + "']";
                    currentpagexpath = currentpagexpath + next;
                    lastpage = By.XPath(currentpagexpath);
                }

                if (TestData.browser == "chrome" || TestData.browser == "ie")
                {
                    IWebElement webElement = WebDriver.driver.FindElement(lastpage);
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriver.driver;
                    executor.ExecuteScript("arguments[0].click();", webElement);
                }
                else
                    WebDriver.driver.FindElement(lastpage).Click();
            }
        }


        ///<summary>
        /// Method to perform sleep action in specific seconds
        ///</summary>
        public void Sleep(int second)
        {
            Thread.Sleep(second * 1000);
        }

        ///<summary>
        /// Method to select item by its text
        ///</summary>
        public void SelectItemByText(string locator, string value)
        {
            SelectElement selectcontrol = new SelectElement(FindWebElement(locator));
            selectcontrol.SelectByText(value);
        }

        /// <summary>
        /// Method to click an element by xpath using javascript
        /// </summary>
        public void ClickItemXpathByJS(string locator)
        {
            IWebElement webElement = WebDriver.driver.FindElement(By.XPath(locator));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriver.driver;
            executor.ExecuteScript("arguments[0].click();", webElement);
        }

        ///<summary>
        /// Method to click an element by xpath
        ///</summary
        public void ClickItemXpath(string locator)
        {
            WebDriver.driver.FindElement(By.XPath(locator)).Click();
        }

        ///<summary>
        /// Method to click on an element
        ///</summary>
        public void Click(string locator)
        {
            if (TestData.browser == "ie" || TestData.browser == "chrome")
            {
                IWebElement webElement = FindWebElement(locator);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriver.driver;
                executor.ExecuteScript("arguments[0].click();", webElement);
            }
            else
            {
                FindWebElement(locator).Click();
            }
        }

        ///<summary>
        /// Method to get user name after log in
        ///</summary>
        public string GetUserName()
        {
            WaitForControl("user tab", 5);
            return GetText("user tab");
        }

        /// <summary>
        /// Method to get the page name based on driver's title
        /// </summary>
        public string GetPageNameOfPageOpened()
        {
            string titleName = WebDriver.driver.Title;
            titleName = titleName.Replace("TestArchitect ™ - ", "");
            return titleName;
        }

        ///<summary>
        /// Method to check whether element presents or not
        ///</summary>
        public bool DoesElementPresent(string locator)
        {
            try
            {
                return FindWebElement(locator).Displayed;
            }

            catch (NoSuchElementException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        ///<summary>
        /// Method to count how many children present in the combobox
        ///</summary>
        public int CountComboboxChildren(string locator)
        {
            return WebDriver.driver.FindElements(By.XPath(locator)).Count;
        }

        #endregion
    }
}



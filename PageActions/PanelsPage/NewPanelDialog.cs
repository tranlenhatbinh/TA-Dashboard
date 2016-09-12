using System;
using OpenQA.Selenium;
using TADASHBOARRD.Common;

namespace TADASHBOARRD.PageActions.PanelsPage
{
    public class NewPanelDialog: GeneralPage.GeneralPage
    {
        #region Methods

        /// <summary>
        /// Method a add new panel
        /// </summary>
        public void AddNewPanel(string name, string series)
        {
            WaitForControl("display name textbox", 5);
            EnterValue("display name textbox", name);
            SelectItemByText("series combobox", ("  " + series));
            Click("ok button");
        }

        /// <summary>
        /// Method to close new panel dialog
        /// </summary>
        public void CloseNewPanelDialog()
        {
            Click("cancel button");
        }

        /// <summary>
        /// Method to check all options of chart type
        /// </summary>
        public void CheckChartTypeOptions()
        {
            WaitForControl("chart type combobox", 5);
            string chartTypeChildren = "//select[@id='cbbChartType']/option";
            WaitForControl(By.XPath(chartTypeChildren), 5);
            int count = CountComboboxChildren(chartTypeChildren);
            for (int i = 1; i <= count; i++)
            {
                string actual = GetTextDynamicElement("chart type child", i.ToString());
                i = Convert.ToInt32(i);
                CheckTextDisplays(TestData.chartTypeArray[i - 1], actual);
            }
        }

        #endregion
    }
}

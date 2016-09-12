using TADASHBOARRD.Common;

namespace TADASHBOARRD.PageActions.DataProfilesPage
{
    public class DataProfilesPage : GeneralPage.GeneralPage
    {
        #region Methods

        ///<summary>
        /// Method to open create profile (general settings) page from data profiles page
        ///</summary>
        public void OpenCreateProfilePage()
        {
            WaitForControl("add new link", 5);
            Click("add new link");
        }

        ///<summary>
        /// Method to delete all profiles
        ///</summary>
        public void DeleteAllProfiles()
        {
            if (DoesElementPresent("check all link") == true)
            {
                Click("check all link");
                Click("delete link");
                AcceptAlert();
            }
        }

        ///<summary>
        /// Method to check correct page loads when navigating through setting pages on the left navigation panel
        ///</summary>
        public void CheckDataProfileOtherSettingPages(string name)
        {
            //Wait for page is loaded
            Sleep(1);
            string xpathDataProfile = string.Format("//a[.='{0}']", name);
            ClickItemXpath(xpathDataProfile);
            CheckDataProfileSettingHeader("display fields tab", TestData.displayFields);
            CheckDataProfileSettingHeader("sort fields tab", TestData.sortFields);
            CheckDataProfileSettingHeader("filter fields tab", TestData.filterFields);
            CheckDataProfileSettingHeader("statistic fields tab", TestData.statisticFields);
            CheckDataProfileSettingHeader("display sub-fields tab", TestData.displaySubFields);
            CheckDataProfileSettingHeader("sort sub-fields tab", TestData.sortSubFields);
            CheckDataProfileSettingHeader("filter sub - fields tab", TestData.filterSubFields);
            CheckDataProfileSettingHeader("statistic sub-fields tab", TestData.statisticSubFields);
        }

        /// <summary>
        /// Method to check the header of the corresponding setting page when navigating through setting pages on the left navigation panel
        /// </summary>
        public void CheckDataProfileSettingHeader(string tab, string header)
        {
            Click(tab);
            // Wait for new page to load
            Sleep(1);
            CheckTextDisplays(header, GetText("fields header"));
        }

        #endregion
    }
}

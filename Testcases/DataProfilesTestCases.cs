using Microsoft.VisualStudio.TestTools.UnitTesting;
using TADASHBOARRD.Common;
using TADASHBOARRD.PageActions.DataProfilesPage;
using TADASHBOARRD.PageActions.GeneralPage;
using TADASHBOARRD.PageActions.LoginPage;

namespace TADASHBOARRD.Testcases
{
    [TestClass]
    public class DataProfilesTestCases : BaseTest
    {
        private LoginPage loginPage;
        private GeneralPage generalPage;
        private DataProfilesPage dataProfilesPage;
        private GeneralSettingsPage generalSettingsPage;
        private DisplayFieldsPage displayFieldsPage;

        [TestMethod]
        public void DA_DP_TC072_Verify_that_all_data_profile_types_are_listed_under_Item_Type_dropped_down_menu()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            generalPage.OpenDataProfilesPage();
            dataProfilesPage = new DataProfilesPage();
            dataProfilesPage.OpenCreateProfilePage();
            generalSettingsPage = new GeneralSettingsPage();
            // VP: Check all data profile types are listed under "Item Type" dropped down menu in create profile page
            generalSettingsPage.CheckItemTypeOptions();
            generalSettingsPage.CancelGeneralSettings();
            //Post-Condition
            dataProfilesPage.Logout();
        }

        [TestMethod]
        public void DA_DP_TC076_Verify_that_for_newly_created_data_profile_user_is_able_to_navigate_through_other_setting_pages_on_the_left_navigation_panel()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            generalPage.OpenDataProfilesPage();
            dataProfilesPage = new DataProfilesPage();
            dataProfilesPage.OpenCreateProfilePage();
            generalSettingsPage = new GeneralSettingsPage();
            generalSettingsPage.CreateNewProfile(TestData.profileName, TestData.defaultItemType, TestData.defaultRelatedData, TestData.actionFinish);
            // VP: Check Display Fields page appears
            // VP: Check Sort Fields page appears
            // VP: Check Filter Fields page appears
            // VP: Check Statistic Fields page appears
            // VP: Check Display Sub-Fields page appears
            // VP: Check Sort Sub-Fields page appears
            // VP: Check Filter Sub-Fields page appears
            // VP: Check Statistic Sub-Fields page appears
            dataProfilesPage.CheckDataProfileOtherSettingPages(TestData.profileName);
        }

        [TestMethod]
        public void DA_DP_TC079_Verify_that_Check_All_Uncheck_All_Links_are_working_correctly()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            generalPage.OpenDataProfilesPage();
            dataProfilesPage = new DataProfilesPage();
            dataProfilesPage.OpenCreateProfilePage();
            generalSettingsPage = new GeneralSettingsPage();
            generalSettingsPage.CreateNewProfile(TestData.profileName, TestData.defaultItemType, TestData.defaultRelatedData, TestData.actionNext);
            displayFieldsPage = new DisplayFieldsPage();
            displayFieldsPage.ClickCheckAllLink();
            // VP: Verify that all checkbox is checked
            Assert.IsTrue(displayFieldsPage.AreAllCheckboxChecked());
            displayFieldsPage.ClickUnCheckAllLink();
            // VP: Verify that all checkbox is unchecked
            Assert.IsTrue(displayFieldsPage.AreAllCheckboxUnChecked());
            displayFieldsPage.Logout();
        }
    }
}

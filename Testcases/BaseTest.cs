using Microsoft.VisualStudio.TestTools.UnitTesting;
using TADASHBOARRD.Common;
using TADASHBOARRD.PageActions.PanelsPage;
using TADASHBOARRD.PageActions.GeneralPage;
using TADASHBOARRD.PageActions.DataProfilesPage;

namespace TADASHBOARRD.Testcases
{
    [TestClass]
    public class BaseTest : CommonActions
    {
        public TestContext TestContext { get; set; }
        NewPanelDialog newPanelDialog = new NewPanelDialog();
        DataProfilesPage dataProfilesPage = new DataProfilesPage();
        GeneralPage generalPage = new GeneralPage();
        PanelConfigurationDialog panelConfigurationDialog= new PanelConfigurationDialog();
        EditPageDialog editPageDialog= new EditPageDialog();

        [AssemblyInitialize]
        public static void AssemblyInitializeMeThod(TestContext TestContext)
        {
            BrowserManager.OpenBrowser(TestData.browser);
        }

        [AssemblyCleanup]
        public static void AssemblyCleapUpMethod()
        {
            BrowserManager.CloseBrowser();
        }

        [TestInitialize]
        public void TestInitializeMethod()
        {          
            NavigateTADashboard();
        }
        [TestCleanup]
        public void TestCleanupMethod()
        {
            switch (TestContext.TestName)
            {
                case "DA_MP_TC020_Verify_that_user_is_able_to_delete_sibbling_page_as_long_as_that_page_has_not_children_page_under_it":
                    generalPage.AcceptAlert();
                    generalPage.DeleteAllPages();
                    generalPage.Logout();
                    WebDriver.driver.Manage().Cookies.DeleteAllCookies();
                    break;
                case "DA_PANEL_TC043_Verify_that_only_integer_number_inputs_from_300_800_are_valid_for_Height_field":
                    panelConfigurationDialog.AcceptAlert();
                    panelConfigurationDialog.CancelPanelConfigurationDialog();
                    generalPage.DeleteAllPages();
                    generalPage.Logout();
                    WebDriver.driver.Manage().Cookies.DeleteAllCookies();
                    break;
                case "DA_DP_TC076_Verify_that_for_newly_created_data_profile_user_is_able_to_navigate_through_other_setting_pages_on_the_left_navigation_panel":
                    dataProfilesPage.OpenDataProfilesPage();
                    dataProfilesPage.DeleteAllProfiles();
                    dataProfilesPage.Logout();
                    WebDriver.driver.Manage().Cookies.DeleteAllCookies();
                    break;
                default:
                    WebDriver.driver.Manage().Cookies.DeleteAllCookies();
                    break;
            }
        }
    }
}

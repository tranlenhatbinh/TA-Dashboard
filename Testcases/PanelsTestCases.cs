using Microsoft.VisualStudio.TestTools.UnitTesting;
using TADASHBOARRD.Common;
using TADASHBOARRD.PageActions.LoginPage;
using TADASHBOARRD.PageActions.GeneralPage;
using TADASHBOARRD.PageActions.PanelsPage;

namespace TADASHBOARRD.Testcases
{
    [TestClass]
    public class PanelsTestCases : BaseTest
    {
        private LoginPage loginPage;
        private GeneralPage generalPage;
        private PanelsPage panelsPage;
        private NewPanelDialog newPanelDialog;
        private NewPageDialog newPageDialog;

        [TestMethod]
        public void DA_PANEL_TC032_Verify_that_user_is_not_allowed_to_create_panel_with_duplicated_Display_Name()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            generalPage.OpenPanelsPage();
            panelsPage = new PanelsPage();
            panelsPage.OpenNewPanelDialog();
            newPanelDialog = new NewPanelDialog();
            newPanelDialog.AddNewPanel(TestData.duplicatedPanelName, TestData.panelSeries);
            panelsPage.OpenNewPanelDialog();
            newPanelDialog.AddNewPanel(TestData.duplicatedPanelName, TestData.panelSeries);
            string actualDuplicateMessage = newPanelDialog.GetTextAlert();
            // VP: Warning message: "Dupicated panel already exists. Please enter a different name" show up
            CheckTextDisplays(actualDuplicateMessage, TestData.errorDuplicatedNamePanelPage);
            // Post-Condition
            newPanelDialog.AcceptAlert();
            newPanelDialog.CloseNewPanelDialog();
            panelsPage.DeleteAllPanels();
            panelsPage.Logout();
        }

        [TestMethod]
        public void DA_PANEL_TC036_Verify_that_all_chart_types_Pie_SingleBar_StackedBar_GroupBar_Line_are_listed_correctly_under_Chart_Type_dropped_down_menu()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            generalPage.OpenAddPageDialog();
            newPageDialog = new NewPageDialog();
            string pageName = CommonActions.GetDateTime();
            newPageDialog.CreateNewPage(pageName, TestData.defaultParentPage, TestData.defaultNumberOfColumns, TestData.defaultDisplayAfter, TestData.statusNotPublic, TestData.levelOne);
            generalPage.OpenNewPanelDialogFromChoosePanels();
            newPanelDialog = new NewPanelDialog();
            // VP: Check that 'Chart Type' are listed 5 options: 'Pie', 'Single Bar', 'Stacked Bar', 'Group Bar' and 'Line'
            newPanelDialog.CheckChartTypeOptions();
            newPanelDialog.CloseNewPanelDialog();
            // Post-Condition
            generalPage.DeleteAllPages();
            generalPage.Logout();
        }


        [TestMethod]
        public void DA_PANEL_TC043_Verify_that_only_integer_number_inputs_from_300_800_are_valid_for_Height_field()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            generalPage.OpenAddPageDialog();
            newPageDialog = new NewPageDialog();
            string pageName = GetDateTime();
            newPageDialog.CreateNewPage(pageName, TestData.defaultParentPage, TestData.defaultNumberOfColumns, TestData.defaultDisplayAfter, TestData.statusNotPublic, TestData.levelOne);
            generalPage.OpenRandomChartPanelInstance();
            PanelConfigurationDialog panelConfigurationDialog = new PanelConfigurationDialog();
            panelConfigurationDialog.EnterValueToHeighThenClickOk(TestData.numberLessThan300);
            // VP: Error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display
            string actualErrorMessage = panelConfigurationDialog.GetTextAlert();
            CheckTextDisplays(TestData.errorMessageWhenEnterOutOfRule, actualErrorMessage);
            panelConfigurationDialog.AcceptAlert();
            panelConfigurationDialog.EnterValueToHeighThenClickOk(TestData.numberMoreThan800);
            // VP: Error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display
            string actualErrorMessage1 = panelConfigurationDialog.GetTextAlert();
            CheckTextDisplays(TestData.errorMessageWhenEnterOutOfRule, actualErrorMessage1);
            panelConfigurationDialog.AcceptAlert();
            panelConfigurationDialog.EnterValueToHeighThenClickOk(TestData.negativeNumber);
            // VP: Error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display
            string actualErrorMessage2 = panelConfigurationDialog.GetTextAlert();
            CheckTextDisplays(TestData.errorMessageWhenEnterOutOfRule, actualErrorMessage2);
            panelConfigurationDialog.AcceptAlert();
            panelConfigurationDialog.EnterValueToHeighThenClickOk(TestData.decimalNumber);
            // VP: Error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display
            string actualErrorMessage3 = panelConfigurationDialog.GetTextAlert();
            CheckTextDisplays(TestData.errorMessageWhenEnterOutOfRule, actualErrorMessage3);
            panelConfigurationDialog.AcceptAlert();
            panelConfigurationDialog.EnterValueToHeighThenClickOk(TestData.character);
            // VP: Error message 'Panel height must be an integer number' display
            string actualErrorMessage4 = panelConfigurationDialog.GetTextAlert();
            CheckTextDisplays(TestData.errorMessageWhenEnterCharacter, actualErrorMessage4);
        }
    }
}

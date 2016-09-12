using Microsoft.VisualStudio.TestTools.UnitTesting;
using TADASHBOARRD.Common;
using TADASHBOARRD.PageActions.LoginPage;
using TADASHBOARRD.PageActions.GeneralPage;

namespace TADASHBOARRD.Testcases
{
    [TestClass]
    public class LoginTestCases : BaseTest
    {
        private LoginPage loginPage;
        private GeneralPage generalPage;
     
        [TestMethod]
        public void DA_LOGIN_TC001_Verify_that_user_can_login_specific_repository_successfully_via_Dashboard_login_page_with_correct_credentials()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            string actualUsername = generalPage.GetUserName();
            // VP: Verify that Dashboard Mainpage appears
            CheckTextDisplays(TestData.validUsername, actualUsername);
            // Post-Condition
            generalPage.Logout();
        }
        
        [TestMethod]
        public void DA_LOGIN_TC002_Verify_that_user_fails_to_login_specific_repository_successfully_via_Dashboard_login_page_with_incorrect_credentials()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.invalidUsername, TestData.invalidPassword);
            string actualMessage = loginPage.GetTextAlert();
            // VP: Verify that Dashboard Error message "Username or password is invalid" appears
            CheckTextDisplays(TestData.errorLoginMessage, actualMessage);
            // Post-Condition
            loginPage.AcceptAlert();
        }
        
        [TestMethod]
        public void DA_LOGIN_TC004_Verify_that_user_is_able_to_login_different_repositories_successfully_after_logging_out_current_repository()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.validUsername, TestData.validPassword);
            generalPage = new GeneralPage();
            generalPage.Logout();
            loginPage.Login(TestData.testRepository, TestData.validUsername, TestData.validPassword);
            string actualUsername = generalPage.GetUserName();
            // VP: Verify that Dashboard Mainpage appears
            CheckTextDisplays(TestData.validUsername, actualUsername);
            // Post-Condition
            generalPage.Logout();
        }

        [TestMethod]
        public void DA_LOGIN_TC006_Verify_that_Password_input_is_case_sensitive()
        {
            loginPage = new LoginPage();
            loginPage.Login(TestData.defaulRepository, TestData.testUsername, TestData.testUppercasePassword);
            generalPage = new GeneralPage();
            string actualUsername = generalPage.GetUserName();
            // VP: Verify that Dashboard Mainpage appears
            CheckTextDisplays(TestData.testUsername, actualUsername);
            generalPage.Logout();
            loginPage.Login(TestData.defaulRepository, TestData.testUsername, TestData.testLowercasePassword);
            string actualMessage = generalPage.GetTextAlert();
            // VP: Verify that Dashboard Error message "Username or password is invalid" appears
            CheckTextDisplays(TestData.errorLoginMessage, actualMessage);
            // Post-Condition
            generalPage.AcceptAlert();
        }
    }
}

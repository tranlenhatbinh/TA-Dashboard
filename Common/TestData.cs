using System.Configuration;

namespace TADASHBOARRD.Common
{
    public class TestData : CommonActions
    {
        public static string runtype = ConfigurationManager.AppSettings["run type"];
        public static string browser = ConfigurationManager.AppSettings["browser"];
        public static string dashBoardURL = ConfigurationManager.AppSettings["url"];
        public static string firefoxVersion = ConfigurationManager.AppSettings["firefox version"];
        public static string chromeVersion = ConfigurationManager.AppSettings["chrome version"];
        public static string ieVersion = ConfigurationManager.AppSettings["ie version"];
        public static string edgeVersion = ConfigurationManager.AppSettings["edge version"];
        public static string hub = ConfigurationManager.AppSettings["hub"];
        public static string validUsername = "administrator";
        public static string validPassword = "";
        public static string defaulRepository = "SampleRepository";
        public static string invalidUsername = "abc";
        public static string invalidPassword = "abc";
        public static string errorLoginMessage = "Username or password is invalid";
        public static string testRepository = "TestRepository";
        public static string testUsername = "test";
        public static string testUppercasePassword = "TEST";
        public static string testLowercasePassword = "test";
        public static string overviewPage = "Overview";
        public static string defaultParentPage = "";
        public static string defaultNumberOfColumns = "";
        public static string defaultDisplayAfter = "";
        public static string statusNotPublic = "";
        public static string statusPublic = "public";
        public static string profileName = "test" + GetDateTime();
        public static string defaultItemType = "";
        public static string defaultRelatedData = "";
        public static string duplicatedPanelName = "Duplicated panel" + GetDateTime();
        public static string panelSeries = "Name";
        public static string numberLessThan300 = "299";
        public static string numberMoreThan800 = "801";
        public static string decimalNumber = "3.1";
        public static string negativeNumber = "-5";
        public static string character = "abc";
        public static string errorMessageWhenEnterOutOfRule = "Panel height must be greater than or equal to 300 and lower than or equal to 800";
        public static string errorMessageWhenEnterCharacter = "Panel height must be an integer number";
        public static string errorDuplicatedNamePanelPage = duplicatedPanelName + " already exists. Please enter a different name.";
        public static string[] chartTypeArray = { "Pie", "Single Bar", "Stacked Bar", "Group Bar", "Line" };
        public static string[] itemTypeArray = { "Test Modules", "Test Cases", "Test Objectives", "Data Sets", "Actions", "Interface Entities", "Test Results", "Test Case Results", "Test Suites", "Bugs" };
        public static string actionFinish = "finish";
        public static string displayFields = "Display Fields";
        public static string sortFields = "Sort Fields";
        public static string filterFields = "Filter Fields";
        public static string statisticFields = "Statistic Fields";
        public static string displaySubFields = "Display Sub-Fields";
        public static string sortSubFields = "Sort Sub-Fields";
        public static string filterSubFields = "Filter Sub-Fields";
        public static string statisticSubFields = "Statistic Sub-Fields";
        public static int levelZero = 0;
        public static int levelOne = 1;
        public static string actionNext = "Next";
    }
}

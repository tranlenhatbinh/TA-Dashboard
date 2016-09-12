namespace TADASHBOARRD.PageActions.GeneralPage
{
    class EditPageDialog: GeneralPage
    {
        #region Methods

        ///<summary>
        /// Method to edit page
        ///</summary>
        public void EditPage(string newPageName, string newParentPage, string newNumberOfColumns, string newDisplayAfter, string newStatus, int newParentLevel)
        {
            WaitForControl("parent page combobox", 5);
            if (newParentPage != "")
            {
                if (newParentLevel == 0)
                {
                    SelectItemByText("parent page combobox", newParentPage);
                }
                if (newParentLevel == 1)
                {
                    SelectItemByText("parent page combobox", ("    " + newParentPage));
                }
                if (newParentLevel == 2)
                {
                    SelectItemByText("parent page combobox", ("        " + newParentPage));
                }
                if (newParentLevel == 3)
                {
                    SelectItemByText("parent page combobox", ("            " + newParentPage));
                }
            }
            WaitForControl("page name textbox", 5);
            EnterValue("page name textbox", newPageName);

            if (newNumberOfColumns != "")
            {
                SelectItemByText("number of columns combobox", newNumberOfColumns);
            }
            if (newDisplayAfter != "")
            {
                SelectItemByText("display after combobox", newDisplayAfter);
            }
            WaitForControl("public checkbox", 5);
            if (newStatus == "public")
            {
                TickCheckbox("public checkbox");
            }

            if (newStatus == "unpublic")
            {
                UntickCheckbox("public checkbox");
            }
            Click("ok button");
            // Wait for created page loads
            Sleep(1);
        }

        #endregion
    }
}

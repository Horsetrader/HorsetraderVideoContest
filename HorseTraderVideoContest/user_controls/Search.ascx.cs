using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace HorseTraderWeb.user_controls
{
    public partial class Search : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                SetSearchValues();
        }

        private void SetSearchValues()
        {
            //Set drop-down list value
            try
            {
                if (Request["inet"] != null)
                    ddlSearchCategories.SelectedValue = Request["inet"];
            }
            catch
            {
                ddlSearchCategories.SelectedIndex = 0;
            }
            //Set textbox value
            try
            {
                if (Request["q"] != null)
                    tbxSearch.Text = Request["q"];
            }
            catch
            {
                tbxSearch.Text = string.Empty;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent(string.Format("{0}/search?q={1}&inet={2}",
                ConfigurationManager.AppSettings["WebsiteURL"], tbxSearch.Text, ddlSearchCategories.SelectedValue));
        }
    }
}
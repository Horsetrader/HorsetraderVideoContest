using System;
using System.Configuration;

namespace HorseTraderWeb.user_controls
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Event Handlers
        protected void btnGoToFastAd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent(ConfigurationManager.AppSettings["WebsiteURL"] + "/viewfullad.asp?fastad=" + txtFastAd.Text);
        }
        #endregion
    }
}
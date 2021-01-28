/* Author: Foster Hangdaan */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheSpamAssassins
{
    public partial class Camp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Camper camper = (Camper)Session["camper"];
            camperNameLbl.Text = String.Format("{0} {1}", camper.FirstName, camper.LastName);
            LoadNumberOfActivities(camper.Username, camper.Password);
        }

        public void LoadNumberOfActivities(string username, string password) 
        {
            ActivityDAO activityDAO = new ActivityDAO(username, password);
            int numOfActivities = activityDAO.GetNumberOfActivities();
            numberOfActivitiesLbL.Text = String.Format("Your Registered Activities: <span class=emphasize_text>{0}</span>", numOfActivities);
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/CamperActivities.aspx");
        }
    }
}
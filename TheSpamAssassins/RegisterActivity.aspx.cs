// Author: Foster Hangdaan
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheSpamAssassins
{
    public partial class RegisterActivity : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorMsgLbl.Visible = false;
            activitiesGV.Visible = true;

            if (!IsPostBack)
            {
                campDayDDL.SelectedIndex = 0;
                LoadAvailableActivities();
            }
        }

        private void LoadAvailableActivities()
        {
            Camper camper = (Camper)Session["camper"];
            ActivityDAO activityDAO = new ActivityDAO(camper.Username, camper.Password);
            List<Activity> availableActivities = activityDAO.GetAvailableActivities(Convert.ToInt32(campDayDDL.SelectedValue));

            if (availableActivities.Count() != 0)
            {
                activitiesGV.DataSource = availableActivities;
                activitiesGV.Columns[0].Visible = true; // Column must be visible before binding data.
                activitiesGV.DataBind();
                activitiesGV.Columns[0].Visible = false;
            }
            else
            {
                errorMsgLbl.Text = "No available activities or you are already registered for the selected camp day.";
                errorMsgLbl.Visible = true;
                activitiesGV.Visible = false;
            }
        }

        protected void campDayDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableActivities();
        }

        protected void activitiesGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Camper camper = (Camper)Session["camper"];
            RegisterDAO registerDAO = new RegisterDAO(camper.Username, camper.Password);
            ActivityDAO activityDAO = new ActivityDAO(camper.Username, camper.Password);
            int index = Convert.ToInt32(e.CommandArgument);
            int activityId = Convert.ToInt32(activitiesGV.Rows[index].Cells[0].Text);

            if (e.CommandName == "RegisterActivity")
            {
                int campDay = Convert.ToInt32(campDayDDL.SelectedValue);
                int registerResult = registerDAO.RegisterActivity(activityId, campDay);

                if (0 == registerResult)
                {
                    errorMsgLbl.Text = "Unable to register for activity.\n";
                    errorMsgLbl.Visible = true;
                }
                else
                    Response.Redirect("~/CamperActivities.aspx");
            }
        }

        protected void activitiesGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true; // to prevent on-screen row editing
        }

        protected void activitiesGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true; // to prevent on-screen row deleting
        }
    }
}
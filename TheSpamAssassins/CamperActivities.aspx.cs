//Matthew Percy
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheSpamAssassins
{
    public partial class CamperActivities : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorMsgLbl.Visible = false;

            if (!IsPostBack)
                LoadRegisteredActivities();
        }

        protected void LoadRegisteredActivities()
        {
            Camper camper = (Camper)Session["camper"];
            RegisterDAO registerDAO = new RegisterDAO(camper.Username, camper.Password);
            ActivityDAO activityDAO = new ActivityDAO(camper.Username, camper.Password);

            ActivityGridView.DataSource = registerDAO.GetRegisteredActivities();
            ActivityGridView.Columns[0].Visible = true;
            ActivityGridView.DataBind();
            ActivityGridView.Columns[0].Visible = false;

            if (activityDAO.GetNumberOfActivities() == 0)
            {
                errorMsgLbl.Text = String.Format("You are not registered in any activities.");
                errorMsgLbl.Visible = true;
            }
        }

        protected void ActivityGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Camper camper = (Camper)Session["camper"];
            CamperDAO camperDAO = new CamperDAO(camper.Username, camper.Password);

            int index = Convert.ToInt32(e.CommandArgument);
            int activityId = Convert.ToInt32(ActivityGridView.Rows[index].Cells[0].Text);

            if (e.CommandName == "UnregisterActivity")
            {
                camperDAO.UnregisterActivity(activityId);
                LoadRegisteredActivities();
                Master.LoadNumberOfActivities(camper.Username, camper.Password);
            }
        }

        protected void ActivityGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Convert Camp Day numbers to words */
                int CampDay = Convert.ToInt32(e.Row.Cells[2].Text);
                switch (CampDay)
                {
                    case 1:
                        e.Row.Cells[2].Text = "Monday";
                        break;
                    case 2:
                        e.Row.Cells[2].Text = "Tuesday";
                        break;
                    case 3:
                        e.Row.Cells[2].Text = "Wednesday";
                        break;
                    case 4:
                        e.Row.Cells[2].Text = "Thursday";
                        break;
                    case 5:
                        e.Row.Cells[2].Text = "Friday";
                        break;
                    default:
                        e.Row.Cells[2].Text = "None";
                        break;
                }
            }
            
        }
    }
}
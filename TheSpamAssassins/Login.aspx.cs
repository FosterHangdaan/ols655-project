/* Author: Mark Brierley */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheSpamAssassins
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            messageLbl.Text = generateQuote();
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                try
                {
                    Camper camperInfo = DatabaseHelper.Login(tboxUsername.Text, tboxPassword.Text);
                    Session.Add("camper", camperInfo);
                    Response.Redirect("~/CamperActivities.aspx");
                }
                catch
                {
                    lblFail.Visible = true;
                }
            }
        }

        private String generateQuote()
        {
            List<String> quotes = new List<String>();
            Random rnd = new Random();

            quotes.Add("I thought what I'd do was I'd pretend I was one of those deaf-mutes.");
            quotes.Add("The tans will fade but the memories will stay forever.");
            quotes.Add("Summertime is always the best of what might be.");
            quotes.Add("When all else fails, take a vacation.");
            quotes.Add("Summer should get a speeding ticket.");
            quotes.Add("I dream of a neverending summer.");
            quotes.Add("It's a smile, it's a kiss, it's a sip of wine, it's summertime!");
            quotes.Add("August is like the Sunday of summer.");
            quotes.Add("If you're not barefoot, then you're overdressed.");
            quotes.Add("Summer will end soon enough, and childhood as well.");

            return quotes[rnd.Next(quotes.Count())];
        }
    }
}
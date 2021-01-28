// Mark Brierley
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;

namespace TheSpamAssassins
{
    public class ActivityDAO
    {
        private string Username { get; set; }
        private string Password { get; set; }

        public ActivityDAO(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public int GetNumberOfActivities()
        {
            OracleConnection conn = new OracleConnection(String.Format("Data Source=Neptune; User Id={0}; Password={1}", Username, Password));
            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) AS num_regs FROM register WHERE camper_username = :Username", conn);
            cmd.Parameters.AddWithValue(":Username", Username);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return Convert.ToInt32(dt.Rows[0]["num_regs"]);
        }

        public List<Activity> GetAvailableActivities(int CampDay)
        {
            List<Activity> availableActivities = new List<Activity>();

            OracleConnection conn = new OracleConnection(String.Format("Data Source=Neptune; User Id={0}; Password={1}", Username, Password));
            OracleCommand cmd = new OracleCommand("SELECT id, name, capacity FROM activity WHERE IS_AVAILABLE(:campday_param, id) = 1 ORDER BY name", conn);
                       
            cmd.Parameters.AddWithValue("campday_param", CampDay); ;            

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
           
            foreach (DataRow dr in dt.Rows)
            {
                availableActivities.Add(new Activity(Convert.ToInt32(dr["id"]), Convert.ToString(dr["name"]), Convert.ToInt32(dr["capacity"])));
            }
           
            return availableActivities;
        }
    }
}
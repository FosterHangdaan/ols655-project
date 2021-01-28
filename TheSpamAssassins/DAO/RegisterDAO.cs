// Author: Foster Hangdaan
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;

namespace TheSpamAssassins
{
    public class RegisterDAO
    {
        private string Username { get; set; }
        private string Password { get; set; }

        public RegisterDAO(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public List<RegisteredActivity> GetRegisteredActivities()
        {
            List<RegisteredActivity> registeredActivities = new List<RegisteredActivity>();

            DataTable dt = new DataTable();

            string sqlQuery = @"
            SELECT activity_id, name, camp_day
            FROM register
            INNER JOIN activity ON activity_id = id
            WHERE camper_username = :username
            ORDER BY camp_day";

            OracleConnection conn = new OracleConnection(String.Format("Data Source=Neptune; User Id={0}; Password={1}", Username, Password));
            OracleCommand cmd = new OracleCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue(":username", Username);
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                int activityId = Convert.ToInt32(dr["activity_id"]);
                string activityName = Convert.ToString(dr["name"]);
                int campDay = Convert.ToInt32(dr["camp_day"]);
                registeredActivities.Add(new RegisteredActivity(activityId, activityName, campDay));
            }
            return registeredActivities;
        }

        public int RegisterActivity(int activityId, int campDay)
        {
            OracleConnection conn = new OracleConnection(String.Format("Data Source=Neptune; User Id={0}; Password={1}", Username, Password));
            OracleCommand cmd = new OracleCommand("REGISTER_ACTIVITY", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("pactivity_id", activityId);
            cmd.Parameters.AddWithValue("pcamp_day", campDay);
            cmd.Parameters.Add("psuccess", OracleType.Int32).Direction = ParameterDirection.Output;

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["psuccess"].Value);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
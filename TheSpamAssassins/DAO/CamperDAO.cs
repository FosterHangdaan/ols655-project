//Matthew Percy
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace TheSpamAssassins
{
    public class CamperDAO
    {
        private string Username { get; set; }
        private string Password { get; set; }

        public CamperDAO(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public Camper ValidateCamper()
        {
            //This method returns the Camper corresponding to the Oracle username and password 
            //(passed to the constructor) as well as the camper's first and last name from the 
            //database for the camper with the specified username, or null if a camper with the 
            //specified username is not found in the database.

            DataTable dt = new DataTable();

            string sqlQuery = @"
            SELECT FIRST_NAME, LAST_NAME FROM CAMPER WHERE USERNAME = :username";

            OracleConnection conn = new OracleConnection(String.Format("Data Source=Neptune; User Id={0}; Password={1}", Username, Password));
            OracleCommand cmd = new OracleCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue(":username", Username);
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                string FirstName = Convert.ToString(dr["FIRST_NAME"]);
                string LastName = Convert.ToString(dr["LAST_NAME"]);
                return new Camper(Username, Password, FirstName, LastName);
            }

            return null;
        }

        public void UnregisterActivity(int ActivityId)
        {
            //This method deletes the activity registration in the database for the 
            //registration with the specified activity id for the current user.
            OracleConnection conn = new OracleConnection(String.Format("Data Source=Neptune; User Id={0}; Password={1}", Username, Password));
            OracleCommand cmd = new OracleCommand(@"DELETE FROM REGISTER WHERE CAMPER_USERNAME = :username AND ACTIVITY_ID = :activityID", conn);

            cmd.Parameters.AddWithValue(":username", Username);
            cmd.Parameters.AddWithValue(":activityID", ActivityId);

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
// Author: Foster Hangdaan, Mark Brierley
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;

namespace TheSpamAssassins
{
    public class DatabaseHelper
    {
        public static Camper Login(string Username, string Password)
        {                        
            OracleConnection conn = new OracleConnection(String.Format("Data Source=Neptune; User Id={0}; Password={1};", Username, Password));
            conn.Open();
            conn.Close();

            CamperDAO camp = new CamperDAO(Username, Password);
            return camp.ValidateCamper();
        }
    }
}
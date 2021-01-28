// Mark Brierley
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSpamAssassins
{
    public class Camper
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Camper(string Username, string Password, string FirstName, string LastName)
        {
            this.Username = Username;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
    }
}
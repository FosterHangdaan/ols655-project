// Mark Brierley
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSpamAssassins
{
    public class RegisteredActivity
    {
        public int ActivityId { get; private set; }
        public string ActivityName { get; private set; }
        public int CampDay { get; private set; }

        public RegisteredActivity(int ActivityId, string ActivityName, int CampDay)
        {
            this.ActivityId = ActivityId;
            this.ActivityName = ActivityName;
            this.CampDay = CampDay;
        }
    }
}
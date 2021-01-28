// Author: Foster Hangdaan
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSpamAssassins
{
    public class Activity
    {
        public int ActivityId { get; private set; }
        public int Capacity { get; private set; }
        public string Name { get; private set; }

        public Activity(int ActivityId, string Name, int Capacity)
        {
            this.ActivityId = ActivityId;
            this.Name = Name;
            this.Capacity = Capacity;
        }
    }
}
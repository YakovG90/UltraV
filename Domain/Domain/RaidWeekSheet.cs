using System;
using System.Collections.Generic;

namespace Domain.Domain
{
    public class RaidWeekSheet
    {
        public RaidWeekSheet(
            DateTime raidWeekStart)
        {
            this.RaidWeekStart = raidWeekStart;
            this.RaidDayInstances = new List<RaidDayInstance>();
        }

        public int Id { get; protected set; }

        public DateTime RaidWeekStart { get; protected set; }

        public List<RaidDayInstance> RaidDayInstances { get; protected set; }

    }
}
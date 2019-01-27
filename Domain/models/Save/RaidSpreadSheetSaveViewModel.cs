using System;
using System.Collections.Generic;

namespace Domain.Models.Save
{
    public class RaidSpreadSheetSaveViewModel
    {
        public DateTime RaidWeekStart { get; set; }

        public IEnumerable<string> RaidDays { get; set; }
    }
}
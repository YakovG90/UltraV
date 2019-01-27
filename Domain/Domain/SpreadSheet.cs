namespace Domain.Domain
{
    using System;
    using System.Collections.Generic;

    public class RaidDayInstance
    {
        public RaidDayInstance(
            DateTime startDate,
            DateTime endDate,
            bool? isLocked)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.CharacterRoster = new List<Character>();

            if (isLocked != null)
            {
                this.IsLocked = isLocked;
            }
        }

        public int Id { get; protected set; }

        public DateTime StartDate { get; protected set; }

        public DateTime EndDate { get; protected set; }

        public List<Character> CharacterRoster { get; protected set; }

        public bool? IsLocked { get; protected set; }

        // Push Identity Integration
        /*
        public List<User> Authors { get; set; }*/

        internal void Update(DateTime? startDate, DateTime? endDate, bool? isLocked)
        {
            if (startDate != null)
            {
                this.StartDate = (DateTime)startDate;
            }

            if (endDate != null)
            {
                this.EndDate = (DateTime)endDate;
            }

            if (isLocked != null)
            {
                this.IsLocked = isLocked;
            }
        }
    }
}
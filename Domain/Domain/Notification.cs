using System;

namespace Domain.Domain
{
    public class Notification
    {
        public Notification(string text, int orderIndex, bool isStickied)
        {
            this.Text = text;
            this.OrderIndex = orderIndex;
            this.IsStickied = isStickied;
            this.DateCreated = DateTime.UtcNow;
        }

        public int Id { get; protected set; }

        public string Text { get; protected set; }

        public int OrderIndex { get; protected set; }

        public bool IsStickied { get; protected set; }

        public DateTime DateCreated { get; protected set; }

        public DateTime DateUpdated { get; protected set; }

        // Push Integration with Identity.
        // public User Author { get; protected set; }

        public void Update(string text, bool? isStickied = null)
        {
            if (!string.IsNullOrEmpty(text))
            {
                this.Text = text;
            }

            if (isStickied != null)
            {
                this.IsStickied = (bool)isStickied;
            }

            this.DateUpdated = DateTime.UtcNow;
        }
    }
}
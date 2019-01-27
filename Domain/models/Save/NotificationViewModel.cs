namespace Domain.Models.Save
{
    public class NotificationViewModel
    {
        public string Text { get; set; }

        public int OrderIndex { get; set; }

        public bool IsStickied { get; set; }
    }
}
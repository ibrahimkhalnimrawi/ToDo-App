namespace Core.Entites
{
    public class ToDoEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAT { get; set; }
    }
}

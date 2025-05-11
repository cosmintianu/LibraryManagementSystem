namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int TotalQuantity { get; set; }
        public int Quantity { get; set; }

    }
}

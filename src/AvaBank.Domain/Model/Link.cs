namespace AvaBank.Domain.Model
{ 
    public partial class Link
    {
        public string Self { get; set; }

        public string First { get; set; }
        
        public string Prev { get; set; }

        public string Next { get; set; }

        public string Last { get; set; }
    }
}
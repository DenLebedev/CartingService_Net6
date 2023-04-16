namespace CartingService.Common.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public List<int> ItemsIDs { get; set; }
    }
}

namespace ProdControlWPF.Models
{
    public sealed class OperationItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? PositionId { get; set; }
    }

    public sealed class PositionItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

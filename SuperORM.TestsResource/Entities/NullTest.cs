namespace SuperORM.TestsResource.Entities
{
    public class NullTest
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? phoneNumber { get; set; }
        public DateTime? expirationDate { get; set; }
        public bool? inactive { get; set; }
    }
}

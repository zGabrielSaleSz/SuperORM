namespace SuperORM.TestsResource.Entities
{
    public class OldUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}

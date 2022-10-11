namespace SuperORM.WebAPI.DTO.Document
{
    public class CreateDocument
    {
        public int IDUser { get; set; }
        public int Type { get; set; }
        public string Number { get; set; }
        public DateTime IssueDate { get; set; }
    }
}

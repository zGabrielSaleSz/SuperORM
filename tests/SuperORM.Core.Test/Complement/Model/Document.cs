using System;

namespace SuperORM.Core.Test.Complement.Model
{
    public class Document
    {
        public int id { get; set; }
        public int idUser { get; set; }
        public int idDocumentType { get; set; }
        public string number { get; set; }
        public DateTime issueDate { get; set; }
    }
}

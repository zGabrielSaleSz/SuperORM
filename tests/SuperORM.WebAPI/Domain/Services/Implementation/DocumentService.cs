using SuperORM.TestsResource.Entities;
using SuperORM.WebAPI.DTO.Document;
using SuperORM.WebAPI.Infrastructure.MySqlImp;

namespace SuperORM.WebAPI.Domain.Services.Implementation
{
    public class DocumentService : IDocumentService
    {
        private UnityOfWork _unityOfWork;
        public DocumentService(UnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public Document Create(CreateDocument createDocument)
        {
            if (Exists(createDocument.IDUser, createDocument.Type))
                throw new Exception("This document type already exists for this user.");

            if (createDocument.IssueDate.Date > DateTime.Now.Date)
                throw new Exception("The issue date cannot be in the future!");

            Document document = new Document
            {
                idUser = createDocument.IDUser,
                idDocumentType = createDocument.Type,
                number = createDocument.Number,
                issueDate = DateTime.Now,
            };
            _unityOfWork.Documents.Insert(document);
            return document;
        }

        public bool Exists(int idUser, int documentType)
        {
            return _unityOfWork.Documents
                .GetSelectable()
                .Where(d => d.idUser == idUser && d.idDocumentType == documentType)
                .FirstOrDefault() != null;
        }
    }
}

using SuperORM.TestsResource.Entities;
using SuperORM.WebAPI.DTO.Document;

namespace SuperORM.WebAPI.Domain.Services
{
    public interface IDocumentService
    {
        bool Exists(int idUser, int documentType);
        Document Create(CreateDocument createDocument);
    }
}

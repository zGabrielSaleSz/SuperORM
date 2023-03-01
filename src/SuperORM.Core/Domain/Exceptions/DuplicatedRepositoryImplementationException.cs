namespace SuperORM.Core.Domain.Exceptions
{
    public class DuplicatedRepositoryImplementationException : SuperOrmException
    {
        public DuplicatedRepositoryImplementationException(string message) : base(message)
        {

        }
    }
}

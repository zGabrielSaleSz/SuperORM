namespace SuperORM.Core.Domain.Exceptions
{
    public class DuplicatedRepositoryRegistry : SuperOrmException
    {
        private const string MESSAGE = "This repository is already added.";
        public DuplicatedRepositoryRegistry(string message = MESSAGE) : base(message)
        {
        }
    }
}

namespace SuperORM.Core.Domain.Exceptions
{
    public class DuplicatedRepositoryRegistryException : SuperOrmException
    {
        private const string MESSAGE = "This repository is already added.";
        public DuplicatedRepositoryRegistryException(string message = MESSAGE) : base(message)
        {
        }
    }
}

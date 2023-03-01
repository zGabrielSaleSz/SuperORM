namespace SuperORM.Core.Domain.Exceptions
{
    public class NotFoundedRepositoryRegistryException : SuperOrmException
    {
        private const string MESSAGE = "This repository wasn't configured previously.";
        public NotFoundedRepositoryRegistryException(string message = MESSAGE) : base(message)
        {
        }
    }
}

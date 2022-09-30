namespace SuperORM.Core.Domain.Exceptions
{
    public class NotFoundedRepositoryRegistry : SuperOrmException
    {
        private const string MESSAGE = "This repository wasn't configured previously.";
        public NotFoundedRepositoryRegistry(string message = MESSAGE) : base(message)
        {
        }
    }
}

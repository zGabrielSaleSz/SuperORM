namespace SuperORM.WebAPI.Domain.Util
{
    public static class PasswordHandler
    {
        public static void Check(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("Password cannot be null or empty!");
        }
    }
}

namespace Movie.DAL.Extensions
{
    public class DuplicateItemException : CustomException
    {
        public DuplicateItemException() { }
        public DuplicateItemException(string message) : base(message) { }
        public DuplicateItemException(string message, Exception innerException) : base(message, innerException) { }
    }
}

namespace Saturno.Domain.Validation
{
    public class ValidationMessage
    {
        public ValidationMessage(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Property { get; set; }

        public string Message { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Saturno.Domain.Common.Validation
{
    public class AbstractValidator
    {
        public List<ValidationMessage> ValidationResult = new List<ValidationMessage>();

        public bool Valid => !Invalid;

        public bool Invalid => ValidationResult.Any();

        public void AddNotification(string property, string message) =>
            ValidationResult.Add(new ValidationMessage(property, message));

        public void AddNotifications(IList<ValidationMessage> validations) =>
            ValidationResult.AddRange(validations);
    }
}

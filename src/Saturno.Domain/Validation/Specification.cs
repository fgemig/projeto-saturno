using System;
using System.Collections.Generic;

namespace Saturno.Domain.Validation
{
    public partial class Specification
    {
        private List<ValidationMessage> Validations = new List<ValidationMessage>();

        public Specification Requires() => this;

        public Specification IsNotNullOrWhiteSpace(string val, string property, string message)
        {
            if (string.IsNullOrWhiteSpace(val))
                Validations.Add(new ValidationMessage(property, message));

            return this;
        }

        public Specification IsNotEquals(string val1, string val2, string property, string message)
        {
            if (val1 != val2)
                Validations.Add(new ValidationMessage(property, message));

            return this;
        }

        public Specification IsNotEmpty(Guid val, string property, string message)
        {
            if (val == Guid.Empty)
                Validations.Add(new ValidationMessage(property, message));

            return this;
        }

        public IList<ValidationMessage> Compile()
        {
            return Validations;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.Helpers.ValidationAttributes
{
    public class NotEqualToAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public NotEqualToAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value?.ToString();

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                throw new ArgumentException($"Property {_comparisonProperty} not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance)?.ToString();

            if (currentValue == comparisonValue)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be different from {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
    }

}

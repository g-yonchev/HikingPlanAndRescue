namespace HikingPlanAndRescue.Web.Infrastructure.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class CompareTrainingDateValidationAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get; }

        public string OtherProperty { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value;

            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(this.OtherProperty);
            DateTime otherDate = (DateTime)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherDate >= date)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}
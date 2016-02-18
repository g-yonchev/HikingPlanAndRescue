namespace HikingPlanAndRescue.Web.Infrastructure.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TrainingDateFormatValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt;
            bool parsed = DateTime.TryParse((string)value, out dt);
            if (!parsed)
            {
                return false;
            }

            return true;
        }
    }
}

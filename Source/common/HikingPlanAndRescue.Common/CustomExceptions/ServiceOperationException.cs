namespace HikingPlanAndRescue.Web.Infrastructure.CustomExceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomServiceOperationException : SystemException
    {
        public CustomServiceOperationException(string message)
            : base(message)
        {

        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

public class StartTimeValidation : ValidationAttribute
{
    public StartTimeValidation()
        : base("Start time must be at least 10 minutes from now and not in the past.") { }

    public override bool IsValid(object value)
    {
        if (value is DateTime startTime)
        {
            DateTime now = DateTime.Now;
            return startTime >= now && startTime >= now.AddMinutes(10);
        }

        return false;
    }
}

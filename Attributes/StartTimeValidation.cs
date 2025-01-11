using System;
using System.ComponentModel.DataAnnotations;

public class StartTimeValidation : ValidationAttribute
{
    public StartTimeValidation()
        : base("Start time must be at least 10 minutes from now.") { }

    public override bool IsValid(object value)
    {
        if (value is DateTime startTime)
        {
            return startTime >= DateTime.Now.AddMinutes(10);
        }

        return false;
    }
}

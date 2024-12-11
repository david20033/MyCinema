using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyCinema.Services
{
    public class EnumServices
    {
        public SelectList GetEnumSelectList<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new
                {
                    Value = Convert.ToInt32(e), 
                    Name = e.ToString()       
                }).ToList();

            return new SelectList(values, "Value", "Name");
        }
    }
}

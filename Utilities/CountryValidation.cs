using System.ComponentModel.DataAnnotations;

namespace MovieSpace.Utilities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false, Inherited =true)]
    public class CountryValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value is string Country)
            {
                string[] validCountries = { "NIGERIA", "GHANA", "USA", "UK", "KOREA", "INDIA", "SPAIN"};
                return validCountries.Contains(Country);
            }

            return false;
        }
    }
}

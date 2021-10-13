using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Shared.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {
        public string AllowedDomain { get; set; }
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            string[] arrEmail = value.ToString().Split('@');
            if(arrEmail.Length>1 && arrEmail[1].ToUpper() == AllowedDomain.ToUpper())
            {
                return null;
            }
            return new ValidationResult($"Domain pada email harus {AllowedDomain}",
                new[] { validationContext.MemberName });
        }
    }
}

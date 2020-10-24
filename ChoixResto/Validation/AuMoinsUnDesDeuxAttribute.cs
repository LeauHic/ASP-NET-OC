using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ChoixResto.Validation
{
    public class AuMoinsUnDesDeuxAttribute : ValidationAttribute
    {
        public string parametre1 { get; set; }
        public string parametre2 { get; set; }

        public AuMoinsUnDesDeuxAttribute() : base("Vous devez saisir au moins l'un des deux restaurants") { }
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            PropertyInfo[] proprietes = validationContext.ObjectType.GetProperties();
            PropertyInfo info1 = proprietes.FirstOrDefault(p => p.Name == parametre1);
            PropertyInfo info2 = proprietes.FirstOrDefault(p => p.Name == parametre2);

            string valeur1 = info1.GetValue(validationContext.ObjectInstance) as string;
            string valeur2 = info2.GetValue(validationContext.ObjectInstance) as string;

            if (string.IsNullOrWhiteSpace(valeur1) && string.IsNullOrWhiteSpace(valeur2))
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }
    }
}
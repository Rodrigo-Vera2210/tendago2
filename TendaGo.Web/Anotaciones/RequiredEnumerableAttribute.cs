using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TendaGo.Web.Anotaciones
{
    public class RequiredEnumerableAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            var enumerable = value as IEnumerable;
            if (enumerable == null) { return false; }

            IEnumerator enumerator = enumerable.GetEnumerator();
            if (enumerator.MoveNext()) { return true; }

            return false;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ValidationType = "requiredenumerable",
                ErrorMessage = ErrorMessageString
            };
        }
    }
}
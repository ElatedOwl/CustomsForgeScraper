using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Reflection;

namespace CFScraper.Domain.FormData
{
    internal class FormDataSerializer
    {
        public FormUrlEncodedContent Serialize<T>(T obj) where T : class
        {
            var formValues = new Dictionary<string, string>();

            foreach(var property in typeof(T).GetProperties())
            {
                if (!PropertyHasFormDataAttribute(property, out FormData formData))
                    continue;

                var propertyValue = property.GetValue(obj);

                if (propertyValue == null)
                    continue;

                formValues.Add(formData.Name, propertyValue.ToString());
            }

            return new FormUrlEncodedContent(formValues);
        }

        private bool PropertyHasFormDataAttribute(PropertyInfo propertyInfo, out FormData formData)
        {
            formData = propertyInfo
                        .GetCustomAttributes(typeof(FormData))
                        .SingleOrDefault() as FormData;

            return formData != null;
        }
    }
}

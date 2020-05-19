using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace YALB
{
    public class FastContactHydrator2 : ContactHydratorBase
    {
        private static readonly KeyValuePair<string, Action<Contact, string>>[] _propertySettersArray;

        private static readonly IEnumerable<KeyValuePair<string, Action<Contact, string>>> _propertySettersEnumerable;

        static FastContactHydrator2()
        {
            var type = typeof(Contact);
            _propertySettersEnumerable = type.GetProperties().Select(prop => new KeyValuePair<string, Action<Contact, string>>(prop.Name, GetSetterAction(prop)));
            _propertySettersArray = _propertySettersEnumerable.ToArray();
        }

        private static Action<Contact, string> GetSetterAction(PropertyInfo property)
        {
            var setterInfo = property.GetSetMethod();
            var paramValueOriginal = Expression.Parameter(property.PropertyType, "value");
            var paramEntity = Expression.Parameter(typeof(Contact), "entity");
            var setterExp = Expression.Call(paramEntity, setterInfo, paramValueOriginal);

            var lambda = (Expression<Action<Contact, string>>)Expression.Lambda(setterExp, paramEntity, paramValueOriginal);

            return lambda.Compile();
        }

        protected override Contact GetContact(PropertyToValueCorrelation[] correlations)
        {
            var contact = new Contact();
            foreach (var setterMapItem in _propertySettersArray)
            {
                var correlation = correlations.FirstOrDefault(x => x.PropertyName == setterMapItem.Key);
                if (correlation?.Value == null)
                    continue;
                setterMapItem.Value(contact, correlation?.Value);
            }
            return contact;
        }

        protected override Contact GetContact2(IEnumerable<PropertyToValueCorrelation> correlations)
        {
            var contact = new Contact();
            int dummySum = _propertySettersArray.Join(correlations, propItem => propItem.Key, corrItem => corrItem.PropertyName, (prop, corr) => { prop.Value(contact, corr.Value); return 1; }).Sum();
            return contact;
        }
    }
}

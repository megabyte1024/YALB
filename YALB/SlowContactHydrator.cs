using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YALB
{
    public class SlowContactHydrator : ContactHydratorBase
    {
        protected static readonly PropertyInfo[] _properties;
        static SlowContactHydrator()
        {
            var type = typeof(Contact);
            _properties = type.GetProperties();
        }

        protected override Contact GetContact(PropertyToValueCorrelation[] correlations)
        {
            var contact = new Contact();
            foreach (var property in _properties)
            {
                var correlation = correlations.FirstOrDefault(x => x.PropertyName == property.Name);
                if (correlation?.Value == null)
                    continue;

                property.SetValue(contact, correlation.Value);
            }
            return contact;
        }

        protected override Contact GetContact2(IEnumerable<PropertyToValueCorrelation> correlation)
        {
            throw new NotImplementedException();
        }
    }
}

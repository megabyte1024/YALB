using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace YALB
{
    public class ManualContactHydrator : ContactHydratorBase
    {
        protected static readonly PropertyInfo[] _properties;
        static ManualContactHydrator()
        {
            var type = typeof(Contact);
            _properties = type.GetProperties();
        }

        protected override Contact GetContact(PropertyToValueCorrelation[] correlations)
        {
            var contact = new Contact();
            foreach (var correlation in correlations)
            {
                switch (correlation.PropertyName)
                {
                    case nameof(Contact.FullName):
                        contact.FullName = correlation.Value;
                        break;
                    case nameof(Contact.Phone):
                        contact.Phone = correlation.Value;
                        break;
                    case nameof(Contact.Age):
                        contact.Age = correlation.Value;
                        break;
                }
            }
            return contact;
        }

        protected override Contact GetContact2(IEnumerable<PropertyToValueCorrelation> correlation)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YALB
{
    public abstract class ContactHydratorBase
    {

        protected static readonly ContactMapSchema[] _mapSchemas = MockHelper.GetFakeData().ToArray();
 
        protected static readonly string _typeNameInUpperCaseInvariant;

        static ContactHydratorBase()
        {
            var type = typeof(Contact);
            _typeNameInUpperCaseInvariant = type.FullName.ToUpperInvariant();
        }

        public PropertyToValueCorrelation[] GetPropertiesValuesWithoutLinq(string rawData)
        {
            var result = new List<PropertyToValueCorrelation>(10);
            var mailPairs = DefaultRawStringParser.ParseWithoutLinq(rawData: rawData, pairDelimiter: Environment.NewLine);
            var mapSchemas = _mapSchemas.ToArray();
            foreach (var item in mapSchemas)
            {
                if (item.EntityName.ToUpperInvariant() != _typeNameInUpperCaseInvariant)
                    continue;

                foreach (var pair in mailPairs)
                {
                    if (!pair.Key.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    result.Add(new PropertyToValueCorrelation { PropertyName = item.Property, Value = pair.Value });
                }
            }
            return result.ToArray();
        }

        public PropertyToValueCorrelation[] GetPropertiesValuesFairWithoutLinq(string rawData)
        {
            var result = new List<PropertyToValueCorrelation>();
            var mailPairs = DefaultRawStringParser.ParseFairWithoutLinq(rawData: rawData, pairDelimiter: Environment.NewLine);
            var mapSchemas = _mapSchemas;
            foreach (var item in mapSchemas)
            {
                if (item.EntityName.ToUpperInvariant() != _typeNameInUpperCaseInvariant)
                    continue;

                foreach (var pair in mailPairs)
                {
                    if (!pair.Key.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    result.Add(new PropertyToValueCorrelation { PropertyName = item.Property, Value = pair.Value });
                }
            }
            return result.ToArray();
        }

        public PropertyToValueCorrelation[] GetPropertiesValues(string rawData)
        {
            Dictionary<string, string> mailPairs = DefaultRawStringParser.ParseWithLinq(rawData: rawData, pairDelimiter: Environment.NewLine);
            var mapSchemas =
                _mapSchemas
                .Where(x => x.EntityName.ToUpperInvariant() == _typeNameInUpperCaseInvariant)
                .Select(x => new { x.Key, x.Property })
                .ToArray();

            return
                mailPairs
                .Join(mapSchemas, x => x.Key, x => x.Key,
                    (x, y) => new PropertyToValueCorrelation { PropertyName = y.Property, Value = x.Value })
                .ToArray();
        }

        public PropertyToValueCorrelation[] GetPropertiesValuesFair(string rawData)
        {
            List<KeyValuePair<string, string>> mailPairs = DefaultRawStringParser.ParseFairWithLinq(rawData: rawData, pairDelimiter: Environment.NewLine);
            var mapSchemas =
                _mapSchemas
                .Where(x => x.EntityName.ToUpperInvariant() == _typeNameInUpperCaseInvariant)
                .Select(x => new { x.Key, x.Property })
                .ToArray();

            return
                mailPairs
                .Join(mapSchemas, x => x.Key, x => x.Key,
                    (x, y) => new PropertyToValueCorrelation { PropertyName = y.Property, Value = x.Value })
                .ToArray();
        }

        public IEnumerable<PropertyToValueCorrelation> GetPropertiesValues2(string rawData)
        {
            var mailPairs = DefaultRawStringParser.ParseWithLinq2(rawData: rawData, pairDelimiter: Environment.NewLine);
            var mapSchemas =
                _mapSchemas
                .Where(x => x.EntityName.ToUpperInvariant() == _typeNameInUpperCaseInvariant)
                .Select(x => new { x.Key, x.Property });

            return
                mailPairs
                .Join(mapSchemas, x => x.Key, x => x.Key,
                    (x, y) => new PropertyToValueCorrelation { PropertyName = y.Property, Value = x.Value });
        }
        protected abstract Contact GetContact(PropertyToValueCorrelation[] correlation);
        
        protected abstract Contact GetContact2(IEnumerable<PropertyToValueCorrelation> correlation);
        
        public Contact HydrateWithLinq(string rawData)
            => GetContact(GetPropertiesValues(rawData));
        
        public Contact HydrateFairWithLinq(string rawData)
            => GetContact(GetPropertiesValuesFair(rawData));
        
        public Contact HydrateWithLinq2(string rawData)
            => GetContact2(GetPropertiesValues2(rawData));
        
        public Contact HydrateWithoutLinq(string rawData)
            => GetContact(GetPropertiesValuesWithoutLinq(rawData));
        
        public Contact HydrateFairWithoutLinq(string rawData)
            => GetContact(GetPropertiesValuesFairWithoutLinq(rawData));
    }
}

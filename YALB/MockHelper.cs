using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace YALB
{
    public interface IStorage
    {
        DbSet<ContactMapSchema> ContactMapSchemas { get; set; }
    }

    public class HabraDbContext : DbContext, IStorage
    {
        public virtual DbSet<ContactMapSchema> ContactMapSchemas { get; set; }
    }

    class MockHelper
    {

        public static IStorage InstanceDb()
        {
            var mock = new Mock<HabraDbContext>();
            mock.Setup(x => x.ContactMapSchemas).ReturnsDbSet(GetFakeData());

            return mock.Object;
        }

        public static IEnumerable<ContactMapSchema> GetFakeData()
        {
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Phone", Property = _contactPhoneAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Telephone", Property = _contactPhoneAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Telefon", Property = _contactPhoneAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Телефон", Property = _contactPhoneAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Контактный телефон", Property = _contactPhoneAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Тел.", Property = _contactPhoneAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Full Name", Property = _contactFullNameAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Vollständiger Name", Property = _contactFullNameAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "ФИО", Property = _contactFullNameAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Фамилия Имя Отчество", Property = _contactFullNameAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Имя", Property = _contactFullNameAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Age", Property = _contactAgeAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Alter", Property = _contactAgeAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Возраст", Property = _contactAgeAssemblyName };
            yield return new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Полных лет", Property = _contactAgeAssemblyName };
        }

        public static List<ContactMapSchema> GetFakeDataOld()
            => new List<ContactMapSchema>
            {
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Phone", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Telephone", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Telefon", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Телефон", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Контактный телефон", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Тел.", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Full Name", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Vollständiger Name", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "ФИО", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Фамилия Имя Отчество", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Имя", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Age", Property = _contactAgeAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Alter", Property = _contactAgeAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Возраст", Property = _contactAgeAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Полных лет", Property = _contactAgeAssemblyName},
            };

        private static string _contactAssemblyName = typeof(Contact).FullName;
        private static string _contactPhoneAssemblyName = nameof(Contact.Phone);
        private static string _contactFullNameAssemblyName = nameof(Contact.FullName);
        private static string _contactAgeAssemblyName = nameof(Contact.Age);
    }
}

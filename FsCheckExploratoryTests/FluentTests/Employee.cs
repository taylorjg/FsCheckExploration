using System;

namespace FsCheckExploratoryTests.FluentTests
{
    public class Employee
    {
        public Employee(string firstName, string lastName, DateTime hireDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _hireDate = hireDate;
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public DateTime HireDate
        {
            get { return _hireDate; }
        }

        public override string ToString()
        {
            return string.Format("{{FirstName: {0}; LastName: {1}; HireDate: {2}}}", FirstName, LastName, HireDate.ToLongDateString());
        }

        private readonly string _firstName;
        private readonly string _lastName;
        private readonly DateTime _hireDate;
    }
}

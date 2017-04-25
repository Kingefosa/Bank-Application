using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class Person
    {
        string name;
        string number;

        public Person(string name, string number)
        {   if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(number) || number.Length < 8)
            {
                throw new Exception("Some arguments are null or nuber too short");
            }
            this.name = name.Trim();
            this.number = number.Trim();
        }

        public string GetName()
        {
            return name;
        }

        public override string ToString()
        {
            return "name: " + name + ", number: " + number;
        }
    }
}

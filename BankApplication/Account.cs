using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BankApplication
{
    public class Account
    {
        private Person person;
        private Money money;
        private int accountNumber;
        private string accountName;

        public Account(Person person, Money money, int accountNumber)
        {
            if (person == null) throw new ArgumentNullException("someArgument");
            this.person = person;
            this.money = money;
            this.accountNumber = accountNumber;
            // accountName is the persons name in lowercase with no spaces, with a number at the end to allow a person to have multiple accounts    
            this.accountName = Regex.Replace(person.GetName().ToLower(), @"\s+", "") + accountNumber;
        }

        public bool Deposit(Money amount)
        {
            decimal newValue = money.getNOK() + amount.getNOK();
            money = new Money("NOK", newValue);
            return true;
        }

        public bool Withdraw(Money amount)
        {
            if(money.getNOK() >= amount.getNOK())
            {
                decimal newValue = money.getNOK() - amount.getNOK();
                money = new Money("NOK",newValue);
                return true;
            }
            return false;
        }

        public Person GetPerson()
        {
            return person;
        }

       public Account DeepCopy()
        {
            return new Account(person, money, accountNumber);
        }

        public decimal GetNOK()
        {
            return money.getNOK();
        }

        public string GetAccountName()
        {
            return accountName;
        }

        public override string ToString()
        {
            return person.ToString() + ", " + money.ToString() + ", accountname: " + accountName;
        }
    }
}

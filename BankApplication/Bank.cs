using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class Bank
    {
        private List<Account> accounts = new List<Account>();
        private readonly decimal MIN_FIRST_DEPOSIT = 0;
        private readonly decimal MIN_DEPOSIT = 0;

        public Account CreateAccount(Person person, Money money)
        {
            if (person == null || money == null || money.getNOK() < MIN_FIRST_DEPOSIT)
            {
                throw new Exception("Some arguments in CreateAccount is null or not enough money for initial deposit");
            }
            int numberOfAccounts = 1;
            Account account;
            foreach(Account a in accounts)
            {
                if (a.GetPerson().Equals(person))
                {
                    numberOfAccounts++;
                }
            }
            
            account = new Account(person, money, numberOfAccounts);
            accounts.Add(account);
            // return deep copy of account, since only want to edit actual accounts through the bank
            return account.DeepCopy();
        }

        public Account[] GetAccountsForCustomer(Person customer)
        {
            if (customer == null)
            {
                throw new Exception("Customer can not be null");
            }
            List<Account> customerAccounts = new List<Account>();
            foreach (Account a in accounts)
            {
                if (a.GetPerson().Equals(customer))
                {
                    // return arrau of account copies. only want to edit actual accounts through bank
                    customerAccounts.Add(a.DeepCopy());
                }
            }
            return customerAccounts.ToArray();
        }

        public bool Deposit(string to, Money amount)
        {
            //chose to use string from instead of Account from as the first argument.
            //my motivation is that I want only the bank to be able to modify accounts
            //and then it makes more sense to pass the id instead of an account object
            if (amount.getNOK() < MIN_DEPOSIT)
            {
                return false;
            }

            bool moneyDeposited = false;
            int index = findAccount(to);
            if(index >= 0)
            {
                moneyDeposited = accounts[index].Deposit(amount);
            }

            return moneyDeposited;
        }

        public bool Withdraw(string from, Money amount)
        {
            //chose to use string from instead of Account from as the first argument. See Deposit-method above
            bool isMoneyWithdrawn = false;
            int index = findAccount(from);
            if(index>= 0)
            {
                isMoneyWithdrawn = accounts[index].Withdraw(amount);

            }

            return isMoneyWithdrawn;
        }

        public bool Transfer(string to, string from, Money amount)
        {
            //chose to use strings instead of Accounts the two first arguments. See Deposit-method above
            bool transferSuccessful = false;
            if(Withdraw(from, amount))
            {
                if(Deposit(to, amount))
                {
                    // both return true, transfer happened
                    transferSuccessful = true;
                }
                else
                {   
                    // deposit failed into to-account failed, deposit money back into from-account, transfer did not happen
                    Deposit(from, amount);
                }
            }
            return transferSuccessful;
        }

        private int findAccount(string accountName)
        {
            bool searchForAccount = true;
            int counter = 0;
            int index = -1;
            Account a;

            while (searchForAccount && counter < accounts.Count)
            {
                a = accounts[counter];
                if (a.GetAccountName().Equals(accountName))
                {
                    index = counter;
                    searchForAccount = false;
                }
                counter++;
            }
            return index;
        }

        public override string ToString()
        {
            string bankinfo = "";
            foreach(Account a in accounts)
            {
                bankinfo += a.ToString() + "\n";
            }
            return bankinfo;
        }
        
    }
}

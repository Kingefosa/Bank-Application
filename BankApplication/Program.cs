using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            Person p1 = new Person("  Espen  ", "99396513"); //handles whitespace
            Person p2 = new Person("Karianne", "99887766");
            Money m1 = new Money("nok", 1000); //handles small letters in currency
            Money m2 = new Money("NOK  ", 9999);//handles whitespace in currency
            Money m3 = new Money("NOK", 1001);

            Account a1 = bank.CreateAccount(p1, m1);
            Account a2 = bank.CreateAccount(p2, m1);
            Account a3 = bank.CreateAccount(p1, m2);

            Console.WriteLine("Number of tests: 8\n");

            if (a1.GetPerson().Equals(p1) && a2.GetPerson().Equals(p2) && a3.GetPerson().Equals(p1))
            {
                Console.WriteLine("Test 1 ok");
            }

            Account[] espensAccounts = bank.GetAccountsForCustomer(p1);
            if (espensAccounts.Length == 2 && espensAccounts[0].GetAccountName() == "espen1" && espensAccounts[1].GetAccountName() == "espen2")
            {
                Console.WriteLine("Test 2 ok");
            }

            Person ingen = new Person("ingen", "66995588");
            Account[] ingenAccounts = bank.GetAccountsForCustomer(ingen);
            if (ingenAccounts.Length == 0)
            {
                Console.WriteLine("Test 3 ok");
            }

            bool withdraw1 = bank.Withdraw("espen1", m1); //withdraw less money than in account, allowed
            bool withdraw2 = bank.Withdraw("espen1", m2); // try withdraw more money than in account, not allowed
            bool deposit1 = bank.Deposit("espen1", m2); //positive amount, allowed
            espensAccounts = bank.GetAccountsForCustomer(p1);

            if (deposit1 && withdraw1 && !withdraw2 && espensAccounts[0].GetNOK() == 9999)
            {
                Console.WriteLine("Test 4 ok");
            }

            // manipulating the accounts here does not change the actual accounts in the bank
            espensAccounts[0].Deposit(new Money("NOK", 1000000));
            Account[] espensAccounts2 = bank.GetAccountsForCustomer(p1);
            if (espensAccounts[0].GetNOK() == 1009999 && espensAccounts2[0].GetNOK() == 9999)
            {
                Console.WriteLine("Test 5 ok");
            }

            bank.Transfer("karianne1", "espen1", m2);
            espensAccounts = bank.GetAccountsForCustomer(p1);
            Account[] kariannesAccounts = bank.GetAccountsForCustomer(p2);

            if (kariannesAccounts.Length == 1 && kariannesAccounts[0].GetNOK() == 10999 && espensAccounts[0].GetNOK() == 0)
            {
                Console.WriteLine("Test 6 ok");
            }

            // Test that transfer is not compleded if the from account has insufficient funds
            bool bool1 = bank.Transfer("karianne1", "espen1", m2);
            espensAccounts = bank.GetAccountsForCustomer(p1);
            kariannesAccounts = bank.GetAccountsForCustomer(p2);
            if (!bool1 && kariannesAccounts[0].GetNOK() == 10999 && espensAccounts[0].GetNOK() == 0)
            {
                Console.WriteLine("Test 7 ok");
            }

            Money usd = new Money("usd", 100);
            if(usd.getNOK() == 855)
            {
                Console.WriteLine("Test 8 ok");
            }

            Console.WriteLine("\nWrite bank to console\n");
            Console.WriteLine(bank.ToString());
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class Money
    {
        // currencies to illustrate handling of such
        // for more currencies these would need their own class
        // with a conversiontable between all possible pairs of currencies
        private decimal nokValue;
        private decimal usdnok = 8.55m;
        private string[] CURRENCIES = { "NOK", "USD" };
       

        public Money(string currency, decimal value)
        {   
            if(value < 0)
            {
                throw new Exception("Money value can not be less than zero");
            }

            int pos = Array.IndexOf(CURRENCIES, currency.Trim().ToUpper());
            if (pos < 0)
            {
                throw new Exception("Currency not found in list of currencies");
            }

            decimal nokValue;
            if(pos == 0)
            {
                nokValue = value;
            }
            else if(pos == 1)
            {
                nokValue = value * usdnok;
            }
            else
            {
                throw new Exception("Unexpected error");
            }

            this.nokValue = nokValue;
        }

        public decimal getNOK()
        {
            return nokValue;
        }

        public decimal getUSD()
        {
            return nokValue / usdnok;
        }

        public override string ToString()
        {
            return "value in NOK: " + nokValue;
        }
    }
}

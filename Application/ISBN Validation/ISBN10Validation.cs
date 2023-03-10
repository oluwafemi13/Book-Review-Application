using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ISBN_Validation
{
    public class ISBN10Validation
    {
       /* protected string ISBN { get; set; }
        public ISBN10Validation(string ISBN)
        {
            this.ISBN = ISBN;
        }*/

        public bool validateISBN10(string ISBN)
        {
            string clearedIn = ISBN.ToUpper().Replace("-", "").Replace(" ", "").Trim();

            //Eingabe nach int[] parsen
            int[] numbers = clearedIn.ToCharArray().Select(i => i == 'X' ? 10 : int.Parse(i.ToString())).ToArray();

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += numbers[i] * (10 - i);
            }

            if (sum % 11 == 0)
            {
                return true;
            }
            return false;
        }
    }
}

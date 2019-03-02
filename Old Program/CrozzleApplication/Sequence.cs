using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrozzleApplication
{
    class Sequence
    {
        public string Word { get; set; }
        public string Score { get; set; }
        public string Length { get; set; }
        public string Ascii { get; set; }
        public string Total { get; set; }
        public override string ToString()
        {
            return Word + "," + Score + "," + Length + "," + Ascii + "," + Total;
        }
        //new match to regex
        public static Boolean Test(string word, string REgex)
        {
            if (Validator.IsDelimited(REgex, Crozzle.StringDelimiters))
                REgex = REgex.Trim(Crozzle.StringDelimiters).Trim();
            return (Regex.IsMatch(word, @"^" + REgex + "$"));
        }
    }
}

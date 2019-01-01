
namespace FrenchRepublicanCalendar.Utility
{
    using System;

    public static class IntUtility
    {
        // https://stackoverflow.com/questions/7040289/converting-integers-to-roman-numerals#11749642

        public static string ToRoman(int number)
        {
            if (number < 0 || number > 3999)
                throw new ArgumentOutOfRangeException("insert value between 1 and 3999");
            if (number < 1) return string.Empty;
            //if (number >= 1000000) return "M\u0305" + ToRoman(number - 1000000);
            //if (number >= 500000) return "D\u0305" + ToRoman(number - 500000);
            //if (number >= 100000) return "C\u0305" + ToRoman(number - 100000);
            //if (number >= 50000) return "L\u0305" + ToRoman(number - 50000);
            //if (number >= 10000) return "X\u0305" + ToRoman(number - 10000);
            //if (number >= 5000) return "V\u0305" + ToRoman(number - 5000);
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
    }
}

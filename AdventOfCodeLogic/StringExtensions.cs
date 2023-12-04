using System.Text;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static int[] ExtractNumbers(this string inputString)
    {
        return  Regex.Matches(inputString, @"\d").Select(x => int.Parse(x.Value)).ToArray();
    }

    static string[] Literals = new [] {@"\d", "one", "two","three","four","five", "six", "seven", "eight", "nine"};
    public static int[] ExtractStringLiteralsAndDigits(this string inputString)
    {
        string joined = string.Join(")|(", Literals);
        var pattern = $"({joined})";
        var matches = Regex.Matches(inputString, pattern);
        foreach (var match in matches){
            
        }
        return matches.Select(x => ParseNumberString(x.Value)).ToArray();
    }    

    static int ParseNumberString(string value){
        if (int.TryParse(value, out var result)){
            return result;
        }

        return Array.IndexOf(Literals, value);
    }

    public static int[] ExtractStringLiteralsAndDigits2(this string inputString)
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < inputString.Length; i++) {
            var theRest = inputString.Substring(i, inputString.Length-i);
            var m = Regex.Match(theRest[0].ToString(), @"\d");
            if (m.Success){
                numbers.Add(int.Parse(m.Value));
            }
            else{
                for (int j = 1; j < Literals.Length; j++){
                    if (theRest.StartsWith(Literals[j], StringComparison.CurrentCultureIgnoreCase)){
                        numbers.Add(j);
                    }
                }
            }
            
        }
        return numbers.ToArray();
    }

    public static bool IsNotAlphaNumeric(this char x, params char[] excludes)
    {
        return !char.IsAsciiDigit(x) && !char.IsAsciiLetter(x) && !excludes.Contains(x);
    }

    public static bool IsNumericOrDot(this string input)
    {
        // Use a regular expression to check if the string contains only digits and dots
        return Regex.IsMatch(input, "^[0-9.]*$");
    }

    public static (bool foundNumber, int number, int firstIndex, int lastIndex) SeekNumberAtIndex(this string line, int startIndex)
    {
        List<char> digits = new List<char>();
        if (startIndex < 0 || startIndex > line.Length-1){
            return (false,0,startIndex,startIndex);
        }

        int rightIndex = startIndex;
        int leftIndex = startIndex-1;
        
        

        bool characterAtIndexIsNumeric = char.IsAsciiDigit(line[rightIndex]);
        
        // Seek right (add to the end)
        while (characterAtIndexIsNumeric)
        {
            digits.Add(line[rightIndex++]);
            characterAtIndexIsNumeric = rightIndex < line.Length && char.IsAsciiDigit(line[rightIndex]);
        }
                
        // Seek left (insert at the start)
        if (digits.Count > 0 && leftIndex >= 0){
            characterAtIndexIsNumeric = char.IsAsciiDigit(line[leftIndex]);

            while (characterAtIndexIsNumeric){
                digits.Insert(0, line[leftIndex--]);
                characterAtIndexIsNumeric = leftIndex >= 0 && char.IsAsciiDigit(line[leftIndex]);
            }
        }


        int result = 0;
        if (digits.Count > 0){
            result = int.Parse(string.Join("", digits));
        }

        return (digits.Count > 0, result, leftIndex+1, rightIndex-1);
    }

    public static (string number, int startIndex)[] GetNumbers(this string line)
    {
        List<(string number, int startIndex)> result = new List<(string number, int startIndex)>();

        // Use a regular expression to find all matches of consecutive digits
        MatchCollection matches = Regex.Matches(line, "\\d+");

        foreach (Match match in matches)
        {
            string number = match.Value;
            int startIndex = match.Index;

            result.Add((number, startIndex));
        }

        return result.ToArray();
    }

    public static (int number, string stringValue) SplitNumberAndString(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            // Return default values or handle the case as needed
            return (0, string.Empty);
        }

        value = value.Trim();

        int endIndex = -1;
        for (int i = 0; i < value.Length; i++)
        {
            if (!char.IsDigit(value[i]))
            {
                endIndex = i;
                break;
            }
        }

        if (endIndex == -1)
        {
            // No non-digit character found, return the entire string as the rest
            return (int.Parse(value), string.Empty);
        }

        int number;
        if (int.TryParse(value.Substring(0, endIndex), out number))
        {
            string restOfString = value.Substring(endIndex).Trim();
            return (number, restOfString);
        }

        // Parsing failed, return default values or handle the case as needed
        return (0, string.Empty);
    }
}
// See https://aka.ms/new-console-template for more information

namespace OneHundredCodingChallenges;

public class Program
{
    public static void Main(string[] args)
    {
        //PrimeNumberOccurrences(1, 10);
        //ReverseString();
        //ReverseNumber();
        Console.WriteLine(AlternateVowelsConsonants("oruder"));
    }

    private static void ReverseNumber()
    {
        Console.WriteLine("Enter number to reverse");
        var number = Convert.ToInt32(Console.ReadLine());
        var reverse = "";
        for (int i = number.ToString().Length - 1; i >=0; i--)
        {
            reverse += number.ToString()[i];
        }
        Console.WriteLine(reverse);
    }

    private static void ReverseString()
    {
        Console.WriteLine("Enter string to reverse:");
        var source = Console.ReadLine();
        var reverse = "";
        for (var i = source.Length - 1; i >= 0; i--)
        {
            reverse += source[i];
        }

        Console.WriteLine(reverse);
    }

    private static void PrimeNumberOccurrences(int n, int m)
    {
        var primes = new List<int>();
        for (var i = n; i <= m; i++)
        {
            if (i <= 1) continue;
            else if (i == 2) primes.Add(i);
            else if (i % 2 == 0) continue;
            else
            {
                // optimization to prevent having to check all the way up to the Nth element as we would find multiples along the way
                var boundary = (int)(Math.Floor(Math.Sqrt(i)));
                var isPrime = true;
                for (int j = 3; j <= boundary; j += 2)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    primes.Add(i);
                }
            }
            
        }

        foreach (var pr in primes)
        {
            Console.WriteLine(pr);
        }
    }

    private static string AlternateVowelsConsonants(string input)
    {
        int vowelCount = 0;
        int consonantCount = 0;

        foreach (char c in input)
        {
            if ("aeiou".Contains(c))
            {
                vowelCount++;
            }
            else
            {
                consonantCount++;
            }
        }

        if (vowelCount != consonantCount)
        {
            return "failed";
        }

        var vowls = new List<char>();
        var consonants = new List<char>();
        foreach (var c in input)
        {
            if ("aeiou".Contains(c))
            {
                vowls.Add(c);
            }
            else
            {
                consonants.Add(c);
            }
        }

        if (HasConsecutiveElements(vowls) || HasConsecutiveElements(consonants))
        {
            return "failed";
        }
        
        vowls.Sort();
        consonants.Sort();
        var result = "";

        while (vowls.Count > 0 && consonants.Count > 0)
        {
            result += vowls[0];
            result += consonants[0];
            
            vowls.RemoveAt(0);
            consonants.RemoveAt(0);
        }

        result += string.Join("", vowls);
        result += string.Join("", consonants);

        return result;
    }

    private static bool HasConsecutiveElements(List<char> list)
    {
        for (var i = 0; i < list.Count - 1; i++)
        {
            if (list[i] == list[i + 1])
            {
                return true;
            }
        }

        return false;
    }
}
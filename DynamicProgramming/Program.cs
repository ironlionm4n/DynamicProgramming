// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Security.Cryptography;

namespace DynamicProgramming;

class Program
{
    // memoization
    // keys will be argument to function
    // value will be return value
    
    private static void Main(string[] args)
    {
        //FibTest();
        //GridTest();
        //CanSumTest();
        var memo = new Dictionary<int, int[]?>();
        var howSumResult = HowSum(7, new int[] { 2, 3 }, memo);
        HowSumConsoleWriteLine(howSumResult);
        memo.Clear();
        howSumResult = HowSum(7, new int[] { 5, 3, 4, 7 }, memo);
        HowSumConsoleWriteLine(howSumResult);
        memo.Clear();
        howSumResult = HowSum(7, new int[] { 2, 4 }, memo);
        HowSumConsoleWriteLine(howSumResult);
        memo.Clear();
        howSumResult = HowSum(8, new[] { 2, 3, 5 }, memo);
        HowSumConsoleWriteLine(howSumResult);
        memo.Clear();
        howSumResult = HowSum(300, new [] {7, 14}, memo);
        HowSumConsoleWriteLine(howSumResult);
    }

    private static void CanSumTest()
    {
        var memo = new Dictionary<int, bool>();
        Console.WriteLine(CanSum(2, new int[] { 1, 1 }, memo));
        Console.WriteLine(CanSum(7, new int[] { 5, 3, 4, 7 }, memo));
        Console.WriteLine(CanSum(300, new int[] { 7, 14 }, memo));
    }

    private static void GridTest()
    {
        var gridDict = new Dictionary<Tuple<int, int>, BigInteger>();
        Console.WriteLine(GridTraveler(3, 3, gridDict));
        Console.WriteLine(GridTraveler(2, 5, gridDict));
        Console.WriteLine(GridTraveler(18, 18, gridDict));
    }

    private static void FibTest()
    {
        Dictionary<int, BigInteger> set = new();
        var fib = NthFib(100, set);
        Console.WriteLine(fib);
    }

    /// <summary>
    /// Memoized fib sequence finder which returns the number in the fib sequence at N
    /// </summary>
    /// <param name="n - position of the sequence"></param>
    /// <param name="memo - Dictionary to cache subtrees"></param>
    /// <returns>BigInteger - fib sequence at position N</returns>
    private static BigInteger NthFib(int n, Dictionary<int, BigInteger> memo)
    {
        if (memo.TryGetValue(n, out var value))
        {
            return value;
        }
        
        if (n <= 2) return 1;

        memo.Add(n, NthFib(n - 1, memo) + NthFib(n - 2, memo));
        return memo[n];
    }

    /// <summary>
    /// Calculates how many ways to travel to the goal on a grid with m by n dimensions
    /// Begin in top-left corner and goal is bottom-right corner.
    /// Can only move down or right
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="cols"></param>
    /// <returns>BigInteger - the number of ways to travel to the goal</returns>
    private static BigInteger GridTraveler(int rows, int cols, Dictionary<Tuple<int,int>, BigInteger> memo)
    {
        var key = Tuple.Create(rows, cols);
        if (memo.TryGetValue(key, out var traveler)) return traveler;
        if (rows == 0 || cols == 0) return 0;
        if (rows == 1 && cols == 1) return 1;
        
        memo[key] = GridTraveler(rows - 1, cols, memo) + GridTraveler(rows, cols - 1, memo);
        return memo[key];
    }

    /// <summary>
    /// Given a target sum and an array of numbers return true/false if there is a subset of hte given set who sum is equal to the target
    /// </summary>
    /// <param name="targetSum"></param>
    /// <param name="numbers"></param>
    /// <param name="memo"></param>
    /// <returns></returns>
    private static bool CanSum(int targetSum, int[] numbers, Dictionary<int, bool> memo)
    {
        if (memo.ContainsKey(targetSum)) return memo[targetSum];
        if (targetSum == 0) return true;
        if (targetSum < 0) return false;
        
        foreach (var number in numbers)
        {
            var remainder = targetSum - number; // memoize the remainder
            
            if (CanSum(remainder, numbers, memo))
            {
                memo[targetSum] = true;
                return true;
            }
        }

        memo[targetSum] = false;
        return false;
    }

    /// <summary>
    /// return an array containing any combination of elements that add up to exactly the targetSum, if no combination is found return null
    /// </summary>
    private static int[]? HowSum(int targetSum, int[] numbers, Dictionary<int, int[]?> memo)
    {
        if (memo.TryGetValue(targetSum, out var value)) return memo[targetSum];
        if (targetSum == 0) return Array.Empty<int>();
        if (targetSum < 0) return null;

        foreach (var number in numbers)
        {
            var remainder = targetSum - number;
            var remainderResult = HowSum(remainder, numbers, memo);
            if (remainderResult != null)
            {
                var remainderList = new List<int>(remainderResult) { number };
                memo.Add(targetSum, remainderList.ToArray());
                return memo[targetSum];
            }
        }

        memo[targetSum] = null;
        return null;
    }

    private static void HowSumConsoleWriteLine(int[]? howSumResult)
    {
        if (howSumResult != null)
        {
            Console.WriteLine(string.Join(", ", howSumResult));
        }
        else
        {
            Console.WriteLine("null");
        }
    }
}
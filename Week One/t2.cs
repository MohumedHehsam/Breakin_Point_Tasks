using System;

class Program
{
    static void Main()
    {
        System.Console.WriteLine("please input your grade ");
        float Grade = float.Parse(Console.ReadLine());
        if (Grade > 100 || Grade < 0)
        {
            Console.WriteLine("Please Input Valid Grade");
            return ;
        }
        if (Grade >= 50)
        {
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("You Passed");
        }
        else
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("You Failed");
        }
    }
}
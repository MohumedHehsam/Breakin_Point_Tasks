using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Input Price Before Taxes");
        float price =float.Parse(Console.ReadLine());
        float taxes= (float).14*price;

        Console.WriteLine($"Price : {price:c1} + {taxes:c1} = {price+taxes:c1}");
    }

}
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("input salary before bonus ");
        float salary = float.Parse(Console.ReadLine());
        float bonus = 0;

        if (salary > 7000)
        {
            bonus = 1000;
        }
        else if (salary > 4000)
        {
            bonus = 500;
        }
        Console.WriteLine($"Salary : {salary} With {bonus} Bnous = {salary+bonus:c1}");
    }

}
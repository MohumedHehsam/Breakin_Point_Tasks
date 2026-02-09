using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter BillAmount");
        decimal BillAmount = decimal.Parse(Console.ReadLine());
        decimal discount =0 ;
        if (BillAmount > 1000)
        {
            discount=10;
        }
        else if (BillAmount>500)
        {
            discount=5;
        }
        else
        {
            discount=0;
        }

        Console.WriteLine($"Bill:{BillAmount:c1} With {discount/100:p1} Discount = {BillAmount*(100-discount)/100:C1}(Total)");

    }

}
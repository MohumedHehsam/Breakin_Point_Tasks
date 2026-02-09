using System;

class Program
{
    static void Main()
    {

        int attemps = 3;
        while (attemps > 0)
        {
            Console.WriteLine($"you have {attemps} attemps left");

            if (CheckSucceed())
                break;
            else
            {
                attemps--;
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Wrong , try again");
                Console.ForegroundColor =ConsoleColor.White;
            }
        }

        if (attemps > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Correct");
        }
        else
        {

            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(" Attemps Exceeded , Account Is Locked");
        }

    }

    static bool CheckSucceed()
    {

        System.Console.WriteLine("Please input username");
        string userName = Console.ReadLine();
        System.Console.WriteLine("Please input Password");
        string password = Console.ReadLine();

        return userName == "admin" && password == "1234" ? true : false;
    }

}
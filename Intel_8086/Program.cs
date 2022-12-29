using System;

namespace Intel_8086
{
    class Program
    {
        static string[,] registers = new string[8, 2] { { "AL", "" }, { "AH", "" }, { "BL", "" }, { "BH", "" }, { "CL", "" }, { "CH", "" }, { "DL", "" }, { "DH", "" } };
        static int Main(string[] args)
        {
            SetRegisters();

            while (true)
            {
                Console.WriteLine("Aktualny status rejestru: ");

                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine(registers[i, 0] + " = " + registers[i, 1]);
                }

                Menu();
                Console.Write("Podaj numer instrukcji którą chcesz wykonać: ");
                string? choice = Console.ReadLine();

                //dwa rejestry na ktorych wykonywane beda operacje
                string firstRegister = "";
                string secondRegister = "";

                switch (choice)
                {
                    case "1":
                        do
                        {
                            Console.WriteLine("Podaj nazwy rejestrów: ");
                            firstRegister = Console.ReadLine();
                            secondRegister = Console.ReadLine();
                        }
                        while (IndexOf(firstRegister.ToUpper()) < 0 || IndexOf(secondRegister.ToUpper()) < 0);
                        MOV(firstRegister.ToUpper(), secondRegister.ToUpper());
                        Console.WriteLine("Nacisnij dowolny klawisz aby kontynuować: ");
                        Console.ReadLine();
                        break;
                    case "2":

                        break;
                    case "9":
                        Console.Clear();
                        return 0;
                        break;
                    default:
                        Console.Write("Nieprawidłowy numer instrukcji");
                        break;
                }
                Console.Clear();
            }
        }
        //////////////////////////////////////////////////////////////////////////////
        //funkcja sprawdzająca poprawnośc inputu
        static bool isHex(string register)
        {
            if (register.Length != 2)
                return false;

            for (int i = 0; i < register.Length; i++)
            {
                char current = register[i];
                if (!(char.IsDigit(current) || (current >= 'A' && current <= 'F')))
                    return false;
            }
            return true;
        }

        static void SetRegisters()
        {
            Console.Clear();
            for (int i = 0; i < 8; i++)
            {
                Console.Write(registers[i, 0] + ": ");
                //sprawdzanie wpisanych wartosci
                registers[i, 1] = Console.ReadLine();
                registers[i, 1] = registers[i, 1].ToUpper();

                while (!isHex(registers[i, 1]))
                {
                    Console.WriteLine("Niepoprawny format. Wpisz liczbę hexadecymalną");
                    Console.Write(registers[i, 0] + ": ");
                    registers[i, 1] = Console.ReadLine();
                    registers[i, 1] = registers[i, 1].ToUpper();
                }
            }
            Console.Clear();
        }

        //Wypisanie menu na ekran
        static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("/////////////////////////////////");
            Console.WriteLine("1. Instrukcja MOV");
            Console.WriteLine("2. Instrukcja XCHG");
            Console.WriteLine("9. Zakończ program");
            Console.WriteLine("/////////////////////////////////");
        }

        //Sprawdzanie miejsca w tabeli które zajmuje dany rejestr
        static int IndexOf(string s)
        {
            int indexOfs = 0;
            for (int i = 0; i < 8; i++)
            {
                if (s == registers[i, 0])
                {
                    indexOfs = i;
                    return indexOfs;
                }
            }
            return -1;
        }

        static void MOV(string firstRegister, string secondRegister)
        {
            registers[IndexOf(firstRegister), 1] = registers[IndexOf(secondRegister), 1];
        }
    }
}
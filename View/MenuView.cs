using System;

namespace Uebungsaufgaben.Views
{
    public static class MenuView
    {
        public static void ShowMainHeader()
        {
            Console.WriteLine("C# Lösungen – 50 Anfänger-Aufgaben");
            Console.WriteLine("Gib eine Aufgabennummer (1..50) ein oder 0 zum Beenden.\n");
        }

        public static int GetTaskNumber()
        {
            Console.Write("Aufgabe #: ");
            if (!int.TryParse(Console.ReadLine(), out int n))
            {
                return -1;
            }
            return n;
        }

        public static void ShowError(string msg)
        {
            Console.WriteLine($"Fehler: {msg}\n");
        }
    }
}

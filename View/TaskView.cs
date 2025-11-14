using System;
using Uebungsaufgaben.Models;

namespace Uebungsaufgaben.Views
{
    public static class TaskView
    {
        public static void ShowTaskInfo(TaskModel task)
        {
            Console.WriteLine("================================");
            Console.WriteLine($"Aufgabe {task.Nummer} – Level {task.Level} ({task.Thema})");
            Console.WriteLine($"Titel: {task.Titel}\n");
            Console.WriteLine("Beschreibung:");
            Console.WriteLine(task.Beschreibung);
            Console.WriteLine("================================\n");
        }

        public static void ShowSolutionCode(string code)
        {
            Console.WriteLine("Lösungscode:");
            Console.WriteLine("--------------------------------");
            Console.WriteLine(code.TrimEnd());
            Console.WriteLine("--------------------------------\n");
        }

        public static void ShowOutputHeader()
        {
            Console.WriteLine("Ausgabe:");
            Console.WriteLine("--------------------------------");
        }

        public static void ShowOutputFooter()
        {
            Console.WriteLine("--------------------------------\n");
            Console.WriteLine("ENTER zum Fortfahren …");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

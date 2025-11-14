using Uebungsaufgaben.Controllers;

namespace Uebungsaufgaben
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var controller = new TaskController();
            controller.Start();
        }
    }
}

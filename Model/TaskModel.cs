namespace Uebungsaufgaben.Models
{
    public class TaskModel
    {
        public int Nummer { get; set; }
        public int Level { get; set; }
        public string Thema { get; set; } = "";
        public string Titel { get; set; } = "";
        public string Beschreibung { get; set; } = "";
    }
}

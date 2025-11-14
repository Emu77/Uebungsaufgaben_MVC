using System;
using System.Collections.Generic;
using System.Globalization;
using Uebungsaufgaben.Models;
using Uebungsaufgaben.Views;

namespace Uebungsaufgaben.Controllers
{
    public class TaskController
    {
        private readonly Dictionary<int, TaskModel> _tasks = new();
        private readonly Dictionary<int, string> _solutionCodes = new();

        private readonly CultureInfo _de = new("de-DE");

        public TaskController()
        {
            LoadTasks();
            LoadSolutionCodes();
        }

        // ======================= Public API =========================

        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                MenuView.ShowMainHeader();

                int n = MenuView.GetTaskNumber();
                if (n == 0)
                    return;

                if (n < 1 || n > 50)
                {
                    MenuView.ShowError("Bitte eine Zahl zwischen 1 und 50 eingeben.");
                    continue;
                }

                if (!_tasks.TryGetValue(n, out var task))
                {
                    MenuView.ShowError("Aufgabe nicht gefunden.");
                    continue;
                }

                TaskView.ShowTaskInfo(task);

                if (_solutionCodes.TryGetValue(n, out var code))
                {
                    TaskView.ShowSolutionCode(code);
                }
                else
                {
                    TaskView.ShowSolutionCode("// Kein Lösungscode hinterlegt.");
                }

                TaskView.ShowOutputHeader();
                try
                {
                    RunTask(n);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler bei der Ausführung: {ex.Message}");
                }
                TaskView.ShowOutputFooter();
            }
        }

        // ======================= Helper: Input ======================

        private double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            var s = Console.ReadLine();
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d)) return d;
            if (double.TryParse(s, NumberStyles.Float, _de, out d)) return d;
            Console.WriteLine("(Ungültige Zahl → 0 angenommen)");
            return 0.0;
        }

        private int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return int.TryParse(Console.ReadLine(), out var i) ? i : 0;
        }

        // ======================= Model-Daten ========================

        private void LoadTasks()
        {
            // Level 1 – Grundlagen / Ausgabe
            _tasks[1] = NewTask(1, 1, "Grundlagen / Ausgabe", "Hallo Welt", "Schreibe ein Programm, das \"Hallo Welt!\" auf der Konsole ausgibt.");
            _tasks[2] = NewTask(2, 1, "Grundlagen / Ausgabe", "Persönliche Begrüßung", "Erstelle ein Programm, das deinen Namen ausgibt.");
            _tasks[3] = NewTask(3, 1, "Grundlagen / Ausgabe", "Mehrere Zeilen", "Gib drei verschiedene Sätze in drei verschiedenen Zeilen aus.");
            _tasks[4] = NewTask(4, 1, "Grundlagen / Ausgabe", "Zahlen ausgeben", "Gib die Zahlen von 1 bis 5 aus (jede in einer eigenen Zeile).");
            _tasks[5] = NewTask(5, 1, "Grundlagen / Ausgabe", "Rechnung ausgeben", "Gib die Rechnung \"5 + 3 = 8\" auf der Konsole aus.");
            _tasks[6] = NewTask(6, 1, "Grundlagen / Ausgabe", "ASCII-Kunst", "Erstelle ein einfaches Bild mit Zeichen (z.B. ein Smiley).");
            _tasks[7] = NewTask(7, 1, "Grundlagen / Ausgabe", "Zitat", "Gib dein Lieblingszitat mit Autor aus.");
            _tasks[8] = NewTask(8, 1, "Grundlagen / Ausgabe", "Formular", "Gib ein kleines Formular mit Name, Alter, Wohnort aus (feste Werte).");
            _tasks[9] = NewTask(9, 1, "Grundlagen / Ausgabe", "Leerzeilen", "Gib Text mit Leerzeilen dazwischen aus, um Abstände zu schaffen.");
            _tasks[10] = NewTask(10, 1, "Grundlagen / Ausgabe", "Kommentare", "Schreibe ein Programm mit mindestens 3 Kommentaren, die erklären, was der Code macht.");

            // Level 2 – Variablen & Datentypen
            _tasks[11] = NewTask(11, 2, "Variablen & Datentypen", "Integer-Variable", "Erstelle eine Variable für dein Alter und gib sie aus.");
            _tasks[12] = NewTask(12, 2, "Variablen & Datentypen", "String-Variable", "Speichere deinen Namen in einer Variable und gib ihn aus.");
            _tasks[13] = NewTask(13, 2, "Variablen & Datentypen", "Mehrere Variablen", "Erstelle Variablen für Name, Alter und Stadt und gib alle aus.");
            _tasks[14] = NewTask(14, 2, "Variablen & Datentypen", "Rechnen mit Variablen", "Erstelle zwei Zahlen-Variablen und gib ihre Summe aus.");
            _tasks[15] = NewTask(15, 2, "Variablen & Datentypen", "Variablen ändern", "Erstelle eine Variable, weise ihr einen Wert zu, ändere den Wert und gib beide Werte aus.");
            _tasks[16] = NewTask(16, 2, "Variablen & Datentypen", "Boolean-Variable", "Erstelle eine bool-Variable (z.B. \"istSchüler\") und gib sie aus.");
            _tasks[17] = NewTask(17, 2, "Variablen & Datentypen", "Double-Variable", "Erstelle eine Variable für eine Kommazahl (z.B. Temperatur) und gib sie aus.");
            _tasks[18] = NewTask(18, 2, "Variablen & Datentypen", "Konstante", "Erstelle eine Konstante für Pi (3.14159) und verwende sie in einer Berechnung.");
            _tasks[19] = NewTask(19, 2, "Variablen & Datentypen", "String-Verkettung", "Kombiniere mehrere Strings zu einem Satz.");
            _tasks[20] = NewTask(20, 2, "Variablen & Datentypen", "Grundrechenarten", "Führe Addition, Subtraktion, Multiplikation und Division mit Variablen aus.");

            // Level 3 – Benutzereingaben
            _tasks[21] = NewTask(21, 3, "Benutzereingaben", "Name eingeben", "Frage den Benutzer nach seinem Namen und begrüße ihn.");
            _tasks[22] = NewTask(22, 3, "Benutzereingaben", "Alter eingeben", "Frage nach dem Alter und gib aus: \"Du bist X Jahre alt.\"");
            _tasks[23] = NewTask(23, 3, "Benutzereingaben", "Zwei Zahlen addieren", "Lass den Benutzer zwei Zahlen eingeben und gib die Summe aus.");
            _tasks[24] = NewTask(24, 3, "Benutzereingaben", "Lieblingsfarbe", "Frage nach der Lieblingsfarbe und gib einen Satz damit aus.");
            _tasks[25] = NewTask(25, 3, "Benutzereingaben", "Geburtsjahrberechnung", "Frage nach dem Alter und berechne das Geburtsjahr (ungefähr).");
            _tasks[26] = NewTask(26, 3, "Benutzereingaben", "Rechteck-Fläche", "Lass Länge und Breite eingeben und berechne die Fläche.");
            _tasks[27] = NewTask(27, 3, "Benutzereingaben", "Temperaturumrechnung", "Rechne Celsius in Fahrenheit um (Formel: F = C * 9/5 + 32).");
            _tasks[28] = NewTask(28, 3, "Benutzereingaben", "Begrüßung personalisieren", "Frage nach Vor- und Nachnamen und gib eine vollständige Begrüßung aus.");
            _tasks[29] = NewTask(29, 3, "Benutzereingaben", "Einfacher Taschenrechner", "Lass zwei Zahlen eingeben und zeige alle Grundrechenarten.");
            _tasks[30] = NewTask(30, 3, "Benutzereingaben", "Kreis-Umfang", "Berechne den Umfang eines Kreises aus dem eingegebenen Radius.");

            // Level 4 – Bedingungen
            _tasks[31] = NewTask(31, 4, "Bedingungen", "Volljährigkeitsprüfung", "Prüfe, ob jemand 18 oder älter ist.");
            _tasks[32] = NewTask(32, 4, "Bedingungen", "Gerade oder ungerade", "Prüfe, ob eine eingegebene Zahl gerade oder ungerade ist.");
            _tasks[33] = NewTask(33, 4, "Bedingungen", "Positiv oder negativ", "Prüfe, ob eine Zahl positiv, negativ oder null ist.");
            _tasks[34] = NewTask(34, 4, "Bedingungen", "Maximum zweier Zahlen", "Finde die größere von zwei eingegebenen Zahlen.");
            _tasks[35] = NewTask(35, 4, "Bedingungen", "Notenberechnung", "Gib bei Punktzahl (0-100) die entsprechende Note aus (1–6).");
            _tasks[36] = NewTask(36, 4, "Bedingungen", "Schaltjahr", "Prüfe, ob ein eingegebenes Jahr ein Schaltjahr ist.");
            _tasks[37] = NewTask(37, 4, "Bedingungen", "Login-System", "Prüfe Benutzername und Passwort und gib \"Zugriff\" oder \"Verweigert\" aus.");
            _tasks[38] = NewTask(38, 4, "Bedingungen", "Rabattrechner", "Gib 10% Rabatt ab einem Einkaufswert von 50€.");
            _tasks[39] = NewTask(39, 4, "Bedingungen", "BMI-Bewertung", "Berechne den BMI und bewerte ihn.");
            _tasks[40] = NewTask(40, 4, "Bedingungen", "Temperatur-Warnung", "Gib eine Warnung aus, wenn die Temperatur über 30°C oder unter 0°C ist.");

            // Level 5 – Schleifen
            _tasks[41] = NewTask(41, 5, "Schleifen", "Zahlen 1–10", "Gib die Zahlen von 1 bis 10 mit einer for-Schleife aus.");
            _tasks[42] = NewTask(42, 5, "Schleifen", "Countdown", "Erstelle einen Countdown von 10 bis 0.");
            _tasks[43] = NewTask(43, 5, "Schleifen", "Gerade Zahlen", "Gib alle geraden Zahlen von 2 bis 20 aus.");
            _tasks[44] = NewTask(44, 5, "Schleifen", "Einmaleins", "Gib das kleine Einmaleins einer eingegebenen Zahl aus (1 bis 10).");
            _tasks[45] = NewTask(45, 5, "Schleifen", "Summe berechnen", "Berechne die Summe aller Zahlen von 1 bis 100.");
            _tasks[46] = NewTask(46, 5, "Schleifen", "Fakultät", "Berechne die Fakultät einer eingegebenen Zahl (z.B. 5! = 120).");
            _tasks[47] = NewTask(47, 5, "Schleifen", "Zahlenraten", "Erstelle ein Zahlenratespiel mit fester Geheimzahl und mehreren Versuchen.");
            _tasks[48] = NewTask(48, 5, "Schleifen", "Durchschnitt", "Lass 5 Zahlen eingeben und berechne den Durchschnitt.");
            _tasks[49] = NewTask(49, 5, "Schleifen", "Quadratzahlen", "Gib die Quadratzahlen von 1 bis 10 aus.");
            _tasks[50] = NewTask(50, 5, "Schleifen", "Passwort-Wiederholung", "Frage so lange nach einem Passwort, bis es korrekt ist.");
        }

        private static TaskModel NewTask(int nr, int level, string thema, string titel, string beschr) =>
            new()
            {
                Nummer = nr,
                Level = level,
                Thema = thema,
                Titel = titel,
                Beschreibung = beschr
            };

        private void LoadSolutionCodes()
        {
            // -------- Level 1 ----------
            _solutionCodes[1] = @"
static void A1()
{
    Console.WriteLine(""Hallo Welt!"");
}";
            _solutionCodes[2] = @"
static void A2()
{
    Console.WriteLine(""Ich heiße Alex."");
}";
            _solutionCodes[3] = @"
static void A3()
{
    Console.WriteLine(""Zeile 1"");
    Console.WriteLine(""Zeile 2"");
    Console.WriteLine(""Zeile 3"");
}";
            _solutionCodes[4] = @"
static void A4()
{
    for (int i = 1; i <= 5; i++)
        Console.WriteLine(i);
}";
            _solutionCodes[5] = @"
static void A5()
{
    Console.WriteLine(""5 + 3 = 8"");
}";
            _solutionCodes[6] = @"
static void A6()
{
    Console.WriteLine(""  _____  "");
    Console.WriteLine("" /     \\"");
    Console.WriteLine(""|  o o  |"");
    Console.WriteLine(""|   ^   |"");
    Console.WriteLine(""| \\\\_/  |"");
    Console.WriteLine("" \\\\_____/"");
}";
            _solutionCodes[7] = @"
static void A7()
{
    Console.WriteLine(""'Stay hungry, stay foolish.' – Steve Jobs"");
}";
            _solutionCodes[8] = @"
static void A8()
{
    Console.WriteLine(""Name: Alex Muster"");
    Console.WriteLine(""Alter: 25"");
    Console.WriteLine(""Wohnort: Berlin"");
}";
            _solutionCodes[9] = @"
static void A9()
{
    Console.WriteLine(""Oben"");
    Console.WriteLine();
    Console.WriteLine(""Mitte"");
    Console.WriteLine();
    Console.WriteLine(""Unten"");
}";
            _solutionCodes[10] = @"
static void A10()
{
    // Kommentar 1: Diese Methode demonstriert Kommentare.
    // Kommentar 2: Kommentare werden vom Compiler ignoriert.
    // Kommentar 3: Benutze sie für Erklärungen.
    Console.WriteLine(""Kommentare im Code – siehe Quelltext."");
}";

            // -------- Level 2 ----------
            _solutionCodes[11] = @"
static void A11()
{
    int alter = 25;
    Console.WriteLine(alter);
}";
            _solutionCodes[12] = @"
static void A12()
{
    string name = ""Alex"";
    Console.WriteLine(name);
}";
            _solutionCodes[13] = @"
static void A13()
{
    string name = ""Alex"";
    int alter = 25;
    string stadt = ""Berlin"";
    Console.WriteLine($""{name}, {alter}, {stadt}"");
}";
            _solutionCodes[14] = @"
static void A14()
{
    int a = 7, b = 5;
    Console.WriteLine(a + b);
}";
            _solutionCodes[15] = @"
static void A15()
{
    int x = 10;
    Console.WriteLine(x);
    x = 20;
    Console.WriteLine(x);
}";
            _solutionCodes[16] = @"
static void A16()
{
    bool istSchueler = false;
    Console.WriteLine(istSchueler);
}";
            _solutionCodes[17] = @"
static void A17()
{
    double t = 21.5;
    Console.WriteLine($""{t} °C"");
}";
            _solutionCodes[18] = @"
static void A18()
{
    const double PI = 3.14159;
    double r = 2;
    Console.WriteLine(PI * r * r);
}";
            _solutionCodes[19] = @"
static void A19()
{
    string s = ""C# "" + ""macht "" + ""Spaß!"";
    Console.WriteLine(s);
}";
            _solutionCodes[20] = @"
static void A20()
{
    int x = 12, y = 4;
    Console.WriteLine($""Add:{x+y}, Sub:{x-y}, Mul:{x*y}, Div:{x/y}"");
}";

            // -------- Level 3 ----------
            _solutionCodes[21] = @"
static void A21()
{
    Console.Write(""Name: "");
    var n = Console.ReadLine();
    Console.WriteLine($""Hallo, {n}!"");
}";
            _solutionCodes[22] = @"
static void A22()
{
    Console.Write(""Alter: "");
    if (int.TryParse(Console.ReadLine(), out int a))
        Console.WriteLine($""Du bist {a} Jahre alt."");
    else
        Console.WriteLine(""Ungültige Eingabe."");
}";
            _solutionCodes[23] = @"
static void A23()
{
    double a = ReadDouble(""Zahl 1: "");
    double b = ReadDouble(""Zahl 2: "");
    Console.WriteLine($""Summe: {a + b}"");
}";
            _solutionCodes[24] = @"
static void A24()
{
    Console.Write(""Lieblingsfarbe: "");
    var f = Console.ReadLine();
    Console.WriteLine($""Schöne Wahl: {f}!"");
}";
            _solutionCodes[25] = @"
static void A25()
{
    int alter = ReadInt(""Alter: "");
    int jahr = DateTime.Now.Year - Math.Max(0, alter);
    Console.WriteLine($""Geburtsjahr (ungefähr): {jahr}"");
}";
            _solutionCodes[26] = @"
static void A26()
{
    double l = ReadDouble(""Länge: "");
    double b = ReadDouble(""Breite: "");
    Console.WriteLine($""Fläche: {l * b}"");
}";
            _solutionCodes[27] = @"
static void A27()
{
    double c = ReadDouble(""°C: "");
    double f = c * 9 / 5 + 32;
    Console.WriteLine($""{c} °C = {f} °F"");
}";
            _solutionCodes[28] = @"
static void A28()
{
    Console.Write(""Vorname: "");
    var v = Console.ReadLine();
    Console.Write(""Nachname: "");
    var n = Console.ReadLine();
    Console.WriteLine($""Willkommen, {v} {n}!"");
}";
            _solutionCodes[29] = @"
static void A29()
{
    double A = ReadDouble(""A: "");
    double B = ReadDouble(""B: "");
    Console.WriteLine($""Add:{A+B}, Sub:{A-B}, Mul:{A*B}, Div:{(B!=0 ? (A/B).ToString() : ""nicht definiert"")}"");
}";
            _solutionCodes[30] = @"
static void A30()
{
    const double PI = 3.14159;
    double r = ReadDouble(""Radius: "");
    Console.WriteLine($""Umfang: {2 * PI * r}"");
}";

            // -------- Level 4 ----------
            _solutionCodes[31] = @"
static void A31()
{
    int alter = ReadInt(""Alter: "");
    Console.WriteLine(alter >= 18 ? ""Volljährig"" : ""Nicht volljährig"");
}";
            _solutionCodes[32] = @"
static void A32()
{
    int z = ReadInt(""Zahl: "");
    Console.WriteLine(z % 2 == 0 ? ""Gerade"" : ""Ungerade"");
}";
            _solutionCodes[33] = @"
static void A33()
{
    int z = ReadInt(""Zahl: "");
    Console.WriteLine(z > 0 ? ""Positiv"" : (z < 0 ? ""Negativ"" : ""Null""));
}";
            _solutionCodes[34] = @"
static void A34()
{
    int a = ReadInt(""A: "");
    int b = ReadInt(""B: "");
    Console.WriteLine(a > b ? a : b);
}";
            _solutionCodes[35] = @"
static void A35()
{
    int p = ReadInt(""Punkte (0-100): "");
    string note = p >= 92 ? ""1"" :
                  p >= 81 ? ""2"" :
                  p >= 67 ? ""3"" :
                  p >= 50 ? ""4"" :
                  p >= 30 ? ""5"" : ""6"";
    Console.WriteLine($""Note: {note}"");
}";
            _solutionCodes[36] = @"
static void A36()
{
    int j = ReadInt(""Jahr: "");
    bool schalt = (j % 400 == 0) || (j % 4 == 0 && j % 100 != 0);
    Console.WriteLine(schalt ? ""Schaltjahr"" : ""Kein Schaltjahr"");
}";
            _solutionCodes[37] = @"
static void A37()
{
    const string USER = ""admin"", PASS = ""1234"";
    Console.Write(""Benutzername: "");
    var u = Console.ReadLine();
    Console.Write(""Passwort: "");
    var p = Console.ReadLine();
    Console.WriteLine(u == USER && p == PASS ? ""Zugriff"" : ""Verweigert"");
}";
            _solutionCodes[38] = @"
static void A38()
{
    double betrag = ReadDouble(""Einkauf (€): "");
    double zahlen = betrag >= 50 ? betrag * 0.9 : betrag;
    Console.WriteLine($""Zu zahlen: {zahlen:F2} €"");
}";
            _solutionCodes[39] = @"
static void A39()
{
    double kg = ReadDouble(""Gewicht (kg): "");
    double m  = ReadDouble(""Größe (m): "");
    double bmi = m > 0 ? kg / (m * m) : 0;
    string kat = bmi < 18.5 ? ""Untergewicht"" :
                 bmi < 25   ? ""Normalgewicht"" :
                 bmi < 30   ? ""Übergewicht"" :
                              ""Adipositas"";
    Console.WriteLine($""BMI: {bmi:F1} – {kat}"");
}";
            _solutionCodes[40] = @"
static void A40()
{
    double t = ReadDouble(""Temperatur (°C): "");
    if (t > 30)      Console.WriteLine(""Warnung: Sehr heiß!"");
    else if (t < 0)  Console.WriteLine(""Warnung: Frost!"");
    else             Console.WriteLine(""Normalbereich."");
}";

            // -------- Level 5 ----------
            _solutionCodes[41] = @"
static void A41()
{
    for (int i = 1; i <= 10; i++)
        Console.Write(i + (i < 10 ? "", "" : ""\n""));
}";
            _solutionCodes[42] = @"
static void A42()
{
    for (int i = 10; i >= 0; i--)
        Console.Write(i + (i > 0 ? "", "" : ""\n""));
}";
            _solutionCodes[43] = @"
static void A43()
{
    for (int i = 2; i <= 20; i += 2)
        Console.Write(i + (i < 20 ? "", "" : ""\n""));
}";
            _solutionCodes[44] = @"
static void A44()
{
    int n = ReadInt(""Zahl: "");
    for (int i = 1; i <= 10; i++)
        Console.WriteLine($""{n} x {i} = {n * i}"");
}";
            _solutionCodes[45] = @"
static void A45()
{
    int sum = 0;
    for (int i = 1; i <= 100; i++)
        sum += i;
    Console.WriteLine(sum);
}";
            _solutionCodes[46] = @"
static void A46()
{
    int f = ReadInt(""n: "");
    long fak = 1;
    for (int i = 2; i <= f; i++)
        fak *= i;
    Console.WriteLine(fak);
}";
            _solutionCodes[47] = @"
static void A47()
{
    const int geheim = 17;
    Console.WriteLine(""Ratespiel (1..50), 5 Versuche"");
    for (int v = 1; v <= 5; v++)
    {
        int t = ReadInt($""Versuch {v}: "");
        if (t == geheim)
        {
            Console.WriteLine(""Richtig!"");
            return;
        }
        Console.WriteLine(t < geheim ? ""Zu klein."" : ""Zu groß."");
    }
    Console.WriteLine($""Leider nein. Die Zahl war {geheim}."");
}";
            _solutionCodes[48] = @"
static void A48()
{
    double sum = 0;
    for (int i = 1; i <= 5; i++)
        sum += ReadDouble($""Zahl {i}: "");
    Console.WriteLine($""Durchschnitt: {sum / 5}"");
}";
            _solutionCodes[49] = @"
static void A49()
{
    for (int i = 1; i <= 10; i++)
        Console.Write((i * i) + (i < 10 ? "", "" : ""\n""));
}";
            _solutionCodes[50] = @"
static void A50()
{
    const string pw = ""geheim"";
    while (true)
    {
        Console.Write(""Passwort: "");
        var e = Console.ReadLine();
        if (e == pw)
        {
            Console.WriteLine(""Korrekt!"");
            break;
        }
        Console.WriteLine(""Falsch, erneut versuchen."");
    }
}";
        }

        // ======================= RunTask: echte Ausführung =========

        private void RunTask(int n)
        {
            switch (n)
            {
                // Level 1
                case 1: A1(); break;
                case 2: A2(); break;
                case 3: A3(); break;
                case 4: A4(); break;
                case 5: A5(); break;
                case 6: A6(); break;
                case 7: A7(); break;
                case 8: A8(); break;
                case 9: A9(); break;
                case 10: A10(); break;

                // Level 2
                case 11: A11(); break;
                case 12: A12(); break;
                case 13: A13(); break;
                case 14: A14(); break;
                case 15: A15(); break;
                case 16: A16(); break;
                case 17: A17(); break;
                case 18: A18(); break;
                case 19: A19(); break;
                case 20: A20(); break;

                // Level 3
                case 21: A21(); break;
                case 22: A22(); break;
                case 23: A23(); break;
                case 24: A24(); break;
                case 25: A25(); break;
                case 26: A26(); break;
                case 27: A27(); break;
                case 28: A28(); break;
                case 29: A29(); break;
                case 30: A30(); break;

                // Level 4
                case 31: A31(); break;
                case 32: A32(); break;
                case 33: A33(); break;
                case 34: A34(); break;
                case 35: A35(); break;
                case 36: A36(); break;
                case 37: A37(); break;
                case 38: A38(); break;
                case 39: A39(); break;
                case 40: A40(); break;

                // Level 5
                case 41: A41(); break;
                case 42: A42(); break;
                case 43: A43(); break;
                case 44: A44(); break;
                case 45: A45(); break;
                case 46: A46(); break;
                case 47: A47(); break;
                case 48: A48(); break;
                case 49: A49(); break;
                case 50: A50(); break;

                default:
                    Console.WriteLine("Aufgabe nicht implementiert.");
                    break;
            }
        }

        // ======================= A1 .. A50 ==========================

        // Level 1
        private void A1() { Console.WriteLine("Hallo Welt!"); }
        private void A2() { Console.WriteLine("Ich heiße Alex."); }
        private void A3() { Console.WriteLine("Zeile 1"); Console.WriteLine("Zeile 2"); Console.WriteLine("Zeile 3"); }
        private void A4() { for (int i = 1; i <= 5; i++) Console.WriteLine(i); }
        private void A5() { Console.WriteLine("5 + 3 = 8"); }
        private void A6()
        {
            Console.WriteLine("  _____  ");
            Console.WriteLine(" /     \\");
            Console.WriteLine("|  o o  |");
            Console.WriteLine("|   ^   |");
            Console.WriteLine("| \\\\_/  |");
            Console.WriteLine(" \\\\_____/");
        }
        private void A7() { Console.WriteLine("\"Stay hungry, stay foolish.\" – Steve Jobs"); }
        private void A8() { Console.WriteLine("Name: Alex Muster"); Console.WriteLine("Alter: 25"); Console.WriteLine("Wohnort: Berlin"); }
        private void A9() { Console.WriteLine("Oben"); Console.WriteLine(); Console.WriteLine("Mitte"); Console.WriteLine(); Console.WriteLine("Unten"); }
        private void A10() { Console.WriteLine("Kommentare im Code – siehe Quelltext."); }

        // Level 2
        private void A11() { int alter = 25; Console.WriteLine(alter); }
        private void A12() { string name = "Alex"; Console.WriteLine(name); }
        private void A13() { string name = "Alex"; int alter = 25; string stadt = "Berlin"; Console.WriteLine($"{name}, {alter}, {stadt}"); }
        private void A14() { int a = 7, b = 5; Console.WriteLine(a + b); }
        private void A15() { int x = 10; Console.WriteLine(x); x = 20; Console.WriteLine(x); }
        private void A16() { bool istSchueler = false; Console.WriteLine(istSchueler); }
        private void A17() { double t = 21.5; Console.WriteLine($"{t} °C"); }
        private void A18() { const double PI = 3.14159; double r = 2; Console.WriteLine(PI * r * r); }
        private void A19() { string s = "C# " + "macht " + "Spaß!"; Console.WriteLine(s); }
        private void A20() { int x = 12, y = 4; Console.WriteLine($"Add:{x + y}, Sub:{x - y}, Mul:{x * y}, Div:{x / y}"); }

        // Level 3
        private void A21() { Console.Write("Name: "); var n = Console.ReadLine(); Console.WriteLine($"Hallo, {n}!"); }
        private void A22() { Console.Write("Alter: "); if (int.TryParse(Console.ReadLine(), out int a)) Console.WriteLine($"Du bist {a} Jahre alt."); else Console.WriteLine("Ungültige Eingabe."); }
        private void A23() { double a = ReadDouble("Zahl 1: "); double b = ReadDouble("Zahl 2: "); Console.WriteLine($"Summe: {a + b}"); }
        private void A24() { Console.Write("Lieblingsfarbe: "); var f = Console.ReadLine(); Console.WriteLine($"Schöne Wahl: {f}!"); }
        private void A25() { int alter = ReadInt("Alter: "); int jahr = DateTime.Now.Year - Math.Max(0, alter); Console.WriteLine($"Geburtsjahr (ungefähr): {jahr}"); }
        private void A26() { double l = ReadDouble("Länge: "); double b = ReadDouble("Breite: "); Console.WriteLine($"Fläche: {l * b}"); }
        private void A27() { double c = ReadDouble("°C: "); double f = c * 9 / 5 + 32; Console.WriteLine($"{c} °C = {f} °F"); }
        private void A28() { Console.Write("Vorname: "); var v = Console.ReadLine(); Console.Write("Nachname: "); var n = Console.ReadLine(); Console.WriteLine($"Willkommen, {v} {n}!"); }
        private void A29() { double A = ReadDouble("A: "); double B = ReadDouble("B: "); Console.WriteLine($"Add:{A + B}, Sub:{A - B}, Mul:{A * B}, Div:{(B != 0 ? (A / B).ToString() : "nicht definiert")}"); }
        private void A30() { const double PI = 3.14159; double r = ReadDouble("Radius: "); Console.WriteLine($"Umfang: {2 * PI * r}"); }

        // Level 4
        private void A31() { int alter = ReadInt("Alter: "); Console.WriteLine(alter >= 18 ? "Volljährig" : "Nicht volljährig"); }
        private void A32() { int z = ReadInt("Zahl: "); Console.WriteLine(z % 2 == 0 ? "Gerade" : "Ungerade"); }
        private void A33() { int z = ReadInt("Zahl: "); Console.WriteLine(z > 0 ? "Positiv" : (z < 0 ? "Negativ" : "Null")); }
        private void A34() { int a = ReadInt("A: "); int b = ReadInt("B: "); Console.WriteLine(a > b ? a : b); }
        private void A35() { int p = ReadInt("Punkte (0-100): "); string note = p >= 92 ? "1" : p >= 81 ? "2" : p >= 67 ? "3" : p >= 50 ? "4" : p >= 30 ? "5" : "6"; Console.WriteLine($"Note: {note}"); }
        private void A36() { int j = ReadInt("Jahr: "); bool schalt = (j % 400 == 0) || (j % 4 == 0 && j % 100 != 0); Console.WriteLine(schalt ? "Schaltjahr" : "Kein Schaltjahr"); }
        private void A37() { const string USER = "admin", PASS = "1234"; Console.Write("Benutzername: "); var u = Console.ReadLine(); Console.Write("Passwort: "); var p = Console.ReadLine(); Console.WriteLine(u == USER && p == PASS ? "Zugriff" : "Verweigert"); }
        private void A38() { double betrag = ReadDouble("Einkauf (€): "); double zahlen = betrag >= 50 ? betrag * 0.9 : betrag; Console.WriteLine($"Zu zahlen: {zahlen:F2} €"); }
        private void A39() { double kg = ReadDouble("Gewicht (kg): "); double m = ReadDouble("Größe (m): "); double bmi = m > 0 ? kg / (m * m) : 0; string kat = bmi < 18.5 ? "Untergewicht" : bmi < 25 ? "Normalgewicht" : bmi < 30 ? "Übergewicht" : "Adipositas"; Console.WriteLine($"BMI: {bmi:F1} – {kat}"); }
        private void A40() { double t = ReadDouble("Temperatur (°C): "); if (t > 30) Console.WriteLine("Warnung: Sehr heiß!"); else if (t < 0) Console.WriteLine("Warnung: Frost!"); else Console.WriteLine("Normalbereich."); }

        // Level 5
        private void A41() { for (int i = 1; i <= 10; i++) Console.Write(i + (i < 10 ? ", " : "\n")); }
        private void A42() { for (int i = 10; i >= 0; i--) Console.Write(i + (i > 0 ? ", " : "\n")); }
        private void A43() { for (int i = 2; i <= 20; i += 2) Console.Write(i + (i < 20 ? ", " : "\n")); }
        private void A44() { int n = ReadInt("Zahl: "); for (int i = 1; i <= 10; i++) Console.WriteLine($"{n} x {i} = {n * i}"); }
        private void A45() { int sum = 0; for (int i = 1; i <= 100; i++) sum += i; Console.WriteLine(sum); }
        private void A46() { int f = ReadInt("n: "); long fak = 1; for (int i = 2; i <= f; i++) fak *= i; Console.WriteLine(fak); }
        private void A47()
        {
            const int geheim = 17;
            Console.WriteLine("Ratespiel (1..50), 5 Versuche");
            for (int v = 1; v <= 5; v++)
            {
                int t = ReadInt($"Versuch {v}: ");
                if (t == geheim) { Console.WriteLine("Richtig!"); return; }
                Console.WriteLine(t < geheim ? "Zu klein." : "Zu groß.");
            }
            Console.WriteLine($"Leider nein. Die Zahl war {geheim}.");
        }
        private void A48() { double sum = 0; for (int i = 1; i <= 5; i++) sum += ReadDouble($"Zahl {i}: "); Console.WriteLine($"Durchschnitt: {sum / 5}"); }
        private void A49() { for (int i = 1; i <= 10; i++) Console.Write((i * i) + (i < 10 ? ", " : "\n")); }
        private void A50()
        {
            const string pw = "geheim";
            while (true)
            {
                Console.Write("Passwort: "); var e = Console.ReadLine();
                if (e == pw) { Console.WriteLine("Korrekt!"); break; }
                Console.WriteLine("Falsch, erneut versuchen.");
            }
        }
    }
}

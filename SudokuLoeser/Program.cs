using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLoeser
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] feld = { { 8, 0, 0, 1, 0, 0, 7, 0, 5 }, { 5, 0, 0, 0, 0, 0, 0, 4, 1 }, { 0, 0, 0, 2, 0, 8, 9, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1, 3, 0 }, { 0, 0, 0, 8, 6, 9, 0, 0, 0 }, { 2, 7, 6, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 3, 1, 9 }, { 9, 8, 2, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 5, 9, 4, 0, 0, 7 } };

            Console.WriteLine("Ausgangsfeld: ");
            Ausgabe(feld);
            Console.WriteLine();

            Console.WriteLine(Loesen(feld));

            Console.WriteLine("Ergebnis: ");
            Ausgabe(feld);
            Console.WriteLine();
            Console.ReadKey();
        }

        static bool Loesen(int[,] feld)
        {
            int letztesX = -1;
            int letztesY = -1;
            //letzte position merken
            for (int x = 0; x < feld.GetLength(0); x++)
            {
                for (int y = 0; y < feld.GetLength(1); y++)
                {
                    //Wenn eine Freie Zahl entdeckt wurde
                    if (feld[x, y] == 0)
                    {
                        letztesX = x; //Letzte X Koordinate Merken
                        letztesY = y; //Letzte Y Koordinate Merken
                        break;
                    }
                }
            }

            //Ende des Spielfeldes wurde erreicht, also keine freie Zahl mehr entdeckt-
            if (letztesX == -1 && letztesY == -1)
            {
                return true;
            }

            //Hier dann Wert Setzen für Loeser
            for (int i = 1; i < 10; i++)
            {
                //Wert von i bei Leerer Stelle x,y eintragen
                feld[letztesX, letztesY] = i;
                //Prüfen ob das Eintragen gültig war, wenn nicht dann nächstes i probieren
                if (!(pruefeSpalte(feld, letztesX) && pruefeZeile(feld, letztesY) && pruefeTeilfeld(feld, letztesX, letztesY)))
                {
                    continue;
                }
                else
                {
                    /* Wenn gültig war Rufe dich selbst auf und Speichere das Ergebnis dises aufrufes
                     * Das ist wichtig, da beim erreichen des Feldes "True" ausgegeben wird,
                     * dieses true wird nun nach all den Aufrufen durch sich selbst ausgewertet
                     * war es true, dann gibt es nun hier auch true zurück an den Eigenaufruf vor sich
                     * und dieser dann wieder an den Eigenaufruf davor etc.
                     */
                    bool status = Loesen(feld);
                    if (status)
                    {
                        return true;
                    }
                }

            }
            /* Konnte keine Gültige Zahl gefunden werden, gib die stelle wieder frei und melde false
             * Das ist wichtig, weil dann in der Status-Prüfung nach den Eigenaufrufen kein "true"
             * zurück gemeldet wird an den Eigenaufruf vor sich, sondern einen Schritt zurück gegangen
             * und ein neuer durchlauf mit der nächsten Zahl gestartet werden muss
             */
            feld[letztesX, letztesY] = 0;
            return false;
        }

        static bool pruefeSpalte(int[,] feld, int x)
        {
            int[] erg = new int[feld.GetLength(1)];
            for (int i = 0; i < erg.Length; i++)
            {
                erg[i] = feld[x, i];
            }
            return Pruefen(erg);
        }

        static bool pruefeZeile(int[,] feld, int y)
        {
            int[] erg = new int[feld.GetLength(0)];
            for (int i = 0; i < erg.Length; i++)
            {
                erg[i] = feld[i, y];
            }
            return Pruefen(erg);
        }

        static bool pruefeTeilfeld(int[,] feld, int x, int y)
        {

            int[][] teilFelder = new int[9][];
            teilFelder[0] = new int[] { feld[0, 0], feld[0, 1], feld[0, 2], feld[1, 0], feld[1, 1], feld[1, 2], feld[2, 0], feld[2, 1], feld[2, 2] };
            teilFelder[1] = new int[] { feld[3, 0], feld[3, 1], feld[3, 2], feld[4, 0], feld[4, 1], feld[4, 2], feld[5, 0], feld[5, 1], feld[5, 2] };
            teilFelder[2] = new int[] { feld[6, 0], feld[6, 1], feld[6, 2], feld[7, 0], feld[7, 1], feld[7, 2], feld[8, 0], feld[8, 1], feld[8, 2] };
            teilFelder[3] = new int[] { feld[0, 3], feld[0, 4], feld[0, 5], feld[1, 3], feld[1, 4], feld[1, 5], feld[2, 3], feld[2, 4], feld[2, 5] };
            teilFelder[4] = new int[] { feld[3, 3], feld[3, 4], feld[3, 5], feld[4, 3], feld[4, 4], feld[4, 5], feld[5, 3], feld[5, 4], feld[5, 5] };
            teilFelder[5] = new int[] { feld[6, 3], feld[6, 4], feld[6, 5], feld[7, 3], feld[7, 4], feld[7, 5], feld[8, 3], feld[8, 4], feld[8, 5] };
            teilFelder[6] = new int[] { feld[0, 6], feld[0, 7], feld[0, 8], feld[1, 6], feld[1, 7], feld[1, 8], feld[2, 6], feld[2, 7], feld[2, 8] };
            teilFelder[7] = new int[] { feld[3, 6], feld[3, 7], feld[3, 8], feld[4, 6], feld[4, 7], feld[4, 8], feld[5, 6], feld[5, 7], feld[5, 8] };
            teilFelder[8] = new int[] { feld[6, 6], feld[6, 7], feld[6, 8], feld[7, 6], feld[7, 7], feld[7, 8], feld[8, 6], feld[8, 7], feld[8, 8] };

            int teilFeld;
            if (x < 3)
            {
                if (y < 3)
                {
                    teilFeld = 0;
                }
                else if (y > 2 && y < 6)
                {
                    teilFeld = 3;
                }
                else
                {
                    teilFeld = 6;
                }
            }
            else if (x > 2 && x < 6)
            {
                if (y < 3)
                {
                    teilFeld = 1;
                }
                else if (y > 2 && y < 6)
                {
                    teilFeld = 4;
                }
                else
                {
                    teilFeld = 7;
                }
            }
            else
            {
                if (y < 3)
                {
                    teilFeld = 2;
                }
                else if (y > 2 && y < 6)
                {
                    teilFeld = 5;
                }
                else
                {
                    teilFeld = 8;
                }
            }

            return Pruefen(teilFelder[teilFeld]);
        }
        static void Ausgabe(int[,] feld)
        {
            Console.Clear();
            Console.WriteLine("    1 2 3   4 5 6   7 8 9 x");
            Console.WriteLine("   ----------------------- ");
            for (int zeile = 0; zeile < 9; zeile++)
            {
                string zAktuell = AusgabeZeile(feld, zeile);
                if (zAktuell.Contains("0"))
                {
                    zAktuell = zAktuell.Replace("0", " ");
                }
                if (zeile == 2 || zeile == 5)
                {
                    Console.Write("{0} | {1} {2} {3} | {4} {5} {6} | {7} {8} {9} |\n", zeile + 1, zAktuell[0], zAktuell[1], zAktuell[2], zAktuell[3], zAktuell[4], zAktuell[5], zAktuell[6], zAktuell[7], zAktuell[8]);
                    Console.WriteLine("  |-----------------------|");
                }
                else
                {
                    Console.Write("{0} | {1} {2} {3} | {4} {5} {6} | {7} {8} {9} |\n", zeile + 1, zAktuell[0], zAktuell[1], zAktuell[2], zAktuell[3], zAktuell[4], zAktuell[5], zAktuell[6], zAktuell[7], zAktuell[8]);
                }
            }
            Console.WriteLine("y  ----------------------- ");
            Console.WriteLine();
        }

        static string AusgabeZeile(int[,] feld, int zeile)
        {
            string zeileAus = "";

            for (int s = 0; s < 9; s++)
            {
                zeileAus += feld[s, zeile];
            }

            return zeileAus;
        }

        static bool Pruefen(int[] feld)
        {
            for (int a = 0; a < feld.Length; a++)
            {
                int anzahl = 0;
                for (int z = 0; z < feld.Length; z++)
                {
                    if (feld[a] == feld[z] && feld[z] != 0)
                    {
                        anzahl++;
                        if (anzahl > 1)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRaya
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] tablero = new char[3, 3];
            InicializarTablero(ref tablero);

            string nombre1 = "";
            string nombre2 = "";

            Console.WriteLine("¡Bienvenido al Tres en Raya de Joan Antoni!");
            Console.WriteLine("___________________________________________");

            Console.WriteLine();

            //Pedimos el nombre al jugdor 1
            while(nombre1.Length < 2)
            {
                Console.WriteLine("Jugador 1. Jugarás con la X. Introduce tu nombre:");
                nombre1 = Console.ReadLine();
                Console.WriteLine();

                if(nombre1.Length < 2)
                {
                    Console.WriteLine("Introduzca un nombre más largo");
                }
            }

            //Pedimos el nombre al jugador 2
            while (nombre2.Length < 2)
            {
                Console.WriteLine("Jugador 2. Jugarás con el O. Introduce tu nombre:");
                nombre2 = Console.ReadLine();
                Console.WriteLine();

                if (nombre2.Length < 2)
                {
                    Console.WriteLine("Introduzca un nombre más largo");
                }
            }

            bool esTurnoJugadorUno = true;

            while(!HayGanador(tablero))
            {
                Console.Clear();

                Console.WriteLine("¡Bienvenido al Tres en Raya de Joan Antoni!");
                Console.WriteLine("___________________________________________");
                Console.WriteLine();

                Console.WriteLine(TableroVisual(tablero));
                Console.WriteLine();

                string nombrejugador = "";

                if(esTurnoJugadorUno)
                {
                    nombrejugador = nombre1;
                }
                else
                {
                    nombrejugador = nombre2;
                }

                string coordenada = "";

                while(!EsCoordenadaValida(coordenada) || EstaYaOcupada(tablero, coordenada))
                {
                    Console.WriteLine($"{nombrejugador}, introduce la coordenada donde quieres jugar");
                    Console.WriteLine("Por ejemplo, A1.");

                    coordenada = Console.ReadLine().ToUpper();

                    if(!EsCoordenadaValida(coordenada))
                    {
                        Console.WriteLine();
                        Console.WriteLine("La coordenada no es válida");
                        Console.WriteLine();
                    }

                    if(EstaYaOcupada(tablero, coordenada))
                    {
                        Console.WriteLine();
                        Console.WriteLine("La coordenada ya ha sido usada en esta partida. Introduzca otra.");
                        Console.WriteLine();
                    }
                }

                char caracterUsado = ' ';

                if(esTurnoJugadorUno)
                {
                    caracterUsado = 'X';
                }
                else
                {
                    caracterUsado = 'O';
                }

                PonerCoordenadas(ref tablero, coordenada, caracterUsado);

                if(HayGanador(tablero))
                {
                    break;
                }

                if(EstaTableroCompleto(tablero))
                {
                    break;
                }

                esTurnoJugadorUno = !esTurnoJugadorUno;
            }

            if(HayGanador(tablero))
            {
                Console.Clear();

                Console.WriteLine("¡Bienvenido al Tres en Raya de Joan Antoni!");
                Console.WriteLine("___________________________________________");
                Console.WriteLine();

                Console.WriteLine(TableroVisual(tablero));
                Console.WriteLine();

                Console.WriteLine("¡Fin del juego!");
                if(Ganador(tablero)  == 'X')
                {
                    Console.WriteLine($"{nombre1} ha ganado esta vez.");
                }
                else
                {
                    Console.WriteLine($"{nombre2} ha ganado esta vez.");
                }
            }

            if (EstaTableroCompleto(tablero))
            {
                Console.Clear();

                Console.WriteLine("¡Bienvenido al Tres en Raya de Joan Antoni!");
                Console.WriteLine("___________________________________________");
                Console.WriteLine();

                Console.WriteLine(TableroVisual(tablero));
                Console.WriteLine();

                Console.WriteLine("¡Fin del juego!");
                Console.WriteLine("Empate. No ha ganado nadie");               
            }
            Console.ReadKey();

        }

        static void InicializarTablero(ref char[,] tablero)
        {
            for(int contador1 = 0; contador1 < 3; contador1++)
            {
                for (int contador2 = 0; contador2 < 3; contador2++)
                {
                    tablero[contador1, contador2] = ' ';
                }
            }
        }

        static string TableroVisual(char[,] tablero)
        {
            string tv = "";
            tv = "    1   2   3" + Environment.NewLine;
            tv +=  "  ┌───┬───┬───┐" + Environment.NewLine;
            tv += $"A │ {tablero[0,0]} │ {tablero[0, 1]} │ {tablero[0, 2]} │" + Environment.NewLine;
            tv += "  ├───┼───┼───┤" + Environment.NewLine;
            tv += $"B │ {tablero[1, 0]} │ {tablero[1, 1]} │ {tablero[1, 2]} │" + Environment.NewLine;
            tv += "  ├───┼───┼───┤" + Environment.NewLine;
            tv += $"C │ {tablero[2, 0]} │ {tablero[2, 1]} │ {tablero[2, 2]} │" + Environment.NewLine;
            tv += "  └───┴───┴───┘";

            return tv;
        }

        static char Ganador(char[,] tablero)
        {
            //HORIZONTALES
            if(tablero[0,0] == tablero[0,1] && tablero[0,1] == tablero[0,2] && tablero[0,0] != ' ')
            {
                return tablero[0,0];
            }
            if (tablero[1, 0] == tablero[1, 1] && tablero[1, 1] == tablero[1, 2] && tablero[1, 0] != ' ')
            {
                return tablero[1,0];
            }
            if (tablero[2, 0] == tablero[2, 1] && tablero[2, 1] == tablero[2, 2] && tablero[2, 0] != ' ')
            {
                return tablero[2,0];
            }
            //VERTICALES
            if (tablero[0, 0] == tablero[1, 0] && tablero[1, 0] == tablero[2, 0] && tablero[0, 0] != ' ')
            {
                return tablero[0, 0];
            }
            if (tablero[0, 1] == tablero[1, 1] && tablero[1, 1] == tablero[2, 1] && tablero[0, 1] != ' ')
            {
                return tablero[0, 1];
            }
            if (tablero[0, 2] == tablero[1, 2] && tablero[1, 2] == tablero[2, 2] && tablero[0, 2] != ' ')
            {
                return tablero[0, 2];
            }
            // DIAGONALES
            if (tablero[0, 0] == tablero[1, 1] && tablero[1, 1] == tablero[2, 2] && tablero[0, 0] != ' ')
            {
                return tablero[0, 0];
            }
            if (tablero[0, 2] == tablero[1, 1] && tablero[1, 2] == tablero[2, 0] && tablero[0, 2] != ' ')
            {
                return tablero[0, 0];
            }

            return ' ';
        }

        static bool HayGanador(char[,] tablero)
        {
            return Ganador(tablero) != ' ';
        }

        static bool EstaYaOcupada(char[,] tablero, int x, int y)
        {
            if(x < 0 || x > 2)
            {
                throw new ArgumentException("El valor de x debe ser 0, 1 o 2", "x");
            }
            if (y < 0 || y > 2)
            {
                throw new ArgumentException("El valor de y debe ser 0, 1 o 2", "y");
            }

            return tablero[x, y] != ' ';
        }

        static bool EstaYaOcupada(char[,] tablero, string coordenada)
        {
            coordenada = coordenada.ToUpper();

            switch (coordenada)
            {
                case "A1":
                    return EstaYaOcupada(tablero, 0, 0);
                case "A2":
                    return EstaYaOcupada(tablero, 0, 1);
                case "A3":
                    return EstaYaOcupada(tablero, 0, 2);
                case "B1":
                    return EstaYaOcupada(tablero, 1, 0);
                case "B2":
                    return EstaYaOcupada(tablero, 1, 1);
                case "B3":
                    return EstaYaOcupada(tablero, 1, 2);
                case "C1":
                    return EstaYaOcupada(tablero, 2, 0);
                case "C2":
                    return EstaYaOcupada(tablero, 2, 1);
                case "C3":
                    return EstaYaOcupada(tablero, 2, 2);
            }
            return false;
        }

        static void PonerCoordenadas(ref char[,] tablero, string coordenada, char letra)
        {
            coordenada = coordenada.ToUpper();

           switch(coordenada)
            {
                case "A1":
                    tablero[0, 0] = letra;
                    return;
                case "A2":
                    tablero[0, 1] = letra;
                    return;
                case "A3":
                    tablero[0, 2] = letra;
                    return;
                case "B1":
                    tablero[1, 0] = letra;
                    return;
                case "B2":
                    tablero[1, 1] = letra;
                    return;
                case "B3":
                    tablero[1, 2] = letra;
                    return;
                case "C1":
                    tablero[2, 0] = letra;
                    return;
                case "C2":
                    tablero[2, 1] = letra;
                    return;
                case "C3":
                    tablero[2, 2] = letra;
                    return;
            }
        }

        static bool EsCoordenadaValida(string coordenada)
        {
            switch (coordenada)
            {
                case "A1":
                case "A2":
                case "A3":
                case "B1":
                case "B2":
                case "B3":
                case "C1":
                case "C2":
                case "C3":
                    return true;
                default:
                    return false;
            }
        }

        static bool EstaTableroCompleto(char[,] tablero)
        {
            if(tablero[0,0] != ' ' && tablero[0,1] != ' ' && tablero[0, 2] != ' '
                && tablero[1, 0] != ' ' && tablero[1, 1] != ' ' && tablero[1, 2] != ' '
                && tablero[2, 0] != ' ' && tablero[2, 1] != ' ' && tablero[2, 2] != ' ')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

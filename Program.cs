using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokupka_Biletov
{

        class Program
        {
            static int Saali_suurus()
            {
                Console.WriteLine("Vali saali suurus: 1,2,3");//Выбор пользователя, размер зала
                int suurus = int.Parse(Console.ReadLine());
                return suurus;
            }
            static int[,] saal = new int[,] { };//создаётся двухмерный массив, для дальнейнего заполнения, либо 0 или 1.
            static int kohad, read;
            static void Saali_taitmine(int suurus)
            {
                Random rnd = new Random();//Выбирается рандомное число
                if (suurus == 1)
                {
                    kohad = 20;
                    read = 10;
                }
                else if (suurus == 2)
                {
                    kohad = 20;
                    read = 20;
                }
                else
                {
                    kohad = 30;
                    read = 20;
                }
                saal = new int[read, kohad];
                for (int rida = 0; rida < read; rida++)//цикл для заполнения зала
                {
                    for (int koht = 0; koht < kohad; koht++)
                    {
                        saal[rida, koht] = rnd.Next(0, 2);
                    }
                }
            }
            static bool Celovek()//функция для выбора одного места, при помощи пользователя
            {
                Console.WriteLine("Rida");
                int pileti_rida = int.Parse(Console.ReadLine());//пользователь пищет ряд
                Console.WriteLine("Kohad");
                int pileti_koht = int.Parse(Console.ReadLine());//пользователь пищет место
                if (saal[pileti_rida - 1, pileti_koht - 1] == 0)
                {
                    saal[pileti_rida - 1, pileti_koht - 1] = 1;
                    Console.ForegroundColor = ConsoleColor.Green;//изменяет цвет шрифта на зелённый
                    Console.WriteLine("Otsid koha");
                    Console.ResetColor();
                    return true;
                }
                else//если на этом месте стоит цифра 1, то будет написано что место заннято
                {
                    Console.ForegroundColor = ConsoleColor.Red;//изменяет цвет шрифта на красный
                    Console.WriteLine("Koht on kinni");
                    Console.ResetColor();
                    return false;
                }
            }
            static void Saal_ekraanile()
            {
                Console.WriteLine("  ");
                Console.Write("       ");
                for (int koht = 0; koht < kohad; koht++)
                {

                    if (koht.ToString().Length == 2)
                    {
                        Console.Write(" {0}", koht + 1); //пишет место
                    }
                    else
                    {
                        Console.Write("  {0}", koht + 1);
                    }
                }
                Console.WriteLine("\n");
                for (int rida = 0; rida < read; rida++)
                {
                    Console.Write($"{rida + 1} rida:  ");// пишет ряд
                    for (int koht = 0; koht < kohad; koht++)
                    {
                        Console.Write(saal[rida, koht] + "  ");
                    }

                    Console.WriteLine("");
                }
            }

            static bool Muuk()
            {
                Console.WriteLine("Rida");
                int pileti_rida = int.Parse(Console.ReadLine());//пользователь пишет ряд
                Console.WriteLine("Mitu piletid");
                int mitu = int.Parse(Console.ReadLine());
                int[] ost = new int[mitu];// дополнительный массив для запоминания mitu.
                int p = (kohad - mitu) / 2;// вычисление середины
                bool t = false;
                int k = 0;
                do
                {
                    if (saal[pileti_rida, p] == 0)
                    {
                        ost[k] = p;
                        Console.ForegroundColor = ConsoleColor.Green;//изменяет цвет шрифта на зелённый
                        Console.WriteLine($"koht {p} on vaba");
                        Console.ResetColor();
                        int cifra = saal[pileti_rida - 1, p - 1] = 1;
                        t = true;
                    }
                    else
                    {
                        Console.WriteLine($"koht {p} kinni");
                        t = false;
                        ost = new int[mitu];
                        k = 0;
                        p = (kohad - mitu) / 2;
                        break;
                    }
                    p = p + 1;
                    k++;
                } while (mitu != k);

                if (t == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;//изменяет цвет шрифта на красный
                    Console.WriteLine("Sinu kohad on:");
                    Console.ResetColor();
                    foreach (var koh in ost)
                    {
                        Console.WriteLine($"{koh}\n");
                    }
                }
                else
                {
                    Console.WriteLine("Selles reas ei ole vabu kohti. Kas tahad teises reas otsida");
                }
                return t;
            }
            public static void Main(string[] args)
            {

                int suurus = Saali_suurus();
                Saali_taitmine(suurus);

                while (true)
                {
                    Saal_ekraanile();//рисует зал
                    Console.WriteLine();

                    int koh = 0;
                    Console.WriteLine("Ise valik - 1 ; automatne valik - 2; lõpetama - 3");//даёт выбор пользователю, выбрать самостоятельно или машина сделает за тебя
                    int veborkak = int.Parse(Console.ReadLine());
                    Console.WriteLine("");
                    if (veborkak == 1)//если пользователь выбирает 1, то срабатывает функция Celovek(); где в дальнейшем человек выбирает себе место
                    {
                        Console.WriteLine("Mitu pieteid tahad osta?");
                        int kogus = int.Parse(Console.ReadLine());
                        for (int i = 0; i < (kohad - 1) * (read - 1); i++)
                        {
                            if (Celovek())
                            {
                                koh++;
                            }
                            if (koh == kogus)
                            {
                                break;
                            }
                        }
                        Console.Clear();

                    }
                    else if (veborkak == 2)//если пользователь выбирает 2, то срабатывает функция Muuk(); где в дальнейшем пользователь указывает ряд и количество билетов, и машина находит(если есть свободные места)
                    {
                        Muuk();
                    }
                    else if (veborkak == 3)
                    {
                        return;
                    }
                }
                Console.ReadLine();
            }

        }
    }
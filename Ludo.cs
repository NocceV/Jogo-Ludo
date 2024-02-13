using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BEM VINDO AO LUDO");
            while (true)
            {
                Console.WriteLine(" POR FAVOR JOGADOR 1 \n Escolhauma das opções:\n 0-Para girar o dado \n 1-Para ver a posição das peças. ");
                int opcoes = int.Parse(Console.ReadLine());
                switch (opcoes)
                {
                    case 0:
                        girarDado();
                        break;
                    case 1:
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
                Console.ReadLine();
            }
        }
        public static void girarDado()
        {
            Random rnd = new Random();
            int dado = rnd.Next(1, 7);
            Console.WriteLine("Dado girado= "+dado);
            

        }
    }
}

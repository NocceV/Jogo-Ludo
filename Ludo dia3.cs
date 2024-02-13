using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================================================");
            Console.WriteLine("      BEM VINDO AO LUDO!");
            Console.WriteLine("========================================================================");
            int P1 = 0, P2 = 0, P3 = 0, P4 = 0;
            int O1 = 0, O2 = 2, O3 = 0, O4 = 0;
            int[,] tabu = tabuleiroJ1();
            int[,] tabuJ2 = tabuleiroJ2();
            while (true)
            {
                funcaoPuloJ2();
                Console.WriteLine("=====================================================");
                menuJ1(P1, P2, P3, P4, tabu);
                Console.WriteLine("=====================================================");
                funcaoPuloJ1();
                Console.WriteLine("=====================================================");
                menuJ2(O1,O2,O3,O4,tabuJ2);
                Console.WriteLine("=====================================================");


            }

        }
        public static void funcaoPuloJ1()
        { //Ajuda a pular a vez do Jogador 1 para o jogador 2
        }
        public static void funcaoPuloJ2()
        {  //Ajuda a pular a vez do Jogador 2 para o jogador 1
        }

        //-------------------------------AÇÔES DO JOGADOR 1 -------------------------------------------------//

        public static int[,] tabuleiroJ1() //cria tabuleiro do jogador 1
        {
            int[,] tabu = new int[4, 57];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    tabu[i, j] = -1;


                }

            }
            return tabu;

        }
        public static int[,] tabuleiroEscolherPeçaJ1(int P1, int P2, int P3, int P4, int[,] tabu)//
        {


            if (P1 == 1)
            {
                tabu[0, 0] = 1;
            }
            if (P2 == 2)
            {
                tabu[1, 0] = 2;
            }
            if (P3 == 3)
            {
                tabu[2, 0] = 3;
            }
            if (P4 == 4)
            {
                tabu[3, 0] = 4;
            }
            return tabu;

        }
        public static void menuJ1(int P1, int P2, int P3, int P4, int[,] tabu) // menu do jogador 1
        {
            Console.WriteLine(" POR FAVOR JOGADOR 1 \n Escolha uma das opções:\n 0-Para girar o dado \n 1-Para ver a posição das peças. ");
            int opcoesJ1 = int.Parse(Console.ReadLine());
            switch (opcoesJ1)
            {
                case 0:
                    int dado = girarDadoJ1();
                    int contadorDeSeis = 0;
                    verificarDadoJ1(P1, P2, P3, P4, dado, contadorDeSeis, tabu);
                    break;
                case 1:
                    break;
                default:
                    Console.WriteLine("Opção inválida , voçê perdeu sua vez :(");
                    break;
            }

        }
        public static int girarDadoJ1()        // Gira o dado do jogador 1
        {
            Random rnd = new Random();
            int dado = rnd.Next(1, 7);
            Console.WriteLine("Dado girado = " + dado);
            return dado;


        }
        public static void verificarDadoJ1(int P1, int P2, int P3, int P4, int dado, int contadorDeSeis, int[,] tabu) // Verifica o valor tirado pelo dado do jogador 1
        {

            if (dado == 6)
            {
                contadorDeSeis++;
                if (contadorDeSeis == 3)
                {
                    Console.WriteLine("Voce tirou o maximo de 6 por rodada,por iso perca sua vez");
                    funcaoPuloJ1();
                }

                else

                {
                    escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu);

                }
            }
            else
            {

                int veri = verificaPecaTabuJ1(tabu);
                if (veri == 0)
                {
                    funcaoPuloJ1();
                }
                else
                {
                    movimentarPecaJ1(dado, tabu);
                }


            }

        }

        public static void escolherPeaoJ1Seis(int P1, int P2, int P3, int P4, int dado, int[,] tabu) // Escolhe o a peça que ira sair para o tabuleiro
        {
            Console.WriteLine("============================================================");
            Console.WriteLine("\nParabéns Jogador 1! voce tirou 6!!");
            Console.WriteLine("\nEscolha uma das peças para colocar no tabuleiro: \n 1-Para peça 1 \n 2-Para peça 2 \n 3-Para peça 3 \n 4-Para peça 4");
            Console.WriteLine("\nSe quiser movimentar uma peça pressione 0");
            int escolhaPeca = int.Parse(Console.ReadLine());

            switch (escolhaPeca)
            {
                case 0:
                    int E = verificaPecaTabuJ1(tabu);
                    if (E == 1 || E == 2 || E == 3 || E == 4)
                    {
                        movimentarPecaJ1(dado, tabu);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Não há nehuma peça no tabuleiro,por favor esolha uma para colocar no jogo");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu);
                        break;
                    }

                case 1:
                    int A = verificaPecaTabuJ1(tabu);
                    if (A != 1)
                    {
                        P1 = 1;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);
                        Console.WriteLine("Peça 1 colocada no tabuleiro");
                        girarDadoJ1();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 1 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu);
                        break;
                    }
                case 2:
                    int B = verificaPecaTabuJ1(tabu);
                    if (B != 2)
                    {
                        P2 = 2;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);
                        Console.WriteLine("Peça 2 colocada no tabuleiro");
                        girarDadoJ1();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 2 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu);
                        break;
                    }
                case 3:
                    int C = verificaPecaTabuJ1(tabu);
                    if (C != 3)
                    {
                        P3 = 3;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);
                        Console.WriteLine("Peça 3 colocada no tabuleiro");
                        girarDadoJ1();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 3 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu);
                        break;
                    }
                case 4:
                    int D = verificaPecaTabuJ1(tabu);
                    if (D != 4)
                    {
                        P4 = 4;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);
                        Console.WriteLine("Peça 4 colocada no tabuleiro");
                        girarDadoJ1();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 4 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu);
                        break;
                    }
                default:
                    Console.WriteLine("Peça inexistente ");
                    escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu);
                    break;

            }
        }
        public static int verificaPecaTabuJ1(int[,] tabu) //verifica se tem peças no tabuleiro
        {
            //Se tiver peça no tabuleuiro chama a funcao movimentarPEcaJ1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    if (tabu[0, j] == 1)
                    {

                        return 1;
                    }
                    else if (tabu[1, j] == 2)
                    {
                        return 2;
                    }
                    else if (tabu[2, j] == 3)
                    {
                        return 3;
                    }
                    else if (tabu[3, j] == 4)
                    {
                        return 4;
                    }
                    else
                    {
                        return 0;
                    }
                }

            }
            return 0;

            //se nao tiver menuJ2
        }
        public static void movimentarPecaJ1(int dado, int[,] tabu) //movimenta as peças no tabuleuro
        {
            Console.WriteLine("Escolha uma peça para movimentar de 1 a 4");
            Console.WriteLine("\nSe quiser pular a vez pressione 0");
            int peca = int.Parse(Console.ReadLine());
            switch (peca)
            {
                case 0:
                    funcaoPuloJ1();
                    break;
                case 1:
                    int A = verificaPecaTabuJ1(tabu);
                    if (A == 1)
                    {
                        Console.WriteLine("Peça 1 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ1();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(dado, tabu);
                        break;
                    }
                case 2:
                    int B = verificaPecaTabuJ1(tabu);
                    if (B == 2)
                    {
                        Console.WriteLine("Peça 2 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ1();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(dado, tabu);
                        break;
                    }
                case 3:
                    int C = verificaPecaTabuJ1(tabu);
                    if (C == 3)
                    {
                        Console.WriteLine("Peça 3 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ1();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(dado, tabu);
                        break;
                    }
                case 4:
                    int D = verificaPecaTabuJ1(tabu);
                    if (D == 4)
                    {
                        Console.WriteLine("Peça 4 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ1();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(dado, tabu);
                        break;
                    }
                default:
                    Console.WriteLine("Opção inválida escolha outra peça");
                    movimentarPecaJ1(dado, tabu);
                    break;

            }


        }


        //----------------------------------AÇÕES DO JOGADOR 2------------------------------------//
        public static int[,] tabuleiroJ2() //cria tabuleiro do jogador 2
        {
            int[,] tabuJ2 = new int[4, 57];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    tabuJ2[i, j] = -1;
                }
            }
            return tabuJ2;
        }
        public static int[,] tabuleiroEscolherPeçaJ2(int O1, int O2, int O3, int O4, int[,] tabuJ2)
        {

            if (O1 == 1)
            {

                tabuJ2[0, 0] = 1;
            }
            if (O2 == 2)
            {
                tabuJ2[1, 0] = 2;
            }
            if (O3 == 3)
            {
                tabuJ2[2, 0] = 3;
            }
            if (O4 == 4)
            {
                tabuJ2[3, 0] = 4;
            }
            return tabuJ2;
        }
        public static void menuJ2(int O1,int O2,int O3,int O4,int[,] tabuJ2) //menu do jogador 2
        {
            Console.WriteLine(" POR FAVOR JOGADOR 2 \n Escolha uma das opções:\n 0-Para girar o dado \n 1-Para ver a posição das peças. ");
            int opcoesJ2 = int.Parse(Console.ReadLine());
            switch (opcoesJ2)
            {
                case 0:
                    int dado = girarDadoJ2();
                    int contadorDeSeis = 0;
                    verificarDadoJ2(O1,O2,O3,O4,dado, contadorDeSeis, tabuJ2);
                    break;
                case 1:
                    break;
                default:
                    Console.WriteLine("Opção inválida , voçê perdeu sua vez :( ");
                    break;
            }

        }




        public static int girarDadoJ2()        // Gira o dado do jogador 2
        {
            Random rnd = new Random();
            int dado = rnd.Next(1, 7);
            Console.WriteLine("Dado girado = " + dado);
            return dado;


        }
        public static void verificarDadoJ2(int O1,int O2,int O3,int O4,int dado, int contadorDeSeis, int[,] tabuJ2) // Verifica o valor tirado pelo dado do jogador 2
        {

            if (dado == 6)
            {
                contadorDeSeis++;
                if (contadorDeSeis == 3)
                {
                    Console.WriteLine("Voçê tirou o máximo de 6 por rodada,por isso perca sua vez ");
                    funcaoPuloJ2();
                }

                else

                {
                    escolherPeaoJ2Seis(O1,O2,O3,O4,dado, tabuJ2);

                }
            }
            else
            {
                int veri = verificaPecaTabuJ2(tabuJ2);
                if (veri == 0)
                {
                    funcaoPuloJ2();
                }
                else
                {
                    movimentarPecaJ2(dado, tabuJ2);
                }
            }

        }


        public static void escolherPeaoJ2Seis(int O1,int O2,int O3,int O4,int dado, int[,] tabuJ2) // Escolhe o a peça que ira sair para o tabuleiro
        {
            Console.WriteLine("============================================================");
            Console.WriteLine("\nParabéns Jogador 2! voce tirou 6!!");
            Console.WriteLine("\nEscolha uma das peças para colocar no tabuleiro: \n 1-Para peça 1 \n 2-Para peça 2 \n 3-Para peça 3 \n 4-Para peça 4");
            Console.WriteLine("\nSe quiser movimentar uma peça pressione 0");
            int escolhaPeca = int.Parse(Console.ReadLine());
            //int P1 = 0, P2 = 0, P3 = 0, P4 = 0;
            switch (escolhaPeca)
            {
                case 0:
                    int E = verificaPecaTabuJ2(tabuJ2);
                    if (E == 1 || E == 2 || E == 3 || E == 4)
                    {
                        movimentarPecaJ2(dado, tabuJ2);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Não há nehuma peça no tabuleiro,por favor ecolha uma para colocar no jogo");
                        escolherPeaoJ2Seis(O1,O2,O3,O4,dado, tabuJ2);
                        break;
                    }
                case 1:
                    int A = verificaPecaTabuJ2(tabuJ2);
                    if (A != 1)
                    {
                        O1 = 1;
                        tabuleiroEscolherPeçaJ2(O1, O2, O3, O4, tabuJ2);
                        Console.WriteLine("Peça 1 colocada no tabuleiro");
                        girarDadoJ2();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 1 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(dado, tabuJ2);
                        break;
                    }
                case 2:
                    int B = verificaPecaTabuJ2(tabuJ2);
                    if (B != 2)
                    {
                        P2 = 2;
                        tabuleiroEscolherPeçaJ2(P1, P2, P3, P4, tabuJ2);
                        Console.WriteLine("Peça 2 colocada no tabuleiro");
                        girarDadoJ2();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 2 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(dado, tabuJ2);
                        break;
                    }
                case 3:
                    int C = verificaPecaTabuJ2(tabuJ2);
                    if (C != 3)
                    {
                        P3 = 3;
                        tabuleiroEscolherPeçaJ2(P1, P2, P3, P4, tabuJ2);
                        Console.WriteLine("Peça 3 colocada no tabuleiro");
                        girarDadoJ2();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 3 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(dado, tabuJ2);
                        break;
                    }
                case 4:
                    int D = verificaPecaTabuJ2(tabuJ2);
                    if (D != 4)
                    {
                        P4 = 4;
                        tabuleiroEscolherPeçaJ2(P1, P2, P3, P4, tabuJ2);
                        Console.WriteLine("Peça 4 colocada no tabuleiro");
                        girarDadoJ2();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 4 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(dado, tabuJ2);
                        break;
                    }
                default:
                    Console.WriteLine("Peça inexistente");
                    escolherPeaoJ2Seis(dado, tabuJ2);
                    break;

            }
        }
        public static int verificaPecaTabuJ2(int[,] tabuJ2) //verifica se tem peças para movimentar no tabuleiro
        {
            //Se tiver peca no tabuleiro chama funcao movimentarpecaJ2

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; i < 57; j++)
                {
                    if (tabuJ2[0, j] == 1)
                    {
                        return 1;
                    }
                    else if (tabuJ2[1, j] == 2)
                    {
                        return 2;
                    }
                    else if (tabuJ2[2, j] == 3)
                    {
                        return 3;
                    }
                    else if (tabuJ2[3, j] == 4)
                    {
                        return 4;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return 0;
            //se nao tiver menuJ1

        }
        public static void movimentarPecaJ2(int dado, int[,] tabuJ2) //movimenta peças no tabuleiro
        {
            Console.WriteLine("Escolha uma peça para movimentar de 1 a 4");
            Console.WriteLine("\nSe quiser pular a vez pressione 0");
            int peca = int.Parse(Console.ReadLine());
            switch (peca)
            {
                case 0:
                    funcaoPuloJ2();
                    break;
                case 1:
                    int A = verificaPecaTabuJ2(tabuJ2);
                    if (A == 1)
                    {
                        Console.WriteLine("Peça 1 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ2();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça");
                        movimentarPecaJ2(dado, tabuJ2);
                        break;
                    }
                case 2:
                    int B = verificaPecaTabuJ2(tabuJ2);
                    if (B == 2)
                    {
                        Console.WriteLine("Peça 2 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ2();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça");
                        movimentarPecaJ2(dado, tabuJ2);
                        break;
                    }
                case 3:
                    int C = verificaPecaTabuJ2(tabuJ2);
                    if (C == 3)
                    {
                        Console.WriteLine("Peça 3 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ2();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça");
                        movimentarPecaJ2(dado, tabuJ2);
                        break;
                    }
                case 4:
                    int D = verificaPecaTabuJ2(tabuJ2);
                    if (D == 4)
                    {
                        Console.WriteLine("Peça 4 escolhida ela irá movimentar " + dado + " casas");
                        if (dado == 6)
                        {
                            girarDadoJ2();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ2(dado, tabuJ2);
                        break;
                    }
                default:
                    Console.WriteLine("Opção inválida escolha outra peça");
                    movimentarPecaJ2(dado, tabuJ2);
                    break;

            }


        }





    }
}

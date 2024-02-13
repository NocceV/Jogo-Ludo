
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)//iniciação do jogo
        {
            inicio();
        }
        public static void inicio()
        {
            Console.WriteLine("========================================================================");
            Console.WriteLine("      BEM VINDO AO LUDO!");
            Console.WriteLine("========================================================================");
            int P1 = 0, P2 = 0, P3 = 0, P4 = 0; //declara as peças do jogador 1
            int O1 = 0, O2 = 0, O3 = 0, O4 = 0; //declara as peças do jogador 2
            int[,] tabu = tabuleiroJ1(); //cria o tabuleiro do jogadorr 1
            int[,] tabuJ2 = tabuleiroJ2(); //cria o tabuleiro do jogador 2
            int ContadorDeSeis = 0;
            while (true)
            {
                funcaoPuloJ2();
                Console.WriteLine("=====================================================");
                menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, ContadorDeSeis); //menu do jogador 1
                Console.WriteLine("=====================================================");
                funcaoPuloJ1();
                Console.WriteLine("=====================================================");
                menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, ContadorDeSeis); //menu do jogador 2
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
            int[,] tabu = new int[4, 84];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    tabu[i, j] = 0;
                }
            }
            return tabu;
        }

        public static int[,] tabuleiroEscolherPeçaJ1(int P1, int P2, int P3, int P4, int[,] tabu) //Define valor para a peça escolhida  
        {
            if (P1 == 1)
            {
                tabu[0, 1] = 1;
            }
            if (P2 == 2)
            {
                tabu[1, 1] = 2;
            }
            if (P3 == 3)
            {
                tabu[2, 1] = 3;
            }
            if (P4 == 4)
            {
                tabu[3, 1] = 4;
            }
            return tabu;
        }

        public static void menuJ1(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) // menu do jogador 1
        {
            gameOverJ1(tabu);
            Console.Write("\n");
            Console.WriteLine(" POR FAVOR JOGADOR 1 \n Escolha uma das opções:\n 0-Para girar o dado \n 1-Para ver a posição das peças. ");
            int opcoesJ1 = int.Parse(Console.ReadLine());
            switch (opcoesJ1)
            {
                case 0: //gira o dado
                    int dado = girarDadoJ1();
                    verificarDadoJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);

                    break;
                case 1: // mostra o tabuleiro
                    mostrarTabuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    break;
                default:
                    Console.WriteLine("Opção inválida , voçê perdeu sua vez :(");
                    break;
            }
        }

        public static void mostrarTabuJ1(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) // função que mostra o tabuleiro do jogador 1
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    Console.Write(tabu[i, j]);
                }
                Console.Write("\n");
            }
            menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
        }

        public static int girarDadoJ1()        // Gira o dado do jogador 1
        {
            Random rnd = new Random();
            int dado = rnd.Next(1, 7);
            Console.WriteLine("Dado girado = " + dado);
            return dado;
        }

        public static void verificarDadoJ1(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) // Verifica o valor tirado pelo dado do jogador 1
        {
            if (dado == 6)//se tirou 6 o valor vem para aqui
            {
                contadorDeSeis++;
                if (contadorDeSeis == 3)
                {
                    Console.WriteLine("Voce tirou o maximo de 6 por rodada,por isso perca sua vez");
                    contadorDeSeis = 0;
                    funcaoPuloJ1();

                }
                else
                {
                    escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                }
            }
            else//se tirou outro valor ele vem para aqui
            {
                contadorDeSeis = 0;
                int soma = 0;
                int veri1 = verificaPecaP1(tabu);
                int veri2 = verificaPecaP2(tabu);
                int veri3 = verificaPecaP3(tabu);
                int veri4 = verificaPecaP4(tabu);
                soma = veri1 + veri2 + veri3 + veri4;
                if (soma == 0)
                {

                    funcaoPuloJ1();
                }
                else
                {
                   
                    movimentarPecaJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                }
            }
        }

        public static void escolherPeaoJ1Seis(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) // Escolhe o a peça que irá sair para o tabuleiro
        {
            Console.WriteLine("============================================================");
            Console.WriteLine("\nParabéns Jogador 1! voce tirou 6!!");
            Console.WriteLine("\nEscolha uma das peças para colocar no tabuleiro: \n 1-Para peça 1 \n 2-Para peça 2 \n 3-Para peça 3 \n 4-Para peça 4");
            Console.WriteLine("\nSe quiser movimentar uma peça pressione 0");
            int escolhaPeca = int.Parse(Console.ReadLine());

            switch (escolhaPeca)
            {
                case 0:
                    int E1 = verificaPecaP1(tabu);
                    int E2 = verificaPecaP2(tabu);
                    int E3 = verificaPecaP3(tabu);
                    int E4 = verificaPecaP4(tabu);
                    if (E1 == 1 || E2 == 2 || E3 == 3 || E4 == 4)
                    {
                        movimentarPecaJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Não há nehuma peça no tabuleiro,por favor esolha uma para colocar no jogo");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }

                case 1:
                    int A = verificaPecaP1(tabu);
                    if (A == 0)
                    {
                        P1 = 1;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);

                        Console.WriteLine("Peça 1 colocada no tabuleiro");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 1 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 2:
                    int B = verificaPecaP2(tabu);
                    if (B == 0)
                    {
                        P2 = 2;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);
                        Console.WriteLine("Peça 2 colocada no tabuleiro");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 2 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 3:
                    int C = verificaPecaP3(tabu);
                    if (C == 0)
                    {
                        P3 = 3;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);
                        Console.WriteLine("Peça 3 colocada no tabuleiro");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 3 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 4:
                    int D = verificaPecaP4(tabu);
                    if (D == 0)
                    {
                        P4 = 4;
                        tabuleiroEscolherPeçaJ1(P1, P2, P3, P4, tabu);
                        Console.WriteLine("Peça 4 colocada no tabuleiro");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 4 ja no taubeliro,escolha outra peça ou movimente uma peça");
                        escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                default:
                    Console.WriteLine("Peça inexistente ");
                    escolherPeaoJ1Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    break;

            }
        }
        public static int verificaPecaP1(int[,] tabu) //verifica se tem a peça 1 no tabuleiro
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[0, j] == 1)
                {
                    return 1;
                }
                if (tabu[0, j] == 10)
                {
                    return 10;
                }
            }
            return 0;
        }
        public static int verificaPecaP2(int[,] tabu)//verifica se tem a peça 2 no tabuleiro
        {


            for (int j = 0; j < 57; j++)
            {
                if (tabu[1, j] == 2)
                {
                    return 2;
                }
                if (tabu[1, j] == 10)
                {
                    return 10;
                }
            }

            return 0;
        }
        public static int verificaPecaP3(int[,] tabu)//verifica se tem a peça 3 no tabuleiro
        {

            for (int j = 0; j < 57; j++)
            {
                if (tabu[2, j] == 3)
                {
                    return 3;
                }
                if (tabu[2, j] == 10)
                {
                    return 10;
                }
            }
            return 0;
        }
        public static int verificaPecaP4(int[,] tabu)//verifica se tem a peça 4 no tabuleiro
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[3, j] == 4)
                {
                    return 4;
                }
                if (tabu[3, j] == 10)
                {
                    return 10;
                }

            }
            return 0;
        }
        public static void movimentarPecaJ1(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) //movimenta as peças no tabuleuro
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
                    int A = verificaPecaP1(tabu);
                    if (A == 1)
                    {
                        int ganhaP1 = verificaGanhaP1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaP1 == 4)
                        {

                            menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaP1 == 5)
                        {
                            funcaoPuloJ1();
                            break;
                        }
                        else
                        {
                            movimentarP1(P1, dado, tabu);
                            Console.WriteLine("Peça 1 escolhida ela irá movimentar " + dado + " casas");
                            comeP1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 2:
                    int B = verificaPecaP2(tabu);
                    if (B == 2)
                    {
                        int ganhaP2 = verificaGanhaP2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaP2 == 4)
                        {

                            menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaP2 == 5)
                        {
                            funcaoPuloJ1();
                            break;
                        }
                        else
                        {
                            movimentarP2(P1, dado, tabu);
                            Console.WriteLine("Peça 2 escolhida ela irá movimentar " + dado + " casas");
                            comeP2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        movimentarP3(P3, dado, tabu);
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 3:
                    int C = verificaPecaP3(tabu);
                    if (C == 3)
                    {
                        int ganhaP3 = verificaGanhaP3(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaP3 == 4)
                        {

                            menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaP3 == 5)
                        {
                            funcaoPuloJ1();
                            break;
                        }
                        else
                        {
                            movimentarP3(P1, dado, tabu);
                            Console.WriteLine("Peça 3 escolhida ela irá movimentar " + dado + " casas");
                            comeP3(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 4:
                    int D = verificaPecaP4(tabu);
                    if (D == 4)
                    {
                        int ganhaP4 = verificaGanhaP4(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaP4 == 4)
                        {

                            menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaP4 == 5)
                        {
                            funcaoPuloJ1();
                            break;
                        }
                        else
                        {
                            movimentarP4(P1, dado, tabu);
                            Console.WriteLine("Peça 4 escolhida ela irá movimentar " + dado + " casas");
                            comeP4(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Peça não esta em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                default:
                    Console.WriteLine("Opção inválida escolha outra peça");
                    movimentarPecaJ1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    break;
            }

        }
        public static int[,] movimentarP1(int P1, int dado, int[,] tabu)//movimenta peça 1 do jogador 1
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[0, j] == 1)
                {
                    tabu[0, j + dado] = 1;
                    tabu[0, j] = 0;
                    return tabu;
                }
            }
            return tabu;
        }
        public static int[,] movimentarP2(int P2, int dado, int[,] tabu)//movimenta peça 2 do jogador 1
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[1, j] == 2)
                {
                    tabu[1, j + dado] = 2;
                    tabu[1, j] = 0;
                    return tabu;
                }
            }
            return tabu;
        }
        public static int[,] movimentarP3(int P3, int dado, int[,] tabu)//movimenta peça 3 do jogador 1
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[2, j] == 3)
                {
                    tabu[2, j + dado] = 3;
                    tabu[2, j] = 0;
                    return tabu;
                }
            }
            return tabu;
        }
        public static int[,] movimentarP4(int P4, int dado, int[,] tabu)//movimenta peça 4 do jogador 1
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[3, j] == 4)
                {
                    tabu[3, j + dado] = 4;
                    tabu[3, j] = 0;
                    return tabu;
                }
            }
            return tabu;

        }
        //===============================Funçoes que as peças comem as outras=========================================
        public static void comeP1(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 1 come alguma peça do jogador 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[0, j] == 1 && tabuJ2[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[0, j] = 0;
                        Console.WriteLine("A peça 1 do jogadorr 1 acaba de comer a peça 1 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[0, j] == 1 && tabuJ2[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[1, j] = 0;
                        Console.WriteLine("A peça 1 do jogador 1 acaba de comer a peça 2 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[0, j] == 1 && tabuJ2[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[2, j] = 0;
                        Console.WriteLine("A peça 1 do jogador 1 acaba de comer a peça 3 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[0, j] == 1 && tabuJ2[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[3, j] = 0;
                        Console.WriteLine("A peça 1 do jogador 1 acaba de comer a peça 4 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static void comeP2(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 2 come alguma peça do jogador 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[1, j] == 2 && tabuJ2[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[0, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 1 acaba de comer a peça 1 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[1, j] == 2 && tabuJ2[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[1, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 1 acaba de comer a peça 2 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[1, j] == 2 && tabuJ2[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[2, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 1 acaba de comer a peça 3 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[1, j] == 2 && tabuJ2[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[3, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 1 acaba de comer a peça 4 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static void comeP3(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 3 come alguma peça do jogador 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[2, j] == 3 && tabuJ2[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[0, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 1 acaba de comer a peça 1 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[2, j] == 3 && tabuJ2[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[1, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 1 acaba de comer a peça 2 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[2, j] == 3 && tabuJ2[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[2, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 1 acaba de comer a peça 3 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[2, j] == 3 && tabuJ2[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[3, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 1 acaba de comer a peça 4 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static void comeP4(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 4 come alguma peça do jogador 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[3, j] == 4 && tabuJ2[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[0, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 1 acaba de comer a peça 1 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[3, j] == 4 && tabuJ2[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[1, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 1 acaba de comer a peça 2 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[3, j] == 4 && tabuJ2[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[2, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 1 acaba de comer a peça 3 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabu[3, j] == 4 && tabuJ2[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabuJ2[3, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 1 acaba de comer a peça 4 do jogador 2!");
                        Console.WriteLine("Jogador 1 ganhe uma jogada:");
                        menuJ1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static int verificaGanhaP1(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peça 1 chegou no final
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[0, j] == 1)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 1 chegou no destino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabu[0, k] = 10;
                            }
                            return 4;
                        }
                        else
                        {
                            Console.WriteLine("Voce precisa de tirar 6 para ganhar,por isso perca sua vez");
                            return 5;
                        }
                    }
                }
            }
            return 6;
        }
        public static int verificaGanhaP2(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peça 2 chegou no final
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[1, j] == 2)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 2 chegou no destino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabu[1, k] = 10;
                            }
                            return 4;
                        }
                        else
                        {
                            Console.WriteLine("Voce precisa de tirar 6 para ganhar,por isso perca sua vez");
                            return 5;
                        }
                    }
                }
            }
            return 6;
        }
        public static int  verificaGanhaP3(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peça 3 chegou no final
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[2, j] == 3)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 3 chegou no destino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabu[2, k] = 10;
                            }
                            return 4;
                        }
                        else
                        {
                            Console.WriteLine("Voce precisa de tirar 6 para ganhar,por isso perca sua vez");
                            return 5;
                        }
                    }
                }
            }
            return 6;
        }
        public static int  verificaGanhaP4(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peça 4 chegou no final
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabu[3, j] == 4)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 4 chegou no destino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabu[3, k] = 10;
                            }
                            return 4;
                        }
                        else
                        {
                            Console.WriteLine("Voce precisa de tirar 6 para ganhar,por isso perca sua vez");
                            return 5;
                        }
                    }
                }
            }
            return 6;
        }
        public static void gameOverJ1(int[,] tabu)//verifica se todas peças chegaram no final
        {
            int cont = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    if (tabu[i, j] == 10)
                    {
                        cont++;
                    }
                }
            }
            if (cont == 228)
            {
                Console.WriteLine("***********************************************");
                Console.WriteLine("PARABENS JOGADOR 1!!!!!,VOCE GANHOU O JOGO!!!!");
                Console.WriteLine("***********************************************");
                Console.ReadLine();
                inicio();
            }
        }

        //----------------------------------AÇÕES DO JOGADOR 2------------------------------------//
        public static int[,] tabuleiroJ2() //cria tabuleiro do jogador 2
        {
            int[,] tabuJ2 = new int[4, 84];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    tabuJ2[i, j] = 0;
                }
            }
            return tabuJ2;
        }
        public static int[,] tabuleiroEscolherPeçaJ2(int O1, int O2, int O3, int O4, int[,] tabuJ2)//Define o valor da peça escolhida
        {

            if (O1 == 1)
            {
                tabuJ2[0, 1] = 1;
            }
            if (O2 == 2)
            {
                tabuJ2[1, 1] = 2;
            }
            if (O3 == 3)
            {
                tabuJ2[2, 1] = 3;
            }
            if (O4 == 4)
            {
                tabuJ2[3, 1] = 4;
            }
            return tabuJ2;
        }
        public static void menuJ2(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) //menu do jogador 2
        {
            gameOverJ2(tabuJ2);
            Console.Write("\n");
            Console.WriteLine(" POR FAVOR JOGADOR 2 \n Escolha uma das opções:\n 0-Para girar o dado \n 1-Para ver a posição das peças. ");
            int opcoesJ2 = int.Parse(Console.ReadLine());
            switch (opcoesJ2)
            {
                case 0:
                    int dado = girarDadoJ2();

                    verificarDadoJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    break;
                case 1:
                    mostrarTabuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    break;
                default:
                    Console.WriteLine("Opção inválida , voçê perdeu sua vez :( ");
                    break;
            }

        }
        public static void mostrarTabuJ2(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//mostra o tabuleiro do jogador 2
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    Console.Write(tabuJ2[i, j]);
                }
                Console.Write("\n");
            }
            menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);

        }



        public static int girarDadoJ2()        // Gira o dado do jogador 2
        {
            Random rnd = new Random();
            int dado = rnd.Next(1, 7);
            Console.WriteLine("Dado girado = " + dado);
            return dado;


        }
        public static void verificarDadoJ2(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) // Verifica o valor tirado pelo dado do jogador 2
        {

            if (dado == 6)
            {
                contadorDeSeis++;
                if (contadorDeSeis == 3)
                {
                    Console.WriteLine("Voçê tirou o máximo de 6 por rodada,por isso perca sua vez ");
                    contadorDeSeis = 0;
                    funcaoPuloJ2();
                }

                else

                {
                    escolherPeaoJ2Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);

                }
            }
            else
            {
                contadorDeSeis = 0;
                int soma2 = 0;
                int veri1J2 = verificaPecaO1(tabuJ2);
                int veri2J2 = verificaPecaO2(tabuJ2);
                int veri3J2 = verificaPecaO3(tabuJ2);
                int veri4J2 = verificaPecaO4(tabuJ2);
                soma2 = veri1J2 + veri2J2 + veri3J2 + veri4J2;
                if (soma2 == 0)
                {
                    funcaoPuloJ2();
                }
                else
                {
                    movimentarPecaJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                }
            }

        }


        public static void escolherPeaoJ2Seis(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) // Escolhe o a peça que ira sair para o tabuleiro
        {
            Console.WriteLine("============================================================");
            Console.WriteLine("\nParabéns Jogador 2! voce tirou 6!!");
            Console.WriteLine("\nEscolha uma das peças para colocar no tabuleiro: \n 1-Para peça 1 \n 2-Para peça 2 \n 3-Para peça 3 \n 4-Para peça 4");
            Console.WriteLine("\nSe quiser movimentar uma peça pressione 0");
            int escolhaPeca = int.Parse(Console.ReadLine());

            switch (escolhaPeca)
            {
                case 0:
                    int H1 = verificaPecaO1(tabuJ2);
                    int H2 = verificaPecaO2(tabuJ2);
                    int H3 = verificaPecaO3(tabuJ2);
                    int H4 = verificaPecaO4(tabuJ2);
                    if (H1 == 1 || H2 == 2 || H3 == 3 || H4 == 4)
                    {
                        movimentarPecaJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Não há nehuma peça no tabuleiro,por favor ecolha uma para colocar no jogo");
                        escolherPeaoJ2Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 1:
                    int A = verificaPecaO1(tabuJ2);
                    if (A == 0)
                    {
                        O1 = 1;
                        tabuleiroEscolherPeçaJ2(O1, O2, O3, O4, tabuJ2);
                        Console.WriteLine("Peça 1 colocada no tabuleiro");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 1 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 2:
                    int B = verificaPecaO2(tabuJ2);
                    if (B == 0)
                    {
                        O2 = 2;
                        tabuleiroEscolherPeçaJ2(O1, O2, O3, O4, tabuJ2);
                        Console.WriteLine("Peça 2 colocada no tabuleiro");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 2 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 3:
                    int C = verificaPecaO3(tabuJ2);
                    if (C == 0)
                    {
                        O3 = 3;
                        tabuleiroEscolherPeçaJ2(O1, O2, O3, O4, tabuJ2);
                        Console.WriteLine("Peça 3 colocada no tabuleiro");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 3 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 4:
                    int D = verificaPecaO4(tabuJ2);
                    if (D == 0)
                    {
                        O4 = 4;
                        tabuleiroEscolherPeçaJ2(O1, O2, O3, O4, tabuJ2);
                        Console.WriteLine("Peça 4 colocada no tabuleiro");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Peça 4 ja inserida no tabuleiro,escolha outra peça ou movimenre uma peça");
                        escolherPeaoJ2Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                default:
                    Console.WriteLine("Peça inexistente");
                    escolherPeaoJ2Seis(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    break;

            }
        }
        public static int verificaPecaO1(int[,] tabuJ2) //verifica se tem peças para movimentar no tabuleiro
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[0, j] == 1)
                {
                    return 1;
                }
                if (tabuJ2[0, j] == 10)
                {
                    return 10;
                }
            }
            return 0;
        }
        public static int verificaPecaO2(int[,] tabuJ2) //verifica se tem peças para movimentar no tabuleiro
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[1, j] == 2)
                {
                    return 2;
                }
                if (tabuJ2[1, j] == 10)
                {
                    return 10;
                }
            }
            return 0;
        }
        public static int verificaPecaO3(int[,] tabuJ2) //verifica se tem peças para movimentar no tabuleiro
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[2, j] == 3)
                {
                    return 3;
                }
                if (tabuJ2[2, j] == 10)
                {
                    return 10;
                }
            }
            return 0;
        }
        public static int verificaPecaO4(int[,] tabuJ2) //verifica se tem peças para movimentar no tabuleiro
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[3, j] == 4)
                {
                    return 4;
                }
                if (tabuJ2[3, j] == 10)
                {
                    return 10;
                }
            }
            return 0;
        }
        public static void movimentarPecaJ2(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis) //movimenta peças no tabuleiro
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
                    int A = verificaPecaO1(tabuJ2);
                    if (A == 1)
                    {
                       
                        int ganhaO1 = verificaGanhaO1(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaO1 == 4)
                        {

                            menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaO1 == 5)
                        {
                            funcaoPuloJ2();
                            break;
                        }
                        else
                        {
                            movimentarO1(O1, dado, tabuJ2);
                            Console.WriteLine("Peça 1 escolhida ela irá movimentar " + dado + " casas");
                            comeO1(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça");
                        movimentarPecaJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 2:
                    int B = verificaPecaO2(tabuJ2);
                    if (B == 2)
                    {
                        int ganhaO2 = verificaGanhaO2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaO2 == 4)
                        {

                            menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaO2 == 5)
                        {

                            funcaoPuloJ2();
                            break;
                        }
                        else
                        {
                            movimentarO2(O2, dado, tabuJ2);
                            Console.WriteLine("Peça 2 escolhida ela irá movimentar " + dado + " casas");
                            comeO2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça");
                        movimentarPecaJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 3:
                    int C = verificaPecaO3(tabuJ2);
                    if (C == 3)
                    {
                        int ganhaO3 = verificaGanhaO3(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaO3 == 4)
                        {

                            menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaO3 == 5)
                        {

                            funcaoPuloJ2();
                            break;
                        }
                        else
                        {
                            movimentarO3(O2, dado, tabuJ2);
                            Console.WriteLine("Peça 3 escolhida ela irá movimentar " + dado + " casas");
                            comeO3(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça");
                        movimentarPecaJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                case 4:
                    int D = verificaPecaO4(tabuJ2);
                    if (D == 4)
                    {
                        int ganhaO4 = verificaGanhaO4(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        if (ganhaO4 == 4)
                        {

                            menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            break;
                        }
                        else if (ganhaO4 == 5)
                        {

                            funcaoPuloJ2();
                            break;
                        }
                        else
                        {
                            movimentarO4(O2, dado, tabuJ2);
                            Console.WriteLine("Peça 4 escolhida ela irá movimentar " + dado + " casas");
                            comeO4(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            if (dado == 6)
                            {
                                menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Peça não está em jogo no tabuleiro,por favor escolha outra peça para movimentar");
                        movimentarPecaJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                        break;
                    }
                default:
                    Console.WriteLine("Opção inválida escolha outra peça");
                    movimentarPecaJ2(P1, P2, P3, P4, dado, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    break;

            }


        }
        public static int[,] movimentarO1(int O1, int dado, int[,] tabuJ2)//movimenta a peça 1
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[0, j] == 1)
                {
                    tabuJ2[0, j + dado] = 1;
                    tabuJ2[0, j] = 0;
                    return tabuJ2;
                }
            }
            return tabuJ2;
        }
        public static int[,] movimentarO2(int O2, int dado, int[,] tabuJ2)//movimenta a peça 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[1, j] == 2)
                {
                    tabuJ2[1, j + dado] = 2;
                    tabuJ2[1, j] = 0;
                    return tabuJ2;
                }
            }
            return tabuJ2;
        }
        public static int[,] movimentarO3(int O3, int dado, int[,] tabuJ2)//movimenta a peça 3
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[2, j] == 3)
                {
                    tabuJ2[2, j + dado] = 3;
                    tabuJ2[2, j] = 0;
                    return tabuJ2;
                }
            }
            return tabuJ2;
        }
        public static int[,] movimentarO4(int O4, int dado, int[,] tabuJ2)//movimenta a peca 4
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[3, j] == 4)
                {
                    tabuJ2[3, j + dado] = 4;
                    tabuJ2[3, j] = 0;
                    return tabuJ2;
                }
            }
            return tabuJ2;
        }

        //===============================Funçoes que as peças comem as outras=========================================
        public static void comeO1(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 1 come alguma peça do jogador 1
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[0, j] == 1 && tabu[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[0, j] = 0;
                        Console.WriteLine("A peça 1 do jogadorr 2 acaba de comer a peça 1 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);

                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[0, j] == 1 && tabu[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[1, j] = 0;
                        Console.WriteLine("A peça 1 do jogador 2 acaba de comer a peça 2 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[0, j] == 1 && tabu[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[2, j] = 0;
                        Console.WriteLine("A peça 1 do jogador 2 acaba de comer a peça 3 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[0, j] == 1 && tabu[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[3, j] = 0;
                        Console.WriteLine("A peça 1 do jogador 2 acaba de comer a peça 4 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static void comeO2(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 2 come alguma peça do jogador 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[1, j] == 2 && tabu[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[0, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 2 acaba de comer a peça 1 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[1, j] == 2 && tabu[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[1, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 2 acaba de comer a peça 2 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[1, j] == 2 && tabu[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[2, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 2 acaba de comer a peça 3 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[1, j] == 2 && tabu[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[3, j] = 0;
                        Console.WriteLine("A peça 2 do jogador 2 acaba de comer a peça 4 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static void comeO3(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 3 come alguma peça do jogador 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[2, j] == 3 && tabu[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[0, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 2 acaba de comer a peça 1 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[2, j] == 3 && tabu[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[1, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 2 acaba de comer a peça 2 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[2, j] == 3 && tabu[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[2, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 2 acaba de comer a peça 3 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[2, j] == 3 && tabu[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[3, j] = 0;
                        Console.WriteLine("A peça 3 do jogador 2 acaba de comer a peça 4 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static void comeO4(int P1, int P2, int P3, int P4, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//Peça 4 come alguma peça do jogador 2
        {
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[3, j] == 4 && tabu[0, j + 26] == 1)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[0, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 2 acaba de comer a peça 1 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[3, j] == 4 && tabu[1, j + 26] == 2)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[1, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 2 acaba de comer a peça 2 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[3, j] == 4 && tabu[2, j + 26] == 3)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[2, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 2 acaba de comer a peça 3 do jogador 1!");
                        Console.WriteLine("Jogador2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[3, j] == 4 && tabu[3, j + 26] == 4)
                {
                    if (j != 1 && j != 9 && j != 14 && j != 22 && j != 27 && j != 35 && j != 40 && j != 48)
                    {
                        tabu[3, j] = 0;
                        Console.WriteLine("A peça 4 do jogador 2 acaba de comer a peça 4 do jogador 1!");
                        Console.WriteLine("Jogador 2 ganhe uma jogada:");
                        menuJ2(P1, P2, P3, P4, tabu, O1, O2, O3, O4, tabuJ2, contadorDeSeis);
                    }
                }
            }
        }
        public static int verificaGanhaO1(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peca 1 chegou no final
        {

            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[0, j] == 1)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 1 chegou no destttino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabuJ2[0, k] = 10;
                            }
                            return 4;
                        }
                        else
                        {
                            Console.WriteLine("Voçe precisa tirar 6 para vencer,por isso perca sua vez");
                            return 5;
                        }
                    }

                }
            }
            return 6;

        }
        
        public static int verificaGanhaO2(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peca 2 chegou no final
        {

            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[1, j] == 2)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 1 chegou no destttino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabuJ2[1, k] = 10;
                            }
                            return 4;
                        }

                        else
                        {
                            Console.WriteLine("Voçe precisa tirar 6 para vencer,por isso perca sua vez");
                            return 5;
                        }
                    }

                }
            }
            return 6;


        }
        public static int verificaGanhaO3(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peca Tres chegou no final
        {

            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[2, j] == 3)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 1 chegou no destttino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabuJ2[2, k] = 10;
                            }
                            return 4;
                        }

                        else
                        {
                            Console.WriteLine("Voçe precisa tirar 6 para vencer,por isso perca sua vez");
                            return 5;
                        }
                    }

                }
            }
            return 6;
        }
        public static int verificaGanhaO4(int P1, int P2, int P3, int P4, int dado, int[,] tabu, int O1, int O2, int O3, int O4, int[,] tabuJ2, int contadorDeSeis)//verifica se a peca 4 chegou no final
        {

            for (int j = 0; j < 57; j++)
            {
                if (tabuJ2[3, j] == 4)
                {
                    if (j > 51)
                    {
                        if (dado == 6 || (dado + j) == 57)
                        {
                            Console.WriteLine("Parabens!!,sua peça 1 chegou no destttino final");
                            for (int k = 0; k < 57; k++)
                            {
                                tabuJ2[3, k] = 10;
                            }
                            return 4;
                        }

                        else
                        {
                            Console.WriteLine("Voçe precisa tirar 6 para vencer,por isso perca sua vez");
                            return 5;
                        }
                    }

                }
            }
            return 6;
        }
        public static void gameOverJ2(int[,] tabuJ2)//verifica se todas as peças chegaram no final eo jogador ganhou
        {
            int cont = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 57; j++)
                {
                    if (tabuJ2[i, j] == 10)
                    {
                        cont++;
                    }
                }
            }
            if (cont == 228)
            {
                Console.WriteLine("***********************************************");
                Console.WriteLine("PARABENS JOGADOR 2!!!!!,VOCE GANHOU O JOGO!!!!");
                Console.WriteLine("***********************************************");
                Console.ReadLine();
                inicio();
            }
        }



    }
}
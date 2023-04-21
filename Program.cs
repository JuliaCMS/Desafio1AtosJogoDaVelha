namespace Desafio1AtosJogoDaVelha
{
    internal class Program
    {
        static void ExibirTitulo()
        {
            Console.WriteLine("\r\n░░█ █▀█ █▀▀ █▀█   █▀▄ ▄▀█   █░█ █▀▀ █░░ █░█ ▄▀█" +
                                              "\r\n█▄█ █▄█ █▄█ █▄█   █▄▀ █▀█   ▀▄▀ ██▄ █▄▄ █▀█ █▀█\n");
            Console.WriteLine("================================================\n");
        }

        static void ExibirTabuleiro(string[,] tabuleiro)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" {tabuleiro[i, 0]} | {tabuleiro[i, 1]} | {tabuleiro[i, 2]}");
                if ((i + 1) < 3)
                    Console.WriteLine("---+---+---");
            }
            Console.WriteLine();
        }

        static bool EscolherModalidade()
        {
            Console.WriteLine("Digite o número da modalidade que voce deseja jogar:");
            Console.WriteLine("[1] - Player VS Player");
            Console.WriteLine("[2] - Contra o Computador (PvE)");
            int escolhaModalidade = int.Parse(Console.ReadLine());

            return escolhaModalidade == 1;
        }

        static bool ValidarJogada(string[,] tabuleiro, int posicao)
        {
            // obtém a posição equivalente na matriz
            int linha = (posicao - 1) / 3;
            int coluna = (posicao - 1) % 3;

            if (posicao < 1 || posicao > 9)
            {
                ExibirTitulo();
                Console.WriteLine("Posição inválida! Tente novamente.");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();
                return false;
            }

            if (tabuleiro[linha, coluna] == "X" || tabuleiro[linha, coluna] == "O")
            {
                ExibirTitulo();
                Console.WriteLine("Posição ocupada! Tente novamente.");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();
                return false;
            }

            return true;
        }

        static bool VerificarVitoria(string[,] tabuleiro, string jogador)
        {
            // verifica se houve vitória nas linhas
            if (tabuleiro[0, 0] == tabuleiro[0, 1] && tabuleiro[0, 1] == tabuleiro[0, 2] ||
                tabuleiro[1, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[1, 2] ||
                tabuleiro[2, 0] == tabuleiro[2, 1] && tabuleiro[2, 1] == tabuleiro[2, 2])
            {
                return true;
            }
            // verifica se houve vitória nas colunas
            if (tabuleiro[0, 0] == tabuleiro[1, 0] && tabuleiro[1, 0] == tabuleiro[2, 0] ||
                tabuleiro[0, 1] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 1] ||
                tabuleiro[0, 2] == tabuleiro[1, 2] && tabuleiro[1, 2] == tabuleiro[2, 2])
            {
                return true;
            }
            // verifica se houve vitória nas diagonais
            if (tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 2] ||
                tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 0])
            {
                return true;
            }

            return false;
        }

        static void JogadaUsuario(string[,] tabuleiro)
        {
            int posicaoUsuario;

            do
            {
                Console.WriteLine("Agora é a sua vez!\n");

                ExibirTabuleiro(tabuleiro);

                Console.Write("Escolha uma posição válida no tabuleiro (1-9): ");
                posicaoUsuario = int.Parse(Console.ReadLine());

            } while (!ValidarJogada(tabuleiro, posicaoUsuario));

            int linha = (posicaoUsuario - 1) / 3;
            int coluna = (posicaoUsuario - 1) % 3;

            tabuleiro[linha, coluna] = "X";
            Console.Clear();
        }

        static void JogadaComputador(string[,] tabuleiro)
        {
            Random gerador = new Random();

            Console.WriteLine("Agora é a vez do computador!\n");

            while (true)
            {
                int posicaoComputador = gerador.Next(1, 10);
                int linha = (posicaoComputador - 1) / 3;
                int coluna = (posicaoComputador - 1) % 3;

                // verifica se a posição gerada está disponível e a preenche
                if (tabuleiro[linha, coluna] == "X" || tabuleiro[linha, coluna] == "O")
                {
                    continue;
                }
                else
                {
                    tabuleiro[linha, coluna] = "O";
                    break;
                }
            }

            ExibirTabuleiro(tabuleiro);
            System.Threading.Thread.Sleep(2500);
            Console.Clear();
        }

        static void PlayerVsPlayer(string[,] tabuleiro)
        {
            int posicaoUsuario;
            int tentativas = 0;
            string jogador = "X";
            bool venceu = false;

            while (tentativas < 9 && !venceu)
            {
                ExibirTitulo();

                do
                {
                    Console.WriteLine($"Agora é a vez de {jogador}!\n");

                    ExibirTabuleiro(tabuleiro);

                    Console.Write("Escolha uma posição válida no tabuleiro (1-9): ");
                    posicaoUsuario = int.Parse(Console.ReadLine());

                    Console.Clear();

                } while (!ValidarJogada(tabuleiro, posicaoUsuario));

                int linha = (posicaoUsuario - 1) / 3;
                int coluna = (posicaoUsuario - 1) % 3;
                tabuleiro[linha, coluna] = jogador;

                venceu = VerificarVitoria(tabuleiro, jogador);
                if (venceu)
                {
                    ExibirTitulo();
                    Console.WriteLine($"{(jogador == "X" ? "X é o vencedor!\nParabéns :D" : "O é o vencedor!\nParabéns :D")}\n");
                }
                else if (tentativas == 8)
                {
                    ExibirTitulo();
                    Console.WriteLine("Empatou!\n");
                }

                jogador = (jogador == "X") ? "O" : "X"; // alterna o jogador da vez

                tentativas++;
            }
        }

        static void PlayerVsEnvironment(string[,] tabuleiro)
        {
            string jogador = "O";
            int tentativas = 0;
            bool venceu = false;

            while (tentativas < 9 && !venceu)
            {
                ExibirTitulo();

                if (jogador == "X")
                {
                    JogadaUsuario(tabuleiro);
                }
                else
                {
                    JogadaComputador(tabuleiro);
                }

                venceu = VerificarVitoria(tabuleiro, jogador);
                if (venceu)
                {
                    ExibirTitulo();
                    Console.WriteLine($"{(jogador == "X" ? "Você ganhou!\nParabéns :D" : "Computador ganhou!")}\n");
                }
                else if (tentativas == 8)
                {
                    ExibirTitulo();
                    Console.WriteLine("Empatou!\n");
                }

                jogador = (jogador == "X") ? "O" : "X"; // alterna o jogador da vez

                tentativas++;
            }
        }

        static void Main(string[] args)
        {
            string[,] tabuleiro = new string[3, 3] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };

            ExibirTitulo();
            ExibirTabuleiro(tabuleiro);

            bool jogarPvP = EscolherModalidade();
            Console.Clear();

            if (jogarPvP)
            {
                PlayerVsPlayer(tabuleiro);
            }
            else
            {
                PlayerVsEnvironment(tabuleiro);
            }

            Console.WriteLine("Deseja jogar novamente? [S/N]");
            string jogarNovamente = Console.ReadLine().ToUpper();
            if (jogarNovamente == "S")
            {
                Console.Clear();
                Main(args);
            }
            else
            {
                Console.Clear();
                ExibirTitulo();
                Console.WriteLine("Fim do jogo.\n");
                Console.WriteLine("▐▓█▀▀▀▀▀▀▀▀▀█▓▌░▄▄▄▄▄░" +
                                           "\r\n▐▓█░░▀░░▀▄░░█▓▌░█▄▄▄█░" +
                                           "\r\n▐▓█░░▄░░▄▀░░█▓▌░█▄▄▄█░" +
                                           "\r\n▐▓█▄▄▄▄▄▄▄▄▄█▓▌░█████░" +
                                           "\r\n░░░░▄▄███▄▄░░░░░█████░" + "\r\n");
            }
        }
    }
}
namespace Desafio1AtosJogoDaVelha
{
    internal class Program
    {
        static void ExibirTabuleiro(string[,] tabuleiro)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" {tabuleiro[i, 0]} | {tabuleiro[i, 1]} | {tabuleiro[i, 2]}");
                if ((i + 1) < 3)
                    Console.WriteLine("---+---+---");
            }
        }

        static void JogadaComputador(string[,] tabuleiro)
        {
            Random gerador = new Random();

            Console.WriteLine("Agora é a vez do computador!");
            while (true)
            {
                int posicaoComputador = gerador.Next(1, 10);

                // obtêm a posição equivalente na matriz
                // onde x da matriz é igual a: (n-1) / qtd de linhas da matriz
                // e y da matriz é igual a: (n-1) % qtd de colunas matriz
                int linha = (posicaoComputador - 1) / 3;
                int coluna = (posicaoComputador - 1) % 3;

                // verifica se a posição escolhida está disponível e faz a seleção
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

        static bool ValidarJogada(string[,] tabuleiro, int posicao)
        {
            int linha = (posicao - 1) / 3;
            int coluna = (posicao - 1) % 3;

            if (posicao < 1 || posicao > 9)
            {
                Console.WriteLine("Posição inválida! Tente novamente.");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();
                return false;
            }

            if (tabuleiro[linha, coluna] == "X" || tabuleiro[linha, coluna] == "O")
            {
                Console.WriteLine("Posição ocupada! Tente novamente.");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();
                return false;
            }

            return true;
        }

        static void JogadaUsuario(string[,] tabuleiro)
        {
            int posicaoUsuario;

            do
            {
                Console.WriteLine("Agora é a sua vez!");

                ExibirTabuleiro(tabuleiro);

                Console.Write("Escolha uma posição válida no tabuleiro (1-9): ");
                posicaoUsuario = int.Parse(Console.ReadLine());

            } while (!ValidarJogada(tabuleiro, posicaoUsuario));

            int linha = (posicaoUsuario - 1) / 3;
            int coluna = (posicaoUsuario - 1) % 3;

            tabuleiro[linha, coluna] = "X";
            Console.Clear();
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

        static void Main(string[] args)
        {
            string[,] tabuleiro = new string[3, 3] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=============");
            Console.WriteLine("JOGO DA VELHA");
            Console.WriteLine("=============");

            ExibirTabuleiro(tabuleiro);

            System.Threading.Thread.Sleep(2500);
            Console.Clear();

            string jogador = "0";
            int tentativas = 0;
            bool venceu = false;
            while (tentativas < 9 && !venceu)
            {
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
                    Console.WriteLine($"{(jogador == "X" ? "Você ganhou!\nParabéns :D" : "Computador ganhou!")}\n");
                }
                else if (tentativas == 8)
                {
                    Console.WriteLine("Empatou!\n");
                }

                jogador = (jogador == "X") ? "O" : "X"; // alterna o jogador da vez
                tentativas++;
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
                Console.WriteLine("Fim do jogo.");
            }
        }
    }
}
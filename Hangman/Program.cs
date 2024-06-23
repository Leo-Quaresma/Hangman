namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            string choosenCategory;
            int guessingsLeft;
            int dificuldade;
            string[] choosenWords;
            string choosenWord;
            bool guessedRight = false;
            bool isGuessing;
            Dictionary<string, string[]> categories = new Dictionary<string, string[]>
            {
                { "filmes", new string[] {"scarface", "oppenheimer", "beetlejuice", "furiosa" } },
                { "gatos", new string[] {"redpoint", "persa", "sphynx" } },
                { "artistas", new string[] { "taylor swift", "john mayer", "billy joel", "elton john", "miley cyrus"} }
            };

            //Seleção da dificudlade
            Console.WriteLine("Digite um número para selecionar a dificuldade.\nQuanto mais fácil, mais vidas!\n1-Fácil \n2-Médio \n3-Difícil \n");
            dificuldade = int.Parse(Console.ReadLine());
            switch (dificuldade)
            {
                case 1:
                    guessingsLeft = 9;
                    break;
                case 2:
                    guessingsLeft = 7;
                    break;
                case 3:
                    guessingsLeft = 4;
                    break;
                default:
                    Console.WriteLine("Dificuldade Inválida, vai ter que jogar no Médio");
                    guessingsLeft = 7;
                    break;
            }
            Console.Clear();
            
            //Seleção da categoria. Uma das palavras da categoria é escolhida aleatoriamente.
            Console.WriteLine("Em qual categoria deseja jogar? \nFilmes \nGatos \nArtistas \n");
            choosenCategory = Console.ReadLine().Trim().ToLower();
            choosenWords = categories[choosenCategory];
            choosenWord = choosenWords[random.Next(0, choosenWords.Length)];
            char[] hiddenWord = new char[choosenWord.Length];
            Array.Fill(hiddenWord, '_');

            List<char> charsGuesseds = new List<char>();
            isGuessing = true;

            Console.Clear();

            //Loop Principal do Jogo. Enquanto houver vidas e o jogador não tiver acertado, o jogo continua
            while(guessingsLeft > 0 && isGuessing)
            {
                //Exibição da palavra em underlines e dos palpites errados já feitos, assim como das tentativas restantes.
                Console.WriteLine($"Palavra: {string.Join("", hiddenWord)}");
                Console.WriteLine($"Palpites restantes: {guessingsLeft}");
                Console.WriteLine($"Seus palpites já foram: {string.Join(", ", charsGuesseds)}");
                Console.WriteLine("------------------");
                Console.WriteLine("Entre uma letra: ");
                char guess = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
                Thread.Sleep(100);


                if (charsGuesseds.Contains(guess))
                {
                    Console.WriteLine("Já chutou essa letra, tente outra!");
                    Thread.Sleep(1500);
                    Console.Clear();
                    continue;
                }

                guessedRight = false;

                charsGuesseds.Add(guess);

                for (int i = 0; i < choosenWord.Length; i++)
                {
                    if (choosenWord[i] == guess)
                    {
                        hiddenWord[i] = guess;
                        guessedRight = true;
                    }
                }

                if (guessedRight)
                {
                    Console.WriteLine("Letra correta!");
                }
                else
                {
                    Console.WriteLine("Letra incorreta");
                    guessingsLeft--;
                }

                if (!hiddenWord.Contains('_'))
                {
                    Console.WriteLine("Você venceu!\nA palavra era: " + choosenWord);
                    Thread.Sleep(3000);
                    isGuessing = false;
                }
                else if (guessingsLeft == 0)
                {
                    Console.WriteLine("Você Perdeu!\nA palavra secreta era" + choosenWord);
                    isGuessing = false;
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}

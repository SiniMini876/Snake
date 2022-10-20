namespace Snake
{
    internal class Snake
    {
        public const bool ContinueGame = true;
        private static int sizeX = Console.WindowWidth;
        private static int sizeY = Console.WindowHeight;
        private static int posx, posy, posAppleX, posAppleY, inputx, inputy;
        private static List<int[]> tale = new List<int[]>();
        /*
         * List: [
         *  [5, 5],
         *  [4, 5],
         *  [3, 5],
         *  [3, 4]
         * ]
         */
        private static async Task Main(string[] args)
        {

            Console.SetCursorPosition((int)(sizeX / 2.5), sizeY / 2);
            //Console.Write("Click any key to start the game!");
            //Console.Read();
            var s = new Snake();
            AppleGenerator();
            await s.StartGame();
            inputx = 1;
        }

        public async Task StartGame()
        {
            while (ContinueGame)
            {
                await MoveAction();
                if (Console.KeyAvailable) await Move();
            }
        }

        public static async
            Task
            MoveAction()
        {
            await Task.Delay(100);
            Console.Clear();
            posx += inputx;
            posy += inputy;
            if (posy < 0 || posx < 0 || posy > sizeY || posx > sizeX)
            {
                Console.WriteLine("Game Over!");
                Environment.Exit(0);
            }

            for (int i = tale.Count - 1; 0 < i; i--)
            {
                tale[i][0] = tale[i - 1][0];
                tale[i][1] = tale[i - 1][1];
                Console.SetCursorPosition(tale[i][0], tale[i][1]);
                Console.Write("O");
            }

            var posHead = new int[] { posx, posy };
            if (tale.Count == 0) tale.Add(posHead);
            else tale[0] = posHead;
            Console.SetCursorPosition(tale[0][0], tale[0][1]);
            Console.Write("O");

            if (posx == posAppleX && posy == posAppleY)
            {
                int[] posNew = new int[] { tale[tale.Count - 1][0] - inputx, tale[tale.Count - 1][1] - inputy };

                tale.Add(posNew);

                tale[0][0] = posAppleX;
                tale[0][1] = posAppleY;
                AppleGenerator();

                Console.SetCursorPosition(0, 0);
                tale.ForEach(x => Console.Write(x[0] + " " + x[1] + "\t"));

            }
            Console.SetCursorPosition(posAppleX, posAppleY);

            Console.Write("A");
            Console.SetCursorPosition(0, 0);
            Console.Write("Score is: " + tale.Count);
        }

        public static async Task Move()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    inputx = 1;
                    inputy = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    inputx = -1;
                    inputy = 0;
                    break;
                case ConsoleKey.UpArrow:
                    inputx = 0;
                    inputy = -1;
                    break;
                case ConsoleKey.DownArrow:
                    inputx = 0;
                    inputy = 1;
                    break;
            }


            if (posx != sizeX)
            {
                await MoveAction();
            }
            if (posy < 0 || posx < 0 || posy > sizeY || posx > sizeX) Environment.Exit(0);
        }

        public static void AppleGenerator()
        {
            var rand = new Random();
            posAppleX = rand.Next(0, sizeX);
            posAppleY = rand.Next(0, sizeY);
        }
    }
}
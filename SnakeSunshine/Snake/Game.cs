using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Snake
{
    public class Game
    {
        public void Main()
        {
            int gameFieldRow = 40;
            int uiFieldRow = 5;

            int gameFieldColumn = 40;
            int uiFieldColumn = 0;

            int wallLength = 1;

            int gameSpeed = 50; //low - means faster

            ConsoleWindow console = 
                new ConsoleWindow
                (gameFieldRow + uiFieldRow + wallLength*2, 
                gameFieldColumn + uiFieldColumn + wallLength*2, 
                "ASCII Snake");

            while (true)
            {
                World world = new World();

                world.Objects.Add(createSnake(gameFieldRow, uiFieldRow, gameFieldColumn, uiFieldColumn));

                foreach (Wall wall in createWalls(gameFieldRow, uiFieldRow, gameFieldColumn, uiFieldColumn))
                {
                    world.Objects.Add(wall);
                }

                createApple(gameFieldRow, uiFieldRow, gameFieldColumn, uiFieldColumn, wallLength, console, world);

                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                checkKeyboardAsync(console, world, world.GetSnake(), token);

                while (console.WindowUpdate())
                {
                    Thread.Sleep(gameSpeed);

                    Snake snake = world.GetSnake();

                    if (snake != null)
                        snake.Move(console.GetKey());

                    GroupObj thatContactWithSnake = world.WithWhatSnakeIsContact();

                    if (thatContactWithSnake != null)
                    {
                        if (thatContactWithSnake.GetType() == new Apple(null).GetType())
                        {
                            world.Objects.Remove(world.GetApple());
                            snake.Lengthen();
                        }
                        else if ((thatContactWithSnake.GetType() == new Wall(null).GetType()))
                        {
                            break;
                        }
                        else if (thatContactWithSnake.GetType() == new Snake().GetType())
                        {
                            break;
                        }
                    }

                    if (!world.IsAppleExist())
                    {
                        createApple(gameFieldRow, uiFieldRow, gameFieldColumn, uiFieldColumn, wallLength, console, world);
                    }

                    Renderer.Render(console, world.Objects);
                }

                cts.Cancel();

                if (!console.WindowUpdate())
                {
                    break;
                }
            }
        }

        private Snake createSnake(int gameFieldRow, int uiFieldRow, int gameFieldColumn, int uiFieldColumn)
        {
            Snake createdSnake = new Snake();

            int i = 0;

            foreach (Obj obj in createdSnake.Parts)
            {
                if (i == 0)
                {
                    createdSnake.Parts[0].Row = uiFieldRow + (gameFieldRow / 2);
                    createdSnake.Parts[0].Column = uiFieldColumn + (gameFieldColumn / 2);
                }
                else
                {
                    createdSnake.Parts[i].Row = createdSnake.Parts[i - 1].Row;
                    createdSnake.Parts[i].Column = createdSnake.Parts[i - 1].Column - 1;
                }

                i++;
            }

            return createdSnake;
        }

        private Wall[] createWalls(int gameFieldRow, int uiFieldRow, int gameFieldColumn, int uiFieldColumn)
        {
            List<Wall> walls = new List<Wall>();

            for (int i = 0; i < gameFieldColumn + uiFieldColumn + 1*2; i++)
            {
                walls.Add(new Wall(new WallObj(uiFieldRow, i + uiFieldColumn)));
            }

            for (int i = 0; i < gameFieldRow + 1; i++)
            {
                walls.Add(new Wall(new WallObj(uiFieldRow + 1 + i, uiFieldColumn)));
                walls.Add(new Wall(new WallObj(uiFieldRow + 1 + i, gameFieldColumn + uiFieldColumn + 1)));
            }

            for (int i = 0; i < gameFieldColumn + uiFieldColumn + 1; i++)
            {
                walls.Add(new Wall(new WallObj(gameFieldRow + uiFieldRow + 1, i + uiFieldColumn)));
            }

            Wall[] wallsArray = walls.ToArray();

            return wallsArray;
        }

        private Apple createApple(int gameFieldRow, int uiFieldRow, int gameFieldColumn, int uiFieldColumn, int wallLength, 
                                  ConsoleWindow console, World world)
        {
            Apple apple = new Apple(null);

            while (true)
            {
                int perhapsAppleRow = new Random().Next(uiFieldRow + wallLength, console.Rows - wallLength);
                int perhapsAppleColumn = new Random().Next(uiFieldColumn + wallLength, console.Cols - wallLength);

                apple = new Apple(new AppleObj(perhapsAppleRow, perhapsAppleColumn));

                if (world.IsEmptyCell(perhapsAppleRow, perhapsAppleColumn))
                {
                    world.Objects.Add(apple);
                    break;
                }
            }

            return apple;
        }

        private async void checkKeyboardAsync(ConsoleWindow console, World world, Snake snake, CancellationToken token)
        {
            if (snake != null)
            {
                await Task.Run(() => sendKeyInfo(console, snake, token));
            }
        }

        /// <summary>
        /// Player mode
        /// </summary>
        /// <param name="console"></param>
        /// <param name="snake"></param>
        /// <param name="token"></param>
        private void sendKeyInfo(ConsoleWindow console, Snake snake, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                snake.TrySetNewDirection(console.GetKey());
            }
        }

        /// <summary>
        /// AI mode
        /// </summary>
        /// <param name="console"></param>
        /// <param name="snake"></param>
        /// <param name="token"></param>
        private void sendKeyInfo(World world, Snake snake, CancellationToken token)
        {
            AI ai = new AI();

            while (!token.IsCancellationRequested)
            {
                snake.TrySetNewDirection(ai.MakeDecision(world));
            }
        }
    }
}

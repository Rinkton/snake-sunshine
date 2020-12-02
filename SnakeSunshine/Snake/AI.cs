using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class AI
    {
        public Key MakeDecision(World world)
        {
            Snake snake = world.GetSnake();

            string[] factors = new string[3];
            int[] bestChoices = new int[3];
            int choice = 0;

            switch (snake.Direction)
            {
                case 0:
                    factors = new string[3]
                    {
                        whatItIs(world, snake.Parts[0].Row, snake.Parts[0].Column - 1),
                        whatItIs(world, snake.Parts[0].Row-1, snake.Parts[0].Column),
                        whatItIs(world, snake.Parts[0].Row, snake.Parts[0].Column + 1),
                    };

                    bestChoices = whatIsBest(factors);

                    choice = finalChoose(bestChoices);

                    if (choice == 0)
                    {
                        return Key.A;
                    }
                    else if (choice == 1)
                    {
                        return Key.W;
                    }
                    else if (choice == 2)
                    {
                        return Key.D;
                    }
                    break;
                case 1:
                    factors = new string[3]
                    {
                        whatItIs(world, snake.Parts[0].Row - 1, snake.Parts[0].Column),
                        whatItIs(world, snake.Parts[0].Row, snake.Parts[0].Column + 1),
                        whatItIs(world, snake.Parts[0].Row + 1, snake.Parts[0].Column),
                    };

                    bestChoices = whatIsBest(factors);

                    choice = finalChoose(bestChoices);

                    if (choice == 0)
                    {
                        return Key.W;
                    }
                    else if (choice == 1)
                    {
                        return Key.D;
                    }
                    else if (choice == 2)
                    {
                        return Key.S;
                    }
                    break;
                case 2:
                    factors = new string[3]
                    {
                        whatItIs(world, snake.Parts[0].Row, snake.Parts[0].Column + 1),
                        whatItIs(world, snake.Parts[0].Row + 1, snake.Parts[0].Column),
                        whatItIs(world, snake.Parts[0].Row, snake.Parts[0].Column - 1),
                    };

                    bestChoices = whatIsBest(factors);

                    choice = finalChoose(bestChoices);

                    if (choice == 0)
                    {
                        return Key.D;
                    }
                    else if (choice == 1)
                    {
                        return Key.S;
                    }
                    else if (choice == 2)
                    {
                        return Key.A;
                    }
                    break;
                case 3:
                    factors = new string[3]
                    {
                        whatItIs(world, snake.Parts[0].Row + 1, snake.Parts[0].Column),
                        whatItIs(world, snake.Parts[0].Row, snake.Parts[0].Column - 1),
                        whatItIs(world, snake.Parts[0].Row - 1, snake.Parts[0].Column),
                    };

                    bestChoices = whatIsBest(factors);

                    choice = finalChoose(bestChoices);

                    if (choice == 0)
                    {
                        return Key.S;
                    }
                    else if (choice == 1)
                    {
                        return Key.A;
                    }
                    else if (choice == 2)
                    {
                        return Key.W;
                    }
                    break;
            }

            throw new Exception("No decision?");
        }

        /// <summary>
        /// Can return null!!!
        /// </summary>
        /// <param name="world"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string whatItIs(World world, int row, int column)
        {
            Apple apple = null;
            while (apple == null)
            {
                apple = world.GetApple();
            }

            GroupObj[] wObjects = world.Objects.ToArray();

            if (apple.Parts[0].Row == row && apple.Parts[0].Column == column)
            {
                return "apple";
            }

            foreach (GroupObj gObj in wObjects)
            {
                if (gObj.GetType() == new Wall(null).GetType())
                {
                    if (gObj.Parts[0].Row == row && gObj.Parts[0].Column == column)
                    {
                        return "wall";
                    }
                }
            }

            Snake snake = world.GetSnake();

            foreach (Obj part in snake.Parts)
            {
                if (part.GetType() != new Head().GetType() && part.Row == row && part.Column == column)
                {
                    return "snake";
                }
            }

            return "nothing";
        }

        private int[] whatIsBest(string[] factors)
        {
            int[] bestChoices = new int[3];

            bestChoices = best(factors, "apple");

            if (bestChoices.Length != 0)
            {
                return bestChoices;
            }

            bestChoices = best(factors, "wall");

            if (bestChoices.Length != 0)
            {
                return bestChoices;
            }

            bestChoices = best(factors, "snake");

            if (bestChoices.Length != 0)
            {
                return bestChoices;
            }

            bestChoices = best(factors, "nothing");

            if (bestChoices.Length != 0)
            {
                return bestChoices;
            }

            throw new Exception("No choices?!");
        }

        private int[] best(string[] factors, string check)
        {
            List<int> bestChoices = new List<int>();
            int i = 0;

            foreach (string factor in factors)
            {
                if (factor == check)
                {
                    bestChoices.Add(i);
                }

                i++;
            }

            return bestChoices.ToArray();
        }

        private int finalChoose(int[] bestChoices)
        {
            int chooseNum = 0;
        
            if (bestChoices.Length > 1)
            {
                chooseNum = new Random().Next(0, bestChoices.Length);
            }

            return chooseNum;
        }
    }
}

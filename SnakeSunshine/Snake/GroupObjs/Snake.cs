using OpenTK.Input;
using System.Collections.Generic;

namespace Snake
{
    class Snake : GroupObj
    {
        /// <summary>
        /// 0 - up |
        /// 1 - right |
        /// 2 - down |
        /// 3 - left.
        /// </summary>
        public byte Direction { get; private set; }
        private byte previousDirection;

        private int tailPreviousRow;
        private int tailPreviousColumn;

        public Snake()
        {
            Parts = new List<Obj> { new Head(), new Body(), new Tail() };
            Direction = 1;
        }

        #region move
        public void Move(Key key)
        {
            if (isNewDirectionCorrect(previousDirection))
            {
                moveSnakeParts();
            }
        }

        private void moveSnakeParts()
        {
            tailPreviousRow = Parts[Parts.Count - 1].Row;
            tailPreviousColumn = Parts[Parts.Count - 1].Column;

            for (int i = Parts.Count-1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Direction)
                    {
                        case 0:
                            Parts[0].Row--;
                            break;
                        case 1:
                            Parts[0].Column++;
                            break;
                        case 2:
                            Parts[0].Row++;
                            break;
                        case 3:
                            Parts[0].Column--;
                            break;
                    }
                }
                else
                {
                    Parts[i].Row = Parts[i - 1].Row;
                    Parts[i].Column = Parts[i - 1].Column;
                }
            }
        }
        #endregion

        #region set new direction
        /// <summary>
        /// Can do not change.
        /// </summary>
        /// <param name="key"></param>
        public void TrySetNewDirection(Key key)
        {
            previousDirection = Direction;

            switch (key)
            {
                case Key.W:
                    Direction = 0;
                    break;
                case Key.D:
                    Direction = 1;
                    break;
                case Key.S:
                    Direction = 2;
                    break;
                case Key.A:
                    Direction = 3;
                    break;
            }

            bool isCorrectDirection = isNewDirectionCorrect(previousDirection);

            if (!isCorrectDirection)
                Direction = previousDirection;
        }

        /// <summary>
        /// Check, is new direction will cause of entered snake to herself.
        /// </summary>
        /// <param name="potentialNewDirection"></param>
        /// <returns></returns>
        private bool isNewDirectionCorrect(int previousDirection)
        {
            return ((previousDirection + 2) % 4) != Direction;
        }
        #endregion

        public void Lengthen()
        {
            Tail tail = (Tail)Parts[Parts.Count - 1];

            Parts.Remove(tail);

            Body newPart = new Body
            {
                Row = tail.Row,
                Column = tail.Column
            };

            Parts.Add(newPart);

            tail.Row = tailPreviousRow;
            tail.Column = tailPreviousColumn;

            Parts.Add(tail);
        }
    }
}

using System;
using System.Collections.Generic;

namespace Snake
{
    class World
    {
        public List<GroupObj> Objects = new List<GroupObj>();

        /// <summary>
        /// Can return null!!!
        /// </summary>
        /// <returns></returns>
        public Snake GetSnake()
        {
            Snake example = new Snake();

            foreach (GroupObj checkObj in Objects)
            {
                if (checkObj.GetType() == example.GetType())
                {
                    return (Snake)checkObj;
                }
            }

            return null;
        }

        /// <summary>
        /// Can return null!!!
        /// </summary>
        /// <returns></returns>
        public Apple GetApple()
        {
            Apple example = new Apple(null);
            GroupObj[] wObjects = Objects.ToArray();

            foreach (GroupObj checkObj in wObjects)
            {
                if (checkObj.GetType() == example.GetType())
                {
                    return (Apple)checkObj;
                }
            }

            return null;
        }

        public bool IsAppleExist()
        {
            Apple perhapsApple = GetApple();

            if (perhapsApple == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsEmptyCell(int row, int column)
        {
            foreach (GroupObj gObj in Objects)
            {
                foreach (Obj obj in gObj.Parts)
                {
                    if (obj.Row == row && obj.Column == column)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #region with what snake is contact
        /// <summary>
        /// Can return null!!!
        /// </summary>
        /// <returns></returns>
        public GroupObj WithWhatSnakeIsContact()
        {
            Snake snake = GetSnake();

            foreach (GroupObj gObj in Objects)
            {
                if (withThisSnakeIsContact(gObj, snake))
                {
                    return gObj;
                }
            }

            return null;
        }

        private bool withThisSnakeIsContact(GroupObj gObj, Snake snake)
        {
            bool isContact = false;

            foreach (Obj obj in gObj.Parts)
            {
                if (obj.GetType() != snake.Parts[0].GetType() && 
                    obj.Row == snake.Parts[0].Row && obj.Column == snake.Parts[0].Column)
                {
                   isContact = true;
                   break;
                }
            }

            return isContact;
        }
        #endregion
    }
}

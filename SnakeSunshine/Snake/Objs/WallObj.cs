using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class WallObj : Obj
    {
        public WallObj(int row, int column)
        {
            Sprite = '#';
            Row = row;
            Column = column;
        }
    }
}

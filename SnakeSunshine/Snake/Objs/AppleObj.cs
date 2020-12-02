using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class AppleObj : Obj
    {
        public AppleObj(int row, int column)
        {
            Sprite = '@';
            Color = Color4.Green;
            Row = row;
            Column = column;
        }
    }
}

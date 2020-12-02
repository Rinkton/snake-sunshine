using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Wall : GroupObj
    {
        public Wall(WallObj wallObj)
        {
            Parts = new List<Obj> { wallObj };
        }
    }
}

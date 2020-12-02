using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Apple : GroupObj
    {
        public Apple(AppleObj appleObj)
        {
            Parts = new List<Obj> { appleObj };
        }
    }
}

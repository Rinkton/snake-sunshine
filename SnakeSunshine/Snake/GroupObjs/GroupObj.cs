using System.Collections.Generic;

namespace Snake
{
    class GroupObj
    {
        public List<Obj> Parts { get; protected set; }

        public GroupObj()
        {
            Parts = new List<Obj> { new Obj() };
        }
    }
}

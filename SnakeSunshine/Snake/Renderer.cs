using OpenTK.Graphics;
using SunshineConsole;
using System.Collections.Generic;

namespace Snake
{
    static class Renderer
    {
        public static void Render(ConsoleWindow console, List<GroupObj> GObjects)
        {
            clearConsole(console);

            foreach (GroupObj gObj in GObjects)
            {
                foreach (Obj obj in gObj.Parts)
                {
                    console.Write(obj.Row, obj.Column, obj.Sprite, obj.Color);
                }
            }
        }

        private static void clearConsole(ConsoleWindow console)
        {
            for (int i = 0; i < console.Rows; i++)
            {
                for (int j = 0; j < console.Cols; j++)
                {
                    console.Write(i, j, ' ', Color4.Black);
                }
            }
        }
    }
}

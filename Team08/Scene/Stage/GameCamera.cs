using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.Stage;
using Microsoft.Xna.Framework.Graphics;

namespace Team08.Scene.Stage
{
    public class GameCamera : StageCamera
    {
        public GameCamera(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }
    }
}

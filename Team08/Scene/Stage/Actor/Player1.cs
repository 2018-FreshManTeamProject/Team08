using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.Def;
using InfinityGame.Device;
using InfinityGame.Device.KeyboardManage;
using InfinityGame.GameGraphics;
using InfinityGame.Stage;
using InfinityGame.Stage.StageContent.Actor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Team08.Scene.Stage.Actor
{
    public class Player1 : Player
    {
        public Player1(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            
        }

        public override void PreUpdate(GameTime gameTime)
        {
            speedv = (IGGamePad.GetRightVelocity(PlayerIndex.One) * 5);
            base.PreUpdate(gameTime);
        }
    }
}

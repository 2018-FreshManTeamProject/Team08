using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfinityGame.GameGraphics;
using InfinityGame.Stage.StageContent.Block;
using InfinityGame.Device;
using InfinityGame.Element;
using Team08.Scene.Stage.Stages;

namespace Team08.Scene.Stage
{
    public class Cheese : Block
    {
        public bool eaten = false;
        Random rnd = new Random();
        public Cheese(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            IsCrimp = false;
        }

        public override void Initialize()
        {
            eaten = false;
            Visible = true;
            Image = ImageManage.GetSImage("cheese.png");
            Size = Size.Parse(Image.Image.Size);
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            if (eaten)
            {
                ((GameStage)Stage).eatedCheese--;
                Initialize();
            }
            base.Initialize();
        }

        public override void LoadContent()
        {
            Initialize();
            base.LoadContent();
        }
    }
}

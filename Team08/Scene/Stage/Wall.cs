using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfinityGame.GameGraphics;
using InfinityGame.Stage.StageObject;
using InfinityGame.Stage.StageObject.Block;
using InfinityGame.Device;
using InfinityGame.Element;
using MouseTrash.Scene.Stage.Actor;

namespace MouseTrash.Scene.Stage
{
    public class Wall : Crimp
    {
        Random rnd = new Random();
        public Wall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : this(aGraphicsDevice, aParent, "Null") { }

        public Wall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            DrawOrder = 6;
        }

        public override void Initialize()
        {
            Size = Size.Parse(Image.Image.Size);
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            base.Initialize();
        }
        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("wall.png");
            base.LoadContent();
        }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            
            base.CalAllColl(tempSO);
        }
    }
}

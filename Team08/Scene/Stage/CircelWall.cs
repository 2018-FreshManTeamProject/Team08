using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.Element;
using InfinityGame.Stage.StageObject.Block;
using InfinityGame.Device;
using InfinityGame.Stage.StageObject;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MouseTrash.Scene.Stage
{
    public class CircelWall : Crimp, ICircle
    {
        private string[] circlewall = new string[] { "circelwall0.png", "circelwall1.png" };
        private Random rnd = new Random();
        private Circle circle;

        public Circle Circle { get => circle; set => circle = value; }

        public CircelWall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            DrawOrder = 6;
        }

        public override void Initialize()
        {
            Visible = true;
            Image = ImageManage.GetSImage(circlewall[rnd.Next(circlewall.Length)]);
            Size = Size.Parse(Image.Image.Size);
            circle = new Circle(this, size.Width / 2);
            Render.Scale = Vector2.One;
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (Size.Width < 5)
                    Visible = false;
            }
            base.Update(gameTime);
        }

        
    }
}

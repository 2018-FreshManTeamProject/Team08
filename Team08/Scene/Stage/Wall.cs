using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame.GameGraphics;
using InfinityGame.Stage.StageContent;
using InfinityGame.Stage.StageContent.Block;
using InfinityGame.Device;
using InfinityGame.Element;
using Microsoft.Xna.Framework;

namespace Team08.Scene.Stage
{
    public class Wall : Crimp
    {
        Random rnd = new Random();
        public Wall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : this(aGraphicsDevice, aParent, "Null") { }

        public Wall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void Initialize()
        {
            Image = ImageManage.GetSImage("wall.png");
            Size = Size.Parse(Image.Image.Size);
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void CalAllColl(Dictionary<string, BaseStageContent> tempbsc)
        {
            string[] nm = tempbsc.Keys.ToArray();
            foreach(var l in nm)
            {
                if (tempbsc.ContainsKey(l))
                {
                    if (tempbsc[l] is Cheese)
                    {
                        tempbsc[l].Initialize();
                    }
                }
            }
            base.CalAllColl(tempbsc);
        }
    }
}

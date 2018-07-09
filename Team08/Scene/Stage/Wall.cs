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
using Team08.Scene.Stage.Actor;

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

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            string[] nm = tempSO.Keys.ToArray();
            foreach(var l in nm)
            {
                if (tempSO.ContainsKey(l))
                {
                    if (tempSO[l] is Cheese)
                    {
                        tempSO[l].Initialize();
                    }
                    //if (tempSO[l] is Player)
                    //{
                    //    ((Player)tempSO[l]).ClearSpeedv();
                    //}
                }
            }
            base.CalAllColl(tempSO);
        }
    }
}

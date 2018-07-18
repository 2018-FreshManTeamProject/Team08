using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.Device;
using InfinityGame.GameGraphics;
using InfinityGame.Stage.StageObject;
using InfinityGame.Stage.StageObject.Block;
using InfinityGame.Element;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MouseTrash.Scene.Stage.Actor;

namespace MouseTrash.Scene.Stage
{
    public class ElasticityWall : Crimp
    {
        private Random rnd = new Random();
        protected int vibrationTime = 500;
        public ElasticityWall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            
        }
        public override void Initialize()
        {
            Size = Size.Parse(Image.Image.Size);
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            base.Initialize();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("elasticitywall.png");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            string[] nm = tempSO.Keys.ToArray();
            foreach (var l in nm)
            {
                if (tempSO.ContainsKey(l))
                {
                    if (tempSO[l] is Player)
                    {
                        bool fx = false;
                        bool fy = false;
                        if (tempSO[l].NewSpace.Center.X < Coordinate.X || tempSO[l].NewSpace.Center.X > Coordinate.X + size.Width)
                        {
                            fx = true;
                        }
                        if (tempSO[l].NewSpace.Center.Y < Coordinate.Y || tempSO[l].NewSpace.Center.Y > Coordinate.Y + size.Height)
                        {
                            fy = true;
                        }
                        ((Player)tempSO[l]).SpeedVibration(vibrationTime);
                        if (tempSO[l].Team == "antivirus")
                            ((Player)tempSO[l]).FlipSpeed(4, fx, fy);
                        else if (tempSO[l].Team == "mouse")
                            ((Player)tempSO[l]).FlipSpeed(20, fx, fy);
                    }
                }
            }
            foreach (var l in tempSO)
            {
                if (l.Value is Wall || l.Value is ElasticityWall)
                {
                    Initialize();
                    break;
                }
            }
            base.CalAllColl(tempSO);
        }
    }
}

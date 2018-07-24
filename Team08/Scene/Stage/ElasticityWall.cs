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
using Microsoft.Xna.Framework.Audio;

using MouseTrash.Scene.Stage.Actor;
using MouseTrash.Scene.Stage.Stages;

namespace MouseTrash.Scene.Stage
{
    public class ElasticityWall : Crimp
    {
        private Random rnd = new Random();
        protected int vibrationTime = 500;
        public ElasticityWall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            DrawOrder = 6;
        }
        public override void Initialize()
        {
            Visible = true;
            float i = rnd.Next(3, 27) / 10f;
            float j = 3 - i;
            Size = Size.Parse(Image.Image.Size);
            Size = new Size((int)(Size.Width * i), (int)(Size.Height * j));
            Render.Scale = new Vector2(i, j);
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            base.Initialize();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("elasticitywall.png");
            sounds["elasticitywall"] = SoundManage.GetSound("elasticitywall.wav");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (Size.Width < 5 || Size.Height < 5)
                    Visible = false;
            }
            base.Update(gameTime);
        }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            string[] nm = tempSO.Keys.ToArray();
            foreach (var l in nm)
            {
                if (((GameStage)Stage).StartTime > 0)
                    break;
                if (tempSO.ContainsKey(l))
                {
                    if (tempSO[l] is Player && ((Player)tempSO[l]).Life)
                    {
                        sounds["elasticitywall"].Stop();
                        sounds["elasticitywall"].Play();
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
                            ((Player)tempSO[l]).FlipSpeed(10, fx, fy);
                    }
                }
            }
            foreach (var l in tempSO)
            {
                if (l.Value is Wall || l.Value is ElasticityWall || l.Value is CircelWall)
                {
                    ((GameStage)Stage).StartTime++;
                    Initialize();
                    break;
                }
            }
            base.CalAllColl(tempSO);
        }
    }
}

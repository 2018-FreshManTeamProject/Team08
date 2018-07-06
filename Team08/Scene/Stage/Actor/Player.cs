using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.Def;
using InfinityGame.Device.KeyboardManage;
using InfinityGame.GameGraphics;
using InfinityGame.Stage;
using InfinityGame.Stage.StageContent;
using InfinityGame.Stage.StageContent.Actor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Team08.Scene.Stage.Stages;

namespace Team08.Scene.Stage.Actor
{
    public class Player : Character
    {
        protected Vector2 speedv = Vector2.Zero;
        protected bool life = true;
        public Player(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            IsCrimp = true;
        }

        public override void Initialize()
        {
            Render.Color = Color.White;
            IsCrimp = true;
            life = true;
            base.Initialize();
        }

        public override void PreUpdate(GameTime gameTime)
        {
            if (speedv != Vector2.Zero)
            {
                Vector2 t = speedv;
                speedv -= t * 0.01f;
                if (speedv.Length() < 0.05f)
                {
                    speedv = Vector2.Zero;
                }
            }
            if (life)
                AddVelocity(speedv, VeloParam.Run);
            else
                speedv = Vector2.Zero;
            if (speedv == Vector2.Zero)
            {
                ImageRunState = 0;
            }
            else
            {
                ImageRunState = 1;
            }
            base.PreUpdate(gameTime);
        }

        public override void CalAllColl(Dictionary<string, BaseStageContent> tempbsc)
        {
            string[] nm = tempbsc.Keys.ToArray();
            foreach (var l in nm)
            {
                if (tempbsc.ContainsKey(l))
                {
                    if (Team == "mouse")
                    {
                        if (tempbsc[l] is Cheese)
                        {
                            tempbsc[l].Render.Color = Color.Red;
                            Eat(tempbsc[l]);
                        }
                    }
                    else if (Team == "cat")
                    {
                        if (tempbsc[l].Team == "mouse")
                        {
                            Eat(tempbsc[l]);
                        }
                    }
                }
            }
            base.CalAllColl(tempbsc);
        }

        protected void Eat(BaseStageContent bsc)
        {
            if (Team == "mouse")
            {
                if (!((Cheese)bsc).eaten)
                {
                    ((Cheese)bsc).eaten = true;
                    bsc.Visible = false;
                    ((GameStage)Stage).eatcheese++;
                }
                Console.WriteLine(((GameStage)Stage).eatcheese);
            }
            else if (Team == "cat")
            {
                if (((Player)bsc).life)
                {
                    ((Player)bsc).life = false;
                    ((Player)bsc).Render.Color = Color.Red;
                    ((Player)bsc).IsCrimp = false;
                    ((GameStage)Stage).killmouse++;
                }
            }
        }
    }
}

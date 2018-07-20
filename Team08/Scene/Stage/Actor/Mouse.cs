using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.Device;
using InfinityGame.GameGraphics;
using InfinityGame.Element;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MouseTrash.Scene.Stage.Stages;
using InfinityGame.Stage.StageObject;

namespace MouseTrash.Scene.Stage.Actor
{
    public class Mouse : Player
    {
        public Mouse(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
        }

        public override void Initialize()
        {
            MovePriority = 5;
            Image = ImageManage.GetSImage("nezumi.png");
            Size = Size.Parse(Image.Image.Size) / 2;
            base.Initialize();
        }

        protected override void SetPlayer()
        {
            power = 0.2f;
            mass = 0.1f;
            frictional = 0.1f;
            maxSpeed = 5;
        }

        public override void PreUpdate(GameTime gameTime)
        {
            if (((GameStage)Stage).startTime > 0)
            {
                var tempdict = Detector.Circel(this, 500);
                foreach (var l in tempdict)
                {
                    if (l.Value.Team == "antivirus")
                    {
                        ((GameStage)Stage).startTime++;
                        Initialize();
                        break;
                    }
                }
            }
            base.PreUpdate(gameTime);
        }

        protected override void AccelAnime()
        {
            if ((speedv + actionSpeed).Length() > 5)
                imageTimeCounter++;
        }

        protected override void Eat(StageObj stageObj)
        {
            if (stageObj is TheData && life && !((TheData)stageObj).eaten)
            {
                ((TheData)stageObj).eaten = true;
                stageObj.Visible = false;
                ((GameStage)Stage).eatedTheData++;
                point++;
            }
        }

        protected override void TimeDownAction()
        {
            actionMaxSpeed = 0;
            base.TimeDownAction();
        }

        protected override void Action()
        {
            if (point > 0)
            {
                point--;
                actionMaxSpeed = 5;
                TimeDownCount = 120;
            }
        }
    }
}

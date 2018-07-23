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
using Microsoft.Xna.Framework.Input;

namespace MouseTrash.Scene.Stage.Actor
{
    public class Mouse : Player
    {
        private bool isDamage = false;
        private int selfDamage = 0;

        public int SelfDamage { get { return selfDamage; } }
        public Mouse(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
        }

        public override void Initialize()
        {
            isDamage = false;
            selfDamage = 0;
            MovePriority = 5;
            if (playerControl != null && playerControl.Chara != null)
            {
                Image = ImageManage.GetSImage(playerControl.Chara + ".png");
                Size = Size.Parse(Image.Image.Size) / 2;
            }
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
            if (((GameStage)Stage).StartTime > 0)
            {
                var tempdict = Detector.Circel(this, 500);
                foreach (var l in tempdict)
                {
                    if (l.Value.Team == "antivirus")
                    {
                        ((GameStage)Stage).StartTime++;
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
            if (stageObj is TheData && life && !((TheData)stageObj).Eaten && isDamage)
            {
                ((TheData)stageObj).Eaten = true;
                stageObj.Visible = false;
                ((GameStage)Stage).EatedTheData++;
                point += 10;
                ((GameStage)Stage).MousePoint += 20;
            }
        }

        protected override void TimeDownAction()
        {
            actionMaxSpeed = 0;
            base.TimeDownAction();
        }

        protected override void Action()
        {
            if (point >= 200)
            {
                point -= 200;
                actionMaxSpeed = 5;
                TimeDownCount += 120;
            }
        }

        protected override void ActionB()
        {
            isDamage = (IGGamePad.GetKeyState(playerControl.Player, Buttons.B) && PlayerState["paralysis"] <= 0);
            if (isDamage)
                selfDamage++;
            else if (selfDamage > 0)
                selfDamage--;

            if (selfDamage > 500)
            {
                PlayerState["paralysis"] = selfDamage;
                selfDamage--;
            }
            base.ActionB();
        }

        public override void CalCrimpColl(Dictionary<string, StageObj> tempSO)
        {
            if (isDamage)
            {
                foreach (var l in tempSO)
                {
                    if (l.Value is Wall || l.Value is ElasticityWall || l.Value is CircelWall)
                    {
                        l.Value.Size -= new Size(1, 1);
                        l.Value.Coordinate += new Vector2(0.5f, 0.5f);
                        l.Value.Render.Scale = l.Value.Size.ToVector2() / Size.Parse(l.Value.Image.Image.Size).ToVector2();
                        if (l.Value is ICircle)
                            ((ICircle)l.Value).Circle.Radius -= 0.5f;
                        point++;
                        ((GameStage)Stage).MousePoint++;
                    }
                }
            }
            base.CalCrimpColl(tempSO);
        }
    }
}

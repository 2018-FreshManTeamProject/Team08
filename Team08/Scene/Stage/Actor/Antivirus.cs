using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.Device;
using InfinityGame.GameGraphics;
using InfinityGame.Element;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MouseTrash.Scene.Stage.Stages;
using InfinityGame.Stage.StageObject;
using InfinityGame.Stage;

namespace MouseTrash.Scene.Stage.Actor
{
    public class Antivirus : Player
    {
        private string chara;
        private bool small = false;

        public bool Small { get => small; set => small = value; }

        public Antivirus(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            Render.Scale = Vector2.One / 10;
        }

        public override void Initialize()
        {
            small = false;
            MovePriority = 4;
            chara = "gomi";
            charaImages[chara + "_front_run"] = ImageManage.GetSImage(chara + "_front_run");
            charaImages[chara + "_back_run"] = ImageManage.GetSImage(chara + "_back_run");
            charaImages[chara + "_left_run"] = ImageManage.GetSImage(chara + "_left_run");
            charaImages[chara + "_right_run"] = ImageManage.GetSImage(chara + "_right_run");
            SetChara(chara + "_front_run");
            base.Initialize();
        }

        public override void LoadContent()
        {
            sounds["dush"] = SoundManage.GetSound("dush.wav");
            base.LoadContent();
        }

        public override void AddVelocity(Vector2 velocity, VeloParam veloParam)
        {
            if (veloParam == VeloParam.Run)
                RunChara(velocity);
            base.AddVelocity(velocity, veloParam);
        }

        protected override void RunChara(Vector2 ve)
        {
            if (Math.Abs(ve.Y) >= Math.Abs(ve.X))
            {
                if (ve.Y >= 0)
                    SetChara(chara + "_front_run");
                else
                    SetChara(chara + "_back_run");
            }
            else
            {
                if (ve.X >= 0)
                    SetChara(chara + "_right_run");
                else
                    SetChara(chara + "_left_run");
            }
        }

        protected void SetChara(string charaSide)
        {
            if (nowCharaSide != charaSide)
            {
                if (image != null)
                {
                    if (iTIndex > Image.ImageT.Count - 1)
                    {
                        iTIndex = 1;
                        imageTimeCounter = 0;
                    }
                }
                else
                {
                    iTIndex = 0;
                    imageTimeCounter = 0;
                }
                nowCharaSide = charaSide;
                Image = charaImages[charaSide];
                if (playerControl != null && !IGGamePad.GetKeyState(playerControl.Player, Buttons.B))
                    Size = Size.Parse(Image.Image.Size) / 10;
            }
        }

        protected override void SetPlayer()
        {
            power = 1;
            mass = 2;
            frictional = 0.02f;
            maxSpeed = 7;
        }

        protected override void ActionB()
        {
            if (IGGamePad.GetKeyState(playerControl.Player, Buttons.B) && PlayerState["paralysis"] <= 0)
            {
                small = true;
                if (Render.Scale == Vector2.One / 10)
                {
                    Size = Size.Parse(image.Image.Size) / 40;
                    Render.Scale = Vector2.One / 40;
                    Coordinate += (Size.Parse(image.Image.Size) / 20).ToVector2();
                    actionMaxSpeed = -3;
                }
            }
            else
            {
                small = false;
                if (Render.Scale != Vector2.One / 10)
                {
                    Size = Size.Parse(image.Image.Size) / 10;
                    Coordinate -= (Size.Parse(image.Image.Size) / 20).ToVector2();
                    Render.Scale = Vector2.One / 10;
                    actionMaxSpeed = 0;
                }
            }
            base.ActionB();
        }

        protected override void AccelAnime()
        {
            if ((speedv + actionSpeed).Length() > 7)
                imageTimeCounter++;
        }

        protected override void Eat(StageObj stageObj)
        {
            if (stageObj is Mouse && ((Player)stageObj).Life)
            {
                ((Player)stageObj).SetVibration(1, 1, 1000);
                ((Player)stageObj).ActionSpeed = 10 * (speedv + actionSpeed);
                ((GameStage)Stage).AntivirusPoint += ((Player)stageObj).Point;
                ((GameStage)Stage).MousePoint -= ((Player)stageObj).Point;
                ((Player)stageObj).Life = false;
                stageObj.Color = Color.Red;
                stageObj.CrimpGroup = "";
                stageObj.MovePriority = 6;
                ((GameStage)Stage).KilledMouse++;
            }
        }

        protected override void Action()
        {
            if (TimeDownCount == 0)
            {
                sounds["dush"].Play();
                TimeDownCount = 300;
                Vector2 ve = speedv;
                ve.Normalize();
                actionSpeed = (small) ? ve * 12 : ve * 24;
            }
        }
    }
}

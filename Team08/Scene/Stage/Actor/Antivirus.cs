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

namespace MouseTrash.Scene.Stage.Actor
{
    public class Antivirus : Player
    {
        public Antivirus(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
        }

        public override void Initialize()
        {
            MovePriority = 4;
            chara = "gomi";
            charaImages[chara + "_frontside"] = ImageManage.GetSImage(chara + "_frontside");
            charaImages[chara + "_backside"] = ImageManage.GetSImage(chara + "_backside");
            charaImages[chara + "_leftside"] = ImageManage.GetSImage(chara + "_leftside");
            charaImages[chara + "_rightside"] = ImageManage.GetSImage(chara + "_rightside");
            SetChara(chara + "_frontside");
            base.Initialize();
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
            if (IGGamePad.GetKeyState(player, Buttons.B) && PlayerState["paralysis"] <= 0)
            {
                if (Render.Scale == Vector2.One / 2)
                {
                    Size = Size.Parse(image.Image.Size) / 8;
                    Render.Scale = Vector2.One / 8;
                    Coordinate += (Size.Parse(image.Image.Size) / 4).ToVector2();
                    actionMaxSpeed = -3;
                }
            }
            else
            {
                if (Render.Scale != Vector2.One / 2)
                {
                    Size = Size.Parse(image.Image.Size) / 2;
                    Coordinate -= (Size.Parse(image.Image.Size) / 4).ToVector2();
                    Render.Scale = Vector2.One / 2;
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
            if (((Player)stageObj).Life)
            {
                ((Player)stageObj).SetVibration(1, 1, 1000);
                ((Player)stageObj).ActionSpeed = 10 * (speedv + actionSpeed);
                ((Player)stageObj).Life = false;
                stageObj.Color = Color.Red;
                stageObj.CrimpGroup = "";
                stageObj.MovePriority = 6;
                ((GameStage)Stage).killedMouse++;
            }
        }

        protected override void Action()
        {
            if (TimeDownCount == 0)
            {
                TimeDownCount = 300;
                Vector2 ve = speedv;
                ve.Normalize();
                actionSpeed = ve * 25;
            }
        }
    }
}

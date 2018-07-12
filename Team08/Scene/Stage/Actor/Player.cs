using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InfinityGame.Def;
using InfinityGame.Device;
using InfinityGame.Device.KeyboardManage;
using InfinityGame.GameGraphics;
using InfinityGame.Stage;
using InfinityGame.Stage.StageObject;
using InfinityGame.Stage.StageObject.Actor;
using Team08.Scene.Stage.Stages;

namespace Team08.Scene.Stage.Actor
{
    public class Player : Character
    {

        protected bool life = true;
        Random rnd = new Random();

        protected int point = 0;
        private int timeDownCount = 0;
        public PlayerIndex player;
        private float power = 0;//作用力
        private float mass = 0;//質量
        private float accel = 0;//加速度
        private float magnification = 1;//加速度倍率
        protected Vector2 speedv = Vector2.Zero;//速度
        private Vector2 direction = Vector2.Zero;
        private float frictional = 0.01f;//摩擦力
        private float maxSpeed = 0;//最大速度
        private float actionMaxSpeed = 0;
        private Vector2 actionSpeed = Vector2.Zero;
        public int TimeDownCount { get { return timeDownCount; } set { timeDownCount = value; if (timeDownCount == 0) TimeDownAction(); } }
        public bool Life { get { return life; } }
        public int Point { get { return point; } }
        public Player(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            IsCrimp = true;
        }

        public override void Initialize()
        {
            MovePriority = 5;
            CrimpGroup = Team;
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y));
            Color = Color.White;
            IsCrimp = true;
            life = true;
            actionMaxSpeed = 0;
            timeDownCount = 0;
            actionSpeed = Vector2.Zero;
            speedv = Vector2.Zero;
            point = 0;
            SetPlayer();
            base.Initialize();
        }

        private void SetPlayer()
        {
            if (Team == "cat")
            {
                power = 1;
                mass = 2;
                frictional = 0.02f;
                maxSpeed = 7;
            }
            else if (Team == "mouse")
            {
                power = 0.2f;
                mass = 0.1f;
                frictional = 0.1f;
                maxSpeed = 5;
            }
        }

        public void ClearSpeedv()
        {
            speedv = Vector2.Zero;
        }

        public override void PreUpdate(GameTime gameTime)
        {
            if (((GameStage)Stage).startTime <= 0)
            {
                if (IGGamePad.GetKeyTrigger(player, Buttons.A))
                {
                    Action();
                }
                if (speedv != Vector2.Zero)
                {
                    Vector2 t = speedv;
                    speedv -= t * frictional;
                    if (speedv.Length() < 0.05f)
                    {
                        speedv = Vector2.Zero;
                    }
                }
                if (actionSpeed != Vector2.Zero)
                {
                    actionSpeed -= actionSpeed * frictional * 5;
                    AddVelocity(actionSpeed, VeloParam.Run);
                    if (actionSpeed.Length() < 0.05f)
                    {
                        actionSpeed = Vector2.Zero;
                    }
                }
                if (life)
                {
                    direction = IGGamePad.GetLeftVelocity(player);
                    /*if (Name == "player0")
                    {
                        direction = GameKeyboard.GetVelocity(IGConfig.PlayerKeys);
                    }*/
                    if (direction != Vector2.Zero)
                    {
                        accel = ((direction.Length() * power) / mass) * magnification;
                        direction.Normalize();
                        speedv += direction * accel;
                    }
                    if (speedv.Length() > (maxSpeed + actionMaxSpeed))
                    {
                        speedv.Normalize();
                        speedv *= (maxSpeed + actionMaxSpeed);
                    }
                    AddVelocity(speedv, VeloParam.Run);
                }
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
                if (TimeDownCount > 0)
                {
                    TimeDownCount--;
                }
                base.PreUpdate(gameTime);
            }
            else
            {
                if (Team == "mouse" && !life)
                {
                    ((GameStage)Stage).startTime++;
                    Initialize();
                }
            }
        }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            string[] nm = tempSO.Keys.ToArray();
            foreach (var l in nm)
            {
                if (tempSO.ContainsKey(l))
                {
                    if (Team == "mouse")
                    {
                        if (tempSO[l] is Cheese)
                        {
                            Eat(tempSO[l]);
                        }
                    }
                    else if (Team == "cat")
                    {
                        if (tempSO[l].Team == "mouse")
                        {
                            Eat(tempSO[l]);
                        }
                    }
                    if (((GameStage)Stage).startTime > 0)
                    {
                        if (tempSO[l] is Wall || tempSO[l] is Player)
                        {
                            Initialize();
                            ((GameStage)Stage).startTime++;
                        }
                    }
                }
            }
            base.CalAllColl(tempSO);
        }

        protected void Eat(StageObj stageObj)
        {
            if (Team == "mouse" && life)
            {
                if (!((Cheese)stageObj).eaten)
                {
                    ((Cheese)stageObj).eaten = true;
                    stageObj.Visible = false;
                    ((GameStage)Stage).eatedCheese++;
                    point++;
                }
            }
            else if (Team == "cat")
            {
                if (((Player)stageObj).life)
                {
                    ((Player)stageObj).actionSpeed = 10 * (speedv + actionSpeed);
                    ((Player)stageObj).life = false;
                    stageObj.Color = Color.Red;
                    stageObj.CrimpGroup = "";
                    stageObj.MovePriority = 6;
                    if (((GameStage)Stage).startTime <= 0)
                        ((GameStage)Stage).killedMouse++;
                }
            }
        }

        private void TimeDownAction()
        {
            if (Team == "mouse")
            {
                actionMaxSpeed = 0;
            }
        }

        protected void Action()
        {
            if (Team == "mouse")
            {
                if (point > 0)
                {
                    point--;
                    actionMaxSpeed = 5;
                    TimeDownCount = 180;
                }
            }
            else if (Team == "cat")
            {
                if (timeDownCount == 0)
                {
                    TimeDownCount = 600;
                    Vector2 ve = speedv;
                    ve.Normalize();
                    actionSpeed = ve * 25;
                }
            }
        }
    }
}

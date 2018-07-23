using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InfinityGame.Device;
using InfinityGame.GameGraphics;
using InfinityGame.Stage;
using InfinityGame.Stage.StageObject;
using InfinityGame.Stage.StageObject.Actor;
using InfinityGame.Element;
using MouseTrash.Scene.Stage.Stages;

namespace MouseTrash.Scene.Stage.Actor
{
    public abstract class Player : Character
    {

        protected bool life = true;
        Random rnd = new Random();

        protected int point = 0;
        private int timeDownCount = 0;
        protected float power = 0;//作用力
        protected float mass = 0;//質量
        protected float accel = 0;//加速度
        protected float magnification = 1;//加速度倍率
        protected Vector2 speedv = Vector2.Zero;//速度
        private Vector2 direction = Vector2.Zero;
        protected float frictional = 0.01f;//摩擦力
        protected float maxSpeed = 0;//最大速度
        protected float actionMaxSpeed = 0;
        protected Vector2 actionSpeed = Vector2.Zero;
        private string nowCharaSide;
        protected Dictionary<string, SImage> charaImages = new Dictionary<string, SImage>();
        protected PlayerControl playerControl;
        private Dictionary<string, int> playerState = new Dictionary<string, int>();
        public int TimeDownCount { get { return timeDownCount; } set { timeDownCount = value; if (timeDownCount == 0) TimeDownAction(); } }
        public bool Life { get { return life; } set { life = value; } }
        public int Point { get { return point; } }
        public Vector2 ActionSpeed { get { return actionSpeed; } set { actionSpeed = value; } }
        public PlayerControl PlayerControl { get { return playerControl; } set { playerControl = value; } }

        public Dictionary<string, int> PlayerState { get => playerState; set => playerState = value; }

        public Player(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            DrawOrder = 8;
            IsCrimp = true;
            PassIndex_0 = true;
            ImageRunState = 0;
            Render.Scale = Vector2.One / 2;
            PlayerState["paralysis"] = 0;
        }

        public override void Initialize()
        {
            PlayerState["paralysis"] = 0;
            ImageRunState = 0;
            nowCharaSide = "";
            iTIndex = 0;
            imageTimeCounter = 0;
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

        public void SetControl(PlayerControl playerControl)
        {
            this.playerControl = playerControl;
        }

        public void SpeedVibration(int time)
        {
            float l = 0;
            float r = 0;
            Vector2 ve = (speedv + actionSpeed) / 30;
            if (ve.X < 0)
            {
                l += Math.Abs(ve.X);
            }
            else if (ve.X > 0)
                r += Math.Abs(ve.X);
            if (ve.Y != 0)
            {
                l += Math.Abs(ve.Y);
                r += Math.Abs(ve.Y);
            }
            if (l > 1)
                l = 1;
            if (r > 1)
                r = 1;
            Thread thread = new Thread(() => { Vibration(l, r, time); });
            thread.Start();
        }

        public void SetVibration(float l, float r, int time)
        {
            Thread thread = new Thread(() => { Vibration(l, r, time); });
            thread.Start();
        }

        private void Vibration(float l, float r, int time)
        {
            GamePad.SetVibration(playerControl.Player, l, r);
            Thread.Sleep(time);
            GamePad.SetVibration(playerControl.Player, 0, 0);
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
                    Size = Size.Parse(Image.Image.Size) / 2;
            }
        }

        public void FlipSpeed(float magn, bool fx, bool fy)
        {
            if (actionSpeed != Vector2.Zero)
            {
                Vector2 ve = Vector2.Zero;
                if (fx)
                    ve.X = -actionSpeed.X * magn;
                if (fy)
                    ve.Y = -actionSpeed.Y * magn;
                actionSpeed = ve;
            }
            if (speedv != Vector2.Zero)
            {
                Vector2 ve = Vector2.Zero;
                if (fx)
                    ve.X = -speedv.X * magn;
                else
                    ve.X = speedv.X;
                if (fy)
                    ve.Y = -speedv.Y * magn;
                else
                    ve.Y = speedv.Y;
                actionSpeed += ve;
                speedv = Vector2.Zero;
            }
        }

        protected abstract void SetPlayer();

        public void ClearSpeedv()
        {
            speedv = Vector2.Zero;
        }

        public override void PreUpdate(GameTime gameTime)
        {
            if (((GameStage)Stage).StartTime <= 0)
            {
                if (IGGamePad.GetKeyTrigger(playerControl.Player, Buttons.A) && PlayerState["paralysis"] <= 0)
                {
                    Action();
                }
                ActionB();
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
                    direction = IGGamePad.GetLeftVelocity(playerControl.Player);
                    /*if (Name == "antivirus")
                    {
                        direction = GameKeyboard.GetVelocity(IGConfig.PlayerKeys);
                    }
                    if (Name == "mouse1")
                    {
                        direction = GameKeyboard.GetVelocity(IGConfig.CameraKeys);
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
                    if (speedv != Vector2.Zero && PlayerState["paralysis"] <= 0)
                        AddVelocity(speedv, VeloParam.Run);
                }
                else
                    speedv = Vector2.Zero;
                if (PlayerState["paralysis"] <= 0)
                {
                    AccelAnime();
                    if (speedv == Vector2.Zero && actionSpeed == Vector2.Zero)
                    {
                        ImageRunState = 0;
                    }
                    else
                    {
                        ImageRunState = 1;
                    }
                }
                else
                {
                    ImageRunState = 0;
                }
                if (TimeDownCount > 0)
                {
                    TimeDownCount--;
                }
                if (PlayerState["paralysis"] > 0)
                {
                    int cr = Color.R;
                    int cg = Color.G;
                    int cb = Color.B;
                    cr += 15;
                    cg += 10;
                    cb += 5;
                    if (cr > 255)
                        cr = 0;
                    if (cg > 255)
                        cg = 0;
                    if (cb > 255)
                        cb = 0;
                    Color = new Color(cr, cg, cb);
                    PlayerState["paralysis"]--;
                }
                else
                {
                    if (Color != Color.White && life)
                        Color = Color.White;
                }
                base.PreUpdate(gameTime);
            }
        }

        public override void RunImageT()
        {
            if (image != null &&
                image.ImageFreams.Count > 0 &&
                image.ImageFreams[iTIndex] <= 1 &&
                imageTimeCounter > image.ImageFreams[iTIndex])
            {
                if (iTIndex + 1 < image.ImageT.Count)
                    iTIndex++;
                else
                {
                    if (PassIndex_0 && image.ImageT.Count > 1)
                        iTIndex = 1;
                    else
                        iTIndex = 0;
                }
            }
            base.RunImageT();
        }

        protected abstract void AccelAnime();
        protected virtual void ActionB() { }

        protected virtual void RunChara(Vector2 ve) { }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            string[] nm = tempSO.Keys.ToArray();
            foreach (var l in nm)
            {
                if (tempSO.ContainsKey(l))
                {
                    if (((GameStage)Stage).StartTime <= 0)
                    {
                        Eat(tempSO[l]);
                    }
                    else if (tempSO[l] is Wall || tempSO[l] is Player || tempSO[l] is ElasticityWall)
                    {
                        Initialize();
                        ((GameStage)Stage).StartTime++;
                    }
                }
            }
            base.CalAllColl(tempSO);
        }

        protected abstract void Eat(StageObj stageObj);

        protected virtual void TimeDownAction() { }

        protected abstract void Action();
    }
}

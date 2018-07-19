using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using InfinityGame.Element;
using MouseTrash.Scene.Stage.Stages;

namespace MouseTrash.Scene.Stage.Actor
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
        private string chara;
        private string nowCharaSide;
        private Dictionary<string, SImage> charaImages = new Dictionary<string, SImage>();


        /// <summary>
        /// 状態
        /// </summary>
        public Dictionary<string, int> PlayerState = new Dictionary<string, int>();
        public int TimeDownCount { get { return timeDownCount; } set { timeDownCount = value; if (timeDownCount == 0) TimeDownAction(); } }
        public bool Life { get { return life; } }
        public int Point { get { return point; } }
        public string Chara { get { return chara; } set { chara = value; } }
        public Player(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            DrawOrder = 5;
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
            MovePriority = 5;
            CrimpGroup = Team;
            if (Team == "antivirus")
            {
                //変更
                MovePriority = 4;
                chara = "gomi";
                charaImages[chara + "_frontside"] = ImageManage.GetSImage(chara + "_frontside");
                charaImages[chara + "_backside"] = ImageManage.GetSImage(chara + "_backside");
                charaImages[chara + "_leftside"] = ImageManage.GetSImage(chara + "_leftside");
                charaImages[chara + "_rightside"] = ImageManage.GetSImage(chara + "_rightside");
                SetChara(chara + "_frontside");
            }
            else if (Team == "mouse")
            {
                Image = ImageManage.GetSImage("nezumi.png");
                Size = Size.Parse(Image.Image.Size) / 2;
            }
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
            GamePad.SetVibration(player, l, r);
            Thread.Sleep(time);
            GamePad.SetVibration(player, 0, 0);
        }

        private void SetChara(string charaSide)
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
                if (!IGGamePad.GetKeyState(player, Buttons.B))
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

        private void SetPlayer()
        {
            if (Team == "antivirus")
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
                if (IGGamePad.GetKeyTrigger(player, Buttons.A) && PlayerState["paralysis"] <= 0)
                {
                    Action();
                }
                if (Team == "antivirus")
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
                    if (speedv != Vector2.Zero && PlayerState["paralysis"] <= 0)
                        AddVelocity(speedv, VeloParam.Run);
                }
                else
                    speedv = Vector2.Zero;
                if (PlayerState["paralysis"] <= 0)
                {
                    if (Team == "antivirus")
                    {
                        if ((speedv + actionSpeed).Length() > 7)
                            imageTimeCounter++;
                    }
                    else if (Team == "mouse")
                    {
                        if ((speedv + actionSpeed).Length() > 5)
                            imageTimeCounter++;
                    }
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
            else
            {
                if (Team == "mouse")
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
            }
        }

        public override void AddVelocity(Vector2 velocity, VeloParam veloParam)
        {
            if (veloParam == VeloParam.Run)
            {
                if (Team == "antivirus")
                    RunChara(velocity);
            }
            base.AddVelocity(velocity, veloParam);
        }

        private void RunChara(Vector2 ve)
        {
            if (Math.Abs(ve.Y) >= Math.Abs(ve.X))
            {
                if (ve.Y >= 0)
                    SetChara(chara + "_frontside");
                else
                    SetChara(chara + "_backside");
            }
            else
            {
                if (ve.X >= 0)
                    SetChara(chara + "_rightside");
                else
                    SetChara(chara + "_leftside");
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
                        if (tempSO[l] is TheData)
                        {
                            Eat(tempSO[l]);
                        }
                    }
                    else if (Team == "antivirus")
                    {
                        if (tempSO[l].Team == "mouse")
                        {
                            Eat(tempSO[l]);
                        }
                    }
                    if (((GameStage)Stage).startTime > 0)
                    {
                        if (tempSO[l] is Wall || tempSO[l] is Player || tempSO[l] is ElasticityWall)
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
            if (((GameStage)Stage).startTime <= 0)
            {
                if (Team == "mouse" && life)
                {
                    if (!((TheData)stageObj).eaten)
                    {
                        ((TheData)stageObj).eaten = true;
                        stageObj.Visible = false;
                        ((GameStage)Stage).eatedTheData++;
                        point++;
                    }
                }
                else if (Team == "antivirus")
                {
                    if (((Player)stageObj).life)
                    {
                        ((Player)stageObj).SetVibration(1, 1, 1000);
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
                    TimeDownCount = 120;
                }
            }
            else if (Team == "antivirus")
            {
                if (timeDownCount == 0)
                {
                    TimeDownCount = 300;
                    Vector2 ve = speedv;
                    ve.Normalize();
                    actionSpeed = ve * 25;
                }
            }
        }
    }
}

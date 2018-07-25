using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfinityGame.GameGraphics;
using InfinityGame.Stage;
using InfinityGame.Device;
using InfinityGame.Element;
using InfinityGame.UI.UIContent;
using MouseTrash.Scene.Stage.Actor;
using MouseTrash.Scene.Stage.Stages;


namespace MouseTrash.Scene.Stage
{
    public class GameCamera : StageCamera
    {
        private List<string> marks = new List<string>();
        private string lastTeam;
        private string team;
        private SImage mk;
        private Label message;

        public Label Message { get => message; set => message = value; }

        //public Label action;
        public GameCamera(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void PreLoadContent()
        {
            Message = new Label(graphicsDevice, this);
            //action = new Label(graphicsDevice, this);
            
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            Message.Location = new Point(20, 20);
            Message.BackColor = Color.White * 0.0f;
            Message.BDText.ForeColor = System.Drawing.Color.OrangeRed;
            Message.TextSize = 24f;
            /*action.TextSize = 24f;
            action.BackColor = Color.White * 0.0f;
            action.BDText.ForeColor = System.Drawing.Color.Yellow;*/

            base.DesignContent();
        }

        public override void Initialize()
        {
            marks.Clear();
            lastTeam = "";
            team = "";
            Message.Text = Name;
            /*if (FocusStageObj.Team == "mouse")
                action.Text = "Aボタン加速\r\n個人情報を\r\n集めよう";
            else if (FocusStageObj.Team == "antivirus")
                action.Text = "Aボタンダッシュ\r\nBボダン押し続け\r\n縮小する\r\nウィルスマウスを\r\n捕まえよう";
            action.Location = new Point(size.Width - action.Size.Width - 20, 20);*/
            base.Initialize();
        }

        public override void LoadContent()
        {
            Message.Image = ImageManage.GetSImage("panel01.png");
            Image = ImageManage.GetSImage("gamecamera.png");
            mk = ImageManage.GetSImage("yajiru.png");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            CheckTarget();
            base.Update(gameTime);
        }

        private void CheckTarget()
        {
            if (FocusStageObj != null)
            {
                lastTeam = team;
                team = FocusStageObj.Team;
                if (lastTeam != team)
                {
                    marks.Clear();
                    string[] nm = Stage.stageObjs.Keys.ToArray();
                    foreach (var l in nm)
                    {
                        if (Stage.stageObjs.ContainsKey(l))
                        {
                            if (team == "antivirus")
                            {
                                if (Stage.stageObjs[l].Team == "mouse")
                                {
                                    if (!marks.Contains(l))
                                    {
                                        marks.Add(l);
                                    }
                                }
                            }
                            else if (team == "mouse")
                            {
                                if (Stage.stageObjs[l] is TheData)
                                {
                                    if (!marks.Contains(l))
                                    {
                                        marks.Add(l);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (team == "antivirus")
                    {
                        foreach (var l in marks.ToArray())
                        {
                            if (!((Player)Stage.stageObjs[l]).Life)
                            {
                                marks.Remove(l);
                            }
                        }
                        Message.Text = string.Format($"ウィルスマウス残り{((GameStage)Stage).MouseNum - ((GameStage)Stage).KilledMouse}匹\r\nスキルクールタイム：{((Player)FocusStageObj).TimeDownCount / 60}秒");
                    }
                    else if (team == "mouse")
                    {
                        foreach (var l in marks.ToArray())
                        {
                            if (((TheData)Stage.stageObjs[l]).Eaten)
                            {
                                marks.Remove(l);
                            }
                        }
                        Message.Text = string.Format($"個人情報残り{((GameStage)Stage).TheDataNum - ((GameStage)Stage).EatedTheData}個\r\n加速残り：{((Player)FocusStageObj).TimeDownCount / 60}秒\r\nポイント：{((Player)FocusStageObj).Point}\r\n情報麻痺：{((Mouse)FocusStageObj).SelfDamage}/500");
                    }
                }
            }
        }

        public override void Draw2(GameTime gameTime)
        {
            base.Draw2(gameTime);
            if (team == "antivirus" && !((Antivirus)FocusStageObj).Small)
                return;
            foreach (var l in marks.ToArray())
            {
                if (Stage.stageObjs.ContainsKey(l))
                {
                    if (Stage.stageObjs[l].Coordinate.X + Stage.stageObjs[l].Size.Width < CameraLocation.X ||
                        Stage.stageObjs[l].Coordinate.Y + Stage.stageObjs[l].Size.Height < CameraLocation.Y ||
                        Stage.stageObjs[l].Coordinate.X > CameraLocation.X + size.Width ||
                        Stage.stageObjs[l].Coordinate.Y > CameraLocation.Y + size.Height)
                    {
                        Vector2 ve = Stage.stageObjs[l].Coordinate - CameraCenter;
                        ve.Normalize();
                        ve *= size.Height * 0.3f;
                        Vector2 re = location.ToVector2() + (size.ToVector2() / 2) + ve;
                        float ro = (float)Math.Atan2(ve.X, -ve.Y);
                        spriteBatch.Draw(mk.ImageT[0], re, null, Color.White, ro, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}

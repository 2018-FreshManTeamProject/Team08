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
using Team08.Scene.Stage.Actor;
using Team08.Scene.Stage.Stages;


namespace Team08.Scene.Stage
{
    public class GameCamera : StageCamera
    {
        private List<string> marks = new List<string>();
        private string lastTeam;
        private string team;
        private SImage mk;
        public Label message;
        public Label action;
        public GameCamera(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }
        public override void PreLoadContent()
        {
            message = new Label(graphicsDevice, this);
            action = new Label(graphicsDevice, this);
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            message.Location = new Point(20, 20);
            message.BackColor = Color.White * 0.0f;
            message.BDText.ForeColor = System.Drawing.Color.OrangeRed;
            message.TextSize = 24f;
            action.TextSize = 24f;
            action.BackColor = Color.White * 0.0f;
            action.BDText.ForeColor = System.Drawing.Color.Yellow;
            
            base.DesignContent();
        }

        public override void Initialize()
        {
            message.Text = Name;
            if (FocusStageContent.Team == "mouse")
                action.Text = "Aボタン加速";
            else if (FocusStageContent.Team == "cat")
                action.Text = "Aボタンダッシュ";
            action.Location = new Point(size.Width - action.Size.Width - 20, 20);
            base.Initialize();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("stagescene.png");
            mk = ImageManage.GetSImage("yajiru.png");
            Initialize();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            CheckTarget();
            base.Update(gameTime);
        }

        private void CheckTarget()
        {
            if (FocusStageContent != null)
            {
                lastTeam = team;
                team = FocusStageContent.Team;
                if (lastTeam != team)
                {
                    marks.Clear();
                    string[] nm = Stage.stageContents.Keys.ToArray();
                    foreach (var l in nm)
                    {
                        if (Stage.stageContents.ContainsKey(l))
                        {
                            if (team == "cat")
                            {
                                if (Stage.stageContents[l].Team == "mouse")
                                {
                                    if (!marks.Contains(l))
                                    {
                                        marks.Add(l);
                                    }
                                }
                            }
                            else if (team == "mouse")
                            {
                                if (Stage.stageContents[l] is Cheese)
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
                    if (team == "cat")
                    {
                        foreach (var l in marks.ToArray())
                        {
                            if (!((Player)Stage.stageContents[l]).Life)
                            {
                                marks.Remove(l);
                            }
                        }
                        message.Text = string.Format($"ネズミ残り{((GameStage)Stage).mouseNum - ((GameStage)Stage).killedMouse}匹");
                    }
                    else if (team == "mouse")
                    {
                        foreach (var l in marks.ToArray())
                        {
                            if (((Cheese)Stage.stageContents[l]).eaten)
                            {
                                marks.Remove(l);
                            }
                        }
                        message.Text = string.Format($"チーズ残り{((GameStage)Stage).cheeseNum - ((GameStage)Stage).eatedCheese}個\r\nポイント：{((Player)FocusStageContent).Point}");
                    }
                }
            }
        }

        public override void Draw2(GameTime gameTime)
        {
            base.Draw2(gameTime);
            foreach (var l in marks)
            {
                if (Stage.stageContents.ContainsKey(l))
                {
                    if (Stage.stageContents[l].Coordinate.X + Stage.stageContents[l].Size.Width < CameraLocation.X ||
                        Stage.stageContents[l].Coordinate.Y + Stage.stageContents[l].Size.Height < CameraLocation.Y ||
                        Stage.stageContents[l].Coordinate.X > CameraLocation.X + size.Width ||
                        Stage.stageContents[l].Coordinate.Y > CameraLocation.Y + size.Height)
                    {
                        Vector2 ve = Stage.stageContents[l].Coordinate - CameraCenter;
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

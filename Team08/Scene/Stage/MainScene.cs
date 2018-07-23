using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using InfinityGame;
using InfinityGame.GameGraphics;
using InfinityGame.Scene;
using InfinityGame.Device;
using InfinityGame.Def;
using InfinityGame.Element;
using InfinityGame.Device.KeyboardManage;
using InfinityGame.UI.UIContent;
using MouseTrash.Scene.Stage.Stages;
using MouseTrash.Scene.Stage.UI;
using MouseTrash.Scene.Stage.Actor;

namespace MouseTrash.Scene.Stage
{
    public class MainScene : StageScene
    {
        private BackMenu backMenu;
        private GameOver gameOver;
        private GameStage runStage;
        private Label start;
        public MainScene(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void Initialize()
        {
            start.Visible = true;
            for (int i = 0; i < 4; i++)
            {
                if ((int)((Player)stageCameras["C" + i.ToString()].Stage.stageObjs["antivirus"]).PlayerControl.Player == i)
                {
                    stageCameras["C" + i.ToString()].FocusStageObj = stageCameras["C" + i.ToString()].Stage.stageObjs["antivirus"];
                    continue;
                }
                for (int j = 0; j < 3; j++)
                {
                    if ((int)((Player)stageCameras["C" + i.ToString()].Stage.stageObjs["mouse" + j.ToString()]).PlayerControl.Player == i)
                    {
                        stageCameras["C" + i.ToString()].FocusStageObj = stageCameras["C" + i.ToString()].Stage.stageObjs["mouse" + j.ToString()];
                    }
                }
            }
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            backMenu = new BackMenu(graphicsDevice, this);
            gameOver = new GameOver(graphicsDevice, this);
            start = new Label(graphicsDevice, this);
            new GameCamera(graphicsDevice, this, "C0");
            new GameCamera(graphicsDevice, this, "C1");
            new GameCamera(graphicsDevice, this, "C2");
            new GameCamera(graphicsDevice, this, "C3");
            new Stage01(graphicsDevice, this, "Stage01");
            runStage = (GameStage)stages["Stage01"];
            stages["Stage01"].AutoRender = false;
            base.PreLoadContent();
        }
        protected override void DesignContent()
        {
            start.TextSize = 256f;
            start.BackColor = Color.White * 0.0f;
            start.BDText.ForeColor = System.Drawing.Color.Yellow;
            start.Text = (((GameStage)stages["Stage01"]).StartTime / 60).ToString();
            DesignGameOver();
            stageCameras["C0"].Location = Point.Zero;
            stageCameras["C1"].Location = new Point(size.Width / 2, 0);
            stageCameras["C2"].Location = new Point(0, size.Height / 2);
            stageCameras["C3"].Location = (size / 2).ToPoint();
            stageCameras["C0"].Size = size / 2;
            stageCameras["C1"].Size = size / 2;
            stageCameras["C2"].Size = size / 2;
            stageCameras["C3"].Size = size / 2;
            stageCameras["C0"].Stage = stages["Stage01"];
            stageCameras["C1"].Stage = stages["Stage01"];
            stageCameras["C2"].Stage = stages["Stage01"];
            stageCameras["C3"].Stage = stages["Stage01"];

            base.DesignContent();
        }

        private void DesignGameOver()
        {
            int sW = size.Width;
            int sH = size.Height;
            int eMSW, eMSH;//Size-----WidthとHeight
            eMSW = (int)(sW * 0.8);
            eMSH = (int)(sH * 0.8);
            gameOver.Size = new Size(eMSW, eMSH);
            gameOver.Location = Alignment.GetMXFAlignment(ContentAlignment.MiddleCenter, size, gameOver.Size);
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("stagescene.png");
            sounds["antiviruswin"] = SoundManage.GetSound("antiviruswin.wav");
            sounds["viruswin"] = SoundManage.GetSound("viruswin.wav");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameKeyboard.GetKeyUpTrigger(Keys.Escape))
                OpenBackMenu();
            if (start.Visible)
            {
                start.Text = (((GameStage)stages["Stage01"]).StartTime / 60).ToString();
                start.Location = ((size - start.Size) / 2).ToPoint();
                if (((GameStage)stages["Stage01"]).StartTime == 0)
                {
                    start.Visible = false;
                }
            }
            if ((runStage.MouseWin || runStage.CatWin) && !gameOver.Visible)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                if (runStage.MouseWin && runStage.CatWin)
                {
                    result["winer"] = "引き分け";
                }
                else if (runStage.MouseWin)
                {
                    sounds["viruswin"].Play();
                    result["winer"] = "個人情報大量漏出";
                }
                else if (runStage.CatWin)
                {
                    sounds["antiviruswin"].Play();
                    result["winer"] = "ウィルス全滅";
                }
                result["thedatafind"] = string.Format($"取り戻せた情報量：{runStage.AntivirusPoint}");
                result["thedatalost"] = string.Format($"盗まれた情報量：{runStage.MousePoint}");
                gameOver.ShowResult(result);
            }
            if (!backMenu.Visible && !gameOver.Visible)
                base.Update(gameTime);
            else
            {
                if (backMenu.Visible)
                    backMenu.Update(gameTime);
                if (gameOver.Visible)
                    gameOver.Update(gameTime);
            }
            if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.Start) && gameOver.Visible)
                Initialize();
            else if (IGGamePad.GetKeyTrigger(PlayerIndex.One,Buttons.Back))
            {
                if (gameOver.Visible)
                {
                    IsRun = false;
                    GameRun.scenes["title"].IsRun = true;
                    Initialize();
                }
                else if (!backMenu.Visible)
                    OpenBackMenu();
            }
        }

        private void OpenBackMenu()
        {
            backMenu.Visible = !backMenu.Visible;
            if (backMenu.Visible)
            {
                backMenu.SetFocus();
            }
        }
    }
}

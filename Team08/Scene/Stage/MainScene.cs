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
using Team08.Scene.Stage.Stages;
using Team08.Scene.UI;

namespace Team08.Scene.Stage
{
    public class MainScene : StageScene
    {
        private BackMenu backMenu;
        private GameOver gameOver;
        private GameStage runStage;
        public MainScene(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void PreLoadContent()
        {
            backMenu = new BackMenu(graphicsDevice, this);
            gameOver = new GameOver(graphicsDevice, this);
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
            DesignBackMenu();
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

        private void DesignBackMenu()
        {
            backMenu.Text = GetText("BackMenu");
            int sW = size.Width;
            int sH = size.Height;
            int eMSW, eMSH;//Size-----WidthとHeight
            eMSW = sW / 3;
            eMSH = sH / 2;
            backMenu.Size = new Size(eMSW, eMSH);
            backMenu.Location = Alignment.GetMXFAlignment(ContentAlignment.MiddleCenter, size, backMenu.Size);
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
            stageCameras["C0"].FocusStageContent = stageCameras["C0"].Stage.stageContents["player0"];
            stageCameras["C1"].FocusStageContent = stageCameras["C1"].Stage.stageContents["player1"];
            stageCameras["C2"].FocusStageContent = stageCameras["C2"].Stage.stageContents["player2"];
            stageCameras["C3"].FocusStageContent = stageCameras["C3"].Stage.stageContents["player3"];
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameKeyboard.GetKeyUpTrigger(Keys.Escape))
            {
                backMenu.Visible = !backMenu.Visible;
                if (backMenu.Visible)
                {
                    backMenu.SetFocus();
                }
            }
            if ((runStage.mouseWin || runStage.catWin) && !gameOver.Visible)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                if (runStage.mouseWin && runStage.catWin)
                {
                    result["winer"] = "引き分け";
                }
                else if (runStage.mouseWin)
                {
                    result["winer"] = "勝者ネズミたち";
                }
                else if (runStage.catWin)
                {
                    result["winer"] = "勝者ネコ";
                }
                result["mouse"] = string.Format($"ネズミ残り：{runStage.mouseNum - runStage.killedMouse}/{runStage.mouseNum}");
                result["cheese"] = string.Format($"チーズ残り：{runStage.cheeseNum - runStage.eatedCheese}/{runStage.cheeseNum}");
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
        }

        public override void AfterDraw(GameTime gameTime)
        {
            base.AfterDraw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfinityGame;
using InfinityGame.GameGraphics;
using InfinityGame.Scene;
using InfinityGame.UI.UIContent;
using InfinityGame.Element;
using InfinityGame.Device;
using InfinityGame.Device.MouseManage;
using Team08.Scene.Title.UI;

namespace Team08.Scene.Title
{
    public partial class TitleScene : BaseScene
    {
        private AnimeButton startAB;
        private TaskBar taskBar;
        private StartMenu startMenu;

        public TaskBar TaskBar { get { return taskBar; } }
        public StartMenu StartMenu { get { return startMenu; } }
        public TitleScene(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void PreLoadContent()
        {
            startAB = new AnimeButton(graphicsDevice, this);
            taskBar = new TaskBar(graphicsDevice, this);
            startMenu = new StartMenu(graphicsDevice, this);
            EventRegist();
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            startAB.Size = new Size(128, 128);
            startAB.Location = new Point(10, 10);
            startAB.Text = "Start";//サンプル例後で換える
            base.DesignContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("title.png");
            startAB.Image = ImageManage.GetSImage("button");
            base.LoadContent();
        }

        public void StartGame(object sender, EventArgs e)
        {
            if (startAB.Space.Contains(((GameMouse)sender).MouseState.Position))
            {
                IsRun = false;
                GameRun.scenes["stagescene"].IsRun = true;
                //Start
            }
        }
    }
}

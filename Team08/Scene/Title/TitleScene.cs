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

namespace Team08.Scene.Title
{
    public partial class TitleScene : BaseScene
    {
        private AnimeButton exitAB;
        private AnimeButton startAB;
        public TitleScene(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void PreLoadContent()
        {
            exitAB = new AnimeButton(graphicsDevice, this);
            startAB = new AnimeButton(graphicsDevice, this);

            EventRegist();
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            exitAB.Size = new Size(128, 128);
            exitAB.Location = new Point(10, size.Height - 138);
            exitAB.Text = "Exit";//サンプル例後で換える
            startAB.Size = new Size(128, 128);
            startAB.Location = new Point(10, size.Height - 138 - exitAB.Size.Height - 20);
            startAB.Text = "Start";//サンプル例後で換える
            base.DesignContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("title.png");
            exitAB.Image = ImageManage.GetSImage("button");
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

        public void ExitGame(object sender, EventArgs e)
        {
            if (exitAB.Space.Contains(((GameMouse)sender).MouseState.Position))
            {
                Program.Exit();
            }
        }
    }
}

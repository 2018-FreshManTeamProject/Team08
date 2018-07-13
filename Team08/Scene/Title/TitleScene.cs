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
        private FileIcon antivirus;
        private TaskBar taskBar;
        private StartMenu startMenu;
        private Warning warning;
        private Hacking hacking;
        private bool isShutdown = false;
        private int warningCountDown = 180;

        public TaskBar TaskBar { get { return taskBar; } }
        public StartMenu StartMenu { get { return startMenu; } }
        public Hacking Hacking { get { return hacking; } }
        public TitleScene(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void Initialize()
        {
            warningCountDown = 180;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            antivirus = new FileIcon(graphicsDevice, this);
            taskBar = new TaskBar(graphicsDevice, this);
            startMenu = new StartMenu(graphicsDevice, this);
            warning = new Warning(graphicsDevice, this);
            hacking = new Hacking(graphicsDevice, this);
            EventRegist();
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            antivirus.Size = new Size(128, 128);
            antivirus.Location = new Point(10, 10);
            antivirus.Text = GetText("Antivirus");//サンプル例後で換える
            base.DesignContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("title.png");
            antivirus.Image = ImageManage.GetSImage("antivirus_icon");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (warningCountDown > -1)
                warningCountDown--;
            if (warningCountDown == 0)
            {
                warning.Visible = true;
            }
            if (isShutdown)
            {
                Refract -= 0.1f;
                if (refract <= 0)
                {
                    Refract = 0;
                    OnShutdown();
                }
            }
            base.Update(gameTime);
        }

        public void OpenWarning(object sender, EventArgs e)
        {
            if (!warning.Visible)
                warning.Visible = true;
        }

        public void Shutdown()
        {
            isShutdown = true;
        }

        private void OnShutdown()
        {
            IsRun = false;
            GameRun.scenes["shutdown"].IsRun = true;
            Program.GetGame().IsMouseVisible = false;
        }
    }
}

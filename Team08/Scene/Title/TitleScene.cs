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
using InfinityGame.UI;
using InfinityGame.Element;
using InfinityGame.Device;
using InfinityGame.Device.MouseManage;
using MouseTrash.Scene.Title.UI;
using Microsoft.Xna.Framework.Input;

namespace MouseTrash.Scene.Title
{
    public partial class TitleScene : BaseScene
    {
        private FileIcon antivirus;
        private TaskBar taskBar;
        private StartMenu startMenu;
        private Warning warning;
        private Hacking hacking;
        private Readme readme;
        private Label message;
        private bool isShutdown = false;
        private int warningCountDown = 30;

        public TaskBar TaskBar { get { return taskBar; } }
        public StartMenu StartMenu { get { return startMenu; } }
        public Hacking Hacking { get { return hacking; } }
        public Readme Readme { get { return readme; } }
        public TitleScene(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void Initialize()
        {
            warningCountDown = 30;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            antivirus = new FileIcon(graphicsDevice, this);
            taskBar = new TaskBar(graphicsDevice, this);
            startMenu = new StartMenu(graphicsDevice, this);
            warning = new Warning(graphicsDevice, this);
            hacking = new Hacking(graphicsDevice, this);
            readme = new Readme(graphicsDevice, this);
            message = new Label(graphicsDevice, this);
            message.TextSize = 24f;
            message.Text = "Startボタンでスキャン\r\nBackボタンでシャットダウン\r\nAボタンで確認\r\nBボタンでキャンセル";
            message.Location = new Point(Size.Width - message.Size.Width, 0);
            new WarningMessage(graphicsDevice, this, antivirus);
            EventRegist();
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            antivirus.Size = new Size(128, 128);
            antivirus.Location = new Point(10, 10);
            antivirus.Text = GetText("Antivirus");
            base.DesignContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("mousetrash_logo.png");
            antivirus.Image = ImageManage.GetSImage("antivirus_icon");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!warning.Visible)
            {
                if (warningCountDown > -1)
                    warningCountDown--;
                if (warningCountDown == 0)
                {
                    warningCountDown = 30;
                    antivirus.Warning.Visible = !antivirus.Warning.Visible;
                }
            }
            else
            {
                if (antivirus.Warning.Visible)
                    antivirus.Warning.Visible = false;
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

            if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.Start) && !warning.Visible)
                Start();
            else if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.Back) && !warning.Visible)
                Shutdown();

            base.Update(gameTime);
        }

        public void OpenWarning(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            if (!warning.Visible)
                warning.Visible = true;
            warning.SetFocus();
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

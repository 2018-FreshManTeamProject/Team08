using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.Element;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using InfinityGame.Device;
using InfinityGame.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame.Device.MouseManage;

namespace Team08.Scene.Title.UI
{
    public partial class StartMenu : UIWindow
    {
        private AnimeButton shutdown;
        private AnimeButton antivirus;
        private Label downText;
        private Label antivirusText;

        public StartMenu(GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : base(aGraphicsDevice, aParent)
        {
            canMove = false;
            canClose = false;
            BorderOn = false;
            BackColor = Color.White * 0.0f;
            Visible = false;
        }

        public override void PreLoadContent()
        {
            shutdown = new AnimeButton(graphicsDevice, this);
            shutdown.Text = "";
            antivirus = new AnimeButton(graphicsDevice, this);
            antivirus.Text = "";
            shutdown.ImageEntity.Enable = false;
            downText = new Label(graphicsDevice, this);
            antivirusText = new Label(graphicsDevice, this);
            antivirusText.TextSize = 16f;
            antivirusText.Text = GetText("Antivirus");
            downText.TextSize = 16f;
            downText.Text = GetText("ShutDown");
            Size = new Size(300, 100);
            Location = new Point(0, parent.Size.Height - ((TitleScene)parent).TaskBar.Size.Height - size.Height);
            EventRegist();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("startmenu.png");
            shutdown.Image = ImageManage.GetSImage("shutdown");
            antivirus.Image = ImageManage.GetSImage("antivirus_icon");
            antivirus.Size = new Size(100, 100);
            shutdown.Size = Size.Parse(shutdown.Image.Image.Size);
            antivirus.Location = new Point(-25, size.Height - shutdown.Size.Height - 75);
            shutdown.Location = new Point(0, size.Height - shutdown.Size.Height);
            downText.Location = new Point(60, size.Height - downText.Size.Height - 15);
            antivirusText.Location = new Point(60, size.Height - shutdown.Size.Height - antivirusText.Size.Height - 15);
            base.LoadContent();
        }

        private void ExitGame(object sender, EventArgs e)
        {
            ((TitleScene)parent).Shutdown();
        }
    }
}

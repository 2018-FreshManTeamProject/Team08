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

namespace MouseTrash.Scene.Title.UI
{
    public partial class StartMenu : UIWindow
    {
        private AnimeButton shutdown;
        private AnimeButton antivirus;
        private Button readme;
        private Label downText;
        private Label antivirusText;
        private Label readmeText;

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
            readme = new Button(graphicsDevice, this);
            readme.Text = "";
            readme.NormalColor = Color.White * 0.0f;
            readme.Size = new Size(50, 50);
            readme.BorderOn = false;
            shutdown.ImageEntity.Enable = false;
            downText = new Label(graphicsDevice, this);
            antivirusText = new Label(graphicsDevice, this);
            readmeText = new Label(graphicsDevice, this);
            antivirusText.TextSize = 16f;
            antivirusText.Text = GetText("Antivirus");
            downText.TextSize = 16f;
            downText.Text = GetText("ShutDown");
            readmeText.TextSize = 16f;
            readmeText.Text = GetText("ReadmeText");
            Size = new Size(300, 150);
            Location = new Point(0, parent.Size.Height - ((TitleScene)parent).TaskBar.Size.Height - size.Height);
            EventRegist();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("startmenu.png");
            shutdown.Image = ImageManage.GetSImage("shutdown");
            antivirus.Image = ImageManage.GetSImage("antivirus_icon");
            readme.Image = ImageManage.GetSImage("thedata.png");
            antivirus.Size = new Size(100, 100);
            shutdown.Size = Size.Parse(shutdown.Image.Image.Size);

            antivirus.Location = new Point(-25, size.Height - shutdown.Size.Height - 75);
            shutdown.Location = new Point(0, size.Height - shutdown.Size.Height);
            readme.Location = new Point(0, size.Height - 150);
            downText.Location = new Point(60, size.Height - downText.Size.Height - 15);
            antivirusText.Location = new Point(60, downText.Location.Y - 50);
            readmeText.Location = new Point(60, antivirusText.Location.Y - 50);
            base.LoadContent();
        }

        private void ReadmeBT(object sender, EventArgs e)
        {
            ((TitleScene)parent).Readme.Visible = true;
            ((TitleScene)parent).Readme.SetFocus();
            Close();
        }

        private void OpenWarning(object sender, EventArgs e)
        {
            ((TitleScene)parent).OpenWarning(sender, e);
            Close();
        }

        private void ExitGame(object sender, EventArgs e)
        {
            ((TitleScene)parent).Shutdown();
            Close();
        }
    }
}

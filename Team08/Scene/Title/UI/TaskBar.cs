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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame.Device.MouseManage;

namespace MouseTrash.Scene.Title.UI
{
    public partial class TaskBar : UIWindow
    {
        private Label time;
        private Label date;
        private Label menu;
        private AnimeButton start;
        public TaskBar(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            canMove = false;
            canClose = false;
            BorderOn = false;
        }

        public override void PreLoadContent()
        {
            time = new Label(graphicsDevice, this);
            date = new Label(graphicsDevice, this);
            menu = new Label(graphicsDevice, this);
            start = new AnimeButton(graphicsDevice, this);
            start.ImageEntity.Enable = false;
            time.TextSize = 12f;
            date.TextSize = 12f;
            menu.TextSize = 16f;
            time.Text = DateTime.Now.ToString("HH:mm");
            date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            menu.Text = GetText("Menu");
            Size = new Size(parent.Size.Width, 50);
            start.Size = new Size(50, 50);
            Location = new Point(0, parent.Size.Height - size.Height);
            EventRegist();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            time.Location = new Point(size.Width - date.Size.Width / 2 - time.Size.Width / 2 - 20, 5);
            date.Location = new Point(size.Width - date.Size.Width - 20, 30);
            menu.Location = new Point(start.Size.Width + 5, (size.Height - menu.Size.Height) / 2);
            Image = ImageManage.GetSImage("taskbar.png");
            start.Image = ImageManage.GetSImage("IG");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            time.Text = DateTime.Now.ToString("HH:mm");
            date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            base.Update(gameTime);
        }

        private void OnStartMenu(object sender, EventArgs e)
        {
            ((TitleScene)parent).StartMenu.Visible = !((TitleScene)parent).StartMenu.Visible;
        }
    }
}

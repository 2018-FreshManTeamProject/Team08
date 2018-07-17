using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame.Element;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using InfinityGame.Device;
using InfinityGame.Scene;
using MouseTrash.Scene.Title;

namespace MouseTrash.Scene.Stage.UI
{
    public partial class BackMenu : UIWindow
    {
        public AnimeButton back;
        public AnimeButton title;
        public AnimeButton reset;
        public AnimeButton exit;

        public BackMenu(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            /*BDText.ForeColor = System.Drawing.Color.Yellow;
            BorderColor = Color.DarkRed;
            backColor = Color.PaleVioletRed;*/
        }

        public override void Initialize()
        {
            Visible = false;
            Location = Alignment.GetMXFAlignment(ContentAlignment.MiddleCenter, parent.Size, Size);
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Text = GetText("BackMenu");
            Size = new Size(400, 420);
            Visible = false;
            back = new AnimeButton(graphicsDevice, this);
            title = new AnimeButton(graphicsDevice, this);
            reset = new AnimeButton(graphicsDevice, this);
            exit = new AnimeButton(graphicsDevice, this);
            /*back.BDText.ForeColor = System.Drawing.Color.Yellow;
            title.BDText.ForeColor = System.Drawing.Color.Yellow;
            reset.BDText.ForeColor = System.Drawing.Color.Yellow;
            exit.BDText.ForeColor = System.Drawing.Color.Yellow;
            back.ImageColor = Color.Blue;
            title.ImageColor = Color.Blue;
            reset.ImageColor = Color.Blue;
            exit.ImageColor = Color.Blue;*/
            EventRegist();
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            Size tempsize = new Size(360, 80);
            SetContentSize(tempsize);
            SetContentLocation(tempsize);
            SetContentText();
            SetContentImage();
            base.DesignContent();
        }

        public override void LoadContent()
        {
            /*closeButton.BDText.ForeColor = System.Drawing.Color.Yellow;
            closeButton.EnterColor = Color.Blue;
            closeButton.NormalColor = Color.White * 0.0f;*/
            base.LoadContent();
        }

        private void SetContentSize(Size tempsize)
        {
            back.Size = tempsize;
            title.Size = tempsize;
            reset.Size = tempsize;
            exit.Size = tempsize;

        }
        private void SetContentLocation(Size tempsize)
        {
            back.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + 10);
            title.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + tempsize.Height + 20);
            reset.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + 2 * tempsize.Height + 30);
            exit.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + 3 * tempsize.Height + 40);
        }

        private void SetContentText()
        {
            back.Text = GetText("Back");
            title.Text = GetText("ToTitle");
            reset.Text = GetText("ReSet");
            exit.Text = GetText("ShutDown");
        }

        private void SetContentImage()
        {
            back.Image = ImageManage.GetSImage("button01");
            title.Image = ImageManage.GetSImage("button01");
            reset.Image = ImageManage.GetSImage("button01");
            exit.Image = ImageManage.GetSImage("button01");
        }

        public void Back(object sender, EventArgs e)
        {
            Visible = false;
        }

        public void ToTitle(object sender, EventArgs e)
        {
            ((BaseScene)parent).IsRun = false;
            ((BaseScene)parent).GameRun.scenes["title"].IsRun = true;
            parent.Initialize();
        }

        public void ReSet(object sender, EventArgs e)
        {
            parent.Initialize();
        }

        public void Exit(object sender, EventArgs e)
        {
            ToTitle(sender, e);
            ((TitleScene)((BaseScene)parent).GameRun.scenes["title"]).Shutdown();
        }
    }
}

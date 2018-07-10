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

namespace Team08.Scene.UI
{
    public partial class BackMenu : UIWindow
    {
        public AnimeButton back;
        public AnimeButton title;
        public AnimeButton reset;
        public AnimeButton exit;

        public BackMenu(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            
        }

        public override void Initialize()
        {
            Visible = false;
            Location = Alignment.GetMXFAlignment(ContentAlignment.MiddleCenter, parent.Size, Size);
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Visible = false;
            back = new AnimeButton(graphicsDevice, this);
            title = new AnimeButton(graphicsDevice, this);
            reset = new AnimeButton(graphicsDevice, this);
            exit = new AnimeButton(graphicsDevice, this);

            EventRegist();
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            Size tempsize = new Size(128, 128);
            SetContentSize(tempsize);
            SetContentLocation(tempsize);
            SetContentText();
            SetContentImage();
            base.DesignContent();
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
            Point temp = new Point(BorderSize + 2, TitleSize + 2);

            back.Location = temp;
            title.Location = new Point(temp.X + 20 + tempsize.Width, TitleSize + 2);
            reset.Location = new Point(temp.X + 40 + 2 * tempsize.Width, TitleSize + 2);
            exit.Location = new Point(temp.X, TitleSize + 2 + 20 + tempsize.Height);
        }

        private void SetContentText()
        {
            back.Text = GetText("Back");
            title.Text = GetText("ToTitle");
            reset.Text = GetText("ReSet");
            exit.Text = GetText("Exit");
        }

        private void SetContentImage()
        {
            back.Image = ImageManage.GetSImage("button");
            title.Image = ImageManage.GetSImage("button");
            reset.Image = ImageManage.GetSImage("button");
            exit.Image = ImageManage.GetSImage("button");
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
            Program.Exit();
        }
    }
}

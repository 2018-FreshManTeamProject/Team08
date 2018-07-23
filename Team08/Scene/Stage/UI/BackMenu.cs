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
using Microsoft.Xna.Framework.Input;

namespace MouseTrash.Scene.Stage.UI
{
    public partial class BackMenu : UIWindow
    {
        private AnimeButton backAB;
        private AnimeButton title;
        private AnimeButton reset;
        private AnimeButton exitAB;

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
            Size = new Size(450, 420);
            Visible = false;
            backAB = new AnimeButton(graphicsDevice, this);
            title = new AnimeButton(graphicsDevice, this);
            reset = new AnimeButton(graphicsDevice, this);
            exitAB = new AnimeButton(graphicsDevice, this);
            /*backAB.BDText.ForeColor = System.Drawing.Color.Yellow;
            title.BDText.ForeColor = System.Drawing.Color.Yellow;
            reset.BDText.ForeColor = System.Drawing.Color.Yellow;
            exitAB.BDText.ForeColor = System.Drawing.Color.Yellow;
            backAB.ImageColor = Color.Blue;
            title.ImageColor = Color.Blue;
            reset.ImageColor = Color.Blue;
            exitAB.ImageColor = Color.Blue;*/
            EventRegist();
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            Size tempsize = new Size(400, 80);
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
            sounds["button"] = SoundManage.GetSound("button.wav");
            base.LoadContent();
        }

        private void SetContentSize(Size tempsize)
        {
            backAB.Size = tempsize;
            title.Size = tempsize;
            reset.Size = tempsize;
            exitAB.Size = tempsize;

        }
        private void SetContentLocation(Size tempsize)
        {
            backAB.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + 10);
            title.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + tempsize.Height + 20);
            reset.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + 2 * tempsize.Height + 30);
            exitAB.Location = new Point((Size.Width - tempsize.Width) / 2, TitleSize + 3 * tempsize.Height + 40);
        }

        private void SetContentText()
        {
            backAB.Text = GetText("Back");
            title.Text = GetText("ToTitle");
            reset.Text = GetText("ReSet");
            exitAB.Text = GetText("ShutDown");
        }

        private void SetContentImage()
        {
            backAB.Image = ImageManage.GetSImage("button01");
            title.Image = ImageManage.GetSImage("button01");
            reset.Image = ImageManage.GetSImage("button01");
            exitAB.Image = ImageManage.GetSImage("button01");
        }

        public void Back(object sender, EventArgs e)
        {
            SoundPlay("button");
            Visible = false;
        }

        public void ToTitle(object sender, EventArgs e)
        {
            SoundPlay("button");
            ((BaseScene)parent).IsRun = false;
            ((BaseScene)parent).GameRun.scenes["title"].IsRun = true;
            parent.Initialize();
        }

        public void ReSet(object sender, EventArgs e)
        {
            SoundPlay("button");
            parent.Initialize();
        }

        public void Exit(object sender, EventArgs e)
        {
            SoundPlay("button");
            ToTitle(sender, e);
            ((TitleScene)((BaseScene)parent).GameRun.scenes["title"]).Shutdown();
        }

        private void SoundPlay(string sdnm)
        {
            sounds[sdnm].Stop();
            sounds[sdnm].Play();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.Start))
                    ReSet(null, null);
                else if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.B))
                    Back(null, null);
                else if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.Back))
                    ToTitle(null, null);
            }
            base.Update(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using InfinityGame.Device;
using InfinityGame.Element;
using InfinityGame.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame.Device.MouseManage;

namespace Team08.Scene.Title.UI
{
    public partial class Warning : UIWindow
    {
        private Label warningText;
        private AnimeButton ok;
        private AnimeButton cancel;
        private bool onHacking = false;
        private int timedown = 600;
        private Random rnd = new Random();
        public Warning(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
            Text = GetText("Warning") + " - " + GetText("Antivirus");
        }

        public override void Initialize()
        {
            CanClose = true;
            onHacking = false;
            Visible = false;
            Size = parent.Size / 2;
            Location = ((parent.Size - Size) / 2).ToPoint();
            warningText.TextSize = 36f;
            warningText.Text = GetText("WarningText");
            warningText.Location = ((size - warningText.Size) / 2).ToPoint();
            ok.Visible = true;
            cancel.Visible = true;
            timedown = 600;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = parent.Size / 2;
            Location = ((parent.Size - Size) / 2).ToPoint();
            warningText = new Label(graphicsDevice, this);
            ok = new AnimeButton(graphicsDevice, this);
            cancel = new AnimeButton(graphicsDevice, this);
            ok.Text = GetText("OK");
            cancel.Text = GetText("Cancel");
            ok.Size = new Size(120, 40) * 2;
            cancel.Size = new Size(120, 40) * 2;
            ok.Location = new Point(150, size.Height - ok.Size.Height - 20);
            cancel.Location = (size - cancel.Size).ToPoint() - new Point(150, 20);
            EventRegist();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            ok.Image = ImageManage.GetSImage("button01");
            cancel.Image = ImageManage.GetSImage("button01");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (onHacking)
            {
                if (timedown > 0)
                    timedown--;
                if (timedown == 590)
                {
                    CanClose = false;
                    Text = GetText("Warning");
                    warningText.Text = GetText("OnHackingText");
                    warningText.TextSize = 16f;
                    Size = Size / 2;
                    warningText.Location = ((size - warningText.Size) / 2).ToPoint();
                    ok.Visible = false;
                    cancel.Visible = false;
                }
                if (timedown == 570)
                {
                    Location = new Point(rnd.Next(0, parent.Size.Width - Size.Width), rnd.Next(0, parent.Size.Height - Size.Height));
                    if (!((TitleScene)parent).Hacking.Visible)
                        ((TitleScene)parent).Hacking.Visible = true;
                }
                if ((timedown > 300 && timedown % 60 == 0) ||
                    (timedown > 150 && timedown <= 300 && timedown % 30 == 0) ||
                    (timedown > 75 && timedown <= 150 && timedown % 15 == 0) ||
                    (timedown > 30 && timedown <= 75 && timedown % 6 == 0) ||
                    timedown <= 30)
                {
                    Location = new Point(rnd.Next(0, parent.Size.Width - Size.Width), rnd.Next(0, parent.Size.Height - Size.Height));
                }
            }
            base.Update(gameTime);
        }

        private void OK(object sender, EventArgs e)
        {
            onHacking = true;
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }
    }
}

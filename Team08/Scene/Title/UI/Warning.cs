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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace MouseTrash.Scene.Title.UI
{
    public partial class Warning : UIWindow
    {
        private Label warningText;
        private AnimeButton ok;
        private AnimeButton cancel;
        private bool onHacking = false;
        private int timedown = 600;
        private Random rnd = new Random();
        private bool soundplay = false;
        public Warning(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
            Text = GetText("Warning") + " - " + GetText("Antivirus");
        }

        public override void Initialize()
        {
            soundplay = false;
            if (!sounds["warning02"].GetState(SoundState.Stopped))
                sounds["warning02"].Stop();
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
            sounds["warning01"] = SoundManage.GetSound("warning01.wav");
            sounds["warning02"] = SoundManage.GetSound("warning02.wav");
            sounds["warning02"].SetSELoopPlay(true);
            sounds["button"] = SoundManage.GetSound("button.wav");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (!soundplay)
                {
                    sounds["warning01"].Play();
                    soundplay = true;
                }
                if (onHacking)
                {
                    if (!sounds["warning02"].GetState(SoundState.Playing))
                    {
                        sounds["warning02"].Play();
                    }
                    if (timedown > 570)
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
                        Location = Point.Zero;
                        ((TitleScene)parent).Hacking.Visible = true;
                        ((TitleScene)parent).Hacking.SetFocus();
                        timedown--;
                    }
                    if (timedown < 570 && ((TitleScene)parent).Hacking.Start)
                        timedown--;
                    if ((timedown > 300 && timedown % 60 == 0) ||
                        (timedown > 150 && timedown <= 300 && timedown % 30 == 0) ||
                        (timedown > 75 && timedown <= 150 && timedown % 15 == 0) ||
                        (timedown > 30 && timedown <= 75 && timedown % 6 == 0) ||
                        timedown <= 30)
                    {
                        Location = new Point(rnd.Next(0, parent.Size.Width - Size.Width), rnd.Next(0, parent.Size.Height - Size.Height));
                    }
                }
                if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.A) && !onHacking)
                    OK();
                else if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.B) && !onHacking)
                    Cancel(null, null);
            }
            base.Update(gameTime);
        }

        private void OK(object sender, EventArgs e)
        {
            OK();
        }

        private void OK()
        {
            SoundPlay("button");
            onHacking = true;
        }

        private void Cancel(object sender, EventArgs e)
        {
            SoundPlay("button");
            Close();
        }

        public override void Close()
        {
            soundplay = false;
            sounds["warning02"].Stop();
            base.Close();
        }

        public override void SetFocus()
        {
            if (!onHacking)
                base.SetFocus();
        }

        private void SoundPlay(string sdnm)
        {
            sounds[sdnm].Stop();
            sounds[sdnm].Play();
        }
    }
}

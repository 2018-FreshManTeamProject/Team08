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
        public Warning(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
            Text = GetText("Warning");
        }

        public override void Initialize()
        {
            Visible = false;
            Location = ((parent.Size - Size) / 2).ToPoint();
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = parent.Size / 2;
            Location = ((parent.Size - Size) / 2).ToPoint();
            warningText = new Label(graphicsDevice, this);
            warningText.TextSize = 36f;
            warningText.Text = GetText("WarningText");
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
            warningText.Location = ((size - warningText.Size) / 2).ToPoint();
            base.LoadContent();
        }

        private void OK(object sender, EventArgs e)
        {
            if (!((TitleScene)parent).Hacking.Visible)
                ((TitleScene)parent).Hacking.Visible = true;
            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }
    }
}

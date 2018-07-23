using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MouseTrash.Scene.Title.UI
{
    public class Readme : UIWindow
    {
        private Label message;
        public Readme(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
        }

        public override void Initialize()
        {
            Visible = false;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = parent.Size * 3 / 4;
            Location = ((parent.Size - size) / 2).ToPoint();
            Text = GetText("ReadmeText");
            message = new Label(graphicsDevice, this);
            message.TextSize = 24f;
            message.Text = GetText("Readme");
            message.Location = ((Size - message.Size) / 2).ToPoint();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }
    }
}

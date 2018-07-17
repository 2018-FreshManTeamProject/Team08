using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI.UIContent;
using InfinityGame.Element;
using InfinityGame.Device;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MouseTrash.Scene.Title.UI
{
    public class WarningMessage : Panel
    {
        private Label label;
        private FileIcon focus;
        //private int timedown = 600;
        public WarningMessage(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, FileIcon focus) : base(aGraphicsDevice, aParent)
        {
            BackColor = Color.White * 0.0f;
            Visible = false;
            this.focus = focus;
            focus.Warning = this;
        }

        public override void Initialize()
        {
            //timedown = 600;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            label = new Label(graphicsDevice, this);
            label.TextSize = 32f;
            label.Text = GetText("WarningMessage");
            Size = label.Size + new Size(50, 20);
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            label.Location = Alignment.GetMXFAlignment(ContentAlignment.MiddleRight, size, label.Size);
            Image = ImageManage.GetSImage("warningmessage.png");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            /*if (timedown > 0)
                timedown--;
            if (timedown <= 0 && Visible)
                visible = false;*/
            base.Update(gameTime);
        }
    }
}

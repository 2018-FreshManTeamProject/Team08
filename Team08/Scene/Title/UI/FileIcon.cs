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
    public partial class FileIcon : AnimeButton
    {
        private SImage border;
        private bool showBorder;
        private WarningMessage warning;

        public bool ShowBorder { get { return showBorder; } set { showBorder = value; } }
        public WarningMessage Warning { get { return warning; } set { warning = value; SetLocation(); } }
        public FileIcon(GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : base(aGraphicsDevice, aParent)
        {
            imageEntity.Enable = false;
            CanMove = true;
        }

        public override void SetLocation()
        {
            if (warning != null)
                warning.Location = new Point(location.X + Size.Width + 10, location.Y + ((size.Height - warning.Size.Height) / 2));
            base.SetLocation();
        }

        public override void PreLoadContent()
        {
            TextSize = 16f;
            TextAlign = ContentAlignment.BottomCenter;
            EventRegist();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            border = ImageManage.GetSImage("fileiconborder.png");
            sounds["click"] = SoundManage.GetSound("click.wav");
            base.LoadContent();
        }

        public override void Draw2(GameTime gameTime)
        {
            if (showBorder)
                spriteBatch.Draw(border.ImageT[0], new Rectangle(DrawLocation, size.ToPoint()), Color * refract);
            base.Draw2(gameTime);
        }

        private void Clicked(object sender, EventArgs e)
        {
            sounds["click"].Play();
        }

        private void ShowB(object sender, EventArgs e)
        {
            showBorder = true;
        }

        private void NotShowB(object sender, EventArgs e)
        {
            showBorder = false;
        }
    }
}

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

namespace Team08.Scene.Title.UI
{
    public partial class StartMenu : UIWindow
    {
        private AnimeButton shutdown;
        public StartMenu(GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : base(aGraphicsDevice, aParent)
        {
            canMove = false;
            canClose = false;
            BorderOn = false;
            BackColor = Color.White;
            Visible = false;
        }

        public override void PreLoadContent()
        {
            shutdown = new AnimeButton(graphicsDevice, this);
            shutdown.ImageEntity.Enable = false;
            Size = new Size(300, 500);
            Location = new Point(0, parent.Size.Height - ((TitleScene)parent).TaskBar.Size.Height - size.Height);
            EventRegist();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            shutdown.Image = ImageManage.GetSImage("shutdown");
            shutdown.Size = Size.Parse(shutdown.Image.Image.Size);
            shutdown.Location = new Point(0, size.Height - shutdown.Size.Height);
            base.LoadContent();
        }

        public void ExitGame(object sender, EventArgs e)
        {
            Program.Exit();
        }
    }
}

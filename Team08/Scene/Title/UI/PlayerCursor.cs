using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI.UIContent;
using InfinityGame.Device;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Team08.Scene.Title.UI
{
    public class PlayerCursor : Panel
    {
        private Label lable;
        private string player;
        private int timedown = 30;
        private Point coo = Point.Zero;
        public string Player { get { return player; } }
        public Point Coo { get { return coo; } set { coo = value; } }
        public PlayerCursor(string player, GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : base(aGraphicsDevice, aParent)
        {
            this.player = player;
            BackColor = Color.White * 0.0f;
            ((IPlayerCursor)parent).Players.Add(player, this);
        }

        public override void PreLoadContent()
        {
            lable = new Label(graphicsDevice, this);
            lable.Text = player;
            lable.TextSize = 16f;
            lable.BackColor = Color.White * 0.0f;
            lable.BDText.ForeColor = System.Drawing.Color.Yellow;
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            lable.Location = new Point(0, -lable.Size.Height);
            Image = ImageManage.GetSImage("playercursor.png");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            timedown--;
            if (timedown <= 0)
            {
                Visible = !Visible;
                timedown = 30;
            }
            base.Update(gameTime);
        }
    }
}

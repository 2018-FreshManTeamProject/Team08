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

namespace Team08.Scene.Stage.UI
{
    public class GameOver : UIWindow
    {
        private Label winer;
        private Label mouse;
        private Label cheese;
        private Label message;
        public GameOver(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            CanMove = false;
            BorderOn = false;
            backColor = Color.White * 0.15f;
            canClose = false;
        }

        public override void Initialize()
        {
            Visible = false;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Visible = false;
            winer = new Label(graphicsDevice, this);
            mouse = new Label(graphicsDevice, this);
            cheese = new Label(graphicsDevice, this);
            message = new Label(graphicsDevice, this);
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            winer.BackColor = Color.White * 0.0f;
            mouse.BackColor = Color.White * 0.0f;
            cheese.BackColor = Color.White * 0.0f;
            message.BackColor = Color.White * 0.0f;
            winer.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            mouse.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            cheese.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            message.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            winer.TextSize = 128f;
            mouse.TextSize = 72f;
            cheese.TextSize = 72f;
            message.TextSize = 48f;
            message.Text = "Escボタンで戻る";
            base.DesignContent();
        }

        public void ShowResult(Dictionary<string, string> result)
        {
            winer.Text = result["winer"];
            mouse.Text = result["mouse"];
            cheese.Text = result["cheese"];
            winer.Location = new Point(size.Width / 2 - winer.Size.Width / 2, 10);
            mouse.Location = new Point(size.Width / 2 - mouse.Size.Width / 2, 10 + winer.Location.Y + winer.Size.Height);
            cheese.Location = new Point(size.Width / 2 - cheese.Size.Width / 2, 10 + mouse.Location.Y + mouse.Size.Height);
            message.Location = new Point(size.Width / 2 - message.Size.Width / 2, 100 + cheese.Location.Y + cheese.Size.Height);
            Visible = true;
        }
    }
}

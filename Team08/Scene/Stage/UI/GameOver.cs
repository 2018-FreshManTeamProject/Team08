using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using InfinityGame.Device;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MouseTrash.Scene.Stage.UI
{
    public class GameOver : UIWindow
    {
        private Label winer;
        private Label thedatafind;
        private Label thedatalost;
        private Label message;
        public GameOver(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            CanMove = false;
            BorderOn = false;
            backColor = Color.White * 0.0f;
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
            thedatafind = new Label(graphicsDevice, this);
            thedatalost = new Label(graphicsDevice, this);
            message = new Label(graphicsDevice, this);
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            winer.BackColor = Color.White * 0.0f;
            thedatafind.BackColor = Color.White * 0.0f;
            thedatalost.BackColor = Color.White * 0.0f;
            message.BackColor = Color.White * 0.0f;
            winer.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            thedatafind.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            thedatalost.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            message.BDText.ForeColor = System.Drawing.Color.YellowGreen;
            winer.TextSize = 128f;
            thedatafind.TextSize = 72f;
            thedatalost.TextSize = 72f;
            message.TextSize = 48f;
            message.Text = "Escキーでメニューを開く\r\nStartボタンでリセット\r\nBackボタンでタイトルへ戻る";
            base.DesignContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("gameover.png");
            base.LoadContent();
        }

        public void ShowResult(Dictionary<string, string> result)
        {
            winer.Text = result["winer"];
            thedatafind.Text = result["thedatafind"];
            thedatalost.Text = result["thedatalost"];
            winer.Location = new Point(size.Width / 2 - winer.Size.Width / 2, 10);
            thedatafind.Location = new Point(size.Width / 2 - thedatafind.Size.Width / 2, 10 + winer.Location.Y + winer.Size.Height);
            thedatalost.Location = new Point(size.Width / 2 - thedatalost.Size.Width / 2, 10 + thedatafind.Location.Y + thedatafind.Size.Height);
            message.Location = new Point(size.Width / 2 - message.Size.Width / 2, 100 + thedatalost.Location.Y + thedatalost.Size.Height);
            Visible = true;
        }
    }
}

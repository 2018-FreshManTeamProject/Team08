using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI.UIContent;
using InfinityGame.Device;
using InfinityGame.Element;
using InfinityGame;
using InfinityGame.Scene;
using InfinityGame.Device.KeyboardManage;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Team08.Scene.Stage.Stages;
using Team08.Scene.Stage.Actor;

namespace Team08.Scene.Title.UI
{
    public class PlayerCursor : Panel
    {
        private Label lable;
        private string player;
        private int timedown = 30;
        private Point coo = Point.Zero;
        private CharaIcon focusChara;
        private IPlayerCursor IPC;
        private PlayerIndex pad;
        private GameStage gameStage;
        public string Player { get { return player; } }
        public Point Coo { get { return coo; } set { SetFocusChara(value); } }
        public PlayerCursor(string player, GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : base(aGraphicsDevice, aParent)
        {
            this.player = player;
            BackColor = Color.White * 0.0f;
            IPC = (IPlayerCursor)parent;
            IPC.Players.Add(player, this);
        }

        private void SetFocusChara(Point pt)
        {
            if (IPC.CharasDict.ContainsKey(pt))
            {
                coo = pt;
                focusChara = IPC.Charas[IPC.CharasDict[Coo]];
                if (focusChara != null)
                {
                    Location = focusChara.Location;
                }
            }
        }

        public override void Initialize()
        {
            Coo = Point.Zero;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = new Size(128, 128);
            lable = new Label(graphicsDevice, this);
            lable.Text = player;
            lable.TextSize = 16f;
            lable.BackColor = Color.White * 0.0f;
            lable.BDText.ForeColor = System.Drawing.Color.Yellow;
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            switch (player)
            {
                case "P0":
                    lable.Location = new Point(0, -lable.Size.Height);
                    pad = PlayerIndex.One;
                    break;
                case "P1":
                    lable.Location = new Point(size.Width - lable.Size.Width, -lable.Size.Height);
                    pad = PlayerIndex.Two;
                    break;
                case "P2":
                    lable.Location = new Point(0, size.Height);
                    pad = PlayerIndex.Three;
                    break;
                case "P3":
                    lable.Location = new Point(size.Width - lable.Size.Width, size.Height);
                    pad = PlayerIndex.Four;
                    break;
            }
            Image = ImageManage.GetSImage("playercursor.png");
            gameStage = (GameStage)((StageScene)GameRun.Instance.scenes["stagescene"]).stages["Stage01"];
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
            if (IGGamePad.GetKeyTrigger(pad, Buttons.DPadUp))
            {
                Coo = new Point(Coo.X, Coo.Y - 1);
            }
            else if (IGGamePad.GetKeyTrigger(pad, Buttons.DPadDown))
            {
                Coo = new Point(Coo.X, Coo.Y + 1);
            }
            else if (IGGamePad.GetKeyTrigger(pad, Buttons.DPadLeft))
            {
                Coo = new Point(Coo.X - 1, Coo.Y);
            }
            else if (IGGamePad.GetKeyTrigger(pad, Buttons.DPadRight))
            {
                Coo = new Point(Coo.X + 1, Coo.Y);
            }
            /*if (GameKeyboard.GetKeyTrigger(Keys.W))
            {
                Coo = new Point(Coo.X, Coo.Y - 1);
            }
            else if (GameKeyboard.GetKeyTrigger(Keys.S))
            {
                Coo = new Point(Coo.X, Coo.Y + 1);
            }
            else if (GameKeyboard.GetKeyTrigger(Keys.A))
            {
                Coo = new Point(Coo.X - 1, Coo.Y);
            }
            else if (GameKeyboard.GetKeyTrigger(Keys.D))
            {
                Coo = new Point(Coo.X + 1, Coo.Y);
            }*/
            base.Update(gameTime);
        }

        /// <summary>
        /// ゲーム内のキャラクター設定
        /// </summary>
        public void SetChara()
        {
            char index = player[1];
            string name = "player" + index;
            if (gameStage.stageObjs[name].Team == "mouse")
            {
                ((Player)gameStage.stageObjs[name]).Chara = focusChara.Chara;
            }
        }
    }
}

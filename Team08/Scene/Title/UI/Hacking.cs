using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using InfinityGame.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Team08.Scene.Title.UI
{
    public class Hacking : UIWindow, IPlayerCursor
    {
        private Label time;
        private Dictionary<string, PlayerCursor> players = new Dictionary<string, PlayerCursor>();
        private int timedown = 600;
        public Dictionary<string, PlayerCursor> Players { get { return players; } }


        public Hacking(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
            Text = GetText("Hacking");
            BorderColor = Color.DarkRed;
            backColor = Color.PaleVioletRed;
            CanClose = false;
            CanMove = false;
        }

        public override void Initialize()
        {
            Visible = false;
            timedown = 600;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = parent.Size / 2;
            Location = ((parent.Size - size) / 2).ToPoint();
            time = new Label(graphicsDevice, this);
            time.TextSize = 16f;
            for (int i = 0; i < 4; i++)
            {
                new PlayerCursor("P" + i.ToString(), graphicsDevice, this);
                players["P" + i.ToString()].Coo = new Point(i, 0);
            }
            base.PreLoadContent();
            
        }

        public override void LoadContent()
        {
            time.Location = new Point(border_Left.Size.Width + 10, border_Top.Size.Height + 10);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (timedown > 0)
                    timedown--;
                if (timedown <= 0)
                {
                    ((BaseScene)parent).IsRun = false;
                    ((BaseScene)parent).GameRun.scenes["stagescene"].IsRun = true;
                    parent.Initialize();
                }
                time.Text = (timedown / 60f).ToString();
                foreach (var l in players)
                {
                    Point tempP = new Point(200 * l.Value.Coo.X + 200, 200 * l.Value.Coo.Y + 200);
                    if (tempP != l.Value.Location)
                    {
                        l.Value.Location = tempP;
                    }
                }
                base.Update(gameTime);
            }
        }
    }
}

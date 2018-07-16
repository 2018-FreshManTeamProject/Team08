using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using InfinityGame.Scene;
using InfinityGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Team08.Scene.Title.UI
{
    public class Hacking : UIWindow, IPlayerCursor
    {
        private Label time;
        private Dictionary<string, PlayerCursor> players = new Dictionary<string, PlayerCursor>();
        private Dictionary<string, CharaIcon> charas = new Dictionary<string, CharaIcon>();
        private Dictionary<Point, string> charasDict = new Dictionary<Point, string>();
        private int timedown = 600;
        public Dictionary<string, PlayerCursor> Players { get { return players; } }
        public Dictionary<string, CharaIcon> Charas { get { return charas; } }
        public Dictionary<Point, string> CharasDict { get { return charasDict; } }


        public Hacking(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
            Text = GetText("Hacking");
            BDText.ForeColor = System.Drawing.Color.Yellow;
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
            time.BDText.ForeColor = System.Drawing.Color.Yellow;
            for (int i = 1; i < 9; i++)
            {
                new CharaIcon(i.ToString(), graphicsDevice, this);
            }
            for (int i = 0; i < 4; i++)
            {
                new PlayerCursor("P" + i.ToString(), graphicsDevice, this);
            }
            base.PreLoadContent();

        }

        public override void LoadContent()
        {
            time.Location = new Point(border_Left.Size.Width + 10, border_Top.Size.Height + 10);
            {
                int i = 0;
                foreach (var l in charas)
                {
                    l.Value.Location = new Point(50 + 150 * (i % 4), border_Top.Size.Height + time.Size.Height + 50 + 150 * (i / 4));
                    charasDict.Add(new Point(i % 4, i / 4), l.Key);
                    i++;
                }
            }
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
                    if (GameRun.ActiveScene is TitleScene)
                    {
                        foreach(var l in Players)
                        {
                            l.Value.SetChara();
                        }
                        ((BaseScene)parent).IsRun = false;
                        ((BaseScene)parent).GameRun.scenes["stagescene"].IsRun = true;
                        parent.Initialize();
                    }
                }
                time.Text = GetText("TimeDown") + (timedown / 60f).ToString();
                base.Update(gameTime);
            }
        }
    }
}

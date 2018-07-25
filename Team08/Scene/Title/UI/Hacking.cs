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
using InfinityGame.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using InfinityGame.Element;
using Microsoft.Xna.Framework.Input;

namespace MouseTrash.Scene.Title.UI
{
    public partial class Hacking : UIWindow, IPlayerCursor
    {
        private Label time;
        private Label[] messages = new Label[1/*9*/];
        private bool start = false;
        private AnimeButton ok;
        private Dictionary<string, PlayerCursor> players = new Dictionary<string, PlayerCursor>();
        private Dictionary<string, CharaIcon> charas = new Dictionary<string, CharaIcon>();
        private Dictionary<Point, string> charasDict = new Dictionary<Point, string>();
        private int timedown = 300;
        public Dictionary<string, PlayerCursor> Players { get { return players; } }
        public Dictionary<string, CharaIcon> Charas { get { return charas; } }
        public Dictionary<Point, string> CharasDict { get { return charasDict; } }
        public bool Start { get { return start; } }

        public Hacking(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
            Text = GetText("Hacking");
            BDText.ForeColor = System.Drawing.Color.Yellow;
            BorderColor = Color.DarkRed;
            backColor = new Color(255, 150, 150);
            CanClose = false;
            CanMove = false;
        }

        public override void Initialize()
        {
            ImageColor = Color.White;
            start = false;
            /*for (int i = 1; i < messages.Length; i++)
            {
                messages[i].Visible = true;
            }*/
            messages[0].Visible = false;
            Visible = false;
            time.Visible = false;
            ok.Visible = true;
            foreach (var l in players)
            {
                l.Value.Visible = false;
            }
            foreach (var l in charas)
            {
                l.Value.Visible = false;
            }
            timedown = 300;
            if (!sounds["hacking"].GetState(SoundState.Stopped))
                sounds["hacking"].Stop();
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = parent.Size * 2 / 3;
            Location = ((parent.Size - size) / 2).ToPoint();
            time = new Label(graphicsDevice, this);
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = new Label(graphicsDevice, this);
            }
            ok = new AnimeButton(graphicsDevice, this);
            ok.Text = GetText("OK");
            ok.Size = new Size(120, 40) * 2;
            ok.Location = new Point((size.Width - ok.Size.Width) / 2, size.Height - ok.Size.Height - 20);
            for (int i = 0; i < messages.Length; i++)
            {
                switch (i)
                {
                    case 1:
                    case 2:
                        messages[i].TextSize = 36f;
                        break;
                    case 3:
                    case 4:
                        messages[i].TextSize = 32f;
                        break;
                    default:
                        messages[i].TextSize = 24f;
                        break;
                }
                messages[i].Text = GetText("Help" + i);
            }
            messages[0].BDText.ForeColor = System.Drawing.Color.Yellow;
            /*messages[1].BDText.ForeColor = System.Drawing.Color.Red;
            messages[2].BDText.ForeColor = System.Drawing.Color.FromArgb(220, 220, 80);
            messages[3].BDText.ForeColor = System.Drawing.Color.LawnGreen;
            messages[4].BDText.ForeColor = System.Drawing.Color.Yellow;*/
            time.TextSize = 16f;
            time.BDText.ForeColor = System.Drawing.Color.Yellow;
            for (int i = 0; i < 18; i++)
            {
                new CharaIcon("cursor" + i.ToString(), graphicsDevice, this);
            }
            for (int i = 0; i < 4; i++)
            {
                new PlayerCursor("P" + i.ToString(), graphicsDevice, this);
            }
            EventRegist();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage("help.png");
            ok.Image = ImageManage.GetSImage("button01");
            messages[0].Location = Alignment.GetMXFAlignment(ContentAlignment.MiddleRight, size, messages[0].Size);
            /*messages[1].Location = new Point(0, TitleSize + 50);
            messages[2].Location = new Point(size.Width - messages[2].Size.Width, TitleSize + 50);
            messages[3].Location = new Point((size.Width - messages[3].Size.Width) / 2, messages[2].Location.Y + messages[2].Size.Height + 10);
            messages[5].Location = new Point(0, messages[3].Location.Y + messages[3].Size.Height + 10);
            messages[6].Location = new Point(size.Width - messages[6].Size.Width, messages[3].Location.Y + messages[3].Size.Height + 10);
            messages[4].Location = new Point((size.Width - messages[4].Size.Width) / 2, messages[6].Location.Y + messages[6].Size.Height + 10);
            messages[7].Location = new Point(0, messages[4].Location.Y + messages[4].Size.Height + 10);
            messages[8].Location = new Point(size.Width - messages[8].Size.Width, messages[4].Location.Y + messages[4].Size.Height + 10);
            */

            time.Location = new Point(border_Left.Size.Width + 10, border_Top.Size.Height + 10);
            {
                for (int i = 0; i < Charas.Count; i++)
                {
                    Charas["cursor" + i.ToString()].Location = new Point(50 + 150 * (i % 6), border_Top.Size.Height + time.Size.Height + 50 + 180 * (i / 6));
                    charasDict.Add(new Point(i % 6, i / 6), "cursor" + i.ToString());
                }
            }
            sounds["hacking"] = SoundManage.GetSound("hacking.wav");
            sounds["hacking"].SetSELoopPlay(true);
            sounds["button"] = SoundManage.GetSound("button.wav");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (start)
                {
                    if (parent.gameWindowsIDs[parent.gameWindowsIDs.Count - 1] != UIID)
                    {
                        SetFocus();
                    }
                    if (!sounds["hacking"].GetState(SoundState.Playing))
                    {
                        sounds["hacking"].Play();
                    }
                    if (timedown > 0)
                        timedown--;
                    if (timedown <= 0)
                    {
                        if (GameRun.ActiveScene is TitleScene)
                        {
                            foreach (var l in Players)
                            {
                                l.Value.SetChara();
                            }
                            ((BaseScene)parent).IsRun = false;
                            ((BaseScene)parent).GameRun.scenes["stagescene"].IsRun = true;
                            ((BaseScene)parent).GameRun.scenes["stagescene"].Initialize();
                            parent.Initialize();
                        }
                    }
                    time.Text = GetText("TimeDown") + (timedown / 60f).ToString();
                }
                else if (IGGamePad.GetKeyTrigger(PlayerIndex.One, Buttons.A))
                    OnStart(null, null);
                base.Update(gameTime);
            }

        }

        private void OnStart(object sender, EventArgs e)
        {
            ImageColor = Color.Transparent;
            SoundPlay("button");
            /*for (int i = 1; i < messages.Length; i++)
            {
                messages[i].Visible = false;
            }*/
            messages[0].Visible = true;
            Visible = true;
            time.Visible = true;
            ok.Visible = false;
            foreach (var l in players)
            {
                l.Value.Visible = true;
            }
            foreach (var l in charas)
            {
                l.Value.Visible = true;
            }
            start = true;
        }

        private void SoundPlay(string sdnm)
        {
            sounds[sdnm].Stop();
            sounds[sdnm].Play();
        }
    }
}

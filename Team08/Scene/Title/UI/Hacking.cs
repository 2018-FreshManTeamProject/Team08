﻿using System;
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

namespace MouseTrash.Scene.Title.UI
{
    public class Hacking : UIWindow, IPlayerCursor
    {
        private Label time;
        private Label message;
        private Dictionary<string, PlayerCursor> players = new Dictionary<string, PlayerCursor>();
        private Dictionary<string, CharaIcon> charas = new Dictionary<string, CharaIcon>();
        private Dictionary<Point, string> charasDict = new Dictionary<Point, string>();
        private int timedown = 300;
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
            timedown = 300;
            if (!sounds["hacking"].GetState(SoundState.Stopped))
                sounds["hacking"].Stop();
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = parent.Size / 2;
            Location = ((parent.Size - size) / 2).ToPoint();
            time = new Label(graphicsDevice, this);
            message = new Label(graphicsDevice, this);
            message.TextSize = 24f;
            message.BDText.ForeColor = System.Drawing.Color.Yellow;
            message.Text = GetText("Help01");
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
            message.Location = new Point(Size.Width - message.Size.Width, (Size.Height - message.Size.Height) / 2);
            {
                int i = 0;
                foreach (var l in charas)
                {
                    l.Value.Location = new Point(50 + 150 * (i % 4), border_Top.Size.Height + time.Size.Height + 50 + 180 * (i / 4));
                    charasDict.Add(new Point(i % 4, i / 4), l.Key);
                    i++;
                }
            }
            sounds["hacking"] = SoundManage.GetSound("hacking.wav");
            sounds["hacking"].SetSELoopPlay(true);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
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
                        parent.Initialize();
                    }
                }
                time.Text = GetText("TimeDown") + (timedown / 60f).ToString();
                base.Update(gameTime);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame;
using InfinityGame.GameGraphics;
using InfinityGame.Scene;
using InfinityGame.Device;
using InfinityGame.UI.UIContent;

namespace MouseTrash.Scene.Title
{
    public class StartUp : BaseScene
    {
        private int timeDown = 180;
        private Label startText;
        private bool soundplay = false;
        public StartUp(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void Initialize()
        {
            soundplay = false;
            timeDown = 180;
            Refract = 1.0f;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            startText = new Label(graphicsDevice, this);
            startText.TextSize = 72f;
            startText.Text = GetText("StartText");
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            startText.Location = ((size - startText.Size) / 2).ToPoint();
            Image = ImageManage.GetSImage("login_logout.png");
            sounds["login"] = SoundManage.GetSound("login.wav");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!soundplay)
            {
                sounds["login"].Play();
                soundplay = true;
            }
            if (timeDown > 0)
                timeDown--;
            base.Update(gameTime);
            if (timeDown <= 0)
            {
                timeDown = 0;
                Refract -= 0.1f;
                if (refract <= 0)
                {
                    Refract = 0;
                    EndStart();
                }
            }
        }

        public void EndStart()
        {
            IsRun = false;
            GameRun.scenes["title"].IsRun = true;
            Program.GetGame().IsMouseVisible = true;
        }
    }
}

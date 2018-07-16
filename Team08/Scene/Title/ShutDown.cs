using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfinityGame;
using InfinityGame.Device;
using InfinityGame.GameGraphics;
using InfinityGame.Scene;
using InfinityGame.UI.UIContent;
using Microsoft.Xna.Framework.Audio;


namespace Team08.Scene.Title
{
    public class ShutDown : BaseScene
    {
        private int timeDown = 180;
        private Label endText;
        private bool soundplay = false;
        public ShutDown(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
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
            endText = new Label(graphicsDevice, this);
            endText.TextSize = 36f;
            endText.Text = GetText("EndText");
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            endText.Location = ((size - endText.Size) / 2).ToPoint();
            Image = ImageManage.GetSImage("login_logout.png");
            sounds["logout"] = SoundManage.GetSound("logout.wav");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!soundplay)
            {
                sounds["logout"].Play();
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
                    EndDown();
                }
            }
        }

        public void EndDown()
        {
            Program.Exit();
        }
    }
}

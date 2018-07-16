using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame.GameGraphics;
using InfinityGame.Stage;
using InfinityGame.Stage.StageObject;
using InfinityGame.Device;
using InfinityGame.Element;
using Team08.Scene.Stage.Actor;

namespace Team08.Scene.Stage.Stages
{
    public class GameStage : BaseStage
    {
        protected Random rnd = new Random();
        public int cheeseNum = 20;
        public int eatedCheese = 0;
        public int mouseNum = 3;
        public int killedMouse = 0;
        public int mouseWinNum = 18;
        public int catWinNum = 4;
        public bool mouseWin = false;
        public bool catWin = false;
        public int startTime = 180;
        public GameStage(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void Initialize()
        {
            startTime = 180;
            eatedCheese = 0;
            killedMouse = 0;
            mouseWin = false;
            catWin = false;
            for (int i = 0; i < 4; i++)
            {
                stageObjs["player" + i.ToString()].Team = "mouse";
            }
            int j = rnd.Next(4);
            stageObjs["player" + j.ToString()].Team = "antivirus";

            for (int i = 0; i < cheeseNum; i++)
            {
                if (!stageObjs.ContainsKey("cheese" + i.ToString()))
                {
                    new Cheese(graphicsDevice, this, "cheese" + i.ToString());
                    stageObjs["cheese" + i.ToString()].PreLoadContent();
                    stageObjs["cheese" + i.ToString()].LoadContent();
                }
            }
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            for (int i = 0; i < 4; i++)
            {
                new Player(graphicsDevice, this, "player" + i.ToString());
                ((Player)stageObjs["player" + i.ToString()]).player = (PlayerIndex)i;
            }
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (startTime > 0)
            {
                startTime--;
            }
            else
            {
                if (eatedCheese >= mouseWinNum)
                {
                    mouseWin = true;
                }
                if (killedMouse >= catWinNum)
                {
                    catWin = true;
                }
            }
            base.Update(gameTime);
        }
    }
}

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
using MouseTrash.Scene.Stage.Actor;

namespace MouseTrash.Scene.Stage.Stages
{
    public class GameStage : BaseStage
    {
        protected Random rnd = new Random();
        public int thedataNum = 20;
        public int eatedTheData = 0;
        public int mouseNum = 3;
        public int killedMouse = 0;
        public int mouseWinNum = 18;
        public int catWinNum = 3;
        public bool mouseWin = false;
        public bool catWin = false;
        public int startTime = 180;
        public GameStage(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void Initialize()
        {
            startTime = 180;
            eatedTheData = 0;
            killedMouse = 0;
            mouseWin = false;
            catWin = false;
            for (int i = 0; i < 4; i++)
            {
                stageObjs["player" + i.ToString()].Team = "mouse";
            }
            int j = rnd.Next(4);
            stageObjs["player" + j.ToString()].Team = "antivirus";

            for (int i = 0; i < thedataNum; i++)
            {
                if (!stageObjs.ContainsKey("thedata" + i.ToString()))
                {
                    new TheData(graphicsDevice, this, "thedata" + i.ToString());
                    stageObjs["thedata" + i.ToString()].PreLoadContent();
                    stageObjs["thedata" + i.ToString()].LoadContent();
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
                if (eatedTheData >= mouseWinNum)
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

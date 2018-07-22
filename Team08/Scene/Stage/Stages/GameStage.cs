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
        public int mouseWinNum = 19;
        public int catWinNum = 3;
        public int mousePoint = 0;
        public int antivirusPoint = 0;
        public bool mouseWin = false;
        public bool catWin = false;
        public int startTime = 180;
        public PlayerControl[] players = new PlayerControl[4];
        public GameStage(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void Initialize()
        {
            mousePoint = 0;
            antivirusPoint = 0;
            startTime = 180;
            eatedTheData = 0;
            killedMouse = 0;
            mouseWin = false;
            catWin = false;
            for (int i = 0; i < thedataNum; i++)
            {
                if (!stageObjs.ContainsKey("thedata" + i.ToString()))
                {
                    new TheData(graphicsDevice, this, "thedata" + i.ToString());
                    stageObjs["thedata" + i.ToString()].PreLoadContent();
                    stageObjs["thedata" + i.ToString()].LoadContent();
                }
            }
            int j = rnd.Next(4);
            for (int i = 0; i < 3; i++)
            {
                if (i != j)
                    ((Player)stageObjs["mouse" + i.ToString()]).playerControl = players[i];
                else
                    ((Player)stageObjs["mouse" + i.ToString()]).playerControl = players[3];
            }
            ((Player)stageObjs["antivirus"]).playerControl = players[j];
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            for (int i = 0; i < 4; i++)
            {
                players[i] = new PlayerControl((PlayerIndex)i);
            }
            for (int i = 0; i < 3; i++)
            {
                new Mouse(graphicsDevice, this, "mouse" + i.ToString());
                stageObjs["mouse" + i.ToString()].Team = "mouse";
            }
            new Antivirus(graphicsDevice, this, "antivirus");
            stageObjs["antivirus"].Team = "antivirus";
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

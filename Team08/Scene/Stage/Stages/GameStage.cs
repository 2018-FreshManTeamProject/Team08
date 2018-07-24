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
using Microsoft.Xna.Framework.Audio;

namespace MouseTrash.Scene.Stage.Stages
{
    public class GameStage : BaseStage
    {
        protected Random rnd = new Random();
        private int theDataNum = 20;
        private int eatedTheData = 0;
        private int mouseNum = 3;
        private int killedMouse = 0;
        private int mouseWinNum = 19;
        private int catWinNum = 3;
        private int mousePoint = 0;
        private int antivirusPoint = 0;
        private bool mouseWin = false;
        private bool catWin = false;
        private readonly int StartTimeRe = 240;
        private int startTime = 0;
        private PlayerControl[] players = new PlayerControl[4];

        public int TheDataNum { get => theDataNum; set => theDataNum = value; }
        public int EatedTheData { get => eatedTheData; set => eatedTheData = value; }
        public int MouseNum { get => mouseNum; set => mouseNum = value; }
        public int KilledMouse { get => killedMouse; set => killedMouse = value; }
        public int MouseWinNum { get => mouseWinNum; set => mouseWinNum = value; }
        public int CatWinNum { get => catWinNum; set => catWinNum = value; }
        public int MousePoint { get => mousePoint; set => mousePoint = value; }
        public int AntivirusPoint { get => antivirusPoint; set => antivirusPoint = value; }
        public bool MouseWin { get => mouseWin; set => mouseWin = value; }
        public bool CatWin { get => catWin; set => catWin = value; }
        public int StartTime { get => startTime; set { startTime = value; SetStartTime(); } }
        public PlayerControl[] Players { get => players; set => players = value; }

        public GameStage(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void Initialize()
        {
            sounds["mainscene"].Stop();
            MousePoint = 0;
            AntivirusPoint = 0;
            StartTime = StartTimeRe;
            EatedTheData = 0;
            KilledMouse = 0;
            MouseWin = false;
            CatWin = false;
            for (int i = 0; i < TheDataNum; i++)
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
                    ((Player)stageObjs["mouse" + i.ToString()]).PlayerControl = Players[i];
                else
                    ((Player)stageObjs["mouse" + i.ToString()]).PlayerControl = Players[3];
            }
            ((Player)stageObjs["antivirus"]).PlayerControl = Players[j];
            base.Initialize();
        }

        private void SetStartTime()
        {
            if (startTime > StartTimeRe)
                startTime = StartTimeRe;
        }

        public override void PreLoadContent()
        {
            for (int i = 0; i < 4; i++)
            {
                Players[i] = new PlayerControl((PlayerIndex)i);
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
            sounds["mainscene"] = SoundManage.GetSound("mainscene.wav");
            sounds["mainscene"].SetSELoopPlay(true);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (StartTime > 0)
            {
                StartTime--;
            }
            else
            {
                if (!sounds["mainscene"].GetState(SoundState.Playing))
                    sounds["mainscene"].Play();
                if (EatedTheData >= MouseWinNum)
                {
                    MouseWin = true;
                }
                if (KilledMouse >= CatWinNum)
                {
                    CatWin = true;
                }
            }
            base.Update(gameTime);
        }
    }
}

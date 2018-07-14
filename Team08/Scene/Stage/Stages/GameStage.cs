﻿using System;
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
                stageObjs["player" + i.ToString()].Image = ImageManage.GetSImage("nezumi.png");
                stageObjs["player" + i.ToString()].Size = Size.Parse(stageObjs["player" + i.ToString()].Image.Image.Size) / 2;
                stageObjs["player" + i.ToString()].Render.Scale = Vector2.One / 2;
            }
            int j = rnd.Next(4);
            stageObjs["player" + j.ToString()].Team = "cat";
            stageObjs["player" + j.ToString()].Image = ImageManage.GetSImage("neko.png");
            stageObjs["player" + j.ToString()].Size = Size.Parse(stageObjs["player" + j.ToString()].Image.Image.Size) / 2;
            stageObjs["player" + j.ToString()].Render.Scale = Vector2.One / 2;
            stageObjs["player" + j.ToString()].MovePriority = 9;

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

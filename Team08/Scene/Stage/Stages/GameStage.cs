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
using InfinityGame.Stage.StageContent;
using InfinityGame.Device;
using InfinityGame.Element;
using Team08.Scene.Stage.Actor;

namespace Team08.Scene.Stage.Stages
{
    public class GameStage : BaseStage
    {
        protected Random rnd = new Random();
        public int cheeseNum = 7;
        public int eatedCheese = 0;
        public int mouseNum = 3;
        public int killedMouse = 0;
        public int mouseWinNum = 2;
        public int catWinNum = 2;
        public bool mouseWin = false;
        public bool catWin = false;
        public GameStage(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void Initialize()
        {
            eatedCheese = 0;
            killedMouse = 0;
            mouseWin = false;
            catWin = false;
            for (int i = 0; i < 4; i++)
            {
                stageContents["player" + i.ToString()].CrimpGroup = "mouse";
                stageContents["player" + i.ToString()].Team = "mouse";
                stageContents["player" + i.ToString()].Image = ImageManage.GetSImage("nezumi.png");
                stageContents["player" + i.ToString()].Size = Size.Parse(stageContents["player" + i.ToString()].Image.Image.Size);
                stageContents["player" + i.ToString()].Coordinate = new Vector2(rnd.Next(EndOfLeftUp.X, EndOfRightDown.X), rnd.Next(EndOfLeftUp.Y, EndOfRightDown.Y));
            }
            int j = rnd.Next(4);
            stageContents["player" + j.ToString()].CrimpGroup = "cat";
            stageContents["player" + j.ToString()].Team = "cat";
            stageContents["player" + j.ToString()].Image = ImageManage.GetSImage("neko.png");
            stageContents["player" + j.ToString()].Size = Size.Parse(stageContents["player" + j.ToString()].Image.Image.Size);
            stageContents["player" + j.ToString()].MovePriority = 9;

            for (int i = 0; i < cheeseNum; i++)
            {
                if (!stageContents.ContainsKey("cheese" + i.ToString()))
                {
                    new Cheese(graphicsDevice, this, "cheese" + i.ToString());
                    stageContents["cheese" + i.ToString()].PreLoadContent();
                    stageContents["cheese" + i.ToString()].LoadContent();
                }
            }
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            for (int i = 0; i < 4; i++)
            {
                new Player(graphicsDevice, this, "player" + i.ToString());
                ((Player)stageContents["player" + i.ToString()]).player = (PlayerIndex)i;
            }
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            Initialize();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (eatedCheese >= mouseWinNum)
            {
                mouseWin = true;
            }
            if (killedMouse >= catWinNum)
            {
                catWin = true;
            }
            base.Update(gameTime);
        }
    }
}

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
        public int cheesenum = 7;
        public int eatcheese = 0;
        public int mousenum = 3;
        public int killmouse = 0;
        public bool mousewin = false;
        public bool catwin = false;
        public GameStage(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void Initialize()
        {
            eatcheese = 0;
            killmouse = 0;
            mousewin = false;
            catwin = false;
            for (int i = 0; i < 4; i++)
            {
                stageContents["player" + i.ToString()].CrimpGroup = "mouse";
                stageContents["player" + i.ToString()].Team = "mouse";
                stageContents["player" + i.ToString()].Image = ImageManage.GetSImage("nezumi.png");
                stageContents["player" + i.ToString()].Size = Size.Parse(stageContents["player" + i.ToString()].Image.Image.Size);
            }
            int j = rnd.Next(4);
            stageContents["player" + j.ToString()].CrimpGroup = "cat";
            stageContents["player" + j.ToString()].Team = "cat";
            stageContents["player" + j.ToString()].Image = ImageManage.GetSImage("neko.png");
            stageContents["player" + j.ToString()].MovePriority = 9;

            stageContents["player0"].Coordinate = new Vector2(100, 100);
            stageContents["player1"].Coordinate = new Vector2(450, 100);
            stageContents["player2"].Coordinate = new Vector2(100, 400);
            stageContents["player3"].Coordinate = new Vector2(450, 400);

            for (int i = 0; i < cheesenum; i++)
            {
                if (!stageContents.ContainsKey("cheese" + i.ToString()))
                {
                    new Cheese(graphicsDevice, this, "cheese" + i.ToString());
                    stageContents["cheese" + i.ToString()].PreLoadContent();
                    stageContents["cheese" + i.ToString()].LoadContent();
                    Thread.Sleep(10);
                }
            }
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            new Player0(graphicsDevice, this, "player0");
            new Player1(graphicsDevice, this, "player1");
            new Player2(graphicsDevice, this, "player2");
            new Player3(graphicsDevice, this, "player3");
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            Initialize();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (eatcheese >= 5)
            {
                mousewin = true;
            }
            if (killmouse >= 2)
            {
                catwin = true;
            }
            base.Update(gameTime);
        }
    }
}

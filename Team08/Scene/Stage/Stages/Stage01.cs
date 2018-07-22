using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class Stage01 : GameStage
    {
        private Size ssize;
        public Stage01(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            EndOfLeftUp = new Point(0, 0);
            EndOfRightDown = new Point(3840, 2160);
            ssize = Size.Parse((EndOfRightDown - EndOfLeftUp));
        }

        public override void Initialize()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!stageObjs.ContainsKey("wall" + i.ToString()))
                {
                    new Wall(graphicsDevice, this, "wall" + i.ToString());
                    stageObjs["wall" + i.ToString()].PreLoadContent();
                    stageObjs["wall" + i.ToString()].LoadContent();
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (!stageObjs.ContainsKey("circelwall" + i.ToString()))
                {
                    new CircelWall(graphicsDevice, this, "circelwall" + i.ToString());
                    stageObjs["circelwall" + i.ToString()].PreLoadContent();
                    stageObjs["circelwall" + i.ToString()].LoadContent();
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (!stageObjs.ContainsKey("elasticitywall" + i.ToString()))
                {
                    new ElasticityWall(graphicsDevice, this, "elasticitywall" + i.ToString());
                    stageObjs["elasticitywall" + i.ToString()].PreLoadContent();
                    stageObjs["elasticitywall" + i.ToString()].LoadContent();
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (!stageObjs.ContainsKey("paralysiswall" + i.ToString()))
                {
                    new ParalysisWall(graphicsDevice, this, "paralysiswall" + i.ToString());
                    stageObjs["paralysiswall" + i.ToString()].PreLoadContent();
                    stageObjs["paralysiswall" + i.ToString()].LoadContent();
                }
            }
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            new StageField(graphicsDevice, this, "stageField0");
            new StageLabel(graphicsDevice, this, "date");
            new StageLabel(graphicsDevice, this, "time");
            stageObjs["stageField0"].DrawOrder = 0;
            stageObjs["date"].DrawOrder = 1;
            stageObjs["time"].DrawOrder = 1;

            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            stageObjs["stageField0"].Coordinate = EndOfLeftUp.ToVector2();
            stageObjs["stageField0"].Size = new Size(3840, 2160);
            base.DesignContent();
        }

        public override void LoadContent()
        {
            ((StageLabel)stageObjs["date"]).TextSize = 24f;
            ((StageLabel)stageObjs["time"]).TextSize = 24f;
            ((StageLabel)stageObjs["date"]).Text = DateTime.Now.ToString("yyyy/MM/dd");
            ((StageLabel)stageObjs["time"]).Text = DateTime.Now.ToString("HH:mm");
            stageObjs["date"].Coordinate = new Vector2(ssize.Width - stageObjs["date"].Size.Width - 40, ssize.Height - 40);
            stageObjs["time"].Coordinate = new Vector2(ssize.Width - stageObjs["date"].Size.Width / 2 - stageObjs["time"].Size.Width / 2 - 40, ssize.Height - stageObjs["date"].Size.Height - stageObjs["time"].Size.Height - 20);
            stageObjs["stageField0"].Image = ImageManage.GetSImage("stagefield.png");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ((StageLabel)stageObjs["date"]).Text = DateTime.Now.ToString("yyyy/MM/dd");
            ((StageLabel)stageObjs["time"]).Text = DateTime.Now.ToString("HH:mm");
            base.Update(gameTime);
        }
    }
}

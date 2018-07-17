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

        public Stage01(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            EndOfLeftUp = new Point(0, 0);
            EndOfRightDown = new Point(3840, 2160);
        }

        public override void Initialize()
        {
            for (int i = 0; i < 20; i++)
            {
                if (!stageObjs.ContainsKey("wall" + i.ToString()))
                {
                    new Wall(graphicsDevice, this, "wall" + i.ToString());
                    stageObjs["wall" + i.ToString()].PreLoadContent();
                    stageObjs["wall" + i.ToString()].LoadContent();
                }
            }
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            new StageField(graphicsDevice, this, "stageField0");//例
            base.PreLoadContent();
        }

        protected override void DesignContent()
        {
            stageObjs["stageField0"].Coordinate = EndOfLeftUp.ToVector2();
            stageObjs["stageField0"].Size = new Size(3840, 2160);
            stageObjs["stageField0"].Render.Scale = Vector2.One * 2;

            base.DesignContent();
        }

        public override void LoadContent()
        {
            stageObjs["stageField0"].Image = ImageManage.GetSImage("stagefield.png");
            base.LoadContent();
        }
    }
}

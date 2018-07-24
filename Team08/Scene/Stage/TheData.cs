using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfinityGame.GameGraphics;
using InfinityGame.Stage.StageObject.Block;
using InfinityGame.Device;
using InfinityGame.Element;
using MouseTrash.Scene.Stage.Stages;
using InfinityGame.Stage.StageObject;
using MouseTrash.Scene.Stage.Actor;

namespace MouseTrash.Scene.Stage
{
    public class TheData : Block
    {
        private bool eaten = false;
        private Random rnd = new Random();

        public bool Eaten { get => eaten; set => eaten = value; }

        public TheData(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            IsCrimp = false;
            DrawOrder = 7;
        }

        public override void Initialize()
        {
            Eaten = false;
            Visible = true;
            Image = ImageManage.GetSImage("thedata.png");
            Size = Size.Parse(Image.Image.Size);
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            base.Initialize();
        }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            foreach (var l in tempSO)
            {
                if (l.Value is TheData || l.Value is ElasticityWall || (l.Value is Mouse && ((GameStage)Stage).StartTime > 0))
                {
                    ((GameStage)Stage).StartTime++;
                    Initialize();
                    break;
                }
            }
            base.CalAllColl(tempSO);
        }
    }
}

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

namespace MouseTrash.Scene.Stage
{
    public class TheData : Block
    {
        public bool eaten = false;
        Random rnd = new Random();
        public TheData(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            IsCrimp = false;
        }

        public override void Initialize()
        {
            eaten = false;
            Visible = true;
            Image = ImageManage.GetSImage("thedata.png");
            Size = Size.Parse(Image.Image.Size);
            Coordinate = new Vector2(rnd.Next(Stage.EndOfLeftUp.X, Stage.EndOfRightDown.X - size.Width), rnd.Next(Stage.EndOfLeftUp.Y, Stage.EndOfRightDown.Y - size.Height));
            if (eaten)
            {
                ((GameStage)Stage).eatedTheData--;
                Initialize();
                return;
            }
            base.Initialize();
        }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            foreach (var l in tempSO)
            {
                if (l.Value is TheData || l.Value is Wall || l.Value is ElasticityWall)
                {
                    Initialize();
                    break;
                }
            }
            base.CalAllColl(tempSO);
        }
    }
}

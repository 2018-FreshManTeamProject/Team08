using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfinityGame.Stage.StageObject;
using MouseTrash.Scene.Stage.Actor;

namespace MouseTrash.Scene.Stage
{
    public class ParalysisWall : ElasticityWall
    {
        public ParalysisWall(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {
            Color = Color.Purple;
            vibrationTime = 1000;
        }

        public override void CalAllColl(Dictionary<string, StageObj> tempSO)
        {
            foreach(var l in tempSO)
            {
                if (l.Value is Player && ((Player)l.Value).Life)
                {
                    ((Player)l.Value).PlayerState["paralysis"] = 60;
                }
            }
            base.CalAllColl(tempSO);
        }
    }
}

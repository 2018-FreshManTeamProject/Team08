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
using MouseTrash.Scene.Stage.Stages;

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
                if (((GameStage)Stage).StartTime > 0)
                    break;
                if (l.Value is Player && ((Player)l.Value).Life)
                {
                    int tm = 0;
                    if (l.Value is Mouse)
                        tm = 60;
                    else if (l.Value is Antivirus)
                        tm = 20;
                    ((Player)l.Value).PlayerState["paralysis"] = tm;
                }
            }
            base.CalAllColl(tempSO);
        }
    }
}

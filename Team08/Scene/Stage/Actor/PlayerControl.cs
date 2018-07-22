using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrash.Scene.Stage.Actor
{
    public class PlayerControl
    {
        public string Chara;
        public PlayerIndex Player;

        public PlayerControl(PlayerIndex player)
        {
            Player = player;
        }
    }
}

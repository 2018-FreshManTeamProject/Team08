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
        private string chara;
        private PlayerIndex player;

        public string Chara { get => chara; set => chara = value; }
        public PlayerIndex Player { get => player; set => player = value; }

        public PlayerControl(PlayerIndex player)
        {
            Player = player;
        }
    }
}

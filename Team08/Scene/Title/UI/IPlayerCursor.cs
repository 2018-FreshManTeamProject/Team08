using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MouseTrash.Scene.Title.UI
{
    public interface IPlayerCursor
    {
        Dictionary<string, PlayerCursor> Players { get; }
        Dictionary<string, CharaIcon> Charas { get; }
        Dictionary<Point, string> CharasDict { get; }
    }
}

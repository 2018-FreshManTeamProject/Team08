using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame;
using InfinityGame.GameGraphics;
using InfinityGame.Scene;
using InfinityGame.Element;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Team08.Scene
{
    public class ULoadScene : LoadScene
    {
        public ULoadScene(string aName, GraphicsDevice aGraphicsDevice, BaseDisplay aParent, GameRun aGameRun) : base(aName, aGraphicsDevice, aParent, aGameRun)
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
            startUp.Location = new Point(size.Width / 2 - startUp.Size.Width / 2, size.Height * 2 / 3);
            StreamReader streamReader = new StreamReader("./Sound/loading.wav");
            SoundEffect tse = SoundEffect.FromStream(streamReader.BaseStream);
            sounds.Add("loading", new Sound(tse));
            sounds["loading"].Play();
        }

        public override void Clear()
        {
            sounds["loading"].Stop();
            base.Clear();
        }
    }
}

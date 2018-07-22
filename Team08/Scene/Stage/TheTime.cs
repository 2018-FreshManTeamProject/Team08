using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.Stage.StageObject;
using Microsoft.Xna.Framework.Graphics;
using InfinityGame.UI.UIContent;
using Microsoft.Xna.Framework;
using InfinityGame.Element;

namespace MouseTrash.Scene.Stage
{
    public class TheTime : StageField
    {
        private Label time;
        private Label date;
        public TheTime(GraphicsDevice aGraphicsDevice, BaseDisplay aParent, string aName) : base(aGraphicsDevice, aParent, aName)
        {

        }

        public override void PreLoadContent()
        {
            Size = new Size(300, 100);
            Coordinate = Stage.stageObjs["stageField0"].Coordinate - (Stage.stageObjs["stageField0"].Size + Size).ToVector2();
            time = new Label(graphicsDevice, this);
            date = new Label(graphicsDevice, this);
            time.TextSize = 24f;
            date.TextSize = 24f;
            time.Text = DateTime.Now.ToString("HH:mm");
            date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            time.Location = new Point(size.Width - date.Size.Width / 2 - time.Size.Width / 2 - 20, 5);
            date.Location = new Point(size.Width - date.Size.Width - 20, 30);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            time.Text = DateTime.Now.ToString("HH:mm");
            date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            base.Update(gameTime);
        }
    }
}

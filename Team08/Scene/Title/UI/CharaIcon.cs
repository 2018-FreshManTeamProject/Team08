using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using InfinityGame.Element;
using InfinityGame.Device;
using Microsoft.Xna.Framework.Graphics;

namespace MouseTrash.Scene.Title.UI
{
    public class CharaIcon : Panel
    {
        private string chara;
        public string Chara { get { return chara; } }
        public CharaIcon(string chara, GraphicsDevice aGraphicsDevice, BaseDisplay aParent) : base(aGraphicsDevice, aParent)
        {
            this.chara = chara;
            ((IPlayerCursor)parent).Charas.Add(chara, this);
        }

        public override void PreLoadContent()
        {
            Size = new Size(128, 128);
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            Image = ImageManage.GetSImage(chara +/*"_frontside"*/ ".png");//調整必要
            base.LoadContent();
        }

        /// <summary>
        /// このクラスではこの機能は使用禁止
        /// </summary>
        public override void SetFocus()
        {
            //使用禁止
        }
    }
}

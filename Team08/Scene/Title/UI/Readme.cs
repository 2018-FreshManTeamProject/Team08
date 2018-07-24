using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityGame.GameGraphics;
using InfinityGame.UI;
using InfinityGame.UI.UIContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MouseTrash.Scene.Title.UI
{
    public class Readme : UIWindow
    {
        private Label message;
        public Readme(GraphicsDevice aGraphicsDevice, BaseDisplay parent) : base(aGraphicsDevice, parent)
        {
            Visible = false;
        }

        public override void Initialize()
        {
            Visible = false;
            base.Initialize();
        }

        public override void PreLoadContent()
        {
            Size = parent.Size * 3 / 4;
            Location = ((parent.Size - size) / 2).ToPoint();
            Text = GetText("ReadmeText");
            message = new Label(graphicsDevice, this);
            message.TextSize = 24f;
            #region クレジット
            message.Text = "「MouseTrash」は「InfinityGame」サブエンジンを使用\r\n" +
                           "\r\n" +
                           "「MouseTrash」について：\r\n" +
                           "ゲーム起案：爪橋（暫定名字表示）\r\n" +
                           "ゲーム検討：爪橋（暫定名字表示）\r\n" +
                           "　　　　　　吉村（暫定名字表示）\r\n" +
                           "　　　　　　XIE SHAOJIE\r\n" +
                           "プログラム著作者：XIE SHAOJIE\r\n" +
                           "グラフィック著作者：吉村（主人公関係、暫定名字表示）\r\n" +
                           "                    爪橋（マウス関係、暫定名字表示）\r\n" +
                           "                    XIE SHAOJIE（UI関係）\r\n" +
                           "サンド編集：XIE SHAOJIE\r\n" +
                           "            吉村（暫定名字表示）\r\n" +
                           "\r\n" +
                           "「InfinityGame」について：\r\n" +
                           "バージョン：1.0.1.4\r\n" +
                           "バージョン作成年月日：2018-7-22\r\n" +
                           "著作者：XIE SHAOJIE\r\n" +
                           "\r\n" +
                           "もし「MouseTrash」と「InfinityGame」について疑問などがあったら、\r\n" +
                           "本人たちに連絡してください。";
            #endregion クレジット
            message.Location = ((Size - message.Size) / 2).ToPoint();
            base.PreLoadContent();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }
    }
}

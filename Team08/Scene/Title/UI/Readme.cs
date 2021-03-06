﻿using System;
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
                           "ゲーム起案：橋爪\r\n" +
                           "ゲーム検討：橋爪\r\n" +
                           "　　　　　　吉村 梨菜\r\n" +
                           "　　　　　　謝 少杰\r\n" +
                           "プログラム著作者：謝 少杰\r\n" +
                           "グラフィック著作者：吉村 梨菜（主人公関係、操作ヘルプ）\r\n" +
                           "                    橋爪（マウス関係）\r\n" +
                           "                    謝 少杰（UI関係）\r\n" +
                           "サンド編集：謝 少杰\r\n" +
                           "            吉村 梨菜\r\n" +
                           "\r\n" +
                           "「InfinityGame」について：\r\n" +
                           "バージョン：1.0.1.4\r\n" +
                           "バージョン作成年月日：2018-7-22\r\n" +
                           "著作者：XIE SHAOJIE（謝 少杰）\r\n" +
                           "\r\n" +
                           "「InfinityGame」は勉強のため作られ、今後の持続的な更新予定もなし。\r\n" +
                           "商業エンジンと比べにならないので、ご理解してください。";
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

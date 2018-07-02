// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InfinityGame.Lang;
using InfinityGame.Scene;
using InfinityGame.Device;
using InfinityGame.Def;
using InfinityGame;
using System;
using System.Threading;
using InfinityGame.Device.WindowsScreen;

/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace Team08
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        //private SpriteBatch spriteBatch;//画像をスクリーン上に描画するためのオブジェクト  //このクラスで描画しない
        private string title = "Team08";
        private GameRun gameRun;
        private InfinityGame.Element.Size tempScreen;


        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            IGConfig.IGConfigLoad();
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            if (IGConfig.isFullScreen)
            {
                System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
                int tempsWidth = screen.Bounds.Width;
                int tempsHeight = screen.Bounds.Height;
                tempScreen = new InfinityGame.Element.Size(tempsWidth, tempsHeight);

                if (IGConfig.screen.Width > tempsWidth)
                {
                    IGConfig.screen.Width = tempsWidth;
                }
                if (IGConfig.screen.Height > tempsHeight)
                {
                    IGConfig.screen.Height = tempsHeight;
                }
                ChangeScreen.ChangeResolution(IGConfig.screen.Width, IGConfig.screen.Height);
            }
            graphicsDeviceManager.PreferredBackBufferWidth = IGConfig.screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = IGConfig.screen.Height;
            graphicsDeviceManager.IsFullScreen = IGConfig.isFullScreen;
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";
            IGConfig.MNCT = Content;
            IsMouseVisible = true;


        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述
            FirstInitialize();
            AfterInitialize();
            LastInitialize();
            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }
        public void FirstInitialize()
        {
            Resources.SetGD(GraphicsDevice);
            GameTexts.Initialize(IGConfig.gameLanguage);
            gameRun = new GameRun(GraphicsDevice);
            Window.Title = GameTexts.GetText(title);
        }

        public void AfterInitialize()
        {

        }

        public void LastInitialize()
        {
            gameRun.Initialize();
            gameRun.PreLoad();
        }

        #region 「LoadContent」「UnloadContent」はgameRunの中で自動で処理するため、必要でなくなった。
        //gameRunの中で自動で処理するため、必要じゃなくなった。
        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        /*protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成
            //spriteBatch = new SpriteBatch(GraphicsDevice);  //このクラスで描画しない

            // この下にロジックを記述

            // この上にロジックを記述
            base.LoadContent();
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }*/
        #endregion
        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            //if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
            //     (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            //{
            //}

            // この下に更新ロジックを記述
            gameRun.Update(gameTime);

            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.Black);

            // この下に描画ロジックを記述
            gameRun.Draw(gameTime);
            
            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// ゲームが終了中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting(object sender, EventArgs args)
        {
            ChangeScreen.ChangeResolution(tempScreen.Width, tempScreen.Height);
            gameRun.IsGameRun = false;
            Thread.Sleep(500);
            base.OnExiting(sender, args);
        }

        /// <summary>
        /// ゲームが活動している状態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnActivated(object sender, EventArgs args)
        {
            if (gameRun != null && gameRun.GameMouse != null)
                gameRun.GameMouse.Enable = true;
            base.OnActivated(sender, args);
        }

        /// <summary>
        /// ゲームが活動していない状態
        /// 他のアプリなどを操作している状態。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            if (gameRun != null && gameRun.GameMouse != null)
                gameRun.GameMouse.Enable = false;
            base.OnDeactivated(sender, args);
        }
    }
}

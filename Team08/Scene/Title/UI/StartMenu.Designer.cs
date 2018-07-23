using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrash.Scene.Title.UI
{
    public partial class StartMenu
    {
        private void EventRegist()
        {
            #region AnimeButton
            shutdown.Click += ExitGame;
            antivirus.Click += OpenWarning;
            #endregion
            #region Button
            readme.Click += ReadmeBT;
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team08.Scene.Title.UI
{
    public partial class StartMenu
    {
        private void EventRegist()
        {
            #region AnimeButton
            shutdown.LeftDownToUp += ExitGame;
            #endregion
        }
    }
}

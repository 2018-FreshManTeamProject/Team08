using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team08.Scene.Title
{
    public partial class TitleScene
    {
        private void EventRegist()
        {
            #region AnimeButton
            this.exitAB.LeftDownToUp += ExitGame;
            this.startAB.LeftDownToUp += StartGame;
            #endregion

        }
    }
}

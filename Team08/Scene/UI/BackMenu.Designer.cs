using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team08.Scene.UI
{
    public partial class BackMenu
    {
        private void EventRegist()
        {
            #region AnimeButton
            this.back.LeftDownToUp += Back;
            this.title.LeftDownToUp += ToTitle;
            this.exit.LeftDownToUp += Exit;
            #endregion
        }
    }
}

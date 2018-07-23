using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrash.Scene.Stage.UI
{
    public partial class BackMenu
    {
        private void EventRegist()
        {
            #region AnimeButton
            this.backAB.Click += Back;
            this.title.Click += ToTitle;
            this.reset.Click += ReSet;
            this.exitAB.Click += Exit;
            #endregion
        }
    }
}

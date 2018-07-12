using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team08.Scene.Title.UI
{
    public partial class TaskBar
    {
        private void EventRegist()
        {
            #region AnimeButton
            this.start.LeftDownToUp += OnStartMenu;
            #endregion

        }
    }
}

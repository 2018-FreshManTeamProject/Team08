using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrash.Scene.Title.UI
{
    public partial class TaskBar
    {
        private void EventRegist()
        {
            #region AnimeButton
            this.start.Click += OnStartMenu;
            #endregion

        }
    }
}

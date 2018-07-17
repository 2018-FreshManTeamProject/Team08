using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrash.Scene.Title.UI
{
    public partial class Warning
    {
        private void EventRegist()
        {
            #region AnimeButton
            ok.Click += OK;
            cancel.Click += Cancel;
            #endregion
        }
    }
}

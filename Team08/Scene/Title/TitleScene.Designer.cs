using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrash.Scene.Title
{
    public partial class TitleScene
    {
        private void EventRegist()
        {
            #region AnimeButton
            this.antivirus.Click += OpenWarning;
            #endregion

        }
    }
}

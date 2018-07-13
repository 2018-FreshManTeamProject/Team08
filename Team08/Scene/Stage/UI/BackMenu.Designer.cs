﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team08.Scene.Stage.UI
{
    public partial class BackMenu
    {
        private void EventRegist()
        {
            #region AnimeButton
            this.back.Click += Back;
            this.title.Click += ToTitle;
            this.reset.Click += ReSet;
            this.exit.Click += Exit;
            #endregion
        }
    }
}

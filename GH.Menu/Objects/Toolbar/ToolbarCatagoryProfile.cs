﻿

namespace GH.Menu.Objects.Toolbar
{
    using System.Collections.Generic;

    public class ToolbarCatagoryProfile : List<ToolbarLineProfile>
    {
        public ToolbarCatagoryProfile(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
    }
}

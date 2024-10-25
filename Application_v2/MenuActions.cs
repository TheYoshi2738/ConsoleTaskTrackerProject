using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public class MenuActions
    {
        public string? Title { get; set; }
        public Func<IScreen> Action { get; set; }

        public MenuActions(string? title, Func<IScreen> action)
        {
            Title = title;
            Action = action;
        }
    }
}

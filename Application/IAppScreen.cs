using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppScreen
    {
        List<IAppScreen> AppScreens { get; }
        public string[] MenuItems { get; }
        public void Print();
        public IAppScreen NextScreen(int menuItemIndex);
        public void SetPreviousScreen(IAppScreen screen);
    }
}

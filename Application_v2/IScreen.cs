using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public interface IScreen //А может раз уже много характеристик у интерфейса - это должен быть абстрактный класс?
    {
        string? Title { get; }
        List<string>? ScreenBodyLines { get; }
        IReadOnlyList<MenuActions> Actions { get; }
        AppContext AppContext { get; }
        void UpdateScreenInfo();
    }
}

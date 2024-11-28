namespace Application_v2
{
    public interface IScreen
    {
        string? Title { get; }
        IReadOnlyList<MenuAction> Actions { get; }
        AppContext AppContext { get; }
    }
    public interface IDynamicScreen : IScreen
    {
        void UpdateScreenInfo();
    }
    public interface IBodyInfoScreen
    {
        IReadOnlyList<string> ScreenBodyLines { get; }
    }
    public interface IScreenInput : IScreen
    {
        new IReadOnlyList<MenuActionInput> Actions { get; }
    }
}
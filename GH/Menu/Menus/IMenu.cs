﻿
namespace GH.Menu.Menus
{
    using BlizzardApi.WidgetInterfaces;
    using Objects;

    public interface IMenu : IIndexer<object, object>
    {
        void SetValue(string id, object value);

        object GetValue(string id);

        IMenuObject GetFrameById(string id);

        void UpdatePosition();

        void AnimatedShow();
        void Show();

        void AddElement(int pageIndex, int lineIndex, IObjectProfile profile);
        void RemoveElement(string label);

        IFrame Frame { get; }
    }
}
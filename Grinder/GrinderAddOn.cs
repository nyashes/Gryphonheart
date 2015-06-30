﻿
namespace Grinder
{
    using CsLuaAttributes;
    using Lua;
    using Model.EntityAdaptor;
    using Model.EntityStorage;
    using View;

    [CsLuaAddOn("Grinder", "Grinder", 60200, 
        Author = "Pilus",
        Notes = "Tracking system for items and currencies.",
        SavedVariablesPerCharacter = new []{ EntityStorage.TrackedEntitiesSavedVariableName })]
    public class GrinderAddOn : ICsLuaAddOn
    {
        public void Execute()
        {
            var model = new Model.Model(new EntityAdaptorFactory(), new EntityStorage());
            var view = new View.View(new EntitySelectionDropdownHandler());
            var presenter = new Presenter.Presenter(model, view);

            Core.print("Grinder loaded.");
        }
    }
}

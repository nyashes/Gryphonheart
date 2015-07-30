﻿
namespace GHC
{
    using BlizzardApi.EventEnums;
    using CsLuaAttributes;
    using GH;
    using GH.Integration;
    using GH.Misc;
    using GH.Model;
    using GH.Model.Defaults;
    using Modules.AbilityActionBar;

    [CsLuaAddOn("GHC", "Gryphonheart Crime", 60200, Author = "The Gryphonheart Team", Dependencies = new []{"GH"})]
    public class GHCAddOn : ICsLuaAddOn
    {
        public void Execute()
        {
            Misc.RegisterEvent(SystemEvent.ADDON_LOADED, this.OnAddOnLoaded);

            DefaultQuickButtons.RegisterDefaultButton(new QuickButton(
                "ghcMain",
                6,
                true,
                "Gryphonheart Crime",
                "Interface/ICONS/Ability_Stealth",
                TempShowActionBar,
                AddOnReference.GHC));
        }

        private void OnAddOnLoaded(SystemEvent eventName, object addonName)
        {
            if (addonName.Equals("GHC"))
            {
                AddOnRegister.RegisterAddOn(AddOnReference.GHC);
            }
        }

        private void TempShowActionBar()
        {
            var bar = new ActionBar((frame) => new ActionButtonProxy(frame));
            bar.AddButton("test", "Interface/ICONS/INV_Misc_Bag_11", s => { }, (s, tooltip) => { });
            bar.Show();
        }
    }
}
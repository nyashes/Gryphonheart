﻿namespace GHF.Model
{
    using BlizzardApi.EventEnums;
    using BlizzardApi.Global;
    using BlizzardApi.MiscEnums;
    using CsLua.Collection;
    using GH.Integration;
    using GH.Misc;
    using GH.Model;
    using GH.ObjectHandling;
    using GH.ObjectHandling.Storage;
    using GH.ObjectHandling.Subscription;
    using Lua;
    using MSP;
    using Presenter;

    public class ModelProvider : IModelProvider
    {
        public const string SavedAccountProfiles = "GHF_AccountProfiles";

        public IAddOnIntegration Integration { get; private set; }
        
        public IObjectStore<Profile, string> AccountProfiles { get; private set; }

        private ISubscriptionCenter<Profile, string> subscriptionCenter;
        private MSPProxy msp;


        public ModelProvider()
        {
            var formatter = new TableFormatter<Profile>();
            this.subscriptionCenter = new SubscriptionCenter<Profile, string>();
            this.AccountProfiles = new ObjectStore<Profile, string>(formatter, new SavedDataHandler(SavedAccountProfiles, Global.Api.GetRealmName()), this.subscriptionCenter);

            Misc.RegisterEvent(SystemEvent.VARIABLES_LOADED, this.OnVariablesLoaded);
            this.Integration = (IAddOnIntegration)Global.Api.GetGlobal(AddOnIntegration.GlobalReference);
            this.Integration.RegisterAddOn(AddOnReference.GHF);

            var playerName = Global.Api.UnitName(UnitId.player);
            this.msp = new MSPProxy(new ProfileFormatter());
            this.subscriptionCenter.SubscribeForUpdates(this.msp.Set, profile => profile.Id.Equals(playerName));

            new Presenter(this);
        }

        private void SetPlayerProfileIfMissing()
        {
            var playerName = Global.Api.UnitName(UnitId.player);
            var className = Global.Api.UnitClass(UnitId.player).Value2;
            var gameRace = Global.Api.UnitRace(UnitId.player).Value2;
            var sex = Strings.tostring(Global.Api.UnitSex(UnitId.player));
            var guid = Global.Api.UnitGUID(UnitId.player);

            var ownProfile = this.AccountProfiles.Get(playerName);
            if (ownProfile != null)
            {
                return;
            }

            var profile = new Profile(playerName, className, gameRace, sex, guid);

            this.AccountProfiles.Set(playerName, profile);
        }

        private void OnVariablesLoaded(SystemEvent eventName, object _)
        {
            this.AccountProfiles.LoadFromSaved();
            this.SetPlayerProfileIfMissing();
        }
    }
}
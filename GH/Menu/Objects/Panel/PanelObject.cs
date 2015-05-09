﻿
namespace GH.Menu.Objects.Panel
{
    using System;
    using BlizzardApi;
    using BlizzardApi.WidgetEnums;
    using BlizzardApi.WidgetInterfaces;
    using CsLua;
    using Line;
    using Lua;
    using Page;

    public class PanelObject : BaseObject, IMenuContainer
    {
        private readonly PanelProfile profile;
        private IPage innerPage;
        private readonly IFrame parentFrame;
        private readonly LayoutSettings settings;
        private const double BorderSize = 0;
        private const double ExtraTopSize = 0;
        private readonly string name;

        public PanelObject(PanelProfile profile, IMenuContainer parent, LayoutSettings settings) : base(profile, parent, settings)
        {
            this.profile = profile;
            this.parentFrame = parent.Frame;
            this.settings = settings;
            this.name = UniqueName(Type);
            this.CreateFrame();
        }

        public static PanelObject Initialize(IObjectProfile profile, IMenuContainer parent, LayoutSettings settings)
        {
            return new PanelObject((PanelProfile) profile, parent, settings);
        }

        public static string Type = "Panel";

        public override void SetPosition(double xOff, double yOff, double width, double height)
        {
            this.Frame.SetHeight(height);
            this.Frame.SetWidth(width);
            this.Frame.SetPoint(FramePoint.TOPLEFT, this.parentFrame, FramePoint.TOPLEFT, xOff, -0);
            if (this.innerPage != null)
            { 
                this.innerPage.SetPosition(
                    BorderSize,
                    BorderSize + ExtraTopSize,
                    width - (BorderSize * 2),
                    height - (BorderSize * 2 + ExtraTopSize));
            }
        }

        public override double? GetPreferredHeight()
        {
            if (this.innerPage == null)
            {
                return null;
            }

            var height = this.innerPage.GetPreferredHeight();
            if (height != null)
            {
                return height + BorderSize * 2 + ExtraTopSize;
            }
            return null;
        }

        public override double? GetPreferredWidth()
        {
            if (this.innerPage == null)
            {
                return null;
            }

            var width = this.innerPage.GetPreferredWidth();
            if (width != null)
            {
                return width + BorderSize * 2;
            }
            return null;
        }

        public override object GetValue()
        {
            throw new CsException("Cannot get value on a panel object.");
        }

        public override void SetValue(object value)
        {
            throw new CsException("Cannot force value on a panel object.");
        }

        private void CreateFrame()
        {
            this.Frame = FrameUtil.FrameProvider.CreateFrame(FrameType.Frame, this.name, this.parentFrame) as IFrame;
            this.Frame.SetFrameLevel(this.parentFrame.GetFrameLevel() + 1);
            
            if (this.profile.Count > 0)
            { 
                this.innerPage = new Page(this.profile, this.Frame, this.settings, 1);
            }
        }

        public override IMenuObject GetFrameById(string id)
        {
            return this.innerPage == null ? null : this.innerPage.GetFrameById(id);
        }

        public void AddElement(int lineIndex, IObjectProfile profile)
        {
            if (this.innerPage == null)
            {
                this.innerPage = new Page(new PageProfile() { new LineProfile() { profile }}, this.Frame, this.settings, 1);
            }

            this.innerPage.AddElement(lineIndex, profile);
        }

        public void RemoveElement(string label)
        {
            if (this.innerPage == null)
            {
                return;
            }
            this.innerPage.RemoveElement(label);
        }
    }
}
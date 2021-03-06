﻿

namespace GHD.Presenter.Document
{
    using BlizzardApi.Global;
    using BlizzardApi.WidgetEnums;
    using BlizzardApi.WidgetInterfaces;
    using GH.Menu;
    using GH.Menu.Containers.Menus;
    using GHD.Document;
    using GHD.Document.Elements;
    using GHD.Document.KeyboardInput;
    using View.DocumentMenu;
    using View.DocumentMenu.ToolbarCatagories;

    // /script GHD.Presenter.DocumentMenu.DocumentMenu().Test();

    public class DocumentMenu
    {
        private readonly IMenu menu;
        private IFrame documentContainer;
        private Document document;
        private readonly IKeyboardInputProvider inputProvider;
        private readonly ICursor cursor;

        public DocumentMenu()
        {
            var metaCatagory = new MetaCatagoryProfileGenerator(this.Undo, this.Redo, this.Revert, this.Save);
            var profileGenerator = new DocumentMenuProfileGenerator(metaCatagory);

            this.inputProvider = new TextBoxInputInterpreter();
            this.cursor = new Cursor();

            //this.menu = BaseMenu.CreateMenu(profileGenerator.GenerateMenuProfile());
            this.SetupDocumentArea(this.menu.GetFrameById(DocumentMenuLabels.DocumentArea) as IFrame);
        }

        public void Test()
        {
            this.menu.AnimatedShow();
            this.document = new Document(this.inputProvider, this.cursor);
            this.document.Region.SetParent(this.documentContainer);
            this.document.Region.SetAllPoints(this.documentContainer);
            this.inputProvider.Start();
        }

        private void SetupDocumentArea(IFrame documentAreaFrame)
        {
            var scrollFrame = (IGHM_ScrollFrameTemplate)Global.FrameProvider.CreateFrame(FrameType.ScrollFrame, this.menu.Frame.GetName() + "Scroll", documentAreaFrame,
                "GHM_ScrollFrameTemplate");
            var border = 10;
            scrollFrame.SetParent(documentAreaFrame);
            scrollFrame.SetPoint(FramePoint.TOP, 0, 3);
            scrollFrame.SetPoint(FramePoint.BOTTOM, 0, border - 3);
            scrollFrame.SetPoint(FramePoint.LEFT, -1 - border, 0);
            scrollFrame.SetPoint(FramePoint.RIGHT, -13 + border, 0);
            scrollFrame.ShowScrollBarBackgrounds();

            this.documentContainer = (IFrame)Global.FrameProvider.CreateFrame(FrameType.Frame);
            this.documentContainer.SetParent(scrollFrame);
            scrollFrame.SetScrollChild(this.documentContainer);
            this.documentContainer.SetPoint(FramePoint.TOPLEFT, scrollFrame, FramePoint.TOPLEFT);

            this.documentContainer.SetWidth(800);
            this.documentContainer.SetHeight(1000);
        }

        private void Save()
        {
            
        }

        private void Revert()
        {
            
        }

        private void Undo()
        {
            
        }

        private void Redo()
        {
            
        }
    }
}

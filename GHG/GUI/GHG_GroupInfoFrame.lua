--===================================================
--
--				GHG_GroupInfoFrame
--  			GHG_GroupInfoFrame.lua
--
--	          (description)
--
-- 	  (c)2013 The Gryphonheart Team
--			All rights reserved
--===================================================
local api;
GHI_Event("VARIABLES_LOADED",function()
	api = GHG_GroupAPI(UnitGUID("player"))
end);
local loc = GHG_Loc();


local GetGroupIndex = function()
	return GHG_GroupInfoFrame:GetParent():GetParent().selectedSideTab
end

local GroupInvite = function(name)
	api.InvitePlayerToGroup(GetGroupIndex(),name);
end

function GHG_UpdateGroupInfo()

end

StaticPopupDialogs["GHG_ADD_GROUP_MEMBER"] = {
	text = loc.ADD_MEMBER_TEXT,
	button1 = OKAY,
	button2 = CANCEL,
	hasEditBox = 1,
	autoCompleteParams = AUTOCOMPLETE_LIST.GUILD_INVITE,
	maxLetters = 12,
	OnAccept = function(self)
		GroupInvite(self.editBox:GetText());
	end,
	OnShow = function(self)
		self.editBox:SetFocus();
	end,
	OnHide = function(self)
		ChatEdit_FocusActiveWindow();
		self.editBox:SetText("");
	end,
	EditBoxOnEnterPressed = function(self)
		local parent = self:GetParent();
		GroupInvite(parent.editBox:GetText());
		parent:Hide();
	end,
	EditBoxOnEscapePressed = function(self)
		self:GetParent():Hide();
	end,
	timeout = 0,
	exclusive = 1,
	whileDead = 1,
	hideOnEscape = 1
};

StaticPopupDialogs["GHG_GROUP_INVITE"] = {
	text = "",
	button1 = ACCEPT,
	button2 = CANCEL,
	OnAccept = function(self)
		api.AcceptGroupInvitation();
	end,
	OnCancel = function(self)
		api.DeclineGroupInvitation();
	end,
	timeout = 0,
	exclusive = 1,
	whileDead = 1,
	hideOnEscape = 1
};
GHI_Event("GHG_INVITE_RECEIVED",function(_,groupName,playerName)
	StaticPopupDialogs["GHG_GROUP_INVITE"].text = string.format(loc.INVITATION_TEXT,groupName,playerName);
	if ( StaticPopup_FindVisible("GHG_GROUP_INVITE") ) then
		StaticPopup_Hide("GHG_GROUP_INVITE");
	else
		StaticPopup_Show("GHG_GROUP_INVITE");
	end

end);

local currentLeave;
StaticPopupDialogs["GHG_CONFIRM_GROUP_LEAVE"] = {
	text = "",
	button1 = ACCEPT,
	button2 = CANCEL,
	OnAccept = function(self)
		api.LeaveGroup(currentLeave);
	end,
	OnShow = function(self)
		currentLeave = GetGroupIndex();
		local groupName = api.GetGroupInfo(currentLeave);
		self.text:SetFormattedText(loc.LEAVE_CONFIRM, groupName);
	end,
	timeout = 0,
	whileDead = 1,
	hideOnEscape = 1
};


﻿--
--
--				GHM_ButtonWithIcon
--  			GHM_ButtonWithIcon.lua
--
--	          (description)
--
-- 	  (c)2013 The Gryphonheart Team
--			All rights reserved
--

local count = 1;
function GHM_ButtonWithIcon(profile, parent, settings)
	local frame = CreateFrame("Button","GHM_ButtonWithIcon" .. count, parent);
	count = count + 1;

	frame:SetWidth(32);
	frame:SetHeight(32);

	frame:SetNormalTexture(profile.path);

	frame:SetPushedTexture(profile.path);
	frame:GetPushedTexture():SetVertexColor(0.5,0.5,0.5,1);

	frame:SetBackdrop({bgFile = "",
	edgeFile = "Interface/Tooltips/UI-Tooltip-Border",
	tile = false, tileSize = 16, edgeSize = 16,
	insets = { left = 4, right = 0, top = 4, bottom = 4 }});


	-- Click handler
	if profile.onClick then
		frame:SetScript("OnClick",profile.onClick)
	elseif profile.OnClick then
		frame:SetScript("OnClick",profile.OnClick)
	end

	-- Tooltip
	frame:SetScript("OnEnter", function()
		if profile.tooltip then
			GameTooltip:SetOwner(frame, "ANCHOR_RIGHT");
			GameTooltip:ClearLines()
			GameTooltip:AddLine(profile.tooltip, 1, 0.8196079, 0);
			GameTooltip:Show()
		end
	end);
	frame:SetScript("OnLeave", function(self)
		GameTooltip:Hide();
	end);

	return frame;
end


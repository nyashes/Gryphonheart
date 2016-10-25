﻿--
--
--				GHI_CreateItemAPI
--				GHI_CreateItemAPI.lua
--
	--	API for creation of items
--
-- 		(c)2013 The Gryphonheart Team
--			All rights reserved
--

local class;

function GHI_GetSimpleActionFromArgs(actionType,...)
	local args = {...};
	local info = {
		icon = "",
		details = "Action generated by script",
		type_name = actionType,
	};
	actionType = actionType:lower();

	if actionType == "script" then -- "script", code, delay
		local code,delay = unpack(args);
		info.Type = "script";
		info.code = {code }
		info.delay = delay;
	elseif actionType == "buff" then -- "buff", buffName, buffDetails, buffIcon, untilCanceled, filter, buffType, buffDuration, cancelable, stackable, count, delay, range, alwaysCastOnSelf
		local buffName, buffDetails, buffIcon, untilCanceled, filter, buffType, buffDuration, cancelable, stackable, count, delay, range, alwaysCastOnSelf = unpack(args);
		info.Type = "buff";
		info.buffName = buffName;
		info.buffDetails = buffDetails;
		info.buffIcon = buffIcon;
		info.untillCanceled = untilCanceled;
		info.filter = filter;
		info.buffType = buffType;
		info.buffDuration = buffDuration;
		info.cancelable = cancelable;
		info.stackable = stackable;
		info.count = count;
		info.delay = delay;
		info.range = range;
		info.alwaysCastOnSelf = alwaysCastOnSelf;
	elseif actionType == "equip_item" or actionType == "equipitem" then   -- "equip_item", name, delay
		local name,delay = unpack(args);
		info.Type = "equip_item";
		info.item_name = name;
		info.delay = delay;
	elseif actionType == "expression" then -- "expression", expression_type, text, delay
		local expression_type, text, delay = unpack(args);
		info.Type = "expression";
		info.expression_type = expression_type;
		info.text = text;
		info.delay = delay;
	elseif actionType == "random_expression" or actionType == "randomexpression" then  -- "random_expression", allow_same, expressionType1, expressionText1, expressionType2, expressionText2, etc...
		info.Type = "random_expression";
		info.allow_same = args[1];
		local i = 2;
		info.text = {};
		info.expression_type = {};
		while (args[i] and args[i+1]) do
			info.expression_type[i/2] = args[i];
			info.text[i/2] = args[i+1];
			i = i + 2;
		end
	elseif actionType == "bag" then -- "bag", size, texture
		local size, texture = unpack(args);
		info.Type = "bag";
		info.size = size;
		info.texture = texture;
	elseif actionType == "book" then -- "book", title, pages, material, font, normalSize, h1Size, h2Size
		local title, pages, material, font, normalSize, h1Size, h2Size = unpack(args);
		info.Type = "book";
		info.title = title;
		info.material = material;
		info.font = font;
		info.n = normalSize;
		info.h1 = h1Size;
		info.h2 = h2Size;
		for i=1,#(pages) do
			info[i] = pages[i];
		end
	elseif actionType == "sound" then -- "sound", path, delay, range
		local path, delay, range = unpack(args);
		info.Type = "sound";
		info.sound_path = path;
		info.delay = delay;
		info.range = range;
	elseif actionType == "produce_item" or actionType == "produceitem" then -- "produce_item", guid, amount, lootText, delay
		local guid, amount, lootText, delay = unpack(args);
		info.dynamic_rc_type = "produce_item";
		info.Type = "script";
		info.dynamic_rc = true;
		info.guid = guid;
		info.amount = amount;
		info.loot_text = lootText;
		info.delay = delay;
	elseif actionType == "message" then -- "message", text, color, outputType, delay          note: color is a string ('red' etc)
		local text, color, outputType, delay  = unpack(args);
		info.dynamic_rc_type = "message";
		info.Type = "script";
		info.dynamic_rc = true;
		info.text = text;
		info.color = color;
		info.output_type = outputType
		info.delay = delay;
	elseif actionType == "screen_effect" or actionType == "screeneffect" then -- "screen_effect", fadeIn, fadeOut, duration, color, delay  		note: color is a color table ({r=0,g=0,b=1})
		local fadeIn, fadeOut, duration, color, delay = unpack(args);
		info.dynamic_rc_type = "screen_effect";
		info.Type = "script";
		info.dynamic_rc = true;
		info.fade_in = fadeIn;
		info.fade_out = fadeOut;
		info.duration = duration;
		info.color = color;
		info.delay = delay;
	elseif actionType == "remove_buff"  or actionType == "removebuff" then -- "remove_buff", name, filter, amount, delay
		local name, filter, amount, delay = unpack(args);
		info.dynamic_rc_type = "remove_buff";
		info.Type = "script";
		info.dynamic_rc = true;
		info.name = name;
		info.filter = filter;
		info.amount = amount;
		info.delay = delay;
	elseif actionType == "consume_item" or actionType == "consumeitem" then -- "consume_item", guid, amount, delay
		local guid, amount, delay = unpack(args);
		info.dynamic_rc_type = "consume_item";
		info.Type = "script";
		info.dynamic_rc = true;
		info.id = guid;
		info.amount = info.amount;
		info.delay = info.delay;
	else
		info.Type = "script";
		info.code = {string.format("print('Unknown action type',%s)",actionType)}
	end
	return info
end
	
function GHI_CreateItemAPI()
	if class then
		return class;
	end
	class = GHClass("GHI_CreateItemAPI");
	local itemList = GHI_ItemInfoList();
	local containerList = GHI_ContainerList();

	local OneStringGuard = function(f)
		return function(str,...)
			assert(type(str)=="string","First argument must be a string. Got "..type(str));
			return f(str,...);
		end
	end

	local GetItemAPI = function(item)
		local api = {};

		-- Access api
		api.IsEditable = item.IsEditable;
		api.IsCopyable = item.IsCopyable;
		api.SetCopyable = item.SetCopyable;
		api.SetEditable = item.SetEditable;
		api.GetAuthorInfo = item.GetAuthorInfo;
		
		-- Basic
		api.DisplayItemTooltip = item.DisplayItemTooltip;
		api.GetFlavorText = item.GetFlavorText;
		api.GetGUID = item.GetGUID;
		api.GetItemInfo = item.GetItemInfo;
		api.IsConsumed = item.IsConsumed;
		api.SetConsumed = item.SetConsumed;
		api.SetComment = OneStringGuard(item.SetComment);
		api.SetIcon = OneStringGuard(item.SetIcon);
		api.SetName = OneStringGuard(item.SetName);
		api.SetQuality = item.SetQuality;
		api.SetUseText = OneStringGuard(item.SetUseText);
		api.SetStackSize = item.SetStackSize;
		api.SetWhite1 = OneStringGuard(item.SetWhite1);
		api.SetWhite2 = OneStringGuard(item.SetWhite2);

		-- Cooldown
		api.GetCooldown = item.GetCooldown;
		api.GetLastCastTime = item.GetLastCastTime;
		api.IsOnCooldown = item.IsOnCooldown;
		api.SetCooldown = item.SetCooldown;
		api.SetLastCastTime = item.SetLastCastTime;

		-- Version
		api.GetVersions = item.GetVersions;
		api.IncreaseVersion = item.IncreaseVersion;
		api.GetItemComplexity = item.GetItemComplexity;

		-- Standard
		api.AddSimpleAction = function(actionInfo,...)
			if (type(actionInfo)=="table") then
				item.AddSimpleAction(GHI_SimpleAction(actionInfo));
			elseif (type(actionInfo)=="string") then
				item.AddSimpleAction(GHI_SimpleAction(GHI_GetSimpleActionFromArgs(actionInfo,...)));
			else
				error("Usage: item.AddSimpleAction(table) or  item.AddSimpleAction(string,...)",2);
			end
		end;
		
		api.GetSimpleAction = function(index)
			local action = item.GetSimpleAction(index);
			if action then
				return action.GetInfo();
			end
		end;
		api.GetSimpleActionCount = item.GetSimpleActionCount;
		api.RemoveSimpleAction = function(index)
			item.RemoveSimpleAction(item.GetSimpleAction(index));
		end;

		return api;
	end

	local EditItem = function(env, guid)
		if not(type(guid) == "string") then
			error("Usage: GHI_EditItem(guid)",2);
		end

		local item = itemList.GetItemInfo(guid);
		if not(item) then
			error("GHI_EditItem: Item not found.",2);
		end

		local authorGuid = item.GetAuthorInfo();
		local ownerGuid = env.GetOwner();
		if not(item.IsEditable() or authorGuid == ownerGuid) then
			error("GHI_EditItem: Access to item denied",2);
		end

		local api = GetItemAPI(item);
		api.Save = function()
			item.IncreaseVersion();
			itemList.UpdateItem(item);
		end
		return api;
	end

	local NewItem = function(env)
		local ownerGuid = env.GetOwner();
		local item = GHI_ItemInfo({
			authorGuid = ownerGuid,
			authorName = "GHI Script",
		});

		local api = GetItemAPI(item);
		api.Save = function(targetContainerGuid)
			itemList.UpdateItem(item);
			-- targetContainerGuid
			if not(targetContainerGuid) then
				containerList.InsertItemInMainBag(item.GetGUID());
			else
			 	containerList.InsertItemInBag(item.GetGUID(), nil, targetContainerGuid);
			end
		end
		return api;
	end

	class.GetAPI = function(env)
		local a = {};
		a.GHI_EditItem = function(guid)
			return EditItem(env, guid);
		end;
		a.GHI_CreateItem = function()
			return NewItem(env);
		end;
		return a;
	end

	return class;
end


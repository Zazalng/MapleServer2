﻿using MapleServer2.Commands.Core;
using MapleServer2.Data.Static;
using MapleServer2.Tools;
using MapleServer2.Types;

namespace MapleServer2.Commands.Game
{
    public class ItemCommand : InGameCommand
    {
        public ItemCommand()
        {
            Aliases = new[]
            {
                "item"
            };
            Description = "Give an item to the current player.";
            AddParameter("id", "Item id", 20000027);
            AddParameter("amount", "Amount of the same item.", 1);
            AddParameter<int>("rarity", "Item rarity.");
        }

        public override void Execute(GameCommandTrigger trigger)
        {
            int itemId = trigger.Get<int>("id");
            int rarity = trigger.Get<int>("rarity");
            int amount = trigger.Get<int>("amount");

            if (!ItemMetadataStorage.IsValid(itemId))
            {
                trigger.Session.SendNotice("Invalid item: " + itemId);
                return;
            }

            Item item = new Item(itemId)
            {
                Rarity = rarity >= 0 ? rarity : ItemMetadataStorage.GetRarity(itemId),
                Amount = amount
            };
            item.Stats = new ItemStats(item);

            // Simulate looting item
            InventoryController.Add(trigger.Session, item, true);
        }
    }
}
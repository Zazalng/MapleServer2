﻿using Maple2Storage.Types;
using MapleServer2.Commands.Core;
using MapleServer2.Data.Static;
using MapleServer2.Types;
using Color = System.Drawing.Color;

namespace MapleServer2.Commands.Game
{
    public class NpcCommand : InGameCommand
    {
        public NpcCommand()
        {
            Aliases = new[]
            {
                "npc"
            };
            Description = "Spawn a NPC from id.";
            AddParameter("id", "The id of the NPC.", 11003146);
            AddParameter("ani", "The animation of the NPC.", 1);
            AddParameter("dir", "The rotation of the NPC.", 2700);
            AddParameter("coord", "The position of the NPC.", CoordF.From(0, 0, 0));
        }

        public override void Execute(GameCommandTrigger trigger)
        {
            int npcId = trigger.Get<int>("id");

            if (NpcMetadataStorage.GetNpc(npcId) == null)
            {
                trigger.Session.SendNotice($"No NPC was found with the id: {CommandHelpers.Color(npcId, Color.DarkOliveGreen)}");
                return;
            }
            Npc npc = new Npc(npcId)
            {
                Animation = trigger.Get<short>("ani"),
                ZRotation = trigger.Get<short>("dir")
            };

            IFieldObject<Npc> fieldNpc = trigger.Session.FieldManager.RequestFieldObject(npc);
            CoordF coord = trigger.Get<CoordF>("coord");

            if (coord == default)
            {
                fieldNpc.Coord = trigger.Session.FieldPlayer.Coord;
            }
            else
            {
                fieldNpc.Coord = coord;
            }
            trigger.Session.FieldManager.AddNpc(fieldNpc);
        }
    }

    public class MobCommand : InGameCommand
    {
        public MobCommand()
        {
            Aliases = new[]
            {
                "mob"
            };
            Description = "Spawn a MOB from id.";
            AddParameter("id", "The id of the MOB.", 21000001);
            AddParameter("ani", "The animation of the MOB.", 1);
            AddParameter("dir", "The rotation of the MOB.", 2700);
            AddParameter("coord", "The position of the MOB.", CoordF.From(0, 0, 0));
        }

        public override void Execute(GameCommandTrigger trigger)
        {
            int mobId = trigger.Get<int>("id");

            if (NpcMetadataStorage.GetNpc(mobId) == null)
            {
                trigger.Session.SendNotice($"No MOB was found with the id: {CommandHelpers.Color(mobId, Color.DarkOliveGreen)}");
                return;
            }
            Mob mob = new Mob(mobId)
            {
                Animation = trigger.Get<short>("ani"),
                ZRotation = trigger.Get<short>("dir")
            };

            IFieldObject<Mob> fieldMob = trigger.Session.FieldManager.RequestFieldObject(mob);
            CoordF coord = trigger.Get<CoordF>("coord");

            if (coord == default)
            {
                fieldMob.Coord = trigger.Session.FieldPlayer.Coord;
            }
            else
            {
                fieldMob.Coord = coord;
            }
            trigger.Session.FieldManager.AddMob(fieldMob);
        }
    }
}
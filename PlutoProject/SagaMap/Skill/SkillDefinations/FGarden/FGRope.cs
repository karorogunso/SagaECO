using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;

namespace SagaMap.Skill.SkillDefinations.FGarden
{
    public class FGRope : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.FGarden != null)
                {
                    if (pc.FGarden.RopeActor == null)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
                        if (map.Info.Flag.Test(SagaDB.Map.MapFlags.FGarden))
                            createNewRope(args, pc);
                        else
                            MapClient.FromActorPC(pc).SendSystemMessage(Manager.LocalManager.Instance.Strings.FG_CANNOT);
                    }
                    else
                    {
                        if (!Manager.ScriptManager.Instance.Events.ContainsKey(pc.FGarden.RopeActor.EventID))
                        {
                            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
                            if (map.Info.Flag.Test(SagaDB.Map.MapFlags.FGarden))
                            {
                                map = Manager.MapManager.Instance.GetMap(pc.FGarden.RopeActor.MapID);
                                map.DeleteActor(pc.FGarden.RopeActor);
                                createNewRope(args, pc);
                            }
                            else
                                MapClient.FromActorPC(pc).SendSystemMessage(Manager.LocalManager.Instance.Strings.FG_CANNOT);
                        }
                        else
                            MapClient.FromActorPC(pc).SendSystemMessage(Manager.LocalManager.Instance.Strings.FG_ALREADY_CALLED);
                    }

                }
                else
                {
                    MapClient.FromActorPC(pc).SendSystemMessage(Manager.LocalManager.Instance.Strings.FG_NOT_FOUND);
                }
            }
        }

        void createNewRope(SkillArg args, ActorPC pc)
        {
            ActorEvent actor = new ActorEvent(pc);
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);

            actor.MapID = pc.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            uint eventID = Manager.ScriptManager.Instance.GetFreeIDSince(0xF0000100, 1000);
            actor.EventID = eventID;
            pc.FGarden.RopeActor = actor;
            if (Manager.ScriptManager.Instance.Events.ContainsKey(0xF0000100))
            {
                Scripting.EventActor pattern = (Scripting.EventActor)Manager.ScriptManager.Instance.Events[0xF0000100];
                Scripting.EventActor newEvent = pattern.Clone();
                newEvent.Actor = actor;
                newEvent.EventID = actor.EventID;
                Manager.ScriptManager.Instance.Events.Add(newEvent.EventID, newEvent);
            }
            actor.Type = ActorEventTypes.ROPE;
            actor.Title = string.Format(Manager.LocalManager.Instance.Strings.FG_NAME, pc.Name);
            actor.e = new ActorEventHandlers.ItemEventHandler(actor);
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
        }

        #endregion
    }
}

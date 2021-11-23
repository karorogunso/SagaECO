using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Ring;
using SagaDB.Actor;

using SagaMap.Network.Client;

namespace SagaMap.Manager
{
    public class RingManager:Singleton<RingManager>
    {
        Dictionary<uint, Ring> rings = new Dictionary<uint, Ring>();
        public RingManager()
        {

        }

        public Ring GetRing(uint pattern)
        {
            Ring res;
            if (pattern == 0) return null;
            if (rings.ContainsKey(pattern))
                res = rings[pattern];
            else
            {
                res = new Ring();
                res.ID = pattern;
                rings.Add(pattern, res);
                if (res.Name == null)
                {
                    res = MapServer.charDB.GetRing(res.ID);
                    if (res == null)
                    {
                        Logger.ShowDebug("Ring with ID:" + pattern + " not found!", Logger.defaultlogger);
                        rings.Remove(pattern);
                        return null;
                    }
                    rings[res.ID] = res;
                }
                int[] index = res.Members.Keys.ToArray();
                foreach (int i in index)
                {
                    MapClient client = MapClientManager.Instance.FindClient(res.Members[i]);
                    if (client != null)
                    {
                        res.Members[i] = client.Character;
                        if (res.Leader.CharID == client.Character.CharID)
                            res.Leader = client.Character;
                    }
                }
            }
            return res;
        }

        public Ring GetRing(Ring pattern)
        {
            Ring res;
            if (pattern == null) return null;
            if (pattern.ID == 0) return null;
            if (rings.ContainsKey(pattern.ID))
                res = rings[pattern.ID];
            else
            {
                res = pattern;
                rings.Add(pattern.ID, pattern);
                if (res.Name == null)
                {
                    res = MapServer.charDB.GetRing(res.ID);
                    if (res == null)
                    {
                        Logger.ShowDebug("Ring with ID:" + pattern.ID + " not found!", Logger.defaultlogger);
                        rings.Remove(pattern.ID);
                        return null;
                    }
                    rings[res.ID] = res;
                }
                else
                {
                    res = pattern;
                }
                int[] index = res.Members.Keys.ToArray();
                foreach (int i in index)
                {
                    MapClient client = MapClientManager.Instance.FindClient(res.Members[i]);
                    if (client != null)
                    {
                        res.Members[i] = client.Character;
                        if (res.Leader.CharID == client.Character.CharID)
                            res.Leader = client.Character;
                    }
                }
            }
            return res;
        }

        public void PlayerOnline(Ring ring, ActorPC pc)
        {
            if (ring == null)
                return;
            if (!ring.IsMember(pc))
            {
                pc.Ring = null;
                return;
            }
            ring.MemberOnline(pc);
            ActorPC[] list = ring.Members.Values.ToArray();
            foreach (ActorPC i in list)
            {
                if (i == pc || !i.Online) continue;
                MapClient.FromActorPC(i).SendRingMemberInfo(pc);
            }
        }

        public void PlayerOffline(Ring ring, ActorPC pc)
        {
            if (ring == null)
                return;
            pc.Online = false;
            ActorPC[] list = ring.Members.Values.ToArray();
            foreach (ActorPC i in list)
            {
                if (i == pc || !i.Online) continue;
                MapClient.FromActorPC(i).SendRingMemberState(pc);
            }
        }

        public void UpdateRingInfo(Ring ring, Packets.Server.SSMG_RING_INFO.Reason reason)
        {
            if (ring == null)
                return;
            ActorPC[] list = ring.Members.Values.ToArray();
            foreach (ActorPC i in list)
            {
                if (!i.Online) continue;
                MapClient.FromActorPC(i).SendRingInfo(reason);
            }
            if (reason == SagaMap.Packets.Server.SSMG_RING_INFO.Reason.UPDATED)
                MapServer.charDB.SaveRing(ring, false);
        }

        public void RingChat(Ring ring, ActorPC pc, string content)
        {
            if (ring == null)
                return;
            ActorPC[] list = ring.Members.Values.ToArray();
            foreach (ActorPC i in list)
            {
                if (!i.Online) continue;
                MapClient.FromActorPC(i).SendChatRing(pc.Name, content);
            }
        }

        public Ring CreateRing(ActorPC pc, string name)
        {
            Ring ring = new Ring();
            ring.Name = name;
            MapServer.charDB.NewRing(ring);
            if (ring.ID != 0xFFFFFFFF)
            {
                rings.Add(ring.ID, ring);
                ring.Leader = pc;
                AddMember(ring, pc);
                pc.Ring = ring;
                return ring;
            }
            else
                return null;
        }

        public void AddMember(Ring ring, ActorPC pc)
        {
            if (ring == null)
                return;
            if (ring.IsMember(pc))
                return;
            int index = ring.NewMember(pc);
            pc.Ring = ring;
            if (pc.Online)
                MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.RING_NAME_UPDATE, null, pc, true);
            if (ring.MemberCount > 1)
            {
                UpdateRingInfo(ring, SagaMap.Packets.Server.SSMG_RING_INFO.Reason.JOIN);
            }
            else
            {
                ring.Rights[index].SetValue(RingRight.RingMaster, true);
                ring.Rights[index].SetValue(RingRight.AddRight, true);
                ring.Rights[index].SetValue(RingRight.KickRight, true);
                ring.Rights[index].SetValue(RingRight.FFRight, true);
                UpdateRingInfo(ring, SagaMap.Packets.Server.SSMG_RING_INFO.Reason.CREATE);
            }
            MapServer.charDB.SaveRing(ring, true);            
        }

        public void DeleteMember(Ring ring, ActorPC pc, Packets.Server.SSMG_RING_QUIT.Reasons reason)
        {
            if (ring == null)
                return;
            if (!ring.IsMember(pc))
                return;
            ActorPC[] list = ring.Members.Values.ToArray();
            foreach (ActorPC i in list)
            {
                if (!i.Online) continue;
                if (i.CharID == pc.CharID)
                {                    
                    if (i.Online)
                    {
                        MapClient.FromActorPC(i).SendRingMeDelete(reason);
                        i.Ring = null;                    
                        MapClient.FromActorPC(i).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.RING_NAME_UPDATE, null, i, false);
                    }
                    i.Ring = null;                    
                }
                else
                {
                    MapClient.FromActorPC(i).SendRingMemberDelete(pc);
                }
            }
            ring.DeleteMemeber(pc);
            MapServer.charDB.SaveRing(ring, true);            
        }

        public void RingDismiss(Ring ring)
        {
            if (ring == null)
                return;
            ActorPC[] list = ring.Members.Values.ToArray();
            foreach (ActorPC i in list)
            {
                if (!i.Online) continue;
                MapClient.FromActorPC(i).SendRingMeDelete(SagaMap.Packets.Server.SSMG_RING_QUIT.Reasons.DISSOLVE);
                i.Ring = null;
                MapClient.FromActorPC(i).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.RING_NAME_UPDATE, null, i, false);
            }
            MapServer.charDB.DeleteRing(ring);
            if (rings.ContainsKey(ring.ID))
                rings.Remove(ring.ID);
        }

        public void SetMemberRight(Ring ring, uint pc, int value)
        {
            if (ring == null)
                return;
            if (!ring.IsMember(pc))
                return;
            ring.Rights[ring.IndexOf(pc)].Value = value;
            ActorPC[] list = ring.Members.Values.ToArray();
            foreach (ActorPC i in list)
            {                
                Packets.Server.SSMG_RING_RIGHT_UPDATE p = new SagaMap.Packets.Server.SSMG_RING_RIGHT_UPDATE();
                p.CharID = pc;
                p.Right = ring.Rights[ring.IndexOf(pc)].Value;
                if (i.Online)
                    MapClient.FromActorPC(i).netIO.SendPacket(p);
            }
        }
    }
}

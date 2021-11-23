using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaMap;
using SagaMap.Manager;
using SagaLib;

namespace SagaScript
{
    public class 帐篷 : EventActor
    {
        int hitCount = 0;
        uint playerCount = 0;

        public 帐篷()
        {
            this.EventID = 0xF0001233;
        }

        public override void OnEvent(ActorPC pc)
        {
            string input;


            if (pc == this.Actor.Caster)
            {
                switch (Select(pc, string.Format("{0}的 帐篷 Hit : {1}", this.Actor.Caster.Name, hitCount), "", "进入帐篷", "收拾帐篷", "什么也不做"))
                {
                    case 1:
                        

                        if (this.Actor.TentMapID == 0)
                        {
                            Map map = MapManager.Instance.GetMap(pc.MapID);
                            this.Actor.TentMapID = MapManager.Instance.CreateMapInstance(this.Actor.Caster, 40000000, pc.MapID, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height));
                            
                        }

                        Map tentMap = MapManager.Instance.GetMap(this.Actor.TentMapID);
                        if(tentMap.Actors.Count >= 5)
                        {
                            Say(pc, 0, "帳蓬已擠滿人了。");
                            return;
                        }

                        pc.BattleStatus = 0;
                        pc.Speed = 200;

                        

                        Warp(pc, this.Actor.TentMapID, 3, 3);

                        break;
                        /*
                    case 2:
                        input = InputBox(pc, "请输入招牌內容", InputType.PetRename);

                        if (input != "")
                            this.Actor.Title = input;
                        break;
                        */
                    case 2:

                        if (pc.TenkActor != null)
                        {
                            SagaMap.Map map = MapManager.Instance.GetMap(pc.MapID);
                            map.DeleteActor(this.Actor);
                            if (ScriptManager.Instance.Events.ContainsKey(this.Actor.EventID))
                            {
                                ScriptManager.Instance.Events.Remove(this.Actor.EventID);
                            }

                            MapManager.Instance.DeleteMapInstance(this.Actor.TentMapID);

                            pc.TenkActor = null;
                            this.Actor.TentMapID = 0;
                        }
                        break;
                    case 3:
                        break;
                }
            }
            else
            {
                switch (Select(pc, string.Format("{0}的 帐篷 Hit : {1}", this.Actor.Caster.Name, hitCount), "", "进入帐篷", "什么也不做"))
                {
                    case 1:

                        if (this.Actor.TentMapID == 0)
                        {
                            Map map = MapManager.Instance.GetMap(pc.MapID);
                            this.Actor.TentMapID = MapManager.Instance.CreateMapInstance(this.Actor.Caster, 40000000, pc.MapID, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height));
                            
                        }

                        Map tentMap = MapManager.Instance.GetMap(this.Actor.TentMapID);
                        if (tentMap.Actors.Count >= 5)
                        {
                            Say(pc, 0, "帳蓬已擠滿人了。");
                            return;
                        }

                        pc.BattleStatus = 0;
                        pc.Speed = 200;



                        Warp(pc, this.Actor.TentMapID, 3, 3);

                        break;

                    case 2:
                        break;
                }
            }

            hitCount++;
        }

        public override EventActor Clone()
        {
            return new 帐篷();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Network.Client;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 犧牲（サクリファイス）
    /// </summary>
    public class VicariouslyResu : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ProcessRevive(dActor,level);
            sActor.HP = 0;
            sActor.e.OnDie();
            args.affectedActors.Add(sActor);
            args.Init();
            args.flag[0] = SagaLib.AttackFlag.DIE;
        }
        #endregion

        private void ProcessRevive(Actor dActor, byte level)
        {
            if(dActor.type== ActorType.PC )
            {
                MapClient client = MapClient.FromActorPC((ActorPC)dActor);
                if (client.Character.Buff.Dead)
                {
                    client.Character.BattleStatus = 0;
                    client.SendChangeStatus();
                    client.Character.TInt["Revive"] = 5;
                    client.EventActivate(0xF1000000);
                    client.Character.HP = (uint)(client.Character.MaxHP * 0.1f * level);
                    Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
                }
            }
        }

    }
}
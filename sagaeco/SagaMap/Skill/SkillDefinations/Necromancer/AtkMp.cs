
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 燃燒的靈魂（ソウルバーン）
    /// </summary>
    public class AtkMp : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.HP > sActor.MaxHP * 0.35f)
                return 0;
            else return -2;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.HP > sActor.MaxHP * 0.35f)
            {
                uint deductHP = (uint)(sActor.MaxHP * 0.35f);
                sActor.HP -= deductHP;
                uint addMP = (uint)(sActor.MaxMP * 0.5f);
                sActor.MP += addMP;
                if (sActor.MP > sActor.MaxMP)
                    sActor.MP = sActor.MaxMP;
                SkillHandler.Instance.ShowVessel(sActor, (int)deductHP, (int)-addMP, 0);
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }
        }
    }
}
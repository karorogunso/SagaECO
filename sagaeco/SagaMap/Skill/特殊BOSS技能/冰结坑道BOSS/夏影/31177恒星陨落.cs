    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    class S31177: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {

            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, short.MaxValue);
            string s = "………";
            foreach (var item in targets)
            {
                if (item.Status.Additions.ContainsKey("HolyVolition"))
                {
                    SkillHandler.RemoveAddition(item,"HolyVolition");
                    s = "为了苟活下去，抛弃信仰，堕落，将灵魂献给恶魔，祈求非人之力的庇护……简直太难看了。这样的你，根本就没有面对真相的资格！";
                }
                SkillHandler.Instance.ShowVessel(item, 209845498);
                item.Buff.Dead = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                item.HP = 0;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, item, true);
                
                //SkillHandler.Instance.ShowEffectOnActor(item, 5004);
            }
            SkillHandler.Instance.ActorSpeak(sActor, s);
        }
        #endregion
    }
}

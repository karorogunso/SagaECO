using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31102 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 700, false);//获取周围5格的目标
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))//检查目标是否可攻击
                {
                    OtherAddition skill = new OtherAddition(null, item, "汇聚吧", 10000);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        item.TInt["SPEEDDOWN"] = 50;
                        SkillHandler.Instance.ShowEffectOnActor(item, 5380);
                        item.Speed = 50;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, item, true);
                        item.Buff.SpeedDown = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);

                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        item.TInt["SPEEDDOWN"] = 0;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, item, true);
                        item.Buff.SpeedDown = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    };
                    SkillHandler.ApplyAddition(item, skill);
                }
            }
            OtherAddition skill2 = new OtherAddition(null, sActor, "汇聚吧buff", 10000);
            skill2.OnAdditionStart += (s, e) =>
                {
                    sActor.Status.max_atk1_rate_skill = 200;
                    sActor.Status.max_atk2_rate_skill = 200;
                    sActor.Status.max_atk3_rate_skill = 200;
                    sActor.Status.min_atk1_rate_skill = 200;
                    sActor.Status.min_atk2_rate_skill = 200;
                    sActor.Status.min_atk3_rate_skill = 200;

                    sActor.Status.min_matk_rate_skill = 200;
                    sActor.Status.max_matk_rate_skill = 200;
                };
            skill2.OnAdditionEnd += (s, e) =>
                {
                    sActor.Status.max_atk1_rate_skill = 100;
                    sActor.Status.max_atk2_rate_skill = 100;
                    sActor.Status.max_atk3_rate_skill = 100;
                    sActor.Status.min_atk1_rate_skill = 100;
                    sActor.Status.min_atk2_rate_skill = 100;
                    sActor.Status.min_atk3_rate_skill = 100;

                    sActor.Status.min_matk_rate_skill = 100;
                    sActor.Status.max_matk_rate_skill = 100;
                };
            SkillHandler.ApplyAddition(sActor, skill2);
        }
    }
}
 
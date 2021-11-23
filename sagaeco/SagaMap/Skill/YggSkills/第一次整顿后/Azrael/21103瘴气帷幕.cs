using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21103 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!pc.Status.Additions.ContainsKey("P_瘴气兵装"))
                return -2;
            if (pc.Status.Additions.ContainsKey("瘴气兵装CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //BUFF所需基础设置
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            int 瘴气帷幕持续时间 = 5000;
            short 影响范围 = 500;
            float 减少伤害 = 0.2f * level;

            //显示特效、附加CD及移除瘴气兵装 
            //TODO:增加释放特效
            SkillHandler.ApplyAddition(sActor, new OtherAddition(null, sActor, "瘴气兵装CD", 10000));
            SkillHandler.RemoveAddition(sActor, "P_瘴气兵装");

            //附加瘴气帷幕的BUFF
            List<Actor> targets = map.GetPlayersArea(sActor, 影响范围, true);
            targets.ForEach(a =>
            {
                OtherAddition skill2 = new OtherAddition(null, a, "瘴气帷幕", 瘴气帷幕持续时间);
                SkillHandler.Instance.ShowEffectOnActor(a, 5020);
                SkillHandler.Instance.ShowEffectOnActor(a, 5023);
                skill2.OnAdditionStart += (s, e) =>
                {
                    SkillHandler.SendSystemMessage(s, "受到了『瘴气防壁』的效果，受到的伤害降低");
                };
                skill2.OnAdditionEnd += (s, e) =>
                {
                    SkillHandler.SendSystemMessage(s, "『瘴气防壁』的效果消失了");
                    sActor.OnBuffCallBackList.Remove("BUFF_瘴气帷幕");
                };
                SkillHandler.ApplyAddition(a, skill2);

                //BUFF处理
                SkillHandler.OnCheckBuffListReduce.Add("BUFF_瘴气帷幕", (attacker, target, damage) =>
                {
                    if (target.Status.Additions.ContainsKey("瘴气帷幕"))
                    {
                        SkillHandler.Instance.ShowEffectOnActor(target, 5021);
                        if (damage > 0)
                            damage = (int)(damage * (1f - 减少伤害));
                    }
                    return damage;
                });
            });
        }
    }
}

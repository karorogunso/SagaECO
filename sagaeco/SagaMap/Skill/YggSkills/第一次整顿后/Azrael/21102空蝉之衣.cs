using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21102 : ISkill
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
            int 硬直时间 = (level >= 2 ? 4000 : 2000);
            short 范围大小 = (short)(level >= 2 ? 300 : 150);
            float factor = 6f +2f * level;
            float HealRate = 0.3f;

            //自身僵直2秒
            SkillHandler.ApplyAddition(sActor, new 硬直(null, sActor, 2000));

            //显示特效、附加CD及移除瘴气兵装
            SkillHandler.Instance.ShowEffectOnActor(sActor, 7949);
            SkillHandler.ApplyAddition(sActor, new OtherAddition(null, sActor, "瘴气兵装CD", 10000));
            SkillHandler.RemoveAddition(sActor, "P_瘴气兵装");

            //附加空蝉之衣的BUFF
            OtherAddition skill2 = new OtherAddition(null, sActor, "空蝉之衣", 2000);
            skill2.OnAdditionStart += (s, e) =>
            {
                s.Buff.Spirit = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
            };
            skill2.OnAdditionEnd += (s, e) =>
            {
                s.Buff.Spirit = false;
                sActor.OnBuffCallBackList.Remove("BUFF_空蝉之衣");
                SkillHandler.RemoveAddition(s, "硬直");
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
            };
            SkillHandler.ApplyAddition(sActor, skill2);

            //BUFF处理
            if (!sActor.OnBuffCallBackList.ContainsKey("BUFF_空蝉之衣"))
            {
                sActor.OnBuffCallBackList.Add("BUFF_空蝉之衣", (x, z, c) =>
                {
                    if (z.Status.Additions.ContainsKey("空蝉之衣"))
                    {
                        SkillHandler.RemoveAddition(z, "空蝉之衣");
                        SkillHandler.Instance.ShowEffectOnActor(z, level >= 2 ? (uint)5320 : 4213);
                        List<Actor> targets = map.GetActorsArea(z, 范围大小, false).FindAll(e => SkillHandler.Instance.CheckValidAttackTarget(z, e));
                        targets.ForEach(e =>
                        {
                            SkillHandler.ApplyAddition(e, new 硬直(null, e, 硬直时间));
                            SkillHandler.Instance.DoDamage(false, z, e, null, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, factor);
                            SkillHandler.Instance.ShowEffectOnActor(e, 8053);
                            SkillHandler.Instance.PushBack(z, e, 1);
                            if (level >= 3) SkillHandler.Instance.HealHPRate(z, HealRate);
                        });
                    }
                    return c;
                });
            }
        }
    }
}

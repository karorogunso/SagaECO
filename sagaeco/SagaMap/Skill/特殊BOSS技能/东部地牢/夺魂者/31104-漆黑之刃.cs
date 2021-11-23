using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31104 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (!sActor.Status.Additions.ContainsKey("瘴气兵装"))//如果BOSS不在瘴气兵装状态
            {
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Dark, 1.8f);//造成180%物理伤害（暗属性）
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5080);
                if (dActor.HP <= 0)//如果造成伤害后目标死亡
                {
                    if (!sActor.Status.Additions.ContainsKey("瘴气兵装"))
                    {
                        瘴气兵装 buff = new 瘴气兵装(null, sActor, 15000);
                        SkillHandler.ApplyAddition(sActor, buff);
                    }
                }
            }
            else//处于瘴气兵装状态
            {
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Dark, 2.2f);//造成280%物理伤害（暗属性）
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5310);
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5080);

                int ran = SagaLib.Global.Random.Next(1, 6);//随机1-6，来选择降低哪个属性
                int value = 25;//定好降多少
                switch(ran)
                {
                    case 1://降低STR
                        if (!dActor.Status.Additions.ContainsKey("STRDOWN"))
                        {
                            STRDOWN sd = new STRDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 2:
                        if (!dActor.Status.Additions.ContainsKey("AGIDOWN"))
                        {
                            AGIDOWN sd = new AGIDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 3:
                        if (!dActor.Status.Additions.ContainsKey("VITDOWN"))
                        {
                            VITDOWN sd = new VITDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 4:
                        if (!dActor.Status.Additions.ContainsKey("INTDOWN"))
                        {
                            INTDOWN sd = new INTDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 5:
                        if (!dActor.Status.Additions.ContainsKey("DEXDOWN"))
                        {
                            DEXDOWN sd = new DEXDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 6:
                        if (!dActor.Status.Additions.ContainsKey("MAGDOWN"))
                        {
                            MAGDOWN sd = new MAGDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                }
            }
        }
    }
}

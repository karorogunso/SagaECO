using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31105 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 500, false);//获取周围5格的目标
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))//检查目标是否可攻击
                {
                    targets.Add(item);//添加目标
                    SkillHandler.Instance.PushBack(sActor, item, 4);//鸡腿4格
                    Stun skill = new Stun(null, item, 3000);
                    SkillHandler.ApplyAddition(item, skill);
                    if (sActor.Status.Additions.ContainsKey("瘴气兵装"))//如果BOSS处于瘴气兵装状态
                    {
                        addbuff(item);//跳转到Addbuff
                        SkillHandler.Instance.ShowEffectOnActor(item, 5293);
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Dark, 2.2f);//对群体造成300%伤害
        }
        void addbuff(Actor dActor)
        {
            int ran = SagaLib.Global.Random.Next(1, 6);//随机1-6，来选择降低哪个属性
            int value = 25;//定好降多少
            switch (ran)
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

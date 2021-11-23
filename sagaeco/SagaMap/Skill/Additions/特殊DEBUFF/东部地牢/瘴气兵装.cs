using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class 瘴气兵装 : DefaultBuff
    {
        public 瘴气兵装(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "瘴气兵装", lifetime, 1000)
        {
            this.OnAdditionStart += this.StartEventForSpeedDown;
            this.OnAdditionEnd += this.EndEventForSpeedDown;
        }

        void StartEventForSpeedDown(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.ActorSpeak(actor, "你们的灵魂，将会成为我的力量！");
            actor.Buff.Undead = true;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            SkillHandler.Instance.ShowEffectOnActor(actor, 5081);
            SkillHandler.Instance.ShowEffectOnActor(actor, 4276);
        }

        void EndEventForSpeedDown(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Undead = false;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            SkillHandler.Instance.ShowEffectOnActor(actor, 5320);

            List<Actor> actors = map.GetActorsArea(actor, 500, false);//获取周围5格的目标
            //List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(actor, item))//检查目标是否可攻击
                {
                    //targets.Add(item);//添加目标
                    SkillHandler.Instance.PushBack(actor, item, 4);//鸡腿4格
                    硬直 skill2 = new 硬直(null, item, 4000);
                    SkillHandler.ApplyAddition(item, skill2);
                    if (actor.Status.Additions.ContainsKey("瘴气兵装"))//如果BOSS处于瘴气兵装状态
                    {
                        addbuff(item);//跳转到Addbuff
                        SkillHandler.Instance.ShowEffectOnActor(item, 5293);
                    }
                    SkillHandler.Instance.DoDamage(false, actor, item, null, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 0, 3f);
                }
            }
            //SkillHandler.Instance.MagicAttack(actor, targets, null, SagaLib.Elements.Dark, 3f);//对群体造成300%伤害

        }


        void addbuff(Actor dActor)
        {
            int ran = SagaLib.Global.Random.Next(1, 6);//随机1-6，来选择降低哪个属性
            int value = 30;//定好降多少
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

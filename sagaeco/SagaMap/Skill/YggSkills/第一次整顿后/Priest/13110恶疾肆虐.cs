using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S13110 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            short range = 500;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> dActors = map.GetActorsArea(dActor, range, false).Where(target => SkillHandler.Instance.CheckValidAttackTarget(sActor, target)).ToList();   //dActors为被传染的目标
            if (dActor.Status.Additions.ContainsKey("Poison1"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Poison1"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                int damage = addition.Variable["Poison1"];
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Poison1"))
                        SkillHandler.RemoveAddition(target, "Poison1");
                    Poison1 newaddition = new Poison1(args.skill, target, lefttime, damage);
                    SkillHandler.ApplyAddition(target, newaddition);
                    //SkillHandler.Instance.ShowEffectOnActor(item, 5114);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Poison2"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Poison2"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                int damage = addition.Variable["Poison2"];
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Poison2"))
                        SkillHandler.RemoveAddition(target, "Poison2");
                    Poison2 newaddition = new Poison2(args.skill, target, lefttime, damage);
                    SkillHandler.ApplyAddition(target, newaddition);
                    //SkillHandler.Instance.ShowEffectOnActor(item, 5114);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Confuse"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Confuse"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Confuse"))
                        SkillHandler.RemoveAddition(target, "Confuse");
                    Confuse newaddition = new Confuse(null, target, lefttime);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Frosen"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Frosen"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Frosen"))
                        SkillHandler.RemoveAddition(target, "Frosen");
                    Freeze newaddition = new Freeze(null, target, lefttime);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Paralyse"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Paralyse"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Paralyse"))
                        SkillHandler.RemoveAddition(target, "Paralyse");
                    Paralyse newaddition = new Paralyse(null, target, lefttime);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Silence"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Silence"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Silence"))
                        SkillHandler.RemoveAddition(target, "Silence");
                    Silence newaddition = new Silence(null, target, lefttime);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Sleep"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Sleep"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Sleep"))
                        SkillHandler.RemoveAddition(target, "Sleep");
                    Sleep newaddition = new Sleep(null, target, lefttime);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Stone"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Stone"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Stone"))
                        SkillHandler.RemoveAddition(target, "Stone");
                    Stone newaddition = new Stone(null, target, lefttime);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
            if (dActor.Status.Additions.ContainsKey("Stun"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["Stun"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("Stun"))
                        SkillHandler.RemoveAddition(target, "Stun");
                    Stun newaddition = new Stun(null, target, lefttime);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
            if (dActor.Status.Additions.ContainsKey("鈍足"))
            {
                DefaultDeBuff addition = dActor.Status.Additions["鈍足"] as DefaultDeBuff;
                int lefttime = addition.RestLifeTime;
                int speedDown = addition.Variable["SpeedDown"];
                foreach (var target in dActors)
                {
                    if (target.Status.Additions.ContainsKey("鈍足"))
                        SkillHandler.RemoveAddition(target, "鈍足");
                    鈍足 newaddition = new 鈍足(null, target, lefttime, speedDown);
                    SkillHandler.ApplyAddition(target, newaddition);
                }
            }
        }
    }
}

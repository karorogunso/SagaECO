using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S42000 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约"))
            {
                return 0;
            }
            else
            {
                return -2;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 20000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorPC me = (ActorPC)sActor;
            if (dActor == sActor) SkillHandler.Instance.ShowEffectOnActor(dActor, 4388);
            if (dActor != null)
            {
                if (dActor.type == ActorType.PC)
                {
                    ActorPC target = (ActorPC)dActor;
                    if (me.Status.Additions.ContainsKey("属性契约"))
                    {
                        switch (((OtherAddition)(me.Status.Additions["属性契约"])).Variable["属性契约"])
                        {
                            case 1://火
                                if (sActor.Status.Additions.ContainsKey("元素解放"))
                                {
                                    AtkMinToMax buffs11 = new AtkMinToMax(args.skill, target, lifetime);
                                    MAtkMinToMax buffs12 = new MAtkMinToMax(args.skill, target, lifetime);
                                    SkillHandler.ApplyAddition(target, buffs11);
                                    SkillHandler.ApplyAddition(target, buffs12);
                                }
                                else
                                {
                                    AtkUp buff11 = new AtkUp(args.skill, target, lifetime, 65);
                                    MAtkUp buff12 = new MAtkUp(args.skill, target, lifetime, 65);
                                    SkillHandler.ApplyAddition(target, buff11);
                                    SkillHandler.ApplyAddition(target, buff12);
                                }
                                break;
                            case 2://水
                                if (sActor.Status.Additions.ContainsKey("元素解放"))
                                {
                                    SkillHandler.Instance.MagicAttack(sActor, target, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Water, -7.5f);
                                }
                                else
                                {
                                    HPRecUp buff21 = new HPRecUp(args.skill, target, lifetime, 200);
                                    MPRecUp buff22 = new MPRecUp(args.skill, target, lifetime, 200);
                                    SPRecUp buff23 = new SPRecUp(args.skill, target, lifetime, 200);
                                    SkillHandler.ApplyAddition(target, buff21);
                                    SkillHandler.ApplyAddition(target, buff22);
                                    SkillHandler.ApplyAddition(target, buff23);
                                }
                                break;
                            case 3://风
                                if (sActor.Status.Additions.ContainsKey("元素解放"))
                                {
                                    HitCriUp buffs31 = new HitCriUp(args.skill, target, lifetime, 50);
                                    HitMagicUp buffs32=new HitMagicUp(args.skill, target, lifetime, 50);
                                    SkillHandler.ApplyAddition(target, buffs31);
                                    SkillHandler.ApplyAddition(target, buffs32);
                                }
                                else
                                {
                                    AspdUp buff31 = new AspdUp(args.skill, target, lifetime, 50);
                                    CspdUp buff32 = new CspdUp(args.skill, target, lifetime, 50);
                                    SkillHandler.ApplyAddition(target, buff31);
                                    SkillHandler.ApplyAddition(target, buff32);
                                }
                                break;
                            case 4://地
                                if (sActor.Status.Additions.ContainsKey("元素解放"))
                                {
                                    DefUp buffs41 = new DefUp(args.skill, target, lifetime, 20);
                                    MDefUp buffs42 = new MDefUp(args.skill, target, lifetime, 20);
                                    SkillHandler.ApplyAddition(target, buffs41);
                                    SkillHandler.ApplyAddition(target, buffs42);
                                }
                                else
                                {
                                    DefAddUp buff41 = new DefAddUp(args.skill, target, lifetime, 50);
                                    MDefAddUp buff42 = new MDefAddUp(args.skill, target, lifetime, 50);
                                    SkillHandler.ApplyAddition(target, buff41);
                                    SkillHandler.ApplyAddition(target, buff42);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}

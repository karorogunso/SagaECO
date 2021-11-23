using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    class fuenriru : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, null);
            List<Actor> affected = new List<Actor>();
            switch (level)
            {
                case 1:
                    int a = new Random().Next(0, 100);
                    if (a <= 30)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Pressure", 500000);
                        SkillHandler.ApplyAddition(sActor, skill);
                        SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Dark, 11.0f);
                    }
                    //foreach (Actor i in actors)
                    //{
                    //    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    //        affected.Add(i);
                    //}
                    //SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Earth, 11.0f);
                    break;
                case 2:
                    args = new SkillArg();
                    args.Init();
                    args.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(2066, 5);
                    int hpheal = (int)(sActor.MaxHP * (float)(5.0f / 100.0f));
                    int mpheal = (int)(sActor.MaxMP * (float)(2.0f / 100.0f));
                    int spheal = (int)(sActor.MaxSP * (float)(2.0f / 100.0f));
                    if (sActor.Status.Additions.ContainsKey("Pressure"))
                    {
                        int mul = new Random().Next(1, 10);
                        hpheal = (int)Math.Min((sActor.MaxHP * (5.0f / 100.0f) * mul), (sActor.MaxHP * (50.0f / 100.0f)));
                        mpheal = (int)Math.Min((sActor.MaxMP * (2.0f / 100.0f) * mul), (sActor.MaxMP * (25.0f / 100.0f)));
                        spheal = (int)Math.Min((sActor.MaxSP * (2.0f / 100.0f)* mul), (sActor.MaxSP * (25.0f / 100.0f)));
                        SkillHandler.RemoveAddition(sActor, "Pressure");
                    }

                    args.hp.Add(hpheal);
                    args.mp.Add(mpheal);
                    args.sp.Add(spheal);
                    args.flag.Add(AttackFlag.HP_HEAL | AttackFlag.SP_HEAL | AttackFlag.MP_HEAL | AttackFlag.NO_DAMAGE);
                    sActor.HP += (uint)hpheal;
                    sActor.MP += (uint)mpheal;
                    sActor.SP += (uint)spheal;
                    if (sActor.HP > sActor.MaxHP)
                        sActor.HP = sActor.MaxHP;
                    if (sActor.MP > sActor.MaxMP)
                        sActor.MP = sActor.MaxMP;
                    if (sActor.SP > sActor.MaxSP)
                        sActor.SP = sActor.MaxSP;
                    SkillHandler.Instance.ShowEffectByActor(sActor, 4178);
                    SkillHandler.Instance.ShowVessel(sActor, -hpheal, -mpheal, -spheal);
                    Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, args, sActor, true);
                    break;
                case 3:
                    foreach (Actor i in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                            affected.Add(i);
                    }
                    float factor = 11.0f;
                    if (sActor.Status.Additions.ContainsKey("Pressure"))
                    {
                        int mul = new Random().Next(0, 10);
                        factor += mul;
                        SkillHandler.RemoveAddition(sActor, "Pressure");
                    }
                    SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Earth, factor);
                    break;

            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S13109 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3f;
            float epboost = 1 + (sActor.BeliefLight / 5000f) * 0.5f;
            float factordec = 0.2f;
            int maxmember = level;
            if (sActor.Status.Additions.ContainsKey("祝福之声"))
            {
                SkillHandler.RemoveAddition(sActor, "祝福之声");
                sActor.MP += 200;
            }
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Party != null)
                {
                    List<Actor> dActors = new List<Actor>();
                    for (int i = 0; i < maxmember; i++)
                    {
                        Actor LowD = null;
                        foreach (var item in pc.Party.Members.Values)
                            if (item.Online && item.HP > 0 && item.HP != item.MaxHP && item.MapID == sActor.MapID && !dActors.Contains(item))
                                if (LowD == null || LowD.MaxHP - LowD.HP < item.MaxHP - item.HP)
                                    LowD = item;
                        if (LowD != null && !dActors.Contains(LowD))
                            dActors.Add(LowD);
                    }
                    if(dActors.Count == 0)
                        dActors.Add(sActor);

                    for (int i = 0; i < dActors.Count; i++)
                    {
                        SkillHandler.Instance.MagicAttack(sActor, dActors[i], args, SkillHandler.DefType.IgnoreAll, Elements.Holy, -factor * epboost);
                        //if ((args.flag[args.affectedActors.IndexOf(dActors[i])] & AttackFlag.CRITICAL) != 0)
                            //Logger.ShowInfo("暴击！");
                        //int damage = SkillHandler.Instance.CalcDamage(false, sActor, dActors[i], null, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, -factor * epboost); 
                        SkillHandler.Instance.ShowEffectOnActor(dActors[i], 4140, sActor);
                        //dActors[i].HP = (uint)(dActors[i].HP - damage);
                        //if (dActors[i].HP > dActors[i].MaxHP)
                            //dActors[i].HP = dActors[i].MaxHP;
                        //SkillHandler.Instance.ShowVessel(dActors[i], damage);
                        factor -= factordec;
                        //SkillHandler.Instance.ShowVessel(dActors[i], -damage);
                        //SkillHandler.Instance.DoDamage(false, sActor, dActors[i], args, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, -factor);
                    }
                }
                else
                    SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, -factor * epboost);
            }
        }
        #endregion
    }
}

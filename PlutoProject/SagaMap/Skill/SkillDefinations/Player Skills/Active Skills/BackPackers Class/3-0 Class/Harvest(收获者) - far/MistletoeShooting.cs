using System;
#pragma warning disable CS0105 // “System”的 using 指令以前在此命名空间中出现过
using System;
#pragma warning restore CS0105 // “System”的 using 指令以前在此命名空间中出现过
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Harvest
{
    /// <summary>
    /// 槲寄生射击(ヤドリギショット)
    /// </summary>
    public class MistletoeShooting : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {



            float[] damagefactors = new float[] { 0, 6.0f, 7.4f, 13.2f, 15.3f, 23.2f };
            float damagefactor = damagefactors[level];
            int dmgend = 0;
            float[] healcut = new float[] { 0, 0.1f, 0.2f, 0.3f, 0.35f, 0.4f };
            //int[] lifetimes = new int[] { 0, 30000, 50000, 75000, 95000, 120000 };
            //int lifetime = lifetimes[level];


            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(dActor.X, dActor.Y, 300, null);
            List<Actor> recoveraffected = new List<Actor>();//治疗集合
            //List<Actor> damageaffected = new List<Actor>();//攻击集合
            //damageaffected.Add(dActor);
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC || act.type == ActorType.PARTNER || act.type == ActorType.PET)
                {
                    recoveraffected.Add(act);
                }

            }
            SkillHandler.Instance.ShowEffectByActor(sActor, 3096);
            int healend = 0;
            //= (int)(healcut[level] * damagefactor * sActor.Status.max_atk1);
            dmgend = (int)(SkillHandler.Instance.CalcDamage(true, sActor, dActor, args, SkillHandler.DefType.Def, sActor.WeaponElement, 0, damagefactor));
            healend = (int)(dmgend * healcut[level]);
            SkillHandler.Instance.FixAttack(sActor, recoveraffected, args, SagaLib.Elements.Holy, -healend);
            
            foreach (Actor i in recoveraffected)
            {

                i.HP += (uint)healend;
                if (i.HP >= i.MaxHP)
                {
                    i.HP = i.MaxHP;
                }
                SkillHandler.Instance.ShowVessel(i, -healend);


            }
            SkillHandler.Instance.ShowEffectByActor(dActor, 4392);
            SkillHandler.Instance.FixAttack(sActor, dActor, args, sActor.WeaponElement, dmgend);
            //SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, damagefactor);
            //SkillHandler.Instance.MagicAttack(sActor, recoveraffected, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, -dmg * healcut[level]); //FixAttack


        }



        //public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        //{


        //    //float[] factor = { 0, 6.0f, 7.4f, 13.2f, 15.3f, 23.2f };

        //    //SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor[level]);
        //    //args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2548, level, 0));
        //}


    }
}

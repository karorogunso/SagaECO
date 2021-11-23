
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gardener
{
    /// <summary>
    /// ガーデニング
    /// </summary>
    public class Gardening : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "Gardening", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            /*
             * ガーデニング †
                Passive 
                習得JOBLV：25 
                効果：飛空庭での植物育成が可能になる。 
                飛空庭に設置した鉢に植えた植物に栄養剤を与え育てることができる。
                成長した植物からはアイテムを採取できる。
                鉢は５つまで設置可能 
                他人の鉢は育成できない（タンスが開けないのと同じ） 
                鉢に植えることができるのは品種改良種とガーデニングで収穫した苗 
                与えられる栄養剤（植物栄養剤）は1日1本（0時更新） 
                成熟後は栄養剤を与えられない 
                成熟後はいつでも収穫可能 
                栽培中の鉢は撤去すると消滅(再配置は可能) 
                収穫すると鉢は消滅 
                植えてから約28日で枯れ木になる
             */
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}


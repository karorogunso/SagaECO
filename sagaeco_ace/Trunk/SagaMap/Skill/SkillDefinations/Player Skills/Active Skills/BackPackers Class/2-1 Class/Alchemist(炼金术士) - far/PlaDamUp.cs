using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 植物傷害增加（植物系ダメージ上昇）
    /// </summary>
    public class PlaDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //ushort[] Values = { 0, 3, 6, 9, 12, 15 };//%

            //ushort value = Values[level];

            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "魔鬼体质", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            float rate = skill.skill.Level * 0.05f;
            int addmaxatk1 = (int)(actor.Status.max_atk1 * rate);
            int addmaxatk2 = (int)(actor.Status.max_atk2 * rate);
            int addmaxatk3 = (int)(actor.Status.max_atk3 * rate);
            int addminatk1 = (int)(actor.Status.min_atk1 * rate);
            int addminatk2 = (int)(actor.Status.min_atk2 * rate);
            int addminatk3 = (int)(actor.Status.min_atk3 * rate);
            int addmaxmatk = (int)(actor.Status.max_matk * rate);
            int addminmatk = (int)(actor.Status.min_matk * rate);
            int reducehp = (int)(actor.MaxHP * rate);

            if (skill.Variable.ContainsKey("魔鬼体质addmaxatk1"))
                skill.Variable.Remove("魔鬼体质addmaxatk1");
            skill.Variable.Add("魔鬼体质addmaxatk1", addmaxatk1);
            actor.Status.max_atk1_skill += (short)addmaxatk1;

            if (skill.Variable.ContainsKey("魔鬼体质addmaxatk2"))
                skill.Variable.Remove("魔鬼体质addmaxatk2");
            skill.Variable.Add("魔鬼体质addmaxatk2", addmaxatk2);
            actor.Status.max_atk2_skill += (short)addmaxatk2;

            if (skill.Variable.ContainsKey("魔鬼体质addmaxatk3"))
                skill.Variable.Remove("魔鬼体质addmaxatk3");
            skill.Variable.Add("魔鬼体质addmaxatk3", addmaxatk3);
            actor.Status.max_atk3_skill += (short)addmaxatk3;

            if (skill.Variable.ContainsKey("魔鬼体质addminatk1"))
                skill.Variable.Remove("魔鬼体质addminatk1");
            skill.Variable.Add("魔鬼体质addminatk1", addminatk1);
            actor.Status.min_atk1_skill += (short)addmaxatk1;

            if (skill.Variable.ContainsKey("魔鬼体质addminatk2"))
                skill.Variable.Remove("魔鬼体质addminatk2");
            skill.Variable.Add("魔鬼体质addminatk2", addminatk2);
            actor.Status.min_atk2_skill += (short)addmaxatk2;

            if (skill.Variable.ContainsKey("魔鬼体质addminatk3"))
                skill.Variable.Remove("魔鬼体质addminatk3");
            skill.Variable.Add("魔鬼体质addminatk3", addminatk3);
            actor.Status.min_atk3_skill += (short)addmaxatk3;

            if (skill.Variable.ContainsKey("魔鬼体质addmaxmatk"))
                skill.Variable.Remove("魔鬼体质addmaxmatk");
            skill.Variable.Add("魔鬼体质addmaxmatk", addmaxmatk);
            actor.Status.max_matk_skill += (short)addmaxmatk;

            if (skill.Variable.ContainsKey("魔鬼体质addminmatk"))
                skill.Variable.Remove("魔鬼体质addminmatk");
            skill.Variable.Add("魔鬼体质addminmatk", addminmatk);
            actor.Status.min_matk_skill += (short)addminmatk;

            if (skill.Variable.ContainsKey("魔鬼体质reducehp"))
                skill.Variable.Remove("魔鬼体质reducehp");
            skill.Variable.Add("魔鬼体质reducehp", reducehp);
            actor.Status.hp_skill -= (short)reducehp;

        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["魔鬼体质addmaxatk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["魔鬼体质addmaxatk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["魔鬼体质addmaxatk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["魔鬼体质addminatk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["魔鬼体质addminatk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["魔鬼体质addminatk3"];
            actor.Status.min_matk_skill -= (short)skill.Variable["魔鬼体质addminmatk"];
            actor.Status.max_matk_skill -= (short)skill.Variable["魔鬼体质addmaxmatk"];
            actor.Status.hp_skill += (short)skill.Variable["魔鬼体质reducehp"];
        }
        #endregion
    }
}
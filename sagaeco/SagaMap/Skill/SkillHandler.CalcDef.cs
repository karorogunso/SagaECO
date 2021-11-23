using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Skill
{
    public partial class SkillHandler
    {
        /// <summary>
        /// 检查BUFF对伤害的影响
        /// </summary>
        /// <param name="dActor">目标</param>
        /// <param name="damage">输入伤害</param>
        /// <returns></returns>
        public int CheckBuffForDamage(Actor dActor, int damage)
        {
            int d = damage;
            if (dActor.Status.Additions.ContainsKey("Invincible"))//绝对壁垒
                damage = 0;
            return damage;
        }
        /// <summary>
        /// 只计算面板左右防御的影响 不考虑任何面板外的状态和判定
        /// </summary>
        /// <param name="dActor"></param>
        /// <param name="defType">可以对物理技能进行魔法类防御判定</param>
        /// <param name="atk"></param>
        /// <param name="ignore">根据deftype无视相应防御类型的百分比值，不可超过100%，即左防50的目标ignore0.5后只计算25左防，若是右放200则计算100</param>
        /// <returns></returns>
        public int CalcPhyDamage(Actor dActor, DefType defType, int atk, float ignore = 0f, float ignoreAdd = 0f)
        {
            int damage, def1 = 0, def2 = 0;
            if (ignore > 1f) ignore = ignore / 100f; //以防用错
            if (ignoreAdd > 1f) ignoreAdd = ignoreAdd / 100f; //以防用错
            if (ignore > 1f) ignore = 1f;
            if (ignoreAdd > 1f) ignoreAdd = 1f;
                switch (defType)
            {
                case DefType.Def:
                    def1 = (int)(dActor.Status.def * (1f - ignore));
                    def2 = (int)(dActor.Status.def_add * (1f - ignoreAdd));
                    break;
                case DefType.MDef:
                    def1 = (int)(dActor.Status.mdef * (1f - ignore));
                    def2 = (int)(dActor.Status.mdef_add * (1f - ignoreAdd));
                    break;
                case DefType.IgnoreDefLeft:
                    def1 = 0;
                    def2 = dActor.Status.def_add;
                    break;
                case DefType.IgnoreDefRight:
                    def1 = dActor.Status.def;
                    def2 = 0;
                    break;
                case DefType.IgnoreMDefLeft:
                    def1 = 0;
                    def2 = dActor.Status.def_add;
                    break;
                case DefType.IgnoreMDefRight:
                    def1 = dActor.Status.mdef;
                    def2 = 0;
                    break;
                case DefType.IgnoreAll:
                    def1 = 0;
                    def2 = 0;
                    break;
            }
            if (def1 > 90) def1 = 90;
            damage = checkPositive(atk * (1f - def1 / 100f) - def2);
            return damage;
        }
        /// <summary>
        ///  只计算面板左右防御的影响 不考虑任何面板外的状态和判定
        /// </summary>
        /// <param name="dActor"></param>
        /// <param name="defType">可以对魔法攻击进行物理防御判定</param>
        /// <param name="atk"></param>
        /// <param name="ignore">根据deftype无视相应防御类型的百分比值，不可超过100%，即左防50的目标ignore0.5后只计算25左防，若是右放200则计算100</param>
        /// <returns></returns>
        public int CalcMagDamage(Actor dActor, DefType defType, int atk, float ignore = 0f, float ignoreAdd = 0f)
        {
            int damage, def1 = 0, def2 = 0;
            if (ignore > 1) ignore = ignore / 100f; //以防用错
            if (ignoreAdd > 1) ignoreAdd = ignoreAdd / 100f; //以防用错
            if (ignore > 1f) ignore = 1f;
            if (ignoreAdd > 1f) ignoreAdd = 1f;
            switch (defType)
            {
                case DefType.Def:
                    def1 = (int)(dActor.Status.def * (1f - ignore));
                    def2 = (int)(dActor.Status.def_add * (1f - ignoreAdd));
                    break;
                case DefType.MDef:
                    def1 = (int)(dActor.Status.mdef * (1f - ignore));
                    def2 = (int)(dActor.Status.mdef_add * (1f - ignoreAdd));
                    break;
                case DefType.IgnoreDefLeft:
                    def1 = 0;
                    def2 = dActor.Status.def_add;
                    break;
                case DefType.IgnoreDefRight:
                    def1 = dActor.Status.def;
                    def2 = 0;
                    break;
                case DefType.IgnoreMDefLeft:
                    def1 = 0;
                    def2 = dActor.Status.def_add;
                    break;
                case DefType.IgnoreMDefRight:
                    def1 = dActor.Status.mdef;
                    def2 = 0;
                    break;
                case DefType.IgnoreAll:
                    def1 = 0;
                    def2 = 0;
                    break;
            }
            if (def1 > 90) def1 = 90;
            damage = checkPositive(atk * (1f - def1 / 100f) - def2);
            return damage;
        }

        int checkPositive(float num)
        {
            if (num > 0)
                return (int)num;
            return 0;
        }
    }
}

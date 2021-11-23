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
        int CalcPhyDamage(Actor sActor, Actor dActor, DefType defType, int atk, float ignore, AttackResult res = AttackResult.Hit)
        {
            int damage, def1 = 0, def2 = 0;
            switch (defType)
            {
                case DefType.Def:
                    def1 = dActor.Status.def;
                    def2 = dActor.Status.def_add;
                    break;
                case DefType.MDef:
                    def1 = dActor.Status.mdef;
                    def2 = dActor.Status.mdef_add;
                    break;
                case DefType.IgnoreLeft:
                    def1 = 0;
                    def2 = dActor.Status.def_add;
                    break;
                case DefType.IgnoreRight:
                    def1 = dActor.Status.def;
                    def2 = 0;
                    break;
                case DefType.IgnoreAll:
                    def1 = 0;
                    def2 = 0;
                    break;
            }
            if (res == AttackResult.Critical)
                def2 = 0;

            //damage = (int)(atk * (1.0 - (def2 * (1.0 + def1 / 100.0) * a) / (def2 * (1.0 + def1 / 100.0) * a + 1.0)));
            if (sActor.Status.Purger_Lv > 0)
            {
                def1 -= (10 * sActor.Status.Purger_Lv);
                def2 = (int)((float)def2 * (float)(1.0f - (float)sActor.Status.Purger_Lv / 10.0f));
            }
            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = dActor as ActorPC;
                damage = (int)((float)atk * (1.0f - (float)((float)def1 / 100.0f)) - def2 - (float)((float)(pc.Vit + pc.Status.vit_rev + pc.Status.vit_item + pc.Status.vit_chip + pc.Status.vit_mario + pc.Status.vit_skill) / 3.0f));
            }
            else
            {
                //这个算法经过考察是错误的.那就是说玩家攻击怪物和怪物攻击玩家应用的公式是不一样的
                //damage = (int)((float)atk * (1.0f - (float)((float)def1 / 100.0f)) - def2);
                float divright = atk > def2 ? (float)(atk - def2) : 1.0f;
                float dmgreduce = (1.0f - ((float)def1 / 100.0f));
                damage = (int)(divright * dmgreduce);
            }
            return damage;
        }
        int CalcMagDamage(Actor sActor, Actor dActor, DefType defType, int atk, float ignore)
        {
            int damage, def1 = 0, def2 = 0;
            //double a = 0.008;
            switch (defType)
            {
                case DefType.Def:
                    def1 = dActor.Status.def;
                    def2 = dActor.Status.def_add;
                    break;
                case DefType.MDef:
                    def1 = dActor.Status.mdef;
                    def2 = dActor.Status.mdef_add;
                    break;
                case DefType.IgnoreLeft:
                    def1 = 0;
                    def2 = dActor.Status.mdef_add;
                    break;
                case DefType.IgnoreRight:
                    def1 = dActor.Status.mdef;
                    def2 = 0;
                    break;
                case DefType.IgnoreAll:
                    def1 = 0;
                    def2 = 0;
                    break;
                case DefType.DefIgnoreRight:
                    def1 = dActor.Status.def;
                    def2 = 0;
                    break;
            }
            if (dActor.type == ActorType.PC)
            {
                //damage = (int)(atk * (1.0 - (def2 * (1.0 + def1 / 100.0) * a) / (def2 * (1.0 + def1 / 100.0) * a + 1.0)));
                damage = (int)((float)atk * (1.0f - (float)((float)def1 / 100.0f)) - def2);
            }
            else
            {
                float divright = atk > def2 ? (float)(atk - def2) : 1.0f;
                float dmgreduce = (1.0f - ((float)def1 / 100.0f));
                damage = (int)(divright * dmgreduce);
            }
            return damage;
        }
    }
}

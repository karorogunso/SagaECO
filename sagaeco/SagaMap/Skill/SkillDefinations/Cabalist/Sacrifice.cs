using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Skill;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 献祭
    /// </summary>
    public class Sacrifice : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            ActorPC RealdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            //if (RealdActor.Status.Additions.ContainsKey("Sacrifice"))
            //   return -30;
            /*else*/
            if (RealdActor.HP < (uint)(RealdActor.MaxHP * 0.4f))
                return -12;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000;
            SacrificeBuff sb = new SacrificeBuff(args.skill, sActor, dActor, lifetime, (int)(sActor.MaxHP * 0.1f));
            SkillHandler.ApplyAddition(sActor, sb);
        }
        class SacrificeBuff : DefaultBuff
        {
            public SacrificeBuff(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
                : base(skill, sActor, dActor, "SacrificeBuff", (int)(lifetime * (1f + Math.Max((dActor.Status.debuffee_bonus / 100), -0.9f))), 5000, damage)
            {
                this.OnAdditionStart += this.StartEventHandler;
                this.OnAdditionEnd += this.EndEventHandler;
                this.OnUpdate2 += this.TimerUpdate;
            }
            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill,SkillArg arg, int damage)
            {
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (sActor.HP > 0 && !dActor.Buff.Dead)
                    {
                        SkillHandler.Instance.CauseDamage(sActor, sActor, damage);
                        SkillHandler.Instance.ShowVessel(sActor, damage);
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                int level = skill.skill.Level;
                int max_atk1_add = (int)(actor.Status.max_atk_bs * (0.4 + 0.2 * level));
                int max_atk2_add = (int)(actor.Status.max_atk_bs * (0.4 + 0.2 * level));
                int max_atk3_add = (int)(actor.Status.max_atk_bs * (0.4 + 0.2 * level));
                int min_atk1_add = (int)(actor.Status.min_atk_bs * (0.4 + 0.2 * level));
                int min_atk2_add = (int)(actor.Status.min_atk_bs * (0.4 + 0.2 * level));
                int min_atk3_add = (int)(actor.Status.min_atk_bs * (0.4 + 0.2 * level));
                int max_matk_add = (int)(actor.Status.max_matk_bs * (0.4 + 0.2 * level));
                int min_matk_add = (int)(actor.Status.min_matk_bs * (0.4 + 0.2 * level));
                //int def_add = (int)(actor.Status.def_add * (0.12 + 0.06 * level));
                //int mdef_add = (int)(actor.Status.mdef_add * (0.12 + 0.06 * level));
                //大傷
                if (skill.Variable.ContainsKey("FrameHart1_Max"))
                    skill.Variable.Remove("FrameHart1_Max");
                skill.Variable.Add("FrameHart1_Max", max_atk1_add);
                actor.Status.max_atk1_skill += (short)max_atk1_add;

                if (skill.Variable.ContainsKey("FrameHart2_Max"))
                    skill.Variable.Remove("FrameHart2_Max");
                skill.Variable.Remove("FrameHart2_Max");
                skill.Variable.Add("FrameHart2_Max", max_atk2_add);
                actor.Status.max_atk2_skill += (short)max_atk2_add;

                if (skill.Variable.ContainsKey("FrameHart3_Max"))
                    skill.Variable.Remove("FrameHart3_Max");
                skill.Variable.Add("FrameHart3_Max", max_atk3_add);
                actor.Status.max_atk3_skill += (short)max_atk3_add;

                //小伤
                if (skill.Variable.ContainsKey("FrameHart1_min"))
                    skill.Variable.Remove("FrameHart1_min");
                skill.Variable.Add("FrameHart1_min", min_atk1_add);
                actor.Status.min_atk1_skill += (short)min_atk1_add;

                if (skill.Variable.ContainsKey("FrameHart2_min"))
                    skill.Variable.Remove("FrameHart2_min");
                skill.Variable.Remove("FrameHart2_min");
                skill.Variable.Add("FrameHart2_min", min_atk2_add);
                actor.Status.min_atk2_skill += (short)min_atk2_add;

                if (skill.Variable.ContainsKey("FrameHart3_min"))
                    skill.Variable.Remove("FrameHart3_min");
                skill.Variable.Add("FrameHart3_min", min_atk3_add);
                actor.Status.min_atk3_skill += (short)min_atk3_add;
                //魔伤
                if (skill.Variable.ContainsKey("FrameHart_mAtk_Max"))
                    skill.Variable.Remove("FrameHart_mAtk_Max");
                skill.Variable.Add("FrameHart_mAtk_Max", max_matk_add);
                actor.Status.max_matk_skill += (short)max_matk_add;

                if (skill.Variable.ContainsKey("FrameHart_mAtk_min"))
                    skill.Variable.Remove("FrameHart_mAtk_min");
                skill.Variable.Add("FrameHart_mAtk_min", min_matk_add);
                actor.Status.min_matk_skill += (short)min_matk_add;

                /*//防御
                if (skill.Variable.ContainsKey("FrameHart_Def"))
                    skill.Variable.Remove("FrameHart_Def");
                skill.Variable.Add("FrameHart_Def", def_add);
                actor.Status.def_add_skill += (short)def_add;

                if (skill.Variable.ContainsKey("FrameHart_mDef"))
                    skill.Variable.Remove("FrameHart_mDef");
                skill.Variable.Add("FrameHart_mDef", mdef_add);
                actor.Status.mdef_add_skill += (short)mdef_add;*/
                actor.Buff.Mushroom = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                //大傷
                actor.Status.max_atk1_skill -= (short)skill.Variable["FrameHart1_Max"];
                actor.Status.max_atk2_skill -= (short)skill.Variable["FrameHart2_Max"];
                actor.Status.max_atk3_skill -= (short)skill.Variable["FrameHart3_Max"];
                //小傷
                actor.Status.min_atk1_skill -= (short)skill.Variable["FrameHart1_min"];
                actor.Status.min_atk2_skill -= (short)skill.Variable["FrameHart2_min"];
                actor.Status.min_atk3_skill -= (short)skill.Variable["FrameHart3_min"];
                //魔伤
                actor.Status.max_matk_skill -= (short)skill.Variable["FrameHart_mAtk_Max"];
                actor.Status.min_matk_skill -= (short)skill.Variable["FrameHart_mAtk_min"];
                //防御
                //actor.Status.def_add_skill -= (short)skill.Variable["FrameHart_Def"];
                //actor.Status.mdef_add_skill -= (short)skill.Variable["FrameHart_mDef"];
                actor.Buff.Mushroom = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            }
        }
    }
}
/*actor.Buff.ATフィールド = true;
actor.Buff.AxeDelayCancel = true;
actor.Buff.BowDelayCancel = true;
actor.Buff.Curse = true;
actor.Buff.DelayCancel = true;
actor.Buff.Mushroom = true;
actor.Buff.ShortSwordDelayCancel = true;
actor.Buff.Spirit = true;
actor.Buff.state190 = true;
//actor.Buff.アトラクトマーチ = true;
actor.Buff.イビルソウル = true;
actor.Buff.エンチャントウエポン = true;
actor.Buff.エンチャントブロック = true;
actor.Buff.オーバーチューン = true;
actor.Buff.オーバーレンジ = true;
actor.Buff.オーバーワーク = true;
*/
/*actor.Buff.オラトリオ = true;
actor.Buff.ガンディレイキャンセル = true;
actor.Buff.スタミナテイク = true;
actor.Buff.ソリッドボディ = true;
actor.Buff.ゾンビ = true;
actor.Buff.ダブルアップ = true;*/
//actor.Buff.チャンプモンスターキラー状態 = true;
//actor.Buff.ディレイキャンセル = true;
//actor.Buff.パッシング = true;
//actor.Buff.パパ点火 = true;
//actor.Buff.フェニックス = true;
//actor.Buff.ブラッディウエポン = true;


//actor.Buff.マナの守護 = true;
/*
actor.Buff.ライフテイク = true;
actor.Buff.リフレクション = true;
actor.Buff.リボーン = true;
actor.Buff.ロケットブースター点火 = true;
actor.Buff.必中陣 = true;
actor.Buff.赤くなる = true;*/
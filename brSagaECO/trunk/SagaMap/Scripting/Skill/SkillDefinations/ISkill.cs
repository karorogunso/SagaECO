using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 技能定义接口
    /// </summary>
    public interface ISkill
    {
        /// <summary>
        /// 技能处理过程
        /// </summary>
        /// <param name="sActor">源Actor</param>
        /// <param name="dActor">目标Actor</param>
        /// <param name="args">技能参数</param>
        /// <param name="level">技能等级</param>
        void Proc(Actor sActor, Actor dActor, SkillArg args, byte level);

        /// <summary>
        /// 尝试释放某技能，并返回结果
        /// </summary>
        /// <param name="sActor">源Actor</param>
        /// <param name="dActor">目标Actor</param>
        /// <returns>0表示可释放，小于0则为错误代码</returns>
        int TryCast(ActorPC sActor, Actor dActor, SkillArg args);
        /*
         * 错误代码：
         * 0
 success 
-1～-60
 # システムメッセージ(スキル)
 GAME_SMSG_SKILL_USEERR1,";MPとSPが不足しています";
 GAME_SMSG_SKILL_USEERR2,";使用する触媒が不足しています";
 GAME_SMSG_SKILL_USEERR3,";ターゲットが視認できません";
 GAME_SMSG_SKILL_USEERR4,";ターゲットが見つかりません";
 GAME_SMSG_SKILL_USEERR5,";装備中の武器ではこのスキルを使用できません";
 GAME_SMSG_SKILL_USEERR6,";指定不可能な位置が選択されました";
 GAME_SMSG_SKILL_USEERR7,";スキルを使用できない状態です";
 GAME_SMSG_SKILL_USEERR8,";他のスキルを詠唱している為キャンセルされました";
 GAME_SMSG_SKILL_USEERR9,";遠距離攻撃中の為キャンセルされました";
 GAME_SMSG_SKILL_USEERR10,";スキルを習得していません";
 GAME_SMSG_SKILL_USEERR11,";対象が死んでいる為ターゲットできません";
 GAME_SMSG_SKILL_USEERR12,";スキル使用条件があっていません";
 GAME_SMSG_SKILL_USEERR13,";スキルを使用できません";
 GAME_SMSG_SKILL_USEERR14,";スキルを使用できない対象です";
 GAME_SMSG_SKILL_USEERR15,";MPが不足しています";
 GAME_SMSG_SKILL_USEERR16,";SPが不足しています";
 GAME_SMSG_SKILL_USEERR17,";指定した地形は別のスキルの効果中です";
 GAME_SMSG_SKILL_USEERR18,";近くにテントが張られています";
 GAME_SMSG_SKILL_USEERR19,";アイテム使用中の為キャンセルされました";
 GAME_SMSG_SKILL_USEERR20,";攻撃中の為キャンセルされました";
 GAME_SMSG_SKILL_USEERR21,";反応がありませんでした";
 #エラーコード22はメッセージ無し
 GAME_SMSG_SKILL_USEERR23,";憑依しないと使えません";
 GAME_SMSG_SKILL_USEERR24,";他の憑依者が効果中です";
 GAME_SMSG_SKILL_USEERR25,";憑依中は使えないスキルです";
 GAME_SMSG_SKILL_USEERR26,";使用するのに必要なお金が足りません";
 GAME_SMSG_SKILL_USEERR27,";宿主が健全でないため使用できません";
 GAME_SMSG_SKILL_USEERR28,";宿主が行動不能時のみ使うことができます";
 GAME_SMSG_SKILL_USEERR29,";スキルが使えない場所にいます";
 GAME_SMSG_SKILL_USEERR30,";前回のスキルのディレイが残っています";
 GAME_SMSG_SKILL_USEERR31,";ペットがいないと使えません";
 GAME_SMSG_SKILL_USEERR32,";キャンプの中ではキャンプは禁止です";
 GAME_SMSG_SKILL_USEERR33,";指定した場所に敵が侵入しているため使用できませんでした";
 GAME_SMSG_SKILL_USEERR34,";矢を装備していないと使用できません";
 GAME_SMSG_SKILL_USEERR35,";実包を装備していないと使用できません";
 GAME_SMSG_SKILL_USEERR36,";投擲武器を装備していないと使用できません";
 GAME_SMSG_SKILL_USEERR37,";使用できるテントを所持していません";
 GAME_SMSG_SKILL_USEERR38,";栽培に使用するアイテムを所持していません";
 GAME_SMSG_SKILL_USEERR39,";鑑定できるアイテムを所持していません";
 GAME_SMSG_SKILL_USEERR40,";開けることのできるアイテムを所持していません";
 GAME_SMSG_SKILL_USEERR41,";合成することのできるアイテムを所持していません";
 GAME_SMSG_SKILL_USEERR42,";対象が移動したため射程から外れました";
 GAME_SMSG_SKILL_USEERR43,";「メタモーバトル」中は使用できません";
 GAME_SMSG_SKILL_USEERR44,";これ以上このスキルを設置することは出来ません";
 #エラーコード45はGAME_SYNTHE_FULL_ITEM（アイテム欄に空きがありません）で代用中
 GAME_SMSG_SKILL_USEERR46,";『騎士団演習』中は使用できません";
 GAME_SMSG_SKILL_USEERR47,";ペットが近くにいないと使用できません";
 GAME_SMSG_SKILL_USEERR48,";敵に発見された為、失敗しました";
 GAME_SMSG_SKILL_USEERR49,";宿主中は使えないスキルです";
 GAME_SMSG_SKILL_USEERR50,";再度スキルを使用するには時間を置いてください";
 GAME_SMSG_SKILL_USEERR51,";ペットを使役している間は使用できません";
 GAME_SMSG_SKILL_USEERR52,";ボスへの使用はできません";
 GAME_SMSG_SKILL_USEERR53,";アンデッド状態の対象への使用はできません";
 GAME_SMSG_SKILL_USEERR54,";現在の装備ではこのスキルを使用できません";
 GAME_SMSG_SKILL_USEERR55,";矢の数が足りません";
 GAME_SMSG_SKILL_USEERR56,";実包の数が足りません";
 GAME_SMSG_SKILL_USEERR57,";投擲武器の数が足りません";
 GAME_SMSG_SKILL_USEERR58,";融合できるマリオネットが違います";
 GAME_SMSG_SKILL_USEERR59,";イベント中の為、使用できません";
 GAME_SMSG_SKILL_USEERR60,";設定が不許可になっている為、使用できません";
         */
    }
}

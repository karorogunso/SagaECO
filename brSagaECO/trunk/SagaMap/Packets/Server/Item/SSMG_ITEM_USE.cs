using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_USE : Packet
    {
//DWORD item_id;        //アイテムID （0xFFFFFFFFの場合もある。
//short result;         //0なら成功 それ以外なら失敗
//DROWD from_chara_id;  //アイテム使用者のサーバキャラID？
//DWORD cast;           //キャスト時間っぽい（ミリ秒単位
//DWORD to_chara_id;    //アイテム対象者のサーバキャラID？
//BYTE x?               // 0x00 or 0xFF？
//BYTE y?               // （飛空挺呼び出した場合は値がちゃんと入っている
//WORD skill_id;        //スキルID
//BYTE skill_lv;        //スキルLv

        public SSMG_ITEM_USE()
        {
            this.data = new byte[25];
            this.offset = 2;
            this.ID = 0x09c5;
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        /// <summary>
        /// -1～-30
 ///GAME_SMSG_ITEM_USEERR1,";ターゲットがいません";
 ///GAME_SMSG_ITEM_USEERR2,";指定した座標へは使用できません";
 ///GAME_SMSG_ITEM_USEERR3,";使用できない状態です";
 ///GAME_SMSG_ITEM_USEERR4,";スキル中の為使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR5,";遠距離攻撃中の為使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR6,";視線が通っていません";
 ///GAME_SMSG_ITEM_USEERR7,";イベント中の為使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR8,";アイテムを使用する事が出来ないターゲットです";
 ///GAME_SMSG_ITEM_USEERR9,";行動不能状態の為使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR10,";ゴーレムショップに出品中です";
 ///GAME_SMSG_ITEM_USEERR11,";ゴーレムは既に起動しています";
 ///GAME_SMSG_ITEM_USEERR12,";未鑑定品の為使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR13,";スキルが使えませんでした";
 ///GAME_SMSG_ITEM_USEERR14,";";
 ///GAME_SMSG_ITEM_USEERR15,";宿主はマリオネットにのりうつれません";
 ///GAME_SMSG_ITEM_USEERR16,";条件が合わない為マリオネットにのりうつれません";
 ///GAME_SMSG_ITEM_USEERR17,";このマリオネットは未実装です";
 ///GAME_SMSG_ITEM_USEERR18,";再度マリオネットにのりうつるには時間を置いてください";
 ///GAME_SMSG_ITEM_USEERR19,";アイテム使用中の為使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR20,";攻撃中の為使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR21,";使用できませんでした";
 ///GAME_SMSG_ITEM_USEERR22,";この餌は対象ペットへは与えられません";
 ///GAME_SMSG_ITEM_USEERR23,";指定した対象へはこのアイテムは使用できません";
 ///GAME_SMSG_ITEM_USEERR24,";「メタモーバトル」のみ使用可能です";
 ///GAME_SMSG_ITEM_USEERR25,";プルルに変身中でないと使用出来ません";
 ///GAME_SMSG_ITEM_USEERR26,";このアイテムでは現在のマリオネットを解除できません";
 ///GAME_SMSG_ITEM_USEERR27,";この場所ではマリオネットにのりうつることはできません";
 ///GAME_SMSG_ITEM_USEERR28,";「メタモーバトル」中は使用できません";
 ///GAME_SMSG_ITEM_USEERR29,";このマップでは使用できません";
 ///GAME_SMSG_ITEM_USEERR30,";イベントが用意されていません";
 ///GAME_SMSG_ITEM_USEERR31,";何らかの理由で作成できませんでした";
 ///GAME_SMSG_ITEM_USEERR32,";ロボット騎乗中でないと使用できません";
///
///-31～-100
/// GAME_SMSG_ITEM_USEERR100,";何らかの原因で使用できませんでした";
///
///-101～-160
 ///GAME_SMSG_SKILL_USEERR1,";MPとSPが不足しています";
 ///GAME_SMSG_SKILL_USEERR2,";使用する触媒が不足しています";
 ///GAME_SMSG_SKILL_USEERR3,";ターゲットが視認できません";
 ///GAME_SMSG_SKILL_USEERR4,";ターゲットが見つかりません";
 ///GAME_SMSG_SKILL_USEERR5,";装備中の武器ではこのスキルを使用できません";
 ///GAME_SMSG_SKILL_USEERR6,";指定不可能な位置が選択されました";
 ///GAME_SMSG_SKILL_USEERR7,";スキルを使用できない状態です";
 ///GAME_SMSG_SKILL_USEERR8,";他のスキルを詠唱している為キャンセルされました";
 ///GAME_SMSG_SKILL_USEERR9,";遠距離攻撃中の為キャンセルされました";
 ///GAME_SMSG_SKILL_USEERR10,";スキルを習得していません";
 ///GAME_SMSG_SKILL_USEERR11,";対象が死んでいる為ターゲットできません";
 ///GAME_SMSG_SKILL_USEERR12,";スキル使用条件があっていません";
 ///GAME_SMSG_SKILL_USEERR13,";スキルを使用できません";
 ///GAME_SMSG_SKILL_USEERR14,";スキルを使用できない対象です";
 ///GAME_SMSG_SKILL_USEERR15,";MPが不足しています";
 ///GAME_SMSG_SKILL_USEERR16,";SPが不足しています";
 ///GAME_SMSG_SKILL_USEERR17,";指定した地形は別のスキルの効果中です";
 ///GAME_SMSG_SKILL_USEERR18,";近くにテントが張られています";
 ///GAME_SMSG_SKILL_USEERR19,";アイテム使用中の為キャンセルされました";
 ///GAME_SMSG_SKILL_USEERR20,";攻撃中の為キャンセルされました";
 ///GAME_SMSG_SKILL_USEERR21,";反応がありませんでした";
 ///#エラーコード22はメッセージ無し
 ///GAME_SMSG_SKILL_USEERR23,";憑依しないと使えません";
 ///GAME_SMSG_SKILL_USEERR24,";他の憑依者が効果中です";
 ///GAME_SMSG_SKILL_USEERR25,";憑依中は使えないスキルです";
 ///GAME_SMSG_SKILL_USEERR26,";使用するのに必要なお金が足りません";
 ///GAME_SMSG_SKILL_USEERR27,";宿主が健全でないため使用できません";
 ///GAME_SMSG_SKILL_USEERR28,";宿主が行動不能時のみ使うことができます";
 ///GAME_SMSG_SKILL_USEERR29,";スキルが使えない場所にいます";
 ///GAME_SMSG_SKILL_USEERR30,";前回のスキルのディレイが残っています";
 ///GAME_SMSG_SKILL_USEERR31,";ペットがいないと使えません";
 ///GAME_SMSG_SKILL_USEERR32,";キャンプの中ではキャンプは禁止です";
 ///GAME_SMSG_SKILL_USEERR33,";指定した場所に敵が侵入しているため使用できませんでした";
 ///GAME_SMSG_SKILL_USEERR34,";矢を装備していないと使用できません";
 ///GAME_SMSG_SKILL_USEERR35,";実包を装備していないと使用できません";
 ///GAME_SMSG_SKILL_USEERR36,";投擲武器を装備していないと使用できません";
 ///GAME_SMSG_SKILL_USEERR37,";使用できるテントを所持していません";
 ///GAME_SMSG_SKILL_USEERR38,";栽培に使用するアイテムを所持していません";
 ///GAME_SMSG_SKILL_USEERR39,";鑑定できるアイテムを所持していません";
 ///GAME_SMSG_SKILL_USEERR40,";開けることのできるアイテムを所持していません";
 ///GAME_SMSG_SKILL_USEERR41,";合成することのできるアイテムを所持していません";
 ///GAME_SMSG_SKILL_USEERR42,";対象が移動したため射程から外れました";
 ///GAME_SMSG_SKILL_USEERR43,";「メタモーバトル」中は使用できません";
 ///GAME_SMSG_SKILL_USEERR44,";これ以上このスキルを設置することは出来ません";
 ///#エラーコード45はGAME_SYNTHE_FULL_ITEM（アイテム欄に空きがありません）で代用中
 ///GAME_SMSG_SKILL_USEERR46,";『騎士団演習』中は使用できません";
 ///GAME_SMSG_SKILL_USEERR47,";ペットが近くにいないと使用できません";
 ///GAME_SMSG_SKILL_USEERR48,";敵に発見された為、失敗しました";
 ///GAME_SMSG_SKILL_USEERR49,";宿主中は使えないスキルです";
 ///GAME_SMSG_SKILL_USEERR50,";再度スキルを使用するには時間を置いてください";
 ///GAME_SMSG_SKILL_USEERR51,";ペットを使役している間は使用できません";
 ///GAME_SMSG_SKILL_USEERR52,";ボスへの使用はできません";
 ///GAME_SMSG_SKILL_USEERR53,";アンデッド状態の対象への使用はできません";
 ///GAME_SMSG_SKILL_USEERR54,";現在の装備ではこのスキルを使用できません";
 ///GAME_SMSG_SKILL_USEERR55,";矢の数が足りません";
 ///GAME_SMSG_SKILL_USEERR56,";実包の数が足りません";
 ///GAME_SMSG_SKILL_USEERR57,";投擲武器の数が足りません";
 ///GAME_SMSG_SKILL_USEERR58,";融合できるマリオネットが違います";
 ///GAME_SMSG_SKILL_USEERR59,";イベント中の為、使用できません";
 ///GAME_SMSG_SKILL_USEERR60,";設定が不許可になっている為、使用できません";

        /// </summary>
        public short result
        {
            set
            {
                this.PutShort(value, 6);
            }
        }

        public uint Form_ActorId
        {
            set
            {
                this.PutUInt(value, 8);
            }
        }

        public uint Cast
        {
            set
            {
                this.PutUInt(value, 12);
            }
        }

        public uint To_ActorID
        {
            set
            {
                this.PutUInt(value, 16);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 20);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 21);
            }
        }


        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, 22);
            }
        }

        public byte SkillLV
        {
            set
            {
                this.PutByte(value, 24);
            }
        }





    }

}


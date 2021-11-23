using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
using SagaDB.Item;

namespace SagaScript
{
    public class ネコマタ用 : SagaMap.Scripting.Item
    {
        public ネコマタ用()
        {
            /*            
             * Init(EVENTID, delegate(ActorPC pc)
            {
                if (PetShow(pc, petitemID, target封印モンＩＤ))
                    TakeItem(pc, itemid, 1);
            });
            */

            //ネコマタ（杏）用セーラー
            Init(90000339, delegate(ActorPC pc)
            {
                if (PetShow(pc, 10017908, 10260056))
                    TakeItem(pc, 20050130, 1);
            });
            //ネコマタ（黒）用骑士服
            Init(90000343, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017914, 14370001))
                    TakeItem(pc, 20050131, 1);
            });
            //ネコマタ（黒）用巫女服
            Init(90000343, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017914, 14370002))
                    TakeItem(pc, 20050131, 1);
            });
            //ネコマタ（胡桃・若菜）用水着
            Init(90000357, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017912, 14240001))
                    TakeItem(pc, 20050134, 1);
            });

            //ネコマタ（胡桃・若菜）メイド
            Init(90000357, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017912, 14240002))
                    TakeItem(pc, 20050134, 1);
            });
            //ネコマタ（蓝）用羽衣
            Init(90000362, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017903, 10260057))
                    TakeItem(pc, 20050136, 1);
            });
            //ネコマタ（桃）キティラー服
            Init(90000367, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017900, 10260058))
                    TakeItem(pc, 20050138, 1);
            });
            //ネコマタ（緑）用カントリー服
            Init(90000390, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017918, 10260059))
                    TakeItem(pc, 20050141, 1);
            });
            //ネコマタ（白）用トップモデル服
            Init(90000391, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017923, 10260060))
                    TakeItem(pc, 20050142, 1);
            });
            //ネコマタ（山吹）用和装
            Init(90000403, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017905, 10260061))
                    TakeItem(pc, 20050143, 1);
            });
            //ネコマタ（茜）用ハロウィン服
            Init(90000404, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017907, 10260062))
                    TakeItem(pc, 20050144, 1);
            });
            //ネコマタ（空）用怪盗服
            Init(90000410, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017910, 10260063))
                    TakeItem(pc, 20050145, 1);
            });
            //ネコマタ（菫）用おめかしワンピ
            Init(90000418, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017906, 10260064))
                    TakeItem(pc, 20050146, 1);
            });
            //ネコマタ（桃）用おでかけパーカー
            Init(90000424, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017900, 10260065))
                    TakeItem(pc, 20050147, 1);
            });
            //ネコマタ（茜）用チャイナドレス
            Init(90000436, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017907, 10260066))
                    TakeItem(pc, 20050150, 1);
            });
            //ネコマタ（藍）用黒セーラー
            Init(90000443, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017903, 10260054))
                    TakeItem(pc, 20050151, 1);
            });
            //ネコマタ（杏）用エルブンジャケット
            Init(90000448, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017908, 10260067))
                    TakeItem(pc, 20050152, 1);
            });
            //ネコマタ（白）用ウェディングドレス
            Init(90000448, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017923, 10260068))
                    TakeItem(pc, 20050153, 1);
            });
            //20050154	ネコマタ（空）用さざなみのワンピース	90000475	10260069
            Init(90000475, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017910, 10260069))
                    TakeItem(pc, 20050154, 1);
            });
            //20050156	ネコマタ（緑）用戦闘制服	90000482	10260070
            Init(90000482, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017902, 10260070))
                    TakeItem(pc, 20050156, 1);
            });
            //20050158	ネコマタ（菫）用着物ワンピ	90000499	10260071
            Init(90000499, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017906, 10260071))
                    TakeItem(pc, 20050158, 1);
            });
            //20050166	ネコマタ(胡桃 若菜)用カボチャマント	90000511	10260073

            Init(90000511, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017912, 10260073))
                    TakeItem(pc, 20050166, 1);
            });
            //20050167	ネコマタ（黒）用カボチャマント	90000512	10260074

            Init(90000512, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017914, 10260074))
                    TakeItem(pc, 20050167, 1);
            });
            //20050168	ネコマタ（白）用カボチャマント	90000513	10260075

            Init(90000513, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017923, 10260075))
                    TakeItem(pc, 20050168, 1);
            });
            //20050178	ネコマタ（山吹）用ボーイッシュ服	90000524	10260084
            Init(90000524, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017905, 10260084))
                    TakeItem(pc, 20050178, 1);
            });
            //20050179	ネコマタ（藍）用リボンワンピース	90000528	10260085
            Init(90000528, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017903, 10260085))
                    TakeItem(pc, 20050179, 1);
            });
            //20050191	ネコマタ（桃）用メイド服	90000542	10260086
            Init(90000542, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017900, 10260086))
                    TakeItem(pc, 20050191, 1);
            });

            //20050534	ネコマタ（黒）用舞踏会ドレス	90000425	14370003

            Init(90000425, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017914, 14370003))
                    TakeItem(pc, 20050534, 1);
            });
            //20050535	ネコマタ（胡桃 若菜）用大正袴	90000432	14240003

            Init(90000432, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017912, 14240003))
                    TakeItem(pc, 20050535, 1);
            });
            //20050539	ネコマタ（茜）用学園ブレザー	90000436	10260387

            Init(90000436, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017907, 10260387))
                    TakeItem(pc, 20050539, 1);
            });
            //20050546	ネコマタ（藍）用お嬢様ワンピース	90000362	10260388

            Init(90000362, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017903, 10260388))
                    TakeItem(pc, 20050546, 1);
            });
            //20050547	ネコマタ（杏）用勇者服	90000339	10260389

            Init(90000339, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017908, 10260389))
                    TakeItem(pc, 20050547, 1);
            });

            //20050555	ネコマタ（空）用ウエスタンワンピ	90000410	10260390

            Init(90000410, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017910, 10260390))
                    TakeItem(pc, 20050555, 1);
            });

            //20050556	ネコマタ（白）用ナース服	90000391	14380003
            Init(90000391, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017923, 14380003))
                    TakeItem(pc, 20050556, 1);
            });
            //20050557	ネコマタ（緑）用探偵服	90000390	10260391

            Init(90000390, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017902, 10260391))
                    TakeItem(pc, 20050557, 1);
            });

            //20050558	ネコマタ（菫）用カクテルドレス	90000418	10260392


            Init(90000418, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017906, 10260392))
                    TakeItem(pc, 20050558, 1);
            });
            //20050559	ネコマタ（山吹）用パイロットウェア	90000403	10260393

            Init(90000403, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017905, 10260393))
                    TakeItem(pc, 20050559, 1);
            });
            //20050562	ネコマタ（桃）用小悪魔アイドル服	90000424	10260394

            Init(90000424, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017900, 10260394))
                    TakeItem(pc, 20050562, 1);
            });

            //人形化.
            //added in 20150214
            //

            //20050198	ネコマタハート（桃）	90000584	10260087

            Init(90000584, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017900, 10260087))
                    TakeItem(pc, 20050198, 1);
            });
            //20050199	ネコマタハート（緑）	90000586	10260088
            Init(90000586, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017902, 10260088))
                    TakeItem(pc, 20050199, 1);
            });
            //20050205	ネコマタハート（空）	90000591	10260094

            Init(90000591, delegate(ActorPC pc)
            {

                if (PetShow(pc, 10017910, 10260094))
                    TakeItem(pc, 20050205, 1);
            });
        }
            /*            
             * Init(EVENTID, delegate(ActorPC pc)
            {
                if (PetShow(pc, petitemID, target封印モンＩＤ))
                    TakeItem(pc, itemid, 1);
            });
             * 10017900 桃子
                10017902	背負い魔 ネコマタ（緑）
                10017903	背負い魔 ネコマタ（藍）
                10017905	背負い魔 ネコマタ（山吹）
                10017906	背負い魔 ネコマタ（菫）
                10017907	背負い魔 ネコマタ（茜）
                10017908	背負い魔 ネコマタ（杏）
                10017910	背負い魔 ネコマタ（空）
                10017912	背負い魔 ネコマタ（胡桃 若菜）
                10017914	背負い魔 ネコマタ（黒）
                10017918	背負い魔 ネコマタ（新緑）
                10017919	背負い魔 ネコマタ（覚醒新緑）
                10017921	背負い魔 ネコマタ（覚醒緑）
                10017923	背負い魔 ネコマタ（白）

             * 
             * 

            */

        bool PetShow(ActorPC pc, uint ItemID, uint pictID)
        {
            if (pc.Inventory.Equipments[EnumEquipSlot.PET] != null)
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == ItemID)
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].PictID == 0)
                    {
                        PetShowReplace(pc, pictID);
                        Say(pc, 0, 0, pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.ToString() + "装备了$R下次不能再次使用$R", "");
                        return true;
                    }
                    else
                    {
                        Say(pc, 131, "该猫灵已经装备过道具了$R;");
                    }
                }
                else
                {
                    Say(pc, 131, "该猫灵无法装备该道具$R;");
                }
            }
            else
            {
                Say(pc, 131, "未装备猫灵$R;");
            }
            return false;
        }
    }
}
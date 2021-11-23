using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:初心者嚮導(11000535) X:124 Y:8
namespace SagaScript.M10023400
{
    public class S11000535 : Event
    {
        public S11000535()
        {
            this.EventID = 11000535;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "做什么?", "", "转职", "买东西&传送"))
            {
                case 1:
                    if (pc.JobLevel1 >= 30)
                    {
                        PC_JOB[] srcJob = { PC_JOB.SWORDMAN, PC_JOB.FENCER, PC_JOB.SCOUT, PC_JOB.ARCHER, PC_JOB.WIZARD, PC_JOB.SHAMAN, PC_JOB.VATES, PC_JOB.WARLOCK, PC_JOB.TATARABE, PC_JOB.FARMASIST, PC_JOB.RANGER, PC_JOB.MERCHANT };
                        PC_JOB[] destJOB = { PC_JOB.BLADEMASTER, PC_JOB.KNIGHT, PC_JOB.ASSASSIN, PC_JOB.STRIKER, PC_JOB.SORCERER, PC_JOB.ELEMENTER, PC_JOB.DRUID, PC_JOB.CABALIST, PC_JOB.BLACKSMITH, PC_JOB.ALCHEMIST, PC_JOB.EXPLORER, PC_JOB.TRADER };
                        //PC_JOB.Bounty Hunter,PC_JOB.Dark Stalker,PC_JOB.Command,PC_JOB.Gunner,PC_JOB.Saga,PC_JOB.Enchanter,PC_JOB.Bard,PC_JOB.Necromancer,PC_JOB.Machinery,PC_JOB.Marionest,PC_JOB.Treasurehunter,PC_JOB.Gambler
                        int srcJindex = -1;
                        for (int i = 0; i < srcJob.Length; i++)
                        {
                            if (srcJob[i] == pc.Job)
                            {
                                srcJindex = i;
                                break;
                            }
                        }
                        if (srcJindex >= 0)
                        {
                            if (Select(pc, "请问是要来快速2-1转吗?", "", "是", "不是") == 1)
                            {
                                pc.JEXP = 0;
                                ChangePlayerJob(pc, destJOB[srcJindex]);
                            }
                        }
                    }
                    else
                    {
                        Say(pc, 133, "您的Job等级不够哦!!$R");
                    }
                    break;
                case 2:
                    switch (Select(pc, "买东西吗?", "", "第一商品栏", "第二商品栏", "传送到东边吊桥"))
                    {
                        case 1:
                            OpenShopBuy(pc, 413);
                            break;
                        case 2:
                            OpenShopBuy(pc, 415);
                            break;
                        case 3:
                            Warp(pc, 10023100, 239, 127);
                            break;
                    }
                    break;
            }
        }
    }
}
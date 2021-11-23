using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001602 : Event
    {
        public S11001602()
        {
            this.EventID = 11001602;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "歡迎來到！$R;" +
            "西部要塞！$R;" +
            "$R如果可以的話，就讓$R;" +
            "做爲抵抗運動見習生的$R;" +
            "我來給你帶路吧！$R;", "抵抗運動見習生少年");
            switch (Select(pc, "拜托在城裏帶路？", "", "抵抗運動接待處", "商人", "道具精製師", "鑑定店", "雜貨店", "武器屋", "裁縫屋", "下一頁", "沒事需要拜托"))
            {

                case 1:
                    Navigate(pc, 161, 147);
                    Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                           "它會帶您去「抵抗運動接待處」的。$R;", "抵抗運動見習生少年");
                    return;
                    //break;
                case 2:
                    Navigate(pc, 155, 147);

                    Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                           "它會帶您去「商人」的。$R;", "抵抗運動見習生少年");
                    return;
                    //break;
                case 3:
                    Navigate(pc, 161, 107);
                    Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                           "它會帶您去「道具精製師」的。$R;", "抵抗運動見習生少年");
                    return;
                    //break;
                case 4:
                    Navigate(pc, 155, 107);
                    Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                           "它會帶您去「鑑定店」的。$R;", "抵抗運動見習生少年");
                    return;
                    //break;
                case 5:
                    Navigate(pc, 82, 171);
                    Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                           "它會帶您去「雜貨店」的。$R;", "抵抗運動見習生少年");
                    return;
                    //break;
                case 6:
                    Navigate(pc, 82, 163);
                    Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                           "它會帶您去「武器屋」的。$R;", "抵抗運動見習生少年");
                    return;
                    //break;
                case 7:
                    Navigate(pc, 82, 155);
                    Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                           "它會帶您去「裁縫屋」的。$R;", "抵抗運動見習生少年");
                    return;
                    //break;
                case 8:
                    switch (Select(pc, "拜托在城裏帶路？", "", "占卜店", "妖精", "抵抗運動本部", "沒事需要拜託"))
                    {
                        case 1:
                            Navigate(pc, 82, 83);
                            Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                                   "它會帶您去「占卜店」的。$R;", "抵抗運動見習生少年");
                            return;
                            //break;
                        case 2:
                            Navigate(pc, 59, 155);
                            Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                                   "它會帶您去「妖精」的。$R;", "抵抗運動見習生少年");
                            return;
                            //break;
                        case 3:
                            Navigate(pc, 62, 131);
                            Say(pc, 11001602, 131, "跟著箭頭方向走，$R;" +
                                                   "它會帶您去「抵抗運動本部」的。$R;", "抵抗運動見習生少年");
                            return;
                            //break;
                    }
                    break;
                case 9:
                    Say(pc, 131, "如果有什麽不明白的$R;" +
                    "都可以來找做爲抵抗運動見習生$R;" +
                    "的我哦！$R;", "抵抗運動見習生少年");
                    break;

            }
        }
    }
}         
              
            
            
        
     
    
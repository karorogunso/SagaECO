using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript
{
    public class S11111111 : Event
    {
        public S11111111()
        {
            this.EventID = 11111111;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
            if (pc.CStr["EPLQ"] == DateTime.Now.ToString("yyyy-MM-dd") && pc.CInt["EPMT"] == 0)
            {
                Say(pc, 131, "今天已经领取过了$R;");
            }
            else
            {
                if (pc.CStr["EPLQ"] != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    pc.CStr["EPLQ"] = DateTime.Now.ToString("yyyy-MM-dd");
                    pc.CInt["EPMT"] = 10;
                }
                if (pc.EP == pc.MaxEP)
                {
                    Say(pc, 131, "呃……$P不需要恢复呢$R;");
                }
                else
                {
                    try
                    {
                        string LP = InputBox(pc, string.Format("领取多少?(现在可以领取{0}点)", pc.CInt["EPMT"].ToString()), InputType.ItemCode);
                        if (LP == "")
                            return;
                        ushort temp = ushort.Parse(LP);
                        if (temp < 0)
                            Say(pc, 131, "输入错误哦$R;");
                        else if (temp > pc.CInt["EPMT"])
                            Say(pc, 131, "剩余点数不够哦！$R;");
                        else
                        {
                            if (pc.EP + temp > pc.MaxEP)
                            {
                                Say(pc, 131, "超过上限了哦！$R;");
                            }
                            else
                            {
                                Say(pc, 131, string.Format("確{0}EP$R;", temp.ToString()));
                                pc.EP += temp;
                                pc.CInt["EPMT"] -= temp;
                            }
                        }
                    }
                    catch
                    {
                        Say(pc, 131, "输入错误哦！$R;");
                    }
                }
                }
            }
            else
            {
                 Say(pc, 131, "只有DEM才可以领取EP哦$R;");
            }
        }
    }
}
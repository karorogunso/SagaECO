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
                Say(pc, 131, "�����Ѿ���ȡ����$R;");
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
                    Say(pc, 131, "������$P����Ҫ�ָ���$R;");
                }
                else
                {
                    try
                    {
                        string LP = InputBox(pc, string.Format("��ȡ����?(���ڿ�����ȡ{0}��)", pc.CInt["EPMT"].ToString()), InputType.ItemCode);
                        if (LP == "")
                            return;
                        ushort temp = ushort.Parse(LP);
                        if (temp < 0)
                            Say(pc, 131, "�������Ŷ$R;");
                        else if (temp > pc.CInt["EPMT"])
                            Say(pc, 131, "ʣ���������Ŷ��$R;");
                        else
                        {
                            if (pc.EP + temp > pc.MaxEP)
                            {
                                Say(pc, 131, "����������Ŷ��$R;");
                            }
                            else
                            {
                                Say(pc, 131, string.Format("��_�F{0}EP�I$R;", temp.ToString()));
                                pc.EP += temp;
                                pc.CInt["EPMT"] -= temp;
                            }
                        }
                    }
                    catch
                    {
                        Say(pc, 131, "�������Ŷ��$R;");
                    }
                }
                }
            }
            else
            {
                 Say(pc, 131, "ֻ��DEM�ſ�����ȡEPŶ$R;");
            }
        }
    }
}
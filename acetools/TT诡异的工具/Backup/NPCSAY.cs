
namespace 測試
{
    class NPCSAY
    {
        public string svvv = "";
        public bool Transform(string tbSrc)
        {
            string[] src = tbSrc.Split('\n');
            string NPCNAME = "";
            string PET = "";
            bool judge = false;
            bool talk = false;
            bool choice = false;
            bool item = false;
            bool Switch = false;
            bool sel = false;
            svvv = "";
            foreach (string i in src)
            {
                try
                {
                    string j = i.Replace("\r", "");
                    string[] buf = j.Split(',');
                    string[] command = buf[2].Split(' ');
                    if (buf[3] != "")
                    {
                        svvv += ("//" + buf[3] + "\r\n");
                    }
                    if (buf[1] != "" && !choice)
                    {
                        svvv += ("//" + buf[1] + "\r\n");
                    }
                    switch (command[0].ToLower())
                    {
                        case "judge":
                            judge = true;
                            svvv += "if(";
                            break;
                        case "switch":
                            if (command[1] == "START")
                                Switch = true;
                            if (command[1] == "END")
                                Switch = false;
                            break;
                        case "flag":
                            if (judge)
                            {
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += "&& ";
                                if (command[1] == "OFF")
                                    svvv += "!";
                                svvv += ("_" + command[2] + " ");
                            }
                            else
                            {
                                svvv += ("_" + command[2] + " = ");
                                if (command[1] == "OFF")
                                    svvv += "false;\r\n";
                                if (command[1] == "ON")
                                    svvv += "true;\r\n";
                            }
                            break;
                        case "me.lv":
                            if (judge)
                            {
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += "&& ";
                                svvv += "pc.Level ";
                                if (command[1] == "=")
                                    svvv += "=";
                                svvv += (command[1] + " ");
                                svvv += command[2];
                                svvv += " ";
                            }
                            break;
                        case "me.fame":
                            if (judge)
                            {
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += "&& ";
                                svvv += "pc.Fame ";
                                if (command[1] == "=")
                                    svvv += "=";
                                svvv += (command[1] + " ");
                                svvv += command[2];
                                svvv += " ";
                            }
                            break;
                        case "me.is_admin":
                            if (judge)
                            {
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += "&& ";
                                svvv += "pc.Account.GMLevel ";
                                if (command[1] == "=")
                                    svvv += "=";
                                svvv += (command[1] + " ");
                                svvv += command[2];
                                svvv += " ";
                            }
                            break;
                        case "pre-itemget":
                            {
                                string[] tmp = command[1].Split(':');
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += "&& ";
                                svvv += string.Format("CheckInventory(pc, {0}, {1}) ", tmp[0], tmp[1]);
                            }
                            break;
                        case "true":
                            if (judge)
                            {
                                if (item)
                                {
                                    svvv += ")\r\n{\r\n if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == " + PET + ")\r\n  {\r\n    Call(";
                                    svvv += command[1];
                                    svvv += ");\r\n    return;\r\n  }\r\n}\r\n";
                                    item = false;
                                    PET = "";
                                }
                                else
                                {
                                    svvv += ")\r\n{\r\n    Call(";
                                    svvv += command[1];
                                    svvv += ");\r\n    return;\r\n}\r\n";
                                }
                            }
                            break;
                        case "false":
                            if (judge)
                            {
                                if (command[1] != "NONE")
                                {
                                    svvv += "else\r\n{\r\n    Call(";
                                    svvv += command[1];
                                    svvv += ");\r\n    return;\r\n}\r\n";
                                }
                                judge = false;
                            }
                            break;
                        case "item":
                            if (judge)
                            {
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += "&& ";
                                if (command[1] == "GET")
                                {
                                    svvv += "CountItem(pc, ";
                                    svvv += command[2];
                                    svvv += ") >= ";
                                    if (command.Length > 3)
                                        svvv += (command[3] + " ");
                                    else
                                        svvv += "1 ";

                                }
                                else if (command[1] == "EQUIP")
                                {
                                    svvv += "pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET)";
                                    PET = command[2];
                                    item = true;
                                }
                            }
                            else if (Switch)
                            {
                                if (command[1] == "GET")
                                {
                                    svvv += "if (CountItem(pc, ";
                                    svvv += command[2];
                                    svvv += ") >= ";
                                    svvv += (command[3] + " )\r\n{\r\n    Call(" + command[4] + ");\r\n  return;\r\n}\r\n");
                                }
                                else if (command[1] == "EQUIP")
                                {
                                    svvv += "if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET)";
                                    svvv += ")\r\n{\r\n if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == " + command[2] + ")\r\n  {\r\n    Call(";
                                    svvv += command[3];
                                    svvv += ");\r\n    return;\r\n  }\r\n}\r\n";
                                }
                            }
                            else
                            {
                                if (command[1] == "GET")
                                    svvv += "GiveItem(pc, ";
                                else if (command[1] == "LOST")
                                    svvv += "TakeItem(pc, ";
                                svvv += command[2];
                                if (command.Length > 3)
                                    svvv += (", " + command[3]);
                                else
                                    svvv += ", 1";
                                svvv += ");\r\n";

                            }
                            break;

                        case "\"talk":
                        case "talk":
                            if (command[1] == "START")
                            {
                                talk = true;
                                svvv = svvv + "Say(pc, " + command[4] + ", " + command[2] + ", ";
                                NPCNAME = command[5];
                            }
                            else if (command[1] == "END")
                            {
                                talk = false;
                                if (NPCNAME != "")
                                {
                                    NPCNAME = NPCNAME.Replace("\"\"\"", "\\\"");
                                    NPCNAME = NPCNAME.Replace("\"\"", "\\\"");
                                    svvv = svvv + ", \"" + NPCNAME + "\");\r\n";
                                    NPCNAME = "";
                                }
                                else
                                    svvv = svvv + ");\r\n";
                            }
                            break;//*/
                        case "se":
                        case "jin":
                            svvv += "PlaySound(pc, ";
                            svvv += (command[1] + ", false, ");
                            svvv += (command[4] + ", ");
                            svvv += (command[5] + ");\r\n");

                            break;
                        case "\"choice":
                        case "choice":
                            if (command[1] == "END")
                            {
                                svvv += "}\r\n";
                                choice = false;
                                sel = false;
                                break;
                            }

                            command = buf[2].Split(' ');
                            choice = true;
                            svvv += string.Format("switch(Select(pc, \"{0}\", \"\"", command[2].Replace("\"", ""));//, command[5]);

                            break;
                        case "selected":
                            sel = true;
                            if (svvv.Substring(svvv.Length - 1, 1) == "\"")
                            {
                                svvv += "))\r\n{\r\n";
                                sel = false;
                            }
                            svvv += string.Format("    case {0}:\r\n        {1} {2};\r\n        break;\r\n", command[1].Replace("@", ""), command[2], command[3]);
                            break;
                        case "waitframe":
                            svvv += string.Format("Wait(pc, {0});\r\n", (int.Parse(command[1]) * 1000) / 30);
                            break;
                        case "skillpbonus":
                            svvv += string.Format("SkillPointBonus(pc, {0});\r\n", command[1]);
                            break;
                        case "medic":
                            svvv += "Heal(pc);\r\n";
                            break;
                        case "effect":
                        case "effect_one":
                            switch (int.Parse(command[2]))
                            {
                                case 0:
                                    svvv += string.Format("ShowEffect(pc, {0}, {1}, {2});\r\n", command[3], command[4], command[1]);
                                    break;
                                case 1:
                                    svvv += string.Format("ShowEffect(pc, {0});\r\n", command[1]);
                                    break;
                                case 2:
                                    svvv += string.Format("ShowEffect(pc, {0}, {1});\r\n", command[3], command[1]);
                                    break;
                            }
                            break;
                        case "istrancehost":
                            if (judge)
                            {
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += "&& ";
                                svvv += (" pc.PossesionedActors.Count != 0");
                            }
                            break;
                        case "me.job":
                            if (judge)
                            {
                                if (svvv.Substring(svvv.Length - 1, 1) != "(")
                                    svvv += " && ";
                                if (command[1] == "=")
                                {
                                    svvv += "pc.Job == (PC_JOB)" + command[2];
                                }
                                else if (command[1] == "<" || command[1] == ">")
                                {
                                    svvv += "pc.Job " + command[1] + "(PC_JOB)" + command[2];
                                }
                            }
                            break;
                        case "guide":
                            if (command[1] == "OFF")
                                svvv += "NavigateCancel(pc);\r\n";
                            else
                                svvv += string.Format("Navigate(pc, {0}, {1});\r\n", command[2], command[3]);
                            break;
                        default:
                            if (talk)
                            {
                                NPCNAME = NPCNAME.Replace("\"\"\"\"\"", "\\\"");
                                NPCNAME = NPCNAME.Replace("\"\"\"\"", "\\\"");
                                NPCNAME = NPCNAME.Replace("\"\"\"", "\\\"");
                                NPCNAME = NPCNAME.Replace("\"\"", "\\\"");
                                if (svvv.Substring(svvv.Length - 1, 1) == "\"")
                                    svvv += " +\r\n    ";
                                string txt = "";
                                foreach (string k in command)
                                {
                                    //string a;
                                    //a = k.Replace("\"\"\"", "\\\"");
                                    txt += k;
                                }
                                svvv += "\"" + txt + "$R;\"";
                            }
                            else if (choice && command[0] != "EVENTEND")
                            {
                                svvv += string.Format(", \"{0}\"", command[0]);
                            }
                            else if (sel)
                            {
                                svvv += "}\r\n" + "//" + buf[2] + "\r\n";
                                choice = false;
                                sel = false;
                            }
                            else
                                svvv += ("//" + buf[2] + "\r\n");
                            break;
                    }
                }
                catch { }
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using SagaDB.Item;
using Microsoft.CSharp;
using System.IO;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.SkillDefinations;
using System.Reflection;
using SagaMap.Manager;

namespace SagaMap.Skill
{
    public partial class SkillHandler : Singleton<SkillHandler>
    {
        public Dictionary<uint, ISkill> skillHandlers = new Dictionary<uint, ISkill>();
        public Dictionary<uint, MobISkill> MobskillHandlers = new Dictionary<uint, MobISkill>();

        string path;
        uint skillID;
        public void LoadSkill(string path, bool sendmsg = false, string name = "")
        {
            Logger.ShowInfo("開始加載技能...");
            if (sendmsg)
            {
                MapClientManager.Instance.Announce(name + " 开始重载Skills...");
            }
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } };
            CSharpCodeProvider provider = new CSharpCodeProvider(dic);
            int skillcount = 0;
            this.path = path;
            try
            {
                string[] files = Directory.GetFiles(path, "*cs", SearchOption.AllDirectories);
                Assembly newAssembly;
                int tmp;
                if (files.Length > 0)
                {
                    newAssembly = CompileScript(files, provider,sendmsg);
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        Logger.ShowInfo(string.Format("Containing {0} Skills", tmp));
                        skillcount += tmp;
                        if (sendmsg)
                            MapClientManager.Instance.Announce("Skills重载成功！共载入 " + skillcount + " 个外置Skill");
                    }
                    else
                        if (sendmsg)
                        MapClientManager.Instance.Announce("Skills重载失败了！！共载入 0 个外置Skill");
                }
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                if (sendmsg)
                {
                    MapClientManager.Instance.Announce(ex.ToString());
                    MapClientManager.Instance.Announce("Skills重载失败了！！共载入 0 个外置Skill");
                }
            }
            Logger.ShowInfo(string.Format("外置技能加載數：{0}", skillcount));
        }

        private Assembly CompileScript(string[] Source, CodeDomProvider Provider, bool sendmsg = false)
        {
            //ICodeCompiler compiler = Provider.;
            CompilerParameters parms = new CompilerParameters();
            CompilerResults results;

            // Configure parameters
            parms.CompilerOptions = "/target:library /optimize";
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = true;
            //parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Data.DataSetExtensions.dll");
            //parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll");
            //parms.ReferencedAssemblies.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Reference Assemblies\Microsoft\Framework\v3.5\System.Xml.Linq.dll");
            parms.ReferencedAssemblies.Add("System.dll");
            parms.ReferencedAssemblies.Add("SagaLib.dll");
            parms.ReferencedAssemblies.Add("SagaDB.dll");
            parms.ReferencedAssemblies.Add("SagaMap.exe");
            foreach (string i in Configuration.Instance.ScriptReference)
            {
                parms.ReferencedAssemblies.Add(i);
            }
            // Compile
            results = Provider.CompileAssemblyFromFile(parms, Source);
            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                    if (!error.IsWarning)
                    {
                        Logger.ShowError("Compile Error:" + error.ErrorText, null);
                        Logger.ShowError("File:" + error.FileName + ":" + error.Line, null);
                    }
                }
                return null;
            }
            //get a hold of the actual assembly that was generated
            return results.CompiledAssembly;
        }

        private int LoadAssembly(Assembly newAssembly)
        {
            Module[] newScripts = newAssembly.GetModules();
            int count = 0;
            foreach (Module newScript in newScripts)
            {
                Type[] types = newScript.GetTypes();
                foreach (Type npcType in types)
                {
                    try
                    {
                        if (npcType.IsAbstract == true) continue;
                        if (npcType.GetCustomAttributes(false).Length > 0) continue;
                        ISkill newEvent;

                        newEvent = (ISkill)Activator.CreateInstance(npcType);
                        string t = newEvent.ToString();
                        string id = t.Substring(t.LastIndexOf("S") + 1, t.Length - t.LastIndexOf("S") - 1);

                        if (!uint.TryParse(id, out skillID))
                            continue;

                        if (!this.skillHandlers.ContainsKey(skillID) && skillID != 0)
                            this.skillHandlers.Add(skillID, newEvent);
                        else
                        {
                            if (skillID != 0)
                                skillHandlers[skillID] = newEvent;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    count++;
                }
            }
            return count;
        }
        public void Init()
        {
            skillHandlers.Add(124, new S冰霜之焰());

            //死神
            skillHandlers.Add(21100, new S21100());
            skillHandlers.Add(21102, new S21102());
            skillHandlers.Add(21103, new S21103());
            skillHandlers.Add(21104, new S21104());
            //skillHandlers.Add(21110, new S21110());
            skillHandlers.Add(21111, new S21111());
            skillHandlers.Add(21112, new S21112());
            skillHandlers.Add(21113, new S21113());
            skillHandlers.Add(21114, new S21114());
            skillHandlers.Add(21115, new S21115());
            skillHandlers.Add(21116, new S21116());
            skillHandlers.Add(21117, new S21117());

            //生产系
            //skillHandlers.Add(32100, new S32100());
            skillHandlers.Add(32101, new S32101());

            //活动技能
            skillHandlers.Add(25000, new S25000());
            skillHandlers.Add(25001, new S25001());
            skillHandlers.Add(25002, new S25002());
            skillHandlers.Add(25003, new S25003());
            skillHandlers.Add(25004, new S25004());
            skillHandlers.Add(25005, new S25005());
            skillHandlers.Add(25006, new S25006());
            skillHandlers.Add(25007, new S25007());
            skillHandlers.Add(25008, new S25008());
            skillHandlers.Add(25009, new S25009());
            skillHandlers.Add(25010, new S25010());

            //暗鸣
            /*skillHandlers.Add(31168, new S31168());
            skillHandlers.Add(31169, new S31169());
            skillHandlers.Add(31170, new S31170());
            skillHandlers.Add(31171, new S31171());
            skillHandlers.Add(31172, new S31172());
            skillHandlers.Add(31174, new S31174());
            skillHandlers.Add(31175, new S31175());
            skillHandlers.Add(31176, new S31176());*/

            //泰达米尔龙
            skillHandlers.Add(31140, new S31140());
            skillHandlers.Add(31141, new S31141());
            skillHandlers.Add(31142, new S31142());
            skillHandlers.Add(31143, new S31143());
            //鱼人长老
            skillHandlers.Add(31144, new S31144());
            skillHandlers.Add(31145, new S31145());
            skillHandlers.Add(31146, new S31146());
            skillHandlers.Add(31147, new S31147());
            //常暗
            skillHandlers.Add(31148, new S31148());
            skillHandlers.Add(31149, new S31149());
            skillHandlers.Add(31150, new S31150());
            skillHandlers.Add(31151, new S31151());
            skillHandlers.Add(31152, new S31152());
            //利维坦
            skillHandlers.Add(31153, new S31153());
            skillHandlers.Add(31154, new S31154());
            skillHandlers.Add(31155, new S31155());
            skillHandlers.Add(31156, new S31156());
            skillHandlers.Add(31157, new S31157());
            //萝蕾拉
            skillHandlers.Add(31158, new S31158());
            skillHandlers.Add(31159, new S31159());
            skillHandlers.Add(31160, new S31160());
            skillHandlers.Add(31161, new S31161());
            skillHandlers.Add(31162, new S31162());
            skillHandlers.Add(31163, new S31163());

            //调试用技能
            skillHandlers.Add(9300, new S9300());
            skillHandlers.Add(9301, new S9301());
            skillHandlers.Add(140, new S140());
            skillHandlers.Add(141, new S141());

            //巫女
            skillHandlers.Add(20000, new S20000());
            skillHandlers.Add(20001, new S20001());
            skillHandlers.Add(20002, new S20002());
            skillHandlers.Add(20003, new S20003());
            skillHandlers.Add(20004, new S20004());
            skillHandlers.Add(20005, new S20005());
            skillHandlers.Add(20006, new S20006());
            skillHandlers.Add(20007, new S20007());
            skillHandlers.Add(20008, new S20008());
            skillHandlers.Add(20009, new S20009());
            skillHandlers.Add(20010, new S20010());
            skillHandlers.Add(20011, new S20011());
            skillHandlers.Add(20012, new S20012());
            skillHandlers.Add(20013, new S20013());
            skillHandlers.Add(20014, new S20014());
            skillHandlers.Add(20015, new S20015());
            skillHandlers.Add(20016, new S20016());
            skillHandlers.Add(20017, new S20017());
            skillHandlers.Add(20018, new S20018());
            skillHandlers.Add(20019, new S20019());
            skillHandlers.Add(20020, new S20020());
            skillHandlers.Add(20021, new S20021());
            skillHandlers.Add(20022, new S20022());
            skillHandlers.Add(20023, new S20023());
            skillHandlers.Add(20024, new S20024());
            skillHandlers.Add(20025, new S20025());


            skillHandlers.Add(32000, new S32000());
            skillHandlers.Add(32001, new S32001());
            skillHandlers.Add(32002, new S32002());

            //东部地牢技能
            skillHandlers.Add(31100, new S31100());
            skillHandlers.Add(31101, new S31101());
            skillHandlers.Add(31102, new S31102());
            skillHandlers.Add(31103, new S31103());
            skillHandlers.Add(31104, new S31104());
            skillHandlers.Add(31105, new S31105());
            skillHandlers.Add(31106, new S31106());
            skillHandlers.Add(31107, new S31107());
            skillHandlers.Add(31108, new S31108());
            skillHandlers.Add(31109, new S31109());
            skillHandlers.Add(31164, new S31164());
            skillHandlers.Add(31165, new S31165());

            skillHandlers.Add(31117, new S31117());
            skillHandlers.Add(31118, new S31118());
            skillHandlers.Add(31119, new S31119());
            skillHandlers.Add(31123, new S31123());
            skillHandlers.Add(31124, new S31124());
            skillHandlers.Add(31125, new S31125());
            skillHandlers.Add(31126, new S31126());
            skillHandlers.Add(31127, new S31127());
            skillHandlers.Add(31128, new S31128());
            skillHandlers.Add(31129, new S31129());
            skillHandlers.Add(31130, new S31130());
            //第一次整顿
            skillHandlers.Add(11000, new S11000());
            skillHandlers.Add(11001, new S11001());
            skillHandlers.Add(11002, new S11002());
            skillHandlers.Add(11003, new S11003());
            skillHandlers.Add(11004, new S11004());
            skillHandlers.Add(11005, new S11005());
            skillHandlers.Add(11006, new S11006());
            skillHandlers.Add(11007, new S11007());
            skillHandlers.Add(11008, new S11008());
            skillHandlers.Add(11009, new S11009());
            skillHandlers.Add(11010, new S11010());
            skillHandlers.Add(11100, new S11100());
            skillHandlers.Add(11101, new S11101());
            skillHandlers.Add(11102, new S11102());
            skillHandlers.Add(11103, new S11103());
            skillHandlers.Add(11104, new S11104());
            skillHandlers.Add(11105, new S11105());
            skillHandlers.Add(11106, new S11106());
            skillHandlers.Add(11107, new S11107());
            skillHandlers.Add(11108, new S11108());
            skillHandlers.Add(11109, new S11109());
            skillHandlers.Add(11110, new S11110());

            skillHandlers.Add(11011, new S11011());
            skillHandlers.Add(11120, new S11120());
            skillHandlers.Add(11121, new S11121());
            skillHandlers.Add(11122, new S11122());
            skillHandlers.Add(11123, new S11123());

            skillHandlers.Add(12000, new S12000());
            skillHandlers.Add(12001, new S12001());
            skillHandlers.Add(12002, new S12002());
            skillHandlers.Add(12003, new S12003());
            skillHandlers.Add(12004, new S12004());
            skillHandlers.Add(12005, new S12005());
            skillHandlers.Add(12006, new S12006());
            skillHandlers.Add(12007, new S12007());
            skillHandlers.Add(12008, new S12008());
            skillHandlers.Add(12009, new S12009());
            skillHandlers.Add(12010, new S12010());
            skillHandlers.Add(12011, new S12011());
            skillHandlers.Add(12012, new S12012());
            skillHandlers.Add(12013, new S12013());
            skillHandlers.Add(12014, new S12014());
            skillHandlers.Add(12100, new S12100());
            skillHandlers.Add(12101, new S12101());
            skillHandlers.Add(12102, new S12102());
            skillHandlers.Add(12103, new S12103());
            skillHandlers.Add(12104, new S12104());
            skillHandlers.Add(12105, new S12105());
            skillHandlers.Add(12106, new S12106());
            skillHandlers.Add(12107, new S12107());
            skillHandlers.Add(12108, new S12108());
            skillHandlers.Add(12109, new S12109());
            skillHandlers.Add(12110, new S12110());
            skillHandlers.Add(12112, new S12112());
            skillHandlers.Add(12113, new S12113());
            skillHandlers.Add(12114, new S12114());
            skillHandlers.Add(12115, new S12115());
            skillHandlers.Add(12116, new S12116());
            skillHandlers.Add(12117, new S12117());
            skillHandlers.Add(12118, new S12118());
            skillHandlers.Add(12119, new S12119());

            skillHandlers.Add(13000, new S13000());
            skillHandlers.Add(13001, new S13001());
            skillHandlers.Add(13002, new S13002());
            skillHandlers.Add(13003, new S13003());
            skillHandlers.Add(13004, new S13004());
            skillHandlers.Add(13100, new S13100());
            skillHandlers.Add(13101, new S13101());
            skillHandlers.Add(13102, new S13102());
            skillHandlers.Add(13103, new S13103());
            skillHandlers.Add(13104, new S13104());
            skillHandlers.Add(13105, new S13105());
            skillHandlers.Add(13106, new S13106());
            skillHandlers.Add(13107, new S13107());
            skillHandlers.Add(13108, new S13108());
            skillHandlers.Add(13109, new S13109());
            skillHandlers.Add(13110, new S13110());
            skillHandlers.Add(13200, new S13200());
            skillHandlers.Add(13201, new S13201());
            skillHandlers.Add(13202, new S13202());
            skillHandlers.Add(13203, new S13203());


            skillHandlers.Add(2571, new S2571());
            skillHandlers.Add(22000, new S22000());

            skillHandlers.Add(14000, new S14000());
            skillHandlers.Add(14001, new S14001());
            skillHandlers.Add(14002, new S14002());
            skillHandlers.Add(14003, new S14003());
            skillHandlers.Add(14004, new S14004());
            skillHandlers.Add(14005, new S14005());
            skillHandlers.Add(14006, new S14006());
            skillHandlers.Add(14007, new S14007());
            skillHandlers.Add(14008, new S14008());
            skillHandlers.Add(14009, new S14009());
            skillHandlers.Add(14010, new S14010());
            skillHandlers.Add(14011, new S14011());
            skillHandlers.Add(14012, new S14012());
            skillHandlers.Add(14020, new S14020());
            skillHandlers.Add(14021, new S14021());
            skillHandlers.Add(14022, new S14022());
            skillHandlers.Add(14030, new S14030());
            skillHandlers.Add(14031, new S14031());
            skillHandlers.Add(14032, new S14032());
            skillHandlers.Add(14040, new S14040());
            skillHandlers.Add(14041, new S14041());
            skillHandlers.Add(14042, new S14042());
            skillHandlers.Add(14055, new S14055());

            skillHandlers.Add(31043, new S31043());
            skillHandlers.Add(31040, new S31040());
            skillHandlers.Add(31044, new S31044());
            skillHandlers.Add(31045, new S31045());
            skillHandlers.Add(31046, new S31046());
            skillHandlers.Add(31047, new S31047());
            skillHandlers.Add(31048, new S31048());
            skillHandlers.Add(31049, new S31049());
            skillHandlers.Add(31050, new S31050());
            skillHandlers.Add(31051, new S31051());
            skillHandlers.Add(31052, new S31052());

            skillHandlers.Add(31072, new S31072());
            skillHandlers.Add(31073, new S31073());
            skillHandlers.Add(31075, new S31075());
            skillHandlers.Add(31076, new S31076());
            skillHandlers.Add(31077, new S31077());
            skillHandlers.Add(31078, new S31078());
            skillHandlers.Add(31079, new S31079());
            skillHandlers.Add(31080, new S31080());
            skillHandlers.Add(31081, new S31081());
            skillHandlers.Add(31086, new S31086());
            skillHandlers.Add(31087, new S31087());
            skillHandlers.Add(31088, new S31088());
            skillHandlers.Add(31089, new S31089());
            skillHandlers.Add(31090, new S31090());
            skillHandlers.Add(31091, new S31091());
            #region 新版技能
            skillHandlers.Add(40000, new S40000());//火炎斩

            #region wiz
            //skillHandlers.Add(16003, new S42000());
            skillHandlers.Add(16001, new S42001());
            skillHandlers.Add(16002, new S42002());
            skillHandlers.Add(16004, new S16004());
            skillHandlers.Add(16005, new S16005());
            //wiz火属性
            skillHandlers.Add(16100, new S42100());
            skillHandlers.Add(16101, new S42101());
            skillHandlers.Add(16102, new S42102());
            skillHandlers.Add(16103, new S42103());
            //wiz水属性
            skillHandlers.Add(16200, new S42200());
            skillHandlers.Add(16201, new S42201());
            skillHandlers.Add(16202, new S42202());
            skillHandlers.Add(16203, new S42203());
            //wiz风属性
            skillHandlers.Add(16300, new S42300());
            skillHandlers.Add(16301, new S42301());
            skillHandlers.Add(16302, new S16302());
            skillHandlers.Add(16303, new S42303());
            //wiz地属性
            skillHandlers.Add(16400, new S42400());
            skillHandlers.Add(16401, new S42401());
            skillHandlers.Add(16403, new S42403());
            //wiz无属性
            skillHandlers.Add(16500, new S42500());
            skillHandlers.Add(16501, new S42501());
            skillHandlers.Add(16502, new S42502());
            skillHandlers.Add(16503, new S42503());
            skillHandlers.Add(16504, new S42504());
            skillHandlers.Add(16505, new S42505());
            skillHandlers.Add(16506, new S42506());
            skillHandlers.Add(16507, new S42507());
            skillHandlers.Add(16508, new S42508());
            skillHandlers.Add(16509, new S42509());
            #endregion

            #region priest
            //priest光属性
            skillHandlers.Add(17000, new S43000());
            skillHandlers.Add(17001, new S43001());
            skillHandlers.Add(17002, new S43002());
            skillHandlers.Add(17003, new S43003());
            skillHandlers.Add(17004, new S43004());
            skillHandlers.Add(17005, new S43005());
            skillHandlers.Add(17006, new S43006());
            skillHandlers.Add(17007, new S43007());
            skillHandlers.Add(17008, new S43008());
            skillHandlers.Add(17009, new S43009());
            //priest暗属性
            skillHandlers.Add(17100, new S43100());
            skillHandlers.Add(17101, new S43101());
            skillHandlers.Add(17102, new S43102());
            skillHandlers.Add(17106, new S43106());
            skillHandlers.Add(17107, new S43107());
            skillHandlers.Add(17108, new S43108());
            //priest死灵系
            skillHandlers.Add(17200, new S43200());
            skillHandlers.Add(17201, new S43201());
            skillHandlers.Add(17202, new S43202());
            skillHandlers.Add(17203, new S43203());
            skillHandlers.Add(17205, new S43205());
            skillHandlers.Add(17206, new S43206());
            skillHandlers.Add(17207, new S43207());
            //priest诗人系
            skillHandlers.Add(17300, new S43300());
            skillHandlers.Add(17302, new S43302());
            //skillHandlers.Add(17302, new S43302());
            skillHandlers.Add(17303, new S43303());
            skillHandlers.Add(17304, new S43304());
            skillHandlers.Add(17305, new S43305());


            //warrior新技能
            skillHandlers.Add(18500, new S18500());
            skillHandlers.Add(18501, new S18501());
            skillHandlers.Add(18502, new S18502());
            skillHandlers.Add(18503, new S18503());
            skillHandlers.Add(18504, new S18504());
            skillHandlers.Add(18600, new S18600());

            //warrior斩系
            skillHandlers.Add(18000, new S40000());
            skillHandlers.Add(18001, new S40001());
            skillHandlers.Add(18002, new S40002());
            skillHandlers.Add(18003, new S40003());
            skillHandlers.Add(18010, new S40010());
            skillHandlers.Add(18011, new S40010());
            skillHandlers.Add(18012, new S40010());
            skillHandlers.Add(18013, new S40010());
            skillHandlers.Add(18020, new S40020());
            skillHandlers.Add(18021, new S40020());
            skillHandlers.Add(18022, new S40020());
            skillHandlers.Add(18023, new S40020());
            skillHandlers.Add(18004, new S40004());
            skillHandlers.Add(18005, new S40005());

            //warrior打系
            /*skillHandlers.Add(18100, new S40100());
            skillHandlers.Add(18101, new S40101());
            skillHandlers.Add(18102, new S40102());
            skillHandlers.Add(18103, new S40103());
            skillHandlers.Add(18104, new S40104());*/

            //warrior刺系
            skillHandlers.Add(18200, new S40200());
            skillHandlers.Add(18201, new S40201());
            skillHandlers.Add(18202, new S40202());
            skillHandlers.Add(18212, new S40010());
            skillHandlers.Add(18222, new S40020());
            skillHandlers.Add(18203, new S40203());
            skillHandlers.Add(18204, new S40204());

            //warrior盾系
            skillHandlers.Add(18300, new S40300());
            skillHandlers.Add(18301, new S40301());
            skillHandlers.Add(18302, new S40302());
            skillHandlers.Add(18303, new S40303());
            //skillHandlers.Add(18304, new S40304());
            //skillHandlers.Add(18305, new S40305());

            //warrior咆哮系
            skillHandlers.Add(18400, new S40400());
            skillHandlers.Add(18401, new S40401());
            skillHandlers.Add(18402, new S40402());
            skillHandlers.Add(18403, new S40403());
            skillHandlers.Add(18404, new S40404());
            skillHandlers.Add(18406, new S40406());

            //Marauder暗杀系
            skillHandlers.Add(19000, new S19000());
            skillHandlers.Add(19001, new S19001());
            skillHandlers.Add(19003, new S19003());
            skillHandlers.Add(19004, new S19004());
            skillHandlers.Add(19005, new S19005());
            skillHandlers.Add(19006, new S19006());
            //skillHandlers.Add(19007, new S19007());

            //Marauder毒系
            skillHandlers.Add(19100, new S19100());
            skillHandlers.Add(19101, new S19101());
            skillHandlers.Add(19102, new S19102());
            skillHandlers.Add(19103, new S19103());
            skillHandlers.Add(19104, new S19104());
            skillHandlers.Add(19105, new S19105());

            //Marauder射击系
            skillHandlers.Add(19200, new S19200());
            skillHandlers.Add(19210, new S19210());
            skillHandlers.Add(19220, new S19220());
            skillHandlers.Add(19201, new S19201());
            skillHandlers.Add(19202, new S19202());
            skillHandlers.Add(19203, new S19203());
            skillHandlers.Add(19204, new S19204());
            skillHandlers.Add(19205, new S19205());
            skillHandlers.Add(19206, new S19206());
            skillHandlers.Add(19207, new S19207());
            skillHandlers.Add(19208, new S19208());
            #endregion

            //ENCHANTSKILL
            skillHandlers.Add(26000, new S26000());
            skillHandlers.Add(26001, new S26001());
            skillHandlers.Add(26002, new S26002());
            skillHandlers.Add(26003, new S26003());
            skillHandlers.Add(26004, new S26004());
            skillHandlers.Add(26005, new S26005());
            skillHandlers.Add(26100, new S26100());
            skillHandlers.Add(26101, new S26101());
            skillHandlers.Add(26102, new S26102());
            skillHandlers.Add(26103, new S26103());
            skillHandlers.Add(26104, new S26104());
            skillHandlers.Add(26105, new S26105());
            skillHandlers.Add(26200, new S26200());
            skillHandlers.Add(26201, new S26201());
            skillHandlers.Add(26202, new S26202());
            skillHandlers.Add(26203, new S26203());
            skillHandlers.Add(26204, new S26204());
            skillHandlers.Add(26205, new S26205());
            skillHandlers.Add(26300, new S26300());
            skillHandlers.Add(26301, new S26301());
            skillHandlers.Add(26302, new S26302());
            skillHandlers.Add(26303, new S26303());
            skillHandlers.Add(26304, new S26304());
            skillHandlers.Add(26305, new S26305());
            skillHandlers.Add(26400, new S26400());
            skillHandlers.Add(26401, new S26401());
            skillHandlers.Add(26402, new S26402());
            skillHandlers.Add(26403, new S26403());
            skillHandlers.Add(26404, new S26404());
            skillHandlers.Add(26405, new S26405());
            skillHandlers.Add(26500, new S26500());
            skillHandlers.Add(26501, new S26501());
            skillHandlers.Add(26502, new S26502());
            skillHandlers.Add(26503, new S26503());
            skillHandlers.Add(26504, new S26504());
            skillHandlers.Add(26505, new S26505());


            #endregion
            skillHandlers.Add(30000, new SagaMap.Skill.SkillDefinations.S30000());
            skillHandlers.Add(30002, new SagaMap.Skill.SkillDefinations.S30002());
            skillHandlers.Add(30003, new SagaMap.Skill.SkillDefinations.S30003());
            skillHandlers.Add(30004, new SagaMap.Skill.SkillDefinations.S30004());
            skillHandlers.Add(30005, new SagaMap.Skill.SkillDefinations.S30005());
            skillHandlers.Add(30006, new SagaMap.Skill.SkillDefinations.S30006());
            skillHandlers.Add(30007, new SagaMap.Skill.SkillDefinations.S30007());
            skillHandlers.Add(30008, new SagaMap.Skill.SkillDefinations.S30008());
            skillHandlers.Add(30009, new SagaMap.Skill.SkillDefinations.S30009());
            skillHandlers.Add(30010, new SagaMap.Skill.SkillDefinations.S30010());
            skillHandlers.Add(30011, new SagaMap.Skill.SkillDefinations.S30011());
            skillHandlers.Add(30012, new SagaMap.Skill.SkillDefinations.S30012());
            skillHandlers.Add(30013, new SagaMap.Skill.SkillDefinations.S30013());
            #region オートメディック?アルマ
            skillHandlers.Add(30015, new SagaMap.Skill.SkillDefinations.S30015());
            skillHandlers.Add(30016, new SagaMap.Skill.SkillDefinations.S30016());
            skillHandlers.Add(30017, new SagaMap.Skill.SkillDefinations.S30017());
            skillHandlers.Add(30018, new SagaMap.Skill.SkillDefinations.S30018());
            skillHandlers.Add(30019, new SagaMap.Skill.SkillDefinations.S30019());
            #endregion
            skillHandlers.Add(30020, new SagaMap.Skill.SkillDefinations.S30020());//血魔枪
            skillHandlers.Add(30021, new SagaMap.Skill.SkillDefinations.S30021());//黑暗雷暴
            skillHandlers.Add(30022, new SagaMap.Skill.SkillDefinations.S30022());//雷冰球
            skillHandlers.Add(30023, new SagaMap.Skill.SkillDefinations.S30023());//鲜血之路
            skillHandlers.Add(30024, new SagaMap.Skill.SkillDefinations.S30024());//天降神剑
            skillHandlers.Add(30025, new SagaMap.Skill.SkillDefinations.S30025());//普攻1
            skillHandlers.Add(30026, new SagaMap.Skill.SkillDefinations.S30026());//寒冰之夜
            skillHandlers.Add(30027, new SagaMap.Skill.SkillDefinations.S30027());//爆炸追击


            skillHandlers.Add(30028, new SagaMap.Skill.SkillDefinations.S30028());//冰棍的制作方式
            skillHandlers.Add(30029, new SagaMap.Skill.SkillDefinations.S30029());//来打雪仗吧
            skillHandlers.Add(30030, new SagaMap.Skill.SkillDefinations.S30030());//我要吃冰棍
            skillHandlers.Add(30031, new SagaMap.Skill.SkillDefinations.S30031());//快点下雪吧
            skillHandlers.Add(30032, new SagaMap.Skill.SkillDefinations.S30032());//在雪山怎么少得了雪崩
            skillHandlers.Add(30033, new SagaMap.Skill.SkillDefinations.S30033());//终极冻结

            skillHandlers.Add(31000, new SagaMap.Skill.SkillDefinations.S31000());//死亡滚球
            skillHandlers.Add(31001, new SagaMap.Skill.SkillDefinations.S31001());//娱乐马戏
            skillHandlers.Add(31002, new SagaMap.Skill.SkillDefinations.S31002());//我的守护骑士们
            skillHandlers.Add(31003, new SagaMap.Skill.SkillDefinations.S31003());//陷阱戏法
            skillHandlers.Add(31004, new SagaMap.Skill.SkillDefinations.S31004());//天击地裂
            skillHandlers.Add(31005, new SagaMap.Skill.SkillDefinations.S31005());//死亡滚球小怪的自爆

            skillHandlers.Add(31006, new SagaMap.Skill.SkillDefinations.S31006());//暗影猎杀
            skillHandlers.Add(31007, new SagaMap.Skill.SkillDefinations.S31007());//飞燕
            skillHandlers.Add(31008, new SagaMap.Skill.SkillDefinations.S31008());//无影雷
            skillHandlers.Add(31009, new SagaMap.Skill.SkillDefinations.S31009());//出来吧！我的追随者们！
            skillHandlers.Add(31010, new SagaMap.Skill.SkillDefinations.S31010());//地狱之火
            skillHandlers.Add(31011, new SagaMap.Skill.SkillDefinations.S31011());//死神召唤

            skillHandlers.Add(31020, new S31020());//大恶鬼之修罗狱
            skillHandlers.Add(31021, new S31021());//恶鬼之道
            skillHandlers.Add(31022, new S31022());//鬼魂统御
            skillHandlers.Add(31023, new S31023());//鬼魂统御
            skillHandlers.Add(31024, new S31024());//虚幻斩
            skillHandlers.Add(31025, new S31025());//鬼之影
            skillHandlers.Add(31026, new S31026());//修罗-寒冰斩
            skillHandlers.Add(31027, new S31027());//修罗-寒冰斩
            skillHandlers.Add(31028, new S31028());//修罗-寒冰斩
            skillHandlers.Add(31029, new S31029());//修罗-寒冰斩
            skillHandlers.Add(31030, new S31030());//修罗-寒冰斩
            skillHandlers.Add(31031, new S31031());//修罗-寒冰斩
            skillHandlers.Add(31032, new S31032());//修罗-寒冰斩
            skillHandlers.Add(31033, new S31033());//修罗-寒冰斩
            skillHandlers.Add(31035, new S31035());//修罗-寒冰斩
            skillHandlers.Add(31036, new S31036());//修罗-寒冰斩
            skillHandlers.Add(31037, new S31037());//修罗-寒冰斩
            skillHandlers.Add(31038, new S31038());//修罗-寒冰斩

            skillHandlers.Add(31053, new S31053());//大恶鬼之修罗狱
            skillHandlers.Add(31054, new S31054());//恶鬼之道
            skillHandlers.Add(31055, new S31055());//鬼魂统御
            skillHandlers.Add(31056, new S31056());//鬼魂统御
            skillHandlers.Add(31057, new S31057());//虚幻斩
            skillHandlers.Add(31058, new S31058());//鬼之影
            skillHandlers.Add(31059, new S31059());//修罗-寒冰斩
            skillHandlers.Add(31060, new S31060());//修罗-寒冰斩
            skillHandlers.Add(31061, new S31061());//修罗-寒冰斩
            skillHandlers.Add(31062, new S31062());//修罗-寒冰斩
            skillHandlers.Add(31063, new S31063());//修罗-寒冰斩
            skillHandlers.Add(31064, new S31064());//修罗-寒冰斩
            skillHandlers.Add(31065, new S31065());//修罗-寒冰斩
            skillHandlers.Add(31066, new S31066());//修罗-寒冰斩
            skillHandlers.Add(31067, new S31067());//修罗-寒冰斩
            skillHandlers.Add(31068, new S31068());//修罗-寒冰斩
            skillHandlers.Add(31069, new S31069());//修罗-寒冰斩
            skillHandlers.Add(31070, new S31070());//修罗-寒冰斩
            skillHandlers.Add(31071, new S31071());//修罗-寒冰斩

            skillHandlers.Add(2178, new SagaMap.Skill.SkillDefinations.Swordman.EmergencyAvoid());
            skillHandlers.Add(2187, new SagaMap.Skill.SkillDefinations.Swordman.MagicHit());
            skillHandlers.Add(2153, new SagaMap.Skill.SkillDefinations.Archer.StunBullet());
            skillHandlers.Add(2154, new SagaMap.Skill.SkillDefinations.Archer.BoomBullet());
            skillHandlers.Add(2155, new SagaMap.Skill.SkillDefinations.Archer.PoisonBullet());
            skillHandlers.Add(2156, new SagaMap.Skill.SkillDefinations.Archer.StoneBullet());
            skillHandlers.Add(2159, new SagaMap.Skill.SkillDefinations.Archer.SilenceBullet());
            skillHandlers.Add(2162, new SagaMap.Skill.SkillDefinations.Archer.FreezeBullet());
            skillHandlers.Add(601, new SagaMap.Skill.SkillDefinations.Archer.AssassinateAttack());
            skillHandlers.Add(602, new SagaMap.Skill.SkillDefinations.Archer.Assassinate());
            skillHandlers.Add(2266, new SagaMap.Skill.SkillDefinations.Archer.CrazyArrow());
            skillHandlers.Add(2543, new SagaMap.Skill.SkillDefinations.Archer.mie());
            skillHandlers.Add(3271, new SagaMap.Skill.SkillDefinations.Archer.umi());

            //MobskillHandlers.Add(20017, new SagaMap.Skill.SkillDefinations.X.BlackHoleOfPP());
            #region 巨大咕咕鸡
            MobskillHandlers.Add(20000, new SagaMap.Skill.SkillDefinations.X.BlackHole());
            MobskillHandlers.Add(20001, new SagaMap.Skill.SkillDefinations.X.GuguPoison());
            #endregion

            #region 熊爹
            MobskillHandlers.Add(20005, new SagaMap.Skill.SkillDefinations.X.IceHole());//废弃
            MobskillHandlers.Add(20006, new SagaMap.Skill.SkillDefinations.X.Rowofcloudpalm());//废弃
            MobskillHandlers.Add(20007, new SagaMap.Skill.SkillDefinations.X.Fengshenlegs());//废弃
            MobskillHandlers.Add(20009, new SagaMap.Skill.SkillDefinations.X.Attack());
            #endregion

            #region 领主骑士
            MobskillHandlers.Add(20010, new SagaMap.Skill.SkillDefinations.X.KnightAttack());
            MobskillHandlers.Add(20011, new SagaMap.Skill.SkillDefinations.X.IceHeart());
            MobskillHandlers.Add(20012, new SagaMap.Skill.SkillDefinations.X.Iceroad());
            MobskillHandlers.Add(20013, new SagaMap.Skill.SkillDefinations.X.IceDef());
            #endregion

            #region 天骸鸢
            MobskillHandlers.Add(20015, new SkillDefinations.Elementaler.FireInfernal());
            #endregion

            //skillHandlers.Add(20002, new SagaMap.Skill.SkillDefinations.Wizard.EnergyOneForWeapon());
            //skillHandlers.Add(20004, new SagaMap.Skill.SkillDefinations.Swordman.IaiForWeapon());
            skillHandlers.Add(402, new SkillDefinations.Global.MaxHealMpForWeapon());
            //skillHandlers.Add(20014, new SagaMap.Skill.SkillDefinations.Swordman.Snipe());



            #region c-1 new skill
            skillHandlers.Add(8900, new SkillDefinations.C1skill.ShadowBlast());
            #endregion
            #region Royaldealer
            skillHandlers.Add(989, new SkillDefinations.Royaldealer.Royaldealer());//12月2日实装,lv3    
            skillHandlers.Add(3361, new SkillDefinations.Royaldealer.CAPACommunion());//12月2日实装,lv6
            skillHandlers.Add(3371, new SkillDefinations.Royaldealer.BurnRate());//12月3日实装,lv10(必中未装)
            skillHandlers.Add(2491, new SkillDefinations.Royaldealer.DamageUp());//12月3日实装,lv13
            //noLv20
            skillHandlers.Add(2501, new SkillDefinations.Royaldealer.CriUp());//12月3日实装,lv23
            //noLv25
            skillHandlers.Add(3404, new SkillDefinations.Royaldealer.Rhetoric());//12月3日实装,lv30(强运系统未装)
            //no 35 40 45
            #endregion
            #region Stryder
            skillHandlers.Add(2482, new SkillDefinations.Stryder.Xusihaxambi());//12月2日实装，lv3
            skillHandlers.Add(3352, new SkillDefinations.Stryder.SPCommunion());//12月2日实装，lv6
            //lv10被动不知作用
            skillHandlers.Add(2490, new SkillDefinations.Stryder.StrapFlurry());//12月2日实装，lv13（拖拽尚未实现）
            skillHandlers.Add(3382, new SkillDefinations.Stryder.BannedOutfit());//12月2日实装，lv20（只实现显示BUFF效果）  
            //lv23被动不知作用
            skillHandlers.Add(3385, new SkillDefinations.Stryder.SkillForbid());//12月2日实装，lv25（只实现显示BUFF效果）
            skillHandlers.Add(3402, new SkillDefinations.Stryder.PartyBivouac());//12月2日实装，lv30
            skillHandlers.Add(3403, new SkillDefinations.Stryder.FlurryThunderbolt());//12月2日实装，lv35
            //缺少35、40
            #endregion
            #region Maestro
            skillHandlers.Add(2480, new SkillDefinations.Maestro.WeaponStrengthen());//12月1日实装，lv3（未完成，需要封包）
            skillHandlers.Add(3353, new SkillDefinations.Maestro.ATKCommunion());//12月1日实装，lv6
            skillHandlers.Add(987, new SkillDefinations.Maestro.GreatMaster());//12月1日实装,lv10（加成不明确）
            skillHandlers.Add(2487, new SkillDefinations.Maestro.PotentialWeapon());//12月1日实装，lv13（未完成，需要封包）
            skillHandlers.Add(2489, new SkillDefinations.Maestro.RobotAtkUp());//12月1日实装，lv20
            skillHandlers.Add(2500, new SkillDefinations.Maestro.RobotDefUp());//12月1日实装，lv23
            skillHandlers.Add(2506, new SkillDefinations.Maestro.RobotCSPDUp());//12月1日实装，lv25
            skillHandlers.Add(3401, new SkillDefinations.Maestro.WeaponAtkUp());//12月1日实装，lv30
            //缺少35、40
            skillHandlers.Add(2524, new SkillDefinations.Maestro.RobotLaser());//12月2日实装，lv45
            #endregion
            #region Guardian
            skillHandlers.Add(983, new SkillDefinations.Guardian.SpearMaster());//11月24日实装，LV3习得
            skillHandlers.Add(3355, new SkillDefinations.Guardian.def_addCommunion());
            skillHandlers.Add(3363, new SkillDefinations.Guardian.Guardian());//11月24日实装，LV10习得
            skillHandlers.Add(1102, new SkillDefinations.Guardian.Reflex());//11月24日实装，LV20习得
            skillHandlers.Add(2512, new SkillDefinations.Guardian.ShieldImpact());//11月24日实装，lv30习得
            skillHandlers.Add(2513, new SkillDefinations.Guardian.HelixBeedrill());//11月24日实装，lv35习得
            skillHandlers.Add(2533, new SkillDefinations.Guardian.RustBody());//11月24日实装，lv40习得
            skillHandlers.Add(2532, new SkillDefinations.Guardian.Parry());//11月24日实装，lv45习得
            skillHandlers.Add(1101, new SkillDefinations.Guardian.HatredUp());//11月25日实装，lv13习得

            #endregion
            #region Eraser
            skillHandlers.Add(984, new SkillDefinations.Eraser.EraserMaster());//11月24日实装，lv3习得
            skillHandlers.Add(3358, new SkillDefinations.Eraser.AVOIDCommunion());
            skillHandlers.Add(3364, new SkillDefinations.Eraser.Purger());//11月24日实装，lv10习得
            skillHandlers.Add(2486, new SkillDefinations.Eraser.Efuikasu());//11月24日实装，Lv20习得
            skillHandlers.Add(3387, new SkillDefinations.Eraser.Syaringan());//11月24日实装，lv25习得
            skillHandlers.Add(2516, new SkillDefinations.Eraser.EvilSpirit());//11月24日实装，lv30习得
            skillHandlers.Add(2508, new SkillDefinations.Eraser.Demacia());//11月24日实装，lv35习得
            skillHandlers.Add(2529, new SkillDefinations.Eraser.ShadowSeam());//11月24日实装，lv40习得//未完成
            #endregion
            #region Hawkeye
            skillHandlers.Add(3357, new SkillDefinations.Hawkeye.HITCommunion());
            skillHandlers.Add(985, new SkillDefinations.Hawkeye.HawkeyeMaster());//11月24日实装，lv3习得
            skillHandlers.Add(3365, new SkillDefinations.Hawkeye.EagleEye());//11月24日实装，lv10习得
            skillHandlers.Add(1103, new SkillDefinations.Hawkeye.Nooheito());//11月25日实装，lv13习得
            skillHandlers.Add(2485, new SkillDefinations.Hawkeye.SmokeBall());//11月25日实装，lv20习得
            skillHandlers.Add(1107, new SkillDefinations.Hawkeye.MissRevenge());//11月25日实装，lv23习得
            skillHandlers.Add(2504, new SkillDefinations.Hawkeye.WithinWeeks());//11月25日实装，lv25习得
            skillHandlers.Add(2514, new SkillDefinations.Hawkeye.TimeBomb());//11月25日实装，lv30习得
            skillHandlers.Add(2507, new SkillDefinations.Hawkeye.PointRain());//11月25日实装，lv35习得
            skillHandlers.Add(2531, new SkillDefinations.Hawkeye.LoboCall());//11月25日实装，lv40习得
            skillHandlers.Add(2530, new SkillDefinations.Hawkeye.SejiwuiPoint());//11月25日实装，lv45习得
            #endregion
            #region ForceMaster
            skillHandlers.Add(986, new SkillDefinations.ForceMaster.PlusElement());//11月25日实装
            skillHandlers.Add(3359, new SkillDefinations.ForceMaster.CSPDCommunion());
            //skillHandlers.Add(3366, new SkillDefinations.ForceMaster.ForceMaster());
            skillHandlers.Add(3375, new SkillDefinations.ForceMaster.DecreaseWeapon());//11月25日实装
            skillHandlers.Add(1105, new SkillDefinations.ForceMaster.ForceShield());//11月25日实装
            skillHandlers.Add(3383, new SkillDefinations.ForceMaster.DecreaseShield());//11月26日实装
            skillHandlers.Add(3388, new SkillDefinations.ForceMaster.BarrierShield());//11月26日实装
            skillHandlers.Add(3395, new SkillDefinations.ForceMaster.ForceWave());//11月26日实装,lv30（未完成实装）
            skillHandlers.Add(3394, new SkillDefinations.ForceMaster.ThunderSpray());//11月26日实装，lv35
            skillHandlers.Add(3419, new SkillDefinations.ForceMaster.AdobannaSubiritei());//11月26日实装，lv40
            skillHandlers.Add(3418, new SkillDefinations.ForceMaster.ShockWave());//11月26日实装,lv45
            #endregion
            #region Astralist
            skillHandlers.Add(3372, new SkillDefinations.Astralist.DelayOut());//11月26日实装,lv3
            skillHandlers.Add(3351, new SkillDefinations.Astralist.MPCommunion());
            //skillHandlers.Add(3367, new SkillDefinations.Astralist.Astralist());//11月26日实装,lv10
            //skillHandlers.Add(3377, new SkillDefinations.Astralist.TranceBody());//11月26日实装,lv13（未完成）
            //skillHandlers.Add(3378, new SkillDefinations.Astralist.Relement());//11月26日实装,lv20（未完成）
            //skillHandlers.Add(3384, new SkillDefinations.Astralist.Amplement());//11月26日实装,lv23
            skillHandlers.Add(3389, new SkillDefinations.Astralist.YugenKeiyaku());//11月26日实装,lv25（未完成）
            skillHandlers.Add(3409, new SkillDefinations.Astralist.ElementGun());//11月29日实装,lv30
            skillHandlers.Add(3398, new SkillDefinations.Astralist.EarthQuake());//11月29日实装,lv35
            skillHandlers.Add(3417, new SkillDefinations.Astralist.Contract());//11月29日实装,lv40（未完成）
            skillHandlers.Add(3416, new SkillDefinations.Astralist.FlashStorm());//11月29日实装

            #endregion
            #region Cardinal
            skillHandlers.Add(3373, new SkillDefinations.Cardinal.Frustrate());//11月30日实装
            //skillHandlers.Add(3356, new SkillDefinations.Cardinal.mdef_addCommunion());
            skillHandlers.Add(3368, new SkillDefinations.Cardinal.Cardinal());//11月30日实装
            skillHandlers.Add(3380, new SkillDefinations.Cardinal.StateCure());//11月30日实装
            skillHandlers.Add(1109, new SkillDefinations.Cardinal.AutoHeal());//11月30日实装,lv25
            skillHandlers.Add(3399, new SkillDefinations.Cardinal.CoronaHit());//11月30日实装,lv30
            skillHandlers.Add(3393, new SkillDefinations.Cardinal.HolySacred());//11月30日实装,lv35
            skillHandlers.Add(3415, new SkillDefinations.Cardinal.FischerCary());//11月30日实装,lv40
            skillHandlers.Add(3414, new SkillDefinations.Cardinal.RailGun());//11月30日实装,lv45
            #endregion
            #region SoulTaker
            skillHandlers.Add(3374, new SkillDefinations.SoulTaker.MegaDarkBlaze());
            //skillHandlers.Add(3354, new SkillDefinations.SoulTaker.MATKCommunion());
            //skillHandlers.Add(3369, new SkillDefinations.SoulTaker.SoulTaker());
            skillHandlers.Add(3376, new SkillDefinations.SoulTaker.Transition());//11月30日实装，lv20
            skillHandlers.Add(1110, new SkillDefinations.SoulTaker.SoulTakerMaster());//11月30日实装，lv23
            skillHandlers.Add(3397, new SkillDefinations.SoulTaker.DarkChains());//11月30日实装，lv30
            skillHandlers.Add(3392, new SkillDefinations.SoulTaker.Chasm());//11月30日实装，lv35
            skillHandlers.Add(3420, new SkillDefinations.SoulTaker.DeathSickle());//11月30实装,lv40
            skillHandlers.Add(2526, new SkillDefinations.SoulTaker.fuenriru());//11月30实装,lv45
            #endregion
            //skillHandlers.Add(1606, new SkillDefinations.Monster.Long());
            #region Harvest
            skillHandlers.Add(2481, new SkillDefinations.Harvest.EquipCompose());//12月2日实装，lv3（未完成，需要封包）
            skillHandlers.Add(3360, new SkillDefinations.Harvest.CAPACommunion());//12月2日实装，lv6
            skillHandlers.Add(3370, new SkillDefinations.Harvest.Twine());//12月2日,lv10（未实装，可能需要新的debuff）
            skillHandlers.Add(2488, new SkillDefinations.Harvest.PotentialArmor());//12月2日，lv13（未完成，需要封包）
            skillHandlers.Add(3381, new SkillDefinations.Harvest.TwineSleep());//12月2日,lv20（未实装，可能需要新的debuff）
            skillHandlers.Add(2505, new SkillDefinations.Harvest.EquipComposeCancel());//12月2日实装，lv23（未完成，需要封包）
            skillHandlers.Add(2497, new SkillDefinations.Harvest.Bounce());//12月2日完成实装,lv25
            skillHandlers.Add(2510, new SkillDefinations.Harvest.Winder());//12月2日,lv30（未完成，需要封包）
            skillHandlers.Add(3400, new SkillDefinations.Harvest.CreateNeko());//12月2日,lv35（未完成，需要召唤物技能）
            //缺少40、45
            #endregion
            skillHandlers.Add(3432, new SkillDefinations.Global.ElementStar());
            skillHandlers.Add(101, new SkillDefinations.Global.MaxMPUp());
            skillHandlers.Add(103, new SkillDefinations.Global.HPRecoverUP());
            skillHandlers.Add(104, new SkillDefinations.Global.MPRecoverUP());
            skillHandlers.Add(105, new SkillDefinations.Global.SPRecoverUP());
            skillHandlers.Add(108, new SkillDefinations.Global.AxeMastery());
            skillHandlers.Add(111, new SkillDefinations.Global.MaceMastery());
            skillHandlers.Add(121, new SkillDefinations.Global.TwoMaceMastery());
            skillHandlers.Add(128, new SkillDefinations.Global.TwoHandGunMastery());
            skillHandlers.Add(200, new SkillDefinations.Global.SwordHitUP());
            skillHandlers.Add(202, new SkillDefinations.Global.SpearHitUP());
            skillHandlers.Add(204, new SkillDefinations.Global.AxeHitUP());
            skillHandlers.Add(206, new SkillDefinations.Global.ShortSwordHitUP());
            skillHandlers.Add(208, new SkillDefinations.Global.MaceHitUP());
            skillHandlers.Add(113, new SkillDefinations.Global.BowMastery());
            skillHandlers.Add(903, new SkillDefinations.Global.Identify());
            skillHandlers.Add(907, new SkillDefinations.Global.ThrowHitUP());
            skillHandlers.Add(2021, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(4026, new SkillDefinations.BladeMaster.A_T_PJoint());
            skillHandlers.Add(978, new SkillDefinations.Global.AtkUpByPt());
            skillHandlers.Add(959, new SkillDefinations.Global.RiskAversion());
            skillHandlers.Add(712, new SkillDefinations.Global.Camp());

            skillHandlers.Add(1602, new SkillDefinations.Global.EventMoveSkill());

            skillHandlers.Add(10502, new SkillDefinations.Global.Fish());

            skillHandlers.Add(2078, new SkillDefinations.Repair.MetalRepair());

            #region System
            skillHandlers.Add(3250, new SkillDefinations.FGarden.FGRope());
            #endregion

            #region Mob
            skillHandlers.Add(6444, new SkillDefinations.Monster.Curse());
            skillHandlers.Add(7500, new SkillDefinations.Monster.PoisonPerfume());
            skillHandlers.Add(7501, new SkillDefinations.Monster.RockStone());
            skillHandlers.Add(7502, new SkillDefinations.Monster.ParaizBan());
            skillHandlers.Add(7503, new SkillDefinations.Monster.SleepStrike());
            skillHandlers.Add(7504, new SkillDefinations.Monster.SilentGreen());
            skillHandlers.Add(7505, new SkillDefinations.Monster.SlowLogic());
            skillHandlers.Add(7506, new SkillDefinations.Monster.ConfuseStack());
            skillHandlers.Add(7507, new SkillDefinations.Monster.IceFade());
            skillHandlers.Add(7508, new SkillDefinations.Monster.StunAttack());
            skillHandlers.Add(7509, new SkillDefinations.Monster.EnergyAttack());
            skillHandlers.Add(7510, new SkillDefinations.Monster.FireAttack());
            skillHandlers.Add(7511, new SkillDefinations.Monster.WaterAttack());
            skillHandlers.Add(7512, new SkillDefinations.Monster.WindAttack());
            skillHandlers.Add(7513, new SkillDefinations.Monster.EarthAttack());
            skillHandlers.Add(7514, new SkillDefinations.Monster.LightBallad());
            skillHandlers.Add(7515, new SkillDefinations.Monster.DarkBallad());
            skillHandlers.Add(7516, new SkillDefinations.Monster.Blow());
            skillHandlers.Add(7517, new SkillDefinations.Monster.ConfuseBlow());
            skillHandlers.Add(7518, new SkillDefinations.Monster.StunBlow());
            skillHandlers.Add(7519, new SkillDefinations.Monster.MobHealing());
            skillHandlers.Add(7520, new SkillDefinations.Monster.MobHealing1());
            skillHandlers.Add(7521, new SkillDefinations.Monster.MobAshibarai());
            skillHandlers.Add(7522, new SkillDefinations.Monster.Brandish());
            skillHandlers.Add(7523, new SkillDefinations.Monster.Rush());
            skillHandlers.Add(7524, new SkillDefinations.Monster.Iai());
            skillHandlers.Add(7525, new SkillDefinations.Monster.KabutoWari());
            skillHandlers.Add(7526, new SkillDefinations.Monster.MobBokeboke());
            skillHandlers.Add(7527, new SkillDefinations.Monster.ShockWave());
            skillHandlers.Add(7528, new SkillDefinations.BladeMaster.aStormSword(true));
            skillHandlers.Add(7529, new SkillDefinations.Monster.Phalanx());
            skillHandlers.Add(7530, new SkillDefinations.Monster.WarCry());
            skillHandlers.Add(7531, new SkillDefinations.Monster.ExciaMation());
            skillHandlers.Add(7532, new SkillDefinations.Monster.IceArrow());
            skillHandlers.Add(7533, new SkillDefinations.Monster.DarkOne());
            skillHandlers.Add(7534, new SkillDefinations.Monster.WaterStorm());
            skillHandlers.Add(7535, new SkillDefinations.Monster.DarkStorm());
            skillHandlers.Add(7536, new SkillDefinations.Monster.WaterGroove());
            skillHandlers.Add(7537, new SkillDefinations.Monster.MobParalyzeblow());
            skillHandlers.Add(7538, new SkillDefinations.Monster.MobFireart());
            skillHandlers.Add(7539, new SkillDefinations.Monster.MobWaterart());
            skillHandlers.Add(7540, new SkillDefinations.Monster.MobEarthart());
            skillHandlers.Add(7541, new SkillDefinations.Monster.MobWindart());
            skillHandlers.Add(7542, new SkillDefinations.Monster.MobLightart());
            skillHandlers.Add(7543, new SkillDefinations.Monster.MobDarkart());
            skillHandlers.Add(7544, new SkillDefinations.Monster.MobTrSilenceAtk());
            skillHandlers.Add(7545, new SkillDefinations.Monster.MobTrPoisonAtk());
            skillHandlers.Add(7546, new SkillDefinations.Monster.MobTrPoisonCircle());
            skillHandlers.Add(7547, new SkillDefinations.Monster.MobTrStuinCircle());
            skillHandlers.Add(7548, new SkillDefinations.Monster.MobTrSleepCircle());
            skillHandlers.Add(7549, new SkillDefinations.Monster.MobTrSilenceCircle());
            skillHandlers.Add(7550, new SkillDefinations.Monster.MagPoison());
            skillHandlers.Add(7551, new SkillDefinations.Monster.MagSleep());
            skillHandlers.Add(7552, new SkillDefinations.Monster.MagSlow());
            skillHandlers.Add(7553, new SkillDefinations.Monster.StoneCircle());
            skillHandlers.Add(7554, new SkillDefinations.Monster.HiPoisonCircie());
            skillHandlers.Add(7555, new SkillDefinations.Monster.IceCircle());
            skillHandlers.Add(7556, new SkillDefinations.Monster.HiPoisonCircie());
            skillHandlers.Add(7557, new SkillDefinations.Monster.DeadlyPoison());
            skillHandlers.Add(7558, new SkillDefinations.Monster.MobPerfectcritical());
            skillHandlers.Add(7559, new SkillDefinations.Global.SumSlaveMob(10010100));//古代咕咕雞
            skillHandlers.Add(7560, new SkillDefinations.Global.SumSlaveMob(26000000));//ブリキングRX１
            skillHandlers.Add(7561, new SkillDefinations.Global.SumSlaveMob(10080100));//テンタクル
            skillHandlers.Add(7562, new SkillDefinations.Global.SumSlaveMob(10040100));//ワスプ
            skillHandlers.Add(7563, new SkillDefinations.Global.SumSlaveMob(10030400));//ポーラーベア
            skillHandlers.Add(7564, new SkillDefinations.Monster.FireHighStorm());
            skillHandlers.Add(7565, new SkillDefinations.Monster.WindHighWave());
            skillHandlers.Add(7566, new SkillDefinations.Monster.WindHighStorm());
            skillHandlers.Add(7567, new SkillDefinations.Monster.FireOne());
            skillHandlers.Add(7568, new SkillDefinations.Monster.FireStorm());
            skillHandlers.Add(7569, new SkillDefinations.Monster.WindStorm());
            skillHandlers.Add(7570, new SkillDefinations.Monster.EarthStorm());
            skillHandlers.Add(7571, new SkillDefinations.Monster.LightOne());
            skillHandlers.Add(7572, new SkillDefinations.Monster.DarkHighOne());
            skillHandlers.Add(7573, new SkillDefinations.Enchanter.PoisonMash(true));
            skillHandlers.Add(7574, new SkillDefinations.Monster.MobAvoupSelf());
            skillHandlers.Add(7575, new SkillDefinations.Wizard.EnergyShield(true));
            skillHandlers.Add(7576, new SkillDefinations.Wizard.MagicShield(true));
            skillHandlers.Add(7577, new SkillDefinations.Monster.MobAtkupOne());
            skillHandlers.Add(7578, new SkillDefinations.Monster.MobCharge());
            skillHandlers.Add(7579, new SkillDefinations.Global.SumSlaveMob(10030903));//黑熊
            skillHandlers.Add(7580, new SkillDefinations.Global.SumSlaveMob(26180002));//皮格夫
            skillHandlers.Add(7581, new SkillDefinations.Global.SumSlaveMob(26100003));//木魚
            skillHandlers.Add(7582, new SkillDefinations.Monster.MobTrSleep());
            skillHandlers.Add(7583, new SkillDefinations.Monster.MobTrStun());
            skillHandlers.Add(7584, new SkillDefinations.Monster.MobTrSilence());
            skillHandlers.Add(7585, new SkillDefinations.Monster.MobConfPoisonCircle());
            skillHandlers.Add(7586, new SkillDefinations.Enchanter.ElementCircle(Elements.Fire, true));
            skillHandlers.Add(7587, new SkillDefinations.Enchanter.ElementCircle(Elements.Wind, true));
            skillHandlers.Add(7588, new SkillDefinations.Enchanter.ElementCircle(Elements.Water, true));
            skillHandlers.Add(7589, new SkillDefinations.Enchanter.ElementCircle(Elements.Earth, true));
            skillHandlers.Add(7590, new SkillDefinations.Enchanter.ElementCircle(Elements.Holy, true));
            skillHandlers.Add(7591, new SkillDefinations.Enchanter.ElementCircle(Elements.Dark, true));
            skillHandlers.Add(7592, new SkillDefinations.Global.SumSlaveMob(26180000));//得菩提
            skillHandlers.Add(7593, new SkillDefinations.Global.SumSlaveMob(26100000));//雷魚
            skillHandlers.Add(7594, new SkillDefinations.Global.SumSlaveMob(10030900));//黑熊
            skillHandlers.Add(7595, new SkillDefinations.Global.SumSlaveMob(10310006));//艾卡納J牌
            skillHandlers.Add(7596, new SkillDefinations.Global.SumSlaveMob(10250003));//得菩提
            skillHandlers.Add(7597, new SkillDefinations.Global.SumSlaveMob(30490000, 1));//巨大咕咕銅像
            skillHandlers.Add(7598, new SkillDefinations.Global.SumSlaveMob(30500000, 1));//破壞MkII銅像
            skillHandlers.Add(7599, new SkillDefinations.Global.SumSlaveMob(30510000, 1));//皇路普銅像
            skillHandlers.Add(7600, new SkillDefinations.Global.SumSlaveMob(30520000, 1));//螫針蜂銅像
            skillHandlers.Add(7601, new SkillDefinations.Global.SumSlaveMob(30530000, 1));//白熊銅像

            skillHandlers.Add(7605, new SkillDefinations.Global.SumSlaveMob(30150005, 4));//雑草
            skillHandlers.Add(7606, new SkillDefinations.Monster.MobMeteo());
            skillHandlers.Add(7607, new SkillDefinations.Monster.MobDoughnutFireWall());
            skillHandlers.Add(7608, new SkillDefinations.Monster.MobReflection());

            skillHandlers.Add(7609, new SkillDefinations.Monster.MobElementLoad(7664));//燃燒的路
            skillHandlers.Add(7610, new SkillDefinations.Monster.MobElementLoad(7665));//凍結的路
            skillHandlers.Add(7611, new SkillDefinations.Monster.MobElementLoad(7666));//螺旋風！
            skillHandlers.Add(7612, new SkillDefinations.Monster.MobElementLoad(7667));//私家路
            skillHandlers.Add(7613, new SkillDefinations.Monster.MobElementLoad(7668));//死神
            skillHandlers.Add(7614, new SkillDefinations.Monster.MobElementRandcircle(7669, 5));
            skillHandlers.Add(7615, new SkillDefinations.Monster.MobElementRandcircle(7670, 5));
            skillHandlers.Add(7616, new SkillDefinations.Monster.MobElementRandcircle(7671, 5));
            skillHandlers.Add(7617, new SkillDefinations.Monster.MobElementRandcircle(7672, 5));
            skillHandlers.Add(7618, new SkillDefinations.Monster.MobElementRandcircle(7673, 5));
            skillHandlers.Add(7619, new SkillDefinations.Monster.MobElementRandcircle(7674, 3));
            skillHandlers.Add(7620, new SkillDefinations.Monster.MobElementRandcircle(7675, 3));
            skillHandlers.Add(7621, new SkillDefinations.Monster.MobElementRandcircle(7676, 3));
            skillHandlers.Add(7622, new SkillDefinations.Monster.MobElementRandcircle(7677, 3));
            skillHandlers.Add(7623, new SkillDefinations.Monster.MobElementRandcircle(7678, 3));

            skillHandlers.Add(7648, new SkillDefinations.Monster.MobVitdownOne());
            skillHandlers.Add(7649, new SkillDefinations.Monster.MobCircleAtkup());
            skillHandlers.Add(7650, new SkillDefinations.Enchanter.GravityFall(true));
            skillHandlers.Add(7651, new SkillDefinations.Sage.AReflection());
            skillHandlers.Add(7652, new SkillDefinations.Monster.DelayCancel());
            skillHandlers.Add(7653, new SkillDefinations.Monster.MobCharge3());
            skillHandlers.Add(7654, new SkillDefinations.Global.SumMob(30150007));
            skillHandlers.Add(7655, new SkillDefinations.Global.SumMob(30130003));
            skillHandlers.Add(7656, new SkillDefinations.Global.SumMob(30130005));
            skillHandlers.Add(7657, new SkillDefinations.Global.SumMob(30130007));
            skillHandlers.Add(7658, new SkillDefinations.Global.SumMob(30070052));
            skillHandlers.Add(7659, new SkillDefinations.Global.SumMob(30070054));
            skillHandlers.Add(7660, new SkillDefinations.Global.SumMob(30070056));
            skillHandlers.Add(7661, new SkillDefinations.Global.SumMob(30070058));
            skillHandlers.Add(7662, new SkillDefinations.Global.SumMob(30070060));
            skillHandlers.Add(7663, new SkillDefinations.Global.SumMob(30070062));
            skillHandlers.Add(7664, new SkillDefinations.Monster.MobElementLoadSeq(Elements.Fire));//燃燒的路
            skillHandlers.Add(7665, new SkillDefinations.Monster.MobElementLoadSeq(Elements.Water));//凍結的路
            skillHandlers.Add(7666, new SkillDefinations.Monster.MobElementLoadSeq(Elements.Wind));//螺旋風！
            skillHandlers.Add(7667, new SkillDefinations.Monster.MobElementLoadSeq(Elements.Earth));//私家路
            skillHandlers.Add(7668, new SkillDefinations.Monster.MobElementLoadSeq(Elements.Dark));//死神

            skillHandlers.Add(7669, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Fire));
            skillHandlers.Add(7670, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Water));
            skillHandlers.Add(7671, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Wind));
            skillHandlers.Add(7672, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Earth));
            skillHandlers.Add(7673, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Dark));
            skillHandlers.Add(7674, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Fire));
            skillHandlers.Add(7675, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Water));
            skillHandlers.Add(7676, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Wind));
            skillHandlers.Add(7677, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Earth));
            skillHandlers.Add(7678, new SkillDefinations.Monster.MobElementRandcircleSeq(Elements.Dark));
            skillHandlers.Add(7679, new SkillDefinations.Monster.FireArrow());
            skillHandlers.Add(7680, new SkillDefinations.Monster.WaterArrow());
            skillHandlers.Add(7681, new SkillDefinations.Monster.EarthArrow());
            skillHandlers.Add(7682, new SkillDefinations.Monster.WindArrow());
            skillHandlers.Add(7683, new SkillDefinations.Monster.LightArrow());
            skillHandlers.Add(7684, new SkillDefinations.Monster.DarkArrow());
            skillHandlers.Add(7685, new SkillDefinations.Monster.MobConArrow());
            skillHandlers.Add(7686, new SkillDefinations.Monster.MobChargeArrow());
            skillHandlers.Add(7687, new SkillDefinations.Global.SumSlaveMob(26050003));//ホウオウ
            skillHandlers.Add(7688, new SkillDefinations.Monster.MobTrDrop());
            skillHandlers.Add(7689, new SkillDefinations.Monster.MobConfCircle());
            skillHandlers.Add(7690, new SkillDefinations.Global.SumMob(90010000));
            skillHandlers.Add(7691, new SkillDefinations.Monster.MobMedic());
            skillHandlers.Add(7692, new SkillDefinations.Monster.MobWindHighStorm2());
            skillHandlers.Add(7693, new SkillDefinations.Monster.MobElementLoad(7694));
            skillHandlers.Add(7694, new SkillDefinations.Monster.MobWindHighStorm2());
            skillHandlers.Add(7695, new SkillDefinations.Monster.MobElementRandcircle(7696, 5));
            skillHandlers.Add(7696, new SkillDefinations.Monster.MobWindRandcircleSeq2());
            skillHandlers.Add(7697, new SkillDefinations.Monster.MobElementRandcircle(7698, 5));
            skillHandlers.Add(7698, new SkillDefinations.Monster.MobWindCrosscircleSeq2());

            skillHandlers.Add(7706, new SkillDefinations.Global.SumSlaveMob(10136901));//魔狼
            skillHandlers.Add(7707, new SkillDefinations.Sorcerer.Kyrie());
            //skillHandlers.Add(7709, new SkillDefinations.Sorcerer.Kyrie(SagaMap.Skill.SkillDefinations.Sorcerer.Kyrie.KyrieUser.Boss)); 
            skillHandlers.Add(7709, new SkillDefinations.Monster.MobComaStun());
            skillHandlers.Add(7710, new SkillDefinations.Global.SumMob(90010000));
            skillHandlers.Add(7711, new SkillDefinations.Monster.MobSelfDarkHighStorm());
            skillHandlers.Add(4951, new SkillDefinations.Monster.MobSelfDarkHighStorm());
            skillHandlers.Add(7712, new SkillDefinations.Cabalist.DarkStorm(true));
            skillHandlers.Add(7713, new SkillDefinations.Monster.MobSelfMagStun());
            skillHandlers.Add(7714, new SkillDefinations.Monster.TrDrop2());
            skillHandlers.Add(7715, new SkillDefinations.Monster.MobHpPerDown());
            skillHandlers.Add(7716, new SkillDefinations.Monster.MobDropOut());
            skillHandlers.Add(7717, new SkillDefinations.Global.SumSlaveMob(26180000));//玉桂巴巴

            skillHandlers.Add(7719, new SkillDefinations.Monster.MobTalkSnmnpapa());
            skillHandlers.Add(7720, new SkillDefinations.Global.SumSlaveMob(10120100));//クリムゾンバウ
            skillHandlers.Add(7721, new SkillDefinations.Global.SumSlaveMob(10210002));//タランチュラ
            skillHandlers.Add(7722, new SkillDefinations.Global.SumSlaveMob(10431000));//キメラ(灰)
            skillHandlers.Add(7723, new SkillDefinations.Global.SumSlaveMob(10680900));//ゴースト(黒)
            skillHandlers.Add(7724, new SkillDefinations.Global.SumSlaveMob(10251000));//デカデス
            skillHandlers.Add(7725, new SkillDefinations.Global.SumSlaveMob(10000006));//デカプルル
            skillHandlers.Add(7726, new SkillDefinations.Global.SumSlaveMob(10001005));//メタリカ
            skillHandlers.Add(7727, new SkillDefinations.Global.SumSlaveMob(10211400));//夜叉蜘蛛
            skillHandlers.Add(7728, new SkillDefinations.Shaman.ThunderBall(true));
            skillHandlers.Add(7729, new SkillDefinations.Shaman.EarthBlast(true));
            skillHandlers.Add(7730, new SkillDefinations.Shaman.FireBolt(true));
            skillHandlers.Add(7731, new SkillDefinations.Cabalist.StoneSkin(true));
            skillHandlers.Add(7732, new SkillDefinations.Monster.MobCancelChgstateAll());
            skillHandlers.Add(7733, new SkillDefinations.Global.SumSlaveMob(10580500, 4));//曼陀蘿芥末
            skillHandlers.Add(7734, new SkillDefinations.Monster.MobCircleSelfAtk());
            skillHandlers.Add(7735, new SkillDefinations.Global.SumSlaveMob(10431400, 1));//アルビノキメラ
            skillHandlers.Add(7736, new SkillDefinations.Global.SumSlaveMob(10030901, 1));//最強の魔獣
            skillHandlers.Add(7737, new SkillDefinations.Monster.MobCircleSelfAtk());
            skillHandlers.Add(7738, new SkillDefinations.Wizard.EnergyShock(true));
            skillHandlers.Add(7739, new SkillDefinations.Assassin.Concentricity(true));
            skillHandlers.Add(7740, new SkillDefinations.Monster.MobComboConShot());
            skillHandlers.Add(7741, new SkillDefinations.Monster.MobConShot());
            skillHandlers.Add(7742, new SkillDefinations.Monster.MobComboConAtk());
            skillHandlers.Add(7743, new SkillDefinations.Monster.MobConAtk());
            skillHandlers.Add(7744, new SkillDefinations.Global.SumSlaveMob(10580500));//曼陀蘿芥末
            skillHandlers.Add(7745, new SkillDefinations.Enchanter.AcidMist());
            skillHandlers.Add(7746, new SkillDefinations.Monster.MobEarthDurable());
            skillHandlers.Add(7747, new SkillDefinations.DarkStalker.LifeSteal(true));
            skillHandlers.Add(7748, new SkillDefinations.Monster.MobChargeCircle());
            skillHandlers.Add(7749, new SkillDefinations.Alchemist.PetPlantPoison(true));
            skillHandlers.Add(7750, new SkillDefinations.Monster.MobStrVitAgiDownOne());
            skillHandlers.Add(7751, new SkillDefinations.Monster.MobAtkupSelf());
            skillHandlers.Add(7752, new SkillDefinations.Fencer.MobDefUpSelf(true));
            skillHandlers.Add(7753, new SkillDefinations.Sorcerer.Kyrie(SagaMap.Skill.SkillDefinations.Sorcerer.Kyrie.KyrieUser.Mob));
            skillHandlers.Add(7754, new SkillDefinations.Monster.MobHolyfeather());
            skillHandlers.Add(7755, new SkillDefinations.Monster.MobSalvoFire());
            skillHandlers.Add(7756, new SkillDefinations.Monster.MobAmobm());
            skillHandlers.Add(7757, new SkillDefinations.Global.SumMob(90010001));
            skillHandlers.Add(7758, new SkillDefinations.Sage.EnergyStorm(true));
            skillHandlers.Add(7759, new SkillDefinations.Wizard.EnergyBlast(true));
            skillHandlers.Add(7760, new SkillDefinations.Global.SumSlaveMob(10990000));//バルル
            skillHandlers.Add(7761, new SkillDefinations.Global.SumSlaveMob(10960000));//野生德拉古

            skillHandlers.Add(7766, new SkillDefinations.Global.SumSlaveMob(19070500));//ＤＥＭ－スナイパー

            skillHandlers.Add(7798, new SkillDefinations.Monster.Caputrue());

            skillHandlers.Add(7805, new SkillDefinations.Global.SumSlaveMob(14160500));//ポイズンジェル
            skillHandlers.Add(7806, new SkillDefinations.Global.SumSlaveMob(14160000));//バルーンジェル
            skillHandlers.Add(7807, new SkillDefinations.Global.SumSlaveMob(10060600));//エンジェルフィッシュ
            skillHandlers.Add(7808, new SkillDefinations.Global.SumSlaveMob(10060200));//スイムフィッシュ


            skillHandlers.Add(7810, new SkillDefinations.Monster.Abusoryutoteritori());
            skillHandlers.Add(7811, new SkillDefinations.Global.SumSlaveMob(14110003, 8));//スレイブドラゴン召喚

            skillHandlers.Add(7813, new SkillDefinations.Gunner.CanisterShot(true));

            skillHandlers.Add(7818, new SkillDefinations.Global.SumSlaveMob(14320200));//ＤＥＭ－クリンゲ召喚
            skillHandlers.Add(7819, new SkillDefinations.Global.SumSlaveMob(14330500));//ＤＥＭ－ゲヴェーア召喚

            skillHandlers.Add(7820, new SkillDefinations.Monster.SoulAttack());
            skillHandlers.Add(7821, new SkillDefinations.Monster.VolcanoHall());
            skillHandlers.Add(7822, new SkillDefinations.BountyHunter.IsSeN(true));

            skillHandlers.Add(7830, new SkillDefinations.Global.SumSlaveMob(14560900));//アルルーン召喚
            skillHandlers.Add(7831, new SkillDefinations.Monster.Animadorein());

            skillHandlers.Add(8500, new SkillDefinations.Monster.MobHpHeal());
            skillHandlers.Add(8501, new SkillDefinations.Monster.MobBerserk());

            skillHandlers.Add(9000, new SkillDefinations.Ranger.CswarSleep(true));
            skillHandlers.Add(9001, new SkillDefinations.Ranger.CswarSleep(true));
            skillHandlers.Add(9002, new SkillDefinations.Global.SumMob(26160003));//サークリス

            skillHandlers.Add(9004, new SkillDefinations.Druid.AreaHeal(true));
            skillHandlers.Add(9106, new SkillDefinations.Cabalist.EventSelfDarkStorm(true));
            #endregion

            #region Marionette
            skillHandlers.Add(5008, new SkillDefinations.Marionette.HPRecovery());
            skillHandlers.Add(5009, new SkillDefinations.Marionette.SPRecovery());
            skillHandlers.Add(5010, new SkillDefinations.Marionette.MPRecovery());

            skillHandlers.Add(5507, new SkillDefinations.Marionette.MExclamation());

            skillHandlers.Add(5513, new SkillDefinations.Marionette.MBokeboke());

            skillHandlers.Add(5515, new SkillDefinations.Marionette.MMirror());
            skillHandlers.Add(5516, new SkillDefinations.Marionette.MMirrorSkill());

            skillHandlers.Add(5522, new SkillDefinations.Marionette.MDarkCrosscircle());
            skillHandlers.Add(5523, new SkillDefinations.Marionette.MDarkCrosscircleSeq());
            skillHandlers.Add(5524, new SkillDefinations.Marionette.MCharge3());

            #endregion

            #region Event
            skillHandlers.Add(1500, new SkillDefinations.Event.WeaCreUp());
            skillHandlers.Add(1501, new SkillDefinations.Event.HitUpRateDown());

            skillHandlers.Add(1603, new SkillDefinations.Event.Ryuugankaihou());
            skillHandlers.Add(1604, new SkillDefinations.Event.RyuugankaihouShin());
            skillHandlers.Add(1605, new SkillDefinations.Event.MagicSP());
            skillHandlers.Add(2072, new SkillDefinations.Event.MoneyTime());
            skillHandlers.Add(2265, new SkillDefinations.Event.NormalAttack());
            skillHandlers.Add(2457, new SkillDefinations.Event.SymbolRepair());

            skillHandlers.Add(3067, new SkillDefinations.Event.PowerUP());
            skillHandlers.Add(3069, new SkillDefinations.Event.HPUP());
            skillHandlers.Add(3071, new SkillDefinations.Event.SpeedUP());
            skillHandlers.Add(3145, new SkillDefinations.Event.MagicUP());
            skillHandlers.Add(3269, new SkillDefinations.Event.ChgTrance());
            skillHandlers.Add(5509, new SkillDefinations.Event.Colder());
            skillHandlers.Add(5510, new SkillDefinations.Event.ConflictKick());

            skillHandlers.Add(6415, new SkillDefinations.Event.MoveUp2());
            skillHandlers.Add(6428, new SkillDefinations.Event.MoveUp3());
            skillHandlers.Add(9100, new SkillDefinations.Event.MiniMum());
            skillHandlers.Add(9101, new SkillDefinations.Event.MaxiMum());
            skillHandlers.Add(9102, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9103, new SkillDefinations.Sorcerer.Invisible());
            skillHandlers.Add(9105, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9108, new SkillDefinations.Event.Dango());
            skillHandlers.Add(9109, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9114, new SkillDefinations.Sorcerer.Invisible());
            skillHandlers.Add(9117, new SkillDefinations.Event.ExpUp());
            skillHandlers.Add(9126, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9127, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9128, new SkillDefinations.Sorcerer.Invisible());
            skillHandlers.Add(9129, new SkillDefinations.Global.SumMobCastSkill(19010001, 9130));
            skillHandlers.Add(9130, new SkillDefinations.Event.HpRecoveryMax());
            skillHandlers.Add(9131, new SkillDefinations.Global.SumMobCastSkill(19010002, 9132));
            skillHandlers.Add(9132, new SkillDefinations.Event.HpRecoveryMax());
            skillHandlers.Add(9133, new SkillDefinations.Global.SumMobCastSkill(19010003, 9134));
            skillHandlers.Add(9134, new SkillDefinations.Event.HpRecoveryMax());

            skillHandlers.Add(9140, new SkillDefinations.Global.SumMobCastSkill(19010008, 9143, 50, 9146, 50));
            skillHandlers.Add(9141, new SkillDefinations.Global.SumMobCastSkill(19010009, 9144, 50, 9147, 50));
            skillHandlers.Add(9142, new SkillDefinations.Global.SumMobCastSkill(19010010, 9145, 50, 9148, 50));
            skillHandlers.Add(9143, new SkillDefinations.Event.DefMdefUp());
            skillHandlers.Add(9144, new SkillDefinations.Event.DefMdefUp());
            skillHandlers.Add(9145, new SkillDefinations.Event.DefMdefUp());
            skillHandlers.Add(9146, new SkillDefinations.Event.DefMdefUp());
            skillHandlers.Add(9147, new SkillDefinations.Event.DefMdefUp());
            skillHandlers.Add(9148, new SkillDefinations.Event.DefMdefUp());


            skillHandlers.Add(9139, new SkillDefinations.Tatarabe.EventCampfire());

            skillHandlers.Add(9151, new SkillDefinations.Event.ILoveYou());
            skillHandlers.Add(9152, new SkillDefinations.Event.HpRecoveryMax());
            skillHandlers.Add(9153, new SkillDefinations.Event.HpRecoveryMax());
            skillHandlers.Add(9154, new SkillDefinations.Event.HpRecoveryMax());
            skillHandlers.Add(9155, new SkillDefinations.Vates.Healing());
            skillHandlers.Add(9157, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9162, new SkillDefinations.Vates.Healing());
            skillHandlers.Add(9163, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9174, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9178, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9182, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9185, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(9190, new SkillDefinations.Global.SumMob(30740000));
            skillHandlers.Add(9191, new SkillDefinations.Global.SumMobCastSkill(30750000, 9192, 90, 9193, 10));
            skillHandlers.Add(9192, new SkillDefinations.Event.WeepingWillow1());
            skillHandlers.Add(9193, new SkillDefinations.Event.WeepingWillow2());

            skillHandlers.Add(9197, new SkillDefinations.Sorcerer.Invisible());


            skillHandlers.Add(9208, new SkillDefinations.Global.SumMobCastSkill(19010028, 9209));
            skillHandlers.Add(9209, new SkillDefinations.Event.HpRecoveryMax());

            skillHandlers.Add(9219, new SkillDefinations.Event.MoveUp4());
            skillHandlers.Add(9220, new SkillDefinations.Event.RiceSeed());
            skillHandlers.Add(9221, new SkillDefinations.Event.PanTick());

            skillHandlers.Add(9223, new SkillDefinations.Event.Gravity());
            skillHandlers.Add(9224, new SkillDefinations.Event.Kyrie());
            skillHandlers.Add(9225, new SkillDefinations.Event.AReflection());
            skillHandlers.Add(9226, new SkillDefinations.Event.STR_VIT_AGI_UP());
            skillHandlers.Add(9227, new SkillDefinations.Event.MAG_INT_DEX_UP());
            skillHandlers.Add(9228, new SkillDefinations.Event.AreaHeal());

            skillHandlers.Add(10500, new SkillDefinations.Event.HerosProtection());

            #endregion

            #region Swordman
            skillHandlers.Add(2005, new SkillDefinations.Swordman.SwordCancel());
            skillHandlers.Add(2100, new SkillDefinations.Swordman.Parry());
            skillHandlers.Add(2101, new SkillDefinations.Swordman.Counter());
            skillHandlers.Add(2102, new SkillDefinations.Swordman.Feint());
            skillHandlers.Add(2107, new SkillDefinations.Swordman.Provocation());
            skillHandlers.Add(2111, new SkillDefinations.Swordman.BanishBlow());
            skillHandlers.Add(2114, new SkillDefinations.Swordman.SlowBlow());
            skillHandlers.Add(2120, new SkillDefinations.Swordman.Charge());
            skillHandlers.Add(2117, new SkillDefinations.Swordman.CutDown());

            #endregion

            #region BladeMaster
            skillHandlers.Add(2134, new SkillDefinations.BladeMaster.aEarthAngry());
            skillHandlers.Add(2231, new SkillDefinations.BladeMaster.aWoodHack());
            skillHandlers.Add(2232, new SkillDefinations.BladeMaster.aFalconLongSword());
            skillHandlers.Add(2233, new SkillDefinations.BladeMaster.aMistBehead());
            skillHandlers.Add(2234, new SkillDefinations.BladeMaster.aAnimalCrushing());
            skillHandlers.Add(2235, new SkillDefinations.BladeMaster.aHundredSpriteCry());
            skillHandlers.Add(700, new SkillDefinations.BladeMaster.P_BERSERK());
            skillHandlers.Add(2066, new SkillDefinations.BladeMaster.PetMeditatioon());
            skillHandlers.Add(2109, new SkillDefinations.BladeMaster.PetWarCry());
            skillHandlers.Add(2236, new SkillDefinations.BladeMaster.AtkFly());
            skillHandlers.Add(2237, new SkillDefinations.BladeMaster.SwordEaseSp());
            skillHandlers.Add(2238, new SkillDefinations.BladeMaster.EaseCt());
            skillHandlers.Add(2379, new SkillDefinations.BladeMaster.DoubleCutDown());
            skillHandlers.Add(2380, new SkillDefinations.BladeMaster.DoubleCutDownSeq());

            #endregion

            #region BountyHunter
            skillHandlers.Add(2272, new SkillDefinations.BountyHunter.ArmSlash());
            skillHandlers.Add(2271, new SkillDefinations.BountyHunter.BodySlash());
            skillHandlers.Add(2273, new SkillDefinations.BountyHunter.ChestSlash());
            skillHandlers.Add(2122, new SkillDefinations.BountyHunter.MiNeUChi());
            skillHandlers.Add(2274, new SkillDefinations.BountyHunter.ShieldSlash());
            skillHandlers.Add(2269, new SkillDefinations.BountyHunter.DefUpAvoDown());
            skillHandlers.Add(2268, new SkillDefinations.BountyHunter.AtkUpHitDown());
            skillHandlers.Add(2352, new SkillDefinations.BountyHunter.BeatUp());
            skillHandlers.Add(2401, new SkillDefinations.BountyHunter.MuSoU());
            skillHandlers.Add(2396, new SkillDefinations.BountyHunter.SordAssail());
            skillHandlers.Add(2136, new SkillDefinations.BountyHunter.AShiBaRaI());
            skillHandlers.Add(956, new SkillDefinations.BountyHunter.ConSword());
            //skillHandlers.Add(400, new SkillDefinations.Global.SomeKindDamUp("HumDamUp", SagaDB.Mob.MobType.HUMAN, SagaDB.Mob.MobType.HUMAN_BOSS, SagaDB.Mob.MobType.HUMAN_BOSS_SKILL, SagaDB.Mob.MobType.HUMAN_RIDE, SagaDB.Mob.MobType.HUMAN_SKILL));
            skillHandlers.Add(2275, new SkillDefinations.BountyHunter.PartsSlash());
            skillHandlers.Add(2355, new SkillDefinations.BountyHunter.StrikeBlow());
            skillHandlers.Add(2353, new SkillDefinations.BountyHunter.SolidBody());
            skillHandlers.Add(2400, new SkillDefinations.BountyHunter.IsSeN());
            skillHandlers.Add(2179, new SkillDefinations.BountyHunter.EdgedSlash());
            skillHandlers.Add(2270, new SkillDefinations.BountyHunter.ComboIai());
            #endregion

            #region Gladiator

            skillHandlers.Add(00982, new SkillDefinations.Gladiator.SwordMaster());
            skillHandlers.Add(03350, new SkillDefinations.Gladiator.HPCommunion());
            skillHandlers.Add(3362, new SkillDefinations.Gladiator.Gladiator());//11月23日增加，LV10习得
            skillHandlers.Add(1100, new SkillDefinations.Gladiator.Volunteers());//11月23日增加，LV13习得
            skillHandlers.Add(2484, new SkillDefinations.Gladiator.Ekuviri());
            skillHandlers.Add(2498, new SkillDefinations.Gladiator.DevilStance());
            skillHandlers.Add(2503, new SkillDefinations.Gladiator.Convolution());
            skillHandlers.Add(3391, new SkillDefinations.Gladiator.Pressure());//11月23日增加，LV30习得
            skillHandlers.Add(1113, new SkillDefinations.Gladiator.Sewaibu());//11月23日增加，LV35习得
            skillHandlers.Add(2528, new SkillDefinations.Gladiator.Disarm());//11月23日增加，LV40习得
            skillHandlers.Add(2527, new SkillDefinations.Gladiator.SpeedHit());//11月23日增加，LV45习得
            //skillHandlers.Add(03350, new SkillDefinations.Gladiator.HPCommunion());

            #endregion

            #region Scout
            skillHandlers.Add(2001, new SkillDefinations.Scout.CriUp());
            skillHandlers.Add(2008, new SkillDefinations.Scout.ShortSwordCancel());
            skillHandlers.Add(2139, new SkillDefinations.Scout.ConThrust());
            skillHandlers.Add(2143, new SkillDefinations.Scout.SummerSaltKick());
            skillHandlers.Add(2133, new SkillDefinations.Scout.WalkAround());

            #endregion

            #region Assassin
            skillHandlers.Add(2045, new SkillDefinations.Assassin.PoisonReate());
            skillHandlers.Add(2046, new SkillDefinations.Assassin.PosionReate2());
            skillHandlers.Add(2047, new SkillDefinations.Assassin.PoisonReate3());
            skillHandlers.Add(2069, new SkillDefinations.Assassin.Concentricity());
            skillHandlers.Add(947, new SkillDefinations.Assassin.ConClaw());
            skillHandlers.Add(2062, new SkillDefinations.Assassin.WillPower());
            skillHandlers.Add(910, new SkillDefinations.Assassin.PoisonRegiUp());
            skillHandlers.Add(2250, new SkillDefinations.Assassin.UTuSeMi());
            skillHandlers.Add(2044, new SkillDefinations.Assassin.AppliePoison());
            skillHandlers.Add(2142, new SkillDefinations.Assassin.ScatterPoison());
            skillHandlers.Add(920, new SkillDefinations.Assassin.PoisonRateUp());
            skillHandlers.Add(2251, new SkillDefinations.Assassin.EventSumNinJa());

            #endregion

            #region Command
            skillHandlers.Add(127, new SkillDefinations.Command.HandGunDamUp());
            skillHandlers.Add(2137, new SkillDefinations.Command.Tackle());
            skillHandlers.Add(125, new SkillDefinations.Command.MartialArtDamUp());
            skillHandlers.Add(2141, new SkillDefinations.Command.Rush());
            skillHandlers.Add(2282, new SkillDefinations.Command.FlashHandGrenade());
            skillHandlers.Add(2362, new SkillDefinations.Command.SetBomb());
            skillHandlers.Add(2378, new SkillDefinations.Command.SetBomb2());
            skillHandlers.Add(2359, new SkillDefinations.Command.UpperCut());
            skillHandlers.Add(2413, new SkillDefinations.Command.WildDance());
            skillHandlers.Add(2414, new SkillDefinations.Command.WildDance2());
            skillHandlers.Add(3095, new SkillDefinations.Command.LandTrap());
            skillHandlers.Add(2280, new SkillDefinations.Command.FullNelson());
            skillHandlers.Add(2281, new SkillDefinations.Command.Combination());
            skillHandlers.Add(3131, new SkillDefinations.Command.ChokingGas());
            skillHandlers.Add(2283, new SkillDefinations.Command.Sliding());
            skillHandlers.Add(2284, new SkillDefinations.Command.ClayMore());
            skillHandlers.Add(2361, new SkillDefinations.Command.SealHMSp());
            skillHandlers.Add(2409, new SkillDefinations.Command.RushBom());
            skillHandlers.Add(2410, new SkillDefinations.Command.RushBomSeq());
            skillHandlers.Add(2411, new SkillDefinations.Command.RushBomSeq2());
            skillHandlers.Add(2408, new SkillDefinations.Command.SumCommand());
            //skillHandlers.Add(401, new SkillDefinations.Command.HumHitUp());
            

            #endregion

            #region Wizard
            skillHandlers.Add(3001, new SkillDefinations.Wizard.EnergyOne());
            skillHandlers.Add(3002, new SkillDefinations.Wizard.EnergyGroove());
            skillHandlers.Add(3005, new SkillDefinations.Wizard.Decoy());
            skillHandlers.Add(3113, new SkillDefinations.Wizard.EnergyShield());
            skillHandlers.Add(3279, new SkillDefinations.Wizard.EnergyShield());
            skillHandlers.Add(3114, new SkillDefinations.Wizard.MagicShield());
            skillHandlers.Add(3280, new SkillDefinations.Wizard.MagicShield());
            skillHandlers.Add(3123, new SkillDefinations.Wizard.EnergyShock());
            skillHandlers.Add(3125, new SkillDefinations.Wizard.DancingSword());
            skillHandlers.Add(3124, new SkillDefinations.Wizard.EnergySpear());
            skillHandlers.Add(3127, new SkillDefinations.Wizard.EnergyBlast());
            skillHandlers.Add(3135, new SkillDefinations.Wizard.MagPoison());
            skillHandlers.Add(3136, new SkillDefinations.Wizard.MagStone());
            skillHandlers.Add(3139, new SkillDefinations.Wizard.MagSilence());
            skillHandlers.Add(801, new SkillDefinations.Wizard.MaGaNiInfo());
            skillHandlers.Add(3101, new SkillDefinations.Wizard.Heating());
            skillHandlers.Add(3281, new SkillDefinations.Wizard.MobEnergyShock());
            skillHandlers.Add(3100, new SkillDefinations.Wizard.IntenseHeatSheld());
            skillHandlers.Add(3033, new SkillDefinations.Wizard.Aqualung());
            skillHandlers.Add(3003, new SkillDefinations.Wizard.EnergyWall());
            skillHandlers.Add(503, new SkillDefinations.Wizard.ManDamUp());
            skillHandlers.Add(504, new SkillDefinations.Wizard.ManHitUp());
            skillHandlers.Add(505, new SkillDefinations.Wizard.ManAvoUp());
            skillHandlers.Add(400, new SkillDefinations.Wizard.WandMaster());
            skillHandlers.Add(401, new SkillDefinations.Wizard.EnergyExcess());
            #endregion

            #region Sorcerer
            skillHandlers.Add(3126, new SkillDefinations.Sorcerer.RavingSword());
            skillHandlers.Add(3300, new SkillDefinations.Sorcerer.DevineBarrier());
            skillHandlers.Add(3253, new SkillDefinations.Sorcerer.Teleport());
            skillHandlers.Add(3097, new SkillDefinations.Sorcerer.Invisible());
            skillHandlers.Add(3275, new SkillDefinations.Sorcerer.EnergyBarrier());
            skillHandlers.Add(3276, new SkillDefinations.Sorcerer.MagicBarrier());
            skillHandlers.Add(3256, new SkillDefinations.Sorcerer.MagIntDexDownOne());
            skillHandlers.Add(3255, new SkillDefinations.Sorcerer.StrVitAgiDownOne());
            skillHandlers.Add(3298, new SkillDefinations.Sorcerer.EnergyBarn());
            skillHandlers.Add(3299, new SkillDefinations.Sorcerer.EnergyBarnSEQ());
            skillHandlers.Add(3158, new SkillDefinations.Sorcerer.Desist());
            skillHandlers.Add(3254, new SkillDefinations.Sorcerer.Kyrie());
            skillHandlers.Add(2208, new SkillDefinations.Sorcerer.MaganiAnalysis());
            skillHandlers.Add(3171, new SkillDefinations.Sorcerer.MobInvisibleBreak());
            skillHandlers.Add(3098, new SkillDefinations.Sorcerer.MobInvisibleBreak());
            skillHandlers.Add(3004, new SkillDefinations.Sorcerer.WallSweep());
            skillHandlers.Add(3094, new SkillDefinations.Sorcerer.HexaGram());
            skillHandlers.Add(2252, new SkillDefinations.Sorcerer.DelayCancel());

            #endregion

            #region Vates
            skillHandlers.Add(3111, new SkillDefinations.Vates.HolyBlessing());
            skillHandlers.Add(3080, new SkillDefinations.Vates.HolyHealing());
            skillHandlers.Add(3054, new SkillDefinations.Vates.Healing());
            skillHandlers.Add(3165, new SkillDefinations.Vates.SmallHealing());
            skillHandlers.Add(3055, new SkillDefinations.Vates.Resurrection());
            skillHandlers.Add(3073, new SkillDefinations.Vates.LightOne());
            skillHandlers.Add(3075, new SkillDefinations.Vates.HolyWeapon());
            skillHandlers.Add(3076, new SkillDefinations.Vates.HolyShield());
            skillHandlers.Add(3082, new SkillDefinations.Vates.HolyGroove());
            skillHandlers.Add(3066, new SkillDefinations.Vates.CureStatus("Sleep"));
            skillHandlers.Add(3060, new SkillDefinations.Vates.CureStatus("Poison"));
            skillHandlers.Add(3058, new SkillDefinations.Vates.CureStatus("Stun"));
            skillHandlers.Add(3062, new SkillDefinations.Vates.CureStatus("Silence"));
            skillHandlers.Add(3150, new SkillDefinations.Vates.CureStatus("Stone"));
            skillHandlers.Add(3064, new SkillDefinations.Vates.CureStatus("Confuse"));
            skillHandlers.Add(3152, new SkillDefinations.Vates.CureStatus("鈍足"));
            skillHandlers.Add(3154, new SkillDefinations.Vates.CureStatus("Frosen"));
            skillHandlers.Add(803, new SkillDefinations.Vates.UndeadInfo());
            skillHandlers.Add(3065, new SkillDefinations.Vates.StatusRegi("Sleep"));
            skillHandlers.Add(3059, new SkillDefinations.Vates.StatusRegi("Poison"));
            skillHandlers.Add(3057, new SkillDefinations.Vates.StatusRegi("Stun"));
            skillHandlers.Add(3061, new SkillDefinations.Vates.StatusRegi("Silence"));
            skillHandlers.Add(3149, new SkillDefinations.Vates.StatusRegi("Stone"));
            skillHandlers.Add(3063, new SkillDefinations.Vates.StatusRegi("Confuse"));
            skillHandlers.Add(3151, new SkillDefinations.Vates.StatusRegi("鈍足"));
            skillHandlers.Add(3153, new SkillDefinations.Vates.StatusRegi("Frosen"));
            skillHandlers.Add(3078, new SkillDefinations.Vates.TurnUndead());

            #endregion

            #region Shaman
            skillHandlers.Add(3006, new SkillDefinations.Shaman.FireBolt());
            skillHandlers.Add(3007, new SkillDefinations.Shaman.FireShield());
            skillHandlers.Add(3008, new SkillDefinations.Shaman.FireWeapon());
            skillHandlers.Add(3009, new SkillDefinations.Shaman.FireBlast());
            skillHandlers.Add(3029, new SkillDefinations.Shaman.IceArrow());
            skillHandlers.Add(3030, new SkillDefinations.Shaman.WaterShield());
            skillHandlers.Add(3031, new SkillDefinations.Shaman.WaterWeapon());
            skillHandlers.Add(3032, new SkillDefinations.Shaman.ColdBlast());
            skillHandlers.Add(3041, new SkillDefinations.Shaman.LandKlug());
            skillHandlers.Add(3042, new SkillDefinations.Shaman.EarthShield());
            skillHandlers.Add(3043, new SkillDefinations.Shaman.EarthWeapon());
            skillHandlers.Add(3044, new SkillDefinations.Shaman.EarthBlast());
            skillHandlers.Add(3017, new SkillDefinations.Shaman.ThunderBall());
            skillHandlers.Add(3018, new SkillDefinations.Shaman.WindShield());
            skillHandlers.Add(3019, new SkillDefinations.Shaman.WindWeapon());
            skillHandlers.Add(3020, new SkillDefinations.Shaman.LightningBlast());
            skillHandlers.Add(3011, new SkillDefinations.Shaman.FireWall());
            skillHandlers.Add(3047, new SkillDefinations.Shaman.StoneWall());
            skillHandlers.Add(802, new SkillDefinations.Shaman.ElementIInfo());
            skillHandlers.Add(3000, new SkillDefinations.Shaman.SenseElement());
            skillHandlers.Add(3162, new SkillDefinations.Shaman.ElementAllUp());
            #endregion

            #region Elementaler
            skillHandlers.Add(3016, new SkillDefinations.Elementaler.FireGroove());
            skillHandlers.Add(3028, new SkillDefinations.Elementaler.WindGroove());
            skillHandlers.Add(3040, new SkillDefinations.Elementaler.WaterGroove());
            skillHandlers.Add(3053, new SkillDefinations.Elementaler.EarthGroove());
            skillHandlers.Add(3265, new SkillDefinations.Elementaler.LavaFlow());
            skillHandlers.Add(3036, new SkillDefinations.Elementaler.WaterStorm());
            skillHandlers.Add(3025, new SkillDefinations.Elementaler.WindStorm());
            skillHandlers.Add(3049, new SkillDefinations.Elementaler.EarthStorm());
            skillHandlers.Add(3013, new SkillDefinations.Elementaler.FireStorm());
            skillHandlers.Add(3261, new SkillDefinations.Elementaler.ChainLightning());
            skillHandlers.Add(3260, new SkillDefinations.Elementaler.CatlingGun());
            skillHandlers.Add(3262, new SkillDefinations.Elementaler.WaterNum());
            skillHandlers.Add(3263, new SkillDefinations.Elementaler.EarthNum());
            skillHandlers.Add(3264, new SkillDefinations.Elementaler.WaterWindTurable());
            skillHandlers.Add(3311, new SkillDefinations.Elementaler.SpellCancel());
            skillHandlers.Add(3159, new SkillDefinations.Elementaler.Zen());
            skillHandlers.Add(2209, new SkillDefinations.Elementaler.ElementAnalysis());
            skillHandlers.Add(3301, new SkillDefinations.Elementaler.AquaWave());
            skillHandlers.Add(3306, new SkillDefinations.Elementaler.CycloneGrooveEarth());
            skillHandlers.Add(939, new SkillDefinations.Global.ElementLimitUp(Elements.Earth));//大地守護
            skillHandlers.Add(936, new SkillDefinations.Global.ElementLimitUp(Elements.Fire));//火焰守護
            skillHandlers.Add(937, new SkillDefinations.Global.ElementLimitUp(Elements.Water));//水靈守護
            skillHandlers.Add(938, new SkillDefinations.Global.ElementLimitUp(Elements.Wind));//神風守護
            #endregion

            #region Enchanter
            skillHandlers.Add(3318, new SkillDefinations.Enchanter.GravityFall());
            skillHandlers.Add(3319, new SkillDefinations.Enchanter.ElementalWrath());
            skillHandlers.Add(3294, new SkillDefinations.Enchanter.SpdUp_AvoUp_AtkDown_DefDown());
            skillHandlers.Add(3295, new SkillDefinations.Enchanter.AtkUp_DefUp_SpdDown_AvoDown());
            skillHandlers.Add(2305, new SkillDefinations.Enchanter.SoulOfEarth());
            skillHandlers.Add(2304, new SkillDefinations.Enchanter.SoulOfWind());
            skillHandlers.Add(2303, new SkillDefinations.Enchanter.SoulOfWater());
            skillHandlers.Add(2302, new SkillDefinations.Enchanter.SoulOfFire());
            skillHandlers.Add(3046, new SkillDefinations.Enchanter.PoisonMash());
            skillHandlers.Add(3155, new SkillDefinations.Enchanter.Bind());
            skillHandlers.Add(3052, new SkillDefinations.Enchanter.AcidMist());
            skillHandlers.Add(3010, new SkillDefinations.Enchanter.FirePillar());
            skillHandlers.Add(3048, new SkillDefinations.Enchanter.ElementCircle(Elements.Earth));//大地結界
            skillHandlers.Add(3012, new SkillDefinations.Enchanter.ElementCircle(Elements.Fire));//火焰結界
            skillHandlers.Add(3035, new SkillDefinations.Enchanter.ElementCircle(Elements.Water));//寒冰結界
            skillHandlers.Add(3024, new SkillDefinations.Enchanter.ElementCircle(Elements.Wind));//神風結界
            skillHandlers.Add(3296, new SkillDefinations.Enchanter.ElementBall());
            skillHandlers.Add(3317, new SkillDefinations.Enchanter.EnchantWeapon());
            skillHandlers.Add(3110, new SkillDefinations.Global.ElementRise(Elements.Earth));//大地祝福
            skillHandlers.Add(3107, new SkillDefinations.Global.ElementRise(Elements.Fire));//火焰祝福
            skillHandlers.Add(3109, new SkillDefinations.Global.ElementRise(Elements.Water));//寒氣祝福
            skillHandlers.Add(3108, new SkillDefinations.Global.ElementRise(Elements.Wind));//神風祝福
            #endregion

            #region Acher
            skillHandlers.Add(2050, new SkillDefinations.Archer.BowCancel());
            skillHandlers.Add(2128, new SkillDefinations.Archer.ConArrow());
            #endregion

            #region Warlock
            skillHandlers.Add(3083, new SkillDefinations.Warlock.BlackWidow());
            skillHandlers.Add(3085, new SkillDefinations.Warlock.ShadowBlast());
            skillHandlers.Add(3088, new SkillDefinations.Warlock.DarkWeapon());
            skillHandlers.Add(3093, new SkillDefinations.Warlock.DarkGroove());
            skillHandlers.Add(3133, new SkillDefinations.Warlock.DarkShield());
            skillHandlers.Add(3134, new SkillDefinations.Warlock.ChaosWidow());
            skillHandlers.Add(3140, new SkillDefinations.Warlock.MagSlow());
            skillHandlers.Add(3141, new SkillDefinations.Warlock.MagConfuse());
            skillHandlers.Add(3142, new SkillDefinations.Warlock.MagFreeze());
            skillHandlers.Add(3143, new SkillDefinations.Warlock.MagStun());
            skillHandlers.Add(3112, new SkillDefinations.Global.ElementRise(Elements.Dark));//黑暗加護
            //skillHandlers.Add(941, new SkillDefinations.Global.ElementLimitUp(Elements.Dark));//黑暗守護
            skillHandlers.Add(941, new SkillDefinations.Warlock.SuckBlood());
            #endregion

            #region Cabalist
            skillHandlers.Add(2229, new SkillDefinations.Cabalist.GrimReaper());
            skillHandlers.Add(2230, new SkillDefinations.Cabalist.SoulSteal());
            skillHandlers.Add(3092, new SkillDefinations.Enchanter.ElementCircle(Elements.Dark));//暗黑結界（ダークパワーサークル）
            skillHandlers.Add(3087, new SkillDefinations.Cabalist.Fanaticism());
            skillHandlers.Add(3089, new SkillDefinations.Cabalist.DarkStorm());
            skillHandlers.Add(3274, new SkillDefinations.Cabalist.MoveDownCircle());
            skillHandlers.Add(3021, new SkillDefinations.Cabalist.SleepCloud());
            skillHandlers.Add(949, new SkillDefinations.Cabalist.AllRateUp());
            skillHandlers.Add(3167, new SkillDefinations.Cabalist.DarkChopMark());
            skillHandlers.Add(3166, new SkillDefinations.Cabalist.ChopMark());
            skillHandlers.Add(3270, new SkillDefinations.Cabalist.HitAndAway());
            skillHandlers.Add(3273, new SkillDefinations.Cabalist.StoneSkin());
            skillHandlers.Add(3272, new SkillDefinations.Cabalist.RandMark());
            skillHandlers.Add(3309, new SkillDefinations.Cabalist.ChgstRand());
            skillHandlers.Add(3310, new SkillDefinations.Cabalist.EventSelfDarkStorm());
            skillHandlers.Add(3346, new SkillDefinations.Cabalist.Sacrifice());
            skillHandlers.Add(10000, new SkillDefinations.Cabalist.EffDarkChopMark());
            #endregion

            #region Fencer
            skillHandlers.Add(2007, new SkillDefinations.Fencer.SpearCancel());
            skillHandlers.Add(2003, new SkillDefinations.Fencer.MobDefUpSelf());
            skillHandlers.Add(106, new SkillDefinations.Fencer.GuardUp());
            skillHandlers.Add(2064, new SkillDefinations.Fencer.AstuteStab());
            #endregion

            #region Knight
            skillHandlers.Add(2123, new SkillDefinations.Knight.ShockWave());
            skillHandlers.Add(2247, new SkillDefinations.Knight.AtkUnDead());
            skillHandlers.Add(946, new SkillDefinations.Knight.ConSpear());
            skillHandlers.Add(2065, new SkillDefinations.Knight.AstuteBlow());
            skillHandlers.Add(934, new SkillDefinations.Global.ElementAddUp(Elements.Holy, "LightAddUp"));
            skillHandlers.Add(2041, new SkillDefinations.Knight.DifrectArrow());
            skillHandlers.Add(2063, new SkillDefinations.Knight.AstuteSlash());
            skillHandlers.Add(2245, new SkillDefinations.Knight.CutDownSpear());
            skillHandlers.Add(4025, new SkillDefinations.Knight.DJoint());
            skillHandlers.Add(4029, new SkillDefinations.Knight.AProtect());
            skillHandlers.Add(2248, new SkillDefinations.Knight.HoldShield());
            skillHandlers.Add(2246, new SkillDefinations.Knight.HitRow());
            skillHandlers.Add(2125, new SkillDefinations.Knight.Valkyrie());
            skillHandlers.Add(3251, new SkillDefinations.Knight.VicariouslyResu());
            skillHandlers.Add(2381, new SkillDefinations.Knight.DirlineRandSeq());
            skillHandlers.Add(2382, new SkillDefinations.Knight.DirlineRandSeq2());
            skillHandlers.Add(2061, new SkillDefinations.Knight.Revive());

            #endregion

            #region Tatarabe
            skillHandlers.Add(2009, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2051, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2083, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2185, new SkillDefinations.Tatarabe.AtkRow());
            skillHandlers.Add(800, new SkillDefinations.Tatarabe.RockInfo());
            skillHandlers.Add(2200, new SkillDefinations.Tatarabe.EventCampfire());
            skillHandlers.Add(2071, new SkillDefinations.Tatarabe.PosturetorToise());
            skillHandlers.Add(905, new SkillDefinations.Tatarabe.GoRiKi());
            skillHandlers.Add(2177, new SkillDefinations.Tatarabe.StoneThrow());
            skillHandlers.Add(2135, new SkillDefinations.Tatarabe.ThrowDirt());

            #endregion

            #region Blacksmith
            skillHandlers.Add(2010, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2017, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(3342, new SkillDefinations.Blacksmith.FrameHart());
            skillHandlers.Add(2224, new SkillDefinations.Blacksmith.RockCrash());
            skillHandlers.Add(2198, new SkillDefinations.Blacksmith.FirstAid());
            skillHandlers.Add(2194, new SkillDefinations.Blacksmith.KnifeGrinder());
            skillHandlers.Add(2388, new SkillDefinations.Blacksmith.ThrowNugget());
            skillHandlers.Add(2387, new SkillDefinations.Blacksmith.EearthCrash());
            skillHandlers.Add(6050, new SkillDefinations.Blacksmith.PetAttack());
            skillHandlers.Add(6051, new SkillDefinations.Blacksmith.PetBack());
            skillHandlers.Add(2207, new SkillDefinations.Blacksmith.RockAnalysis());
            skillHandlers.Add(6102, new SkillDefinations.Global.PetCastSkill(6103, "MACHINE"));
            skillHandlers.Add(6103, new SkillDefinations.Blacksmith.PetMacAtk());
            skillHandlers.Add(6101, new SkillDefinations.Blacksmith.PetMacLHitUp());
            skillHandlers.Add(930, new SkillDefinations.Blacksmith.FireAddup());
            skillHandlers.Add(6104, new SkillDefinations.Global.PetCastSkill(6105, "MACHINE"));
            skillHandlers.Add(6105, new SkillDefinations.Blacksmith.PetMacCircleAtk());
            skillHandlers.Add(2262, new SkillDefinations.Blacksmith.SupportRockInfo());
            skillHandlers.Add(2253, new SkillDefinations.Blacksmith.GuideRock());
            skillHandlers.Add(2261, new SkillDefinations.Blacksmith.DurDownCancel());
            skillHandlers.Add(2395, new SkillDefinations.Blacksmith.Balls());
            skillHandlers.Add(942, new SkillDefinations.Blacksmith.BoostPower());
            skillHandlers.Add(409, new SkillDefinations.Blacksmith.StoDamUp());
            skillHandlers.Add(410, new SkillDefinations.Blacksmith.StoHitUp());
            skillHandlers.Add(411, new SkillDefinations.Blacksmith.StoAvoUp());


            #endregion

            #region Machinery
            skillHandlers.Add(2039, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(809, new SkillDefinations.Machinery.MachineInfo());
            skillHandlers.Add(132, new SkillDefinations.Machinery.RobotDamUp());
            skillHandlers.Add(970, new SkillDefinations.Machinery.RobotRecUp());
            skillHandlers.Add(964, new SkillDefinations.Machinery.RobotHpUp());
            skillHandlers.Add(2326, new SkillDefinations.Machinery.RobotAmobm());
            skillHandlers.Add(968, new SkillDefinations.Machinery.RobotHitUp());
            skillHandlers.Add(966, new SkillDefinations.Machinery.RobotDefUp());
            skillHandlers.Add(2323, new SkillDefinations.Machinery.RobotChaff());
            skillHandlers.Add(969, new SkillDefinations.Machinery.RobotAvoUp());
            skillHandlers.Add(965, new SkillDefinations.Machinery.RobotAtkUp());
            skillHandlers.Add(2324, new SkillDefinations.Machinery.MirrorSkill());
            skillHandlers.Add(2325, new SkillDefinations.Machinery.RobotTeleport());
            skillHandlers.Add(2322, new SkillDefinations.Machinery.RobotBerserk());
            skillHandlers.Add(2368, new SkillDefinations.Machinery.RobotChillLaser());
            skillHandlers.Add(2327, new SkillDefinations.Machinery.RobotSalvoFire());
            skillHandlers.Add(2369, new SkillDefinations.Machinery.RobotEcm());
            skillHandlers.Add(2422, new SkillDefinations.Machinery.RobotFireRadiation());
            skillHandlers.Add(2424, new SkillDefinations.Machinery.RobotOverTune());
            skillHandlers.Add(2423, new SkillDefinations.Machinery.RobotSparkBall());
            skillHandlers.Add(2425, new SkillDefinations.Machinery.RobotLovageCannon());
            skillHandlers.Add(506, new SkillDefinations.Machinery.MciDamUp());
            skillHandlers.Add(507, new SkillDefinations.Machinery.MciHitUp());
            skillHandlers.Add(508, new SkillDefinations.Machinery.MciAvoUp());

            #endregion

            #region Farmasist
            skillHandlers.Add(2020, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2034, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2040, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2054, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2085, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2086, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2089, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(3128, new SkillDefinations.Farmasist.Cultivation());
            skillHandlers.Add(807, new SkillDefinations.Farmasist.PlantInfo());
            skillHandlers.Add(804, new SkillDefinations.Farmasist.TreeInfo());
            skillHandlers.Add(2169, new SkillDefinations.Farmasist.GrassTrap());
            skillHandlers.Add(2170, new SkillDefinations.Farmasist.PitTrap());
            skillHandlers.Add(2196, new SkillDefinations.Farmasist.HealingTree());
            #endregion

            #region Alchemist
            skillHandlers.Add(2022, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2118, new SkillDefinations.Alchemist.Phalanx());
            skillHandlers.Add(3096, new SkillDefinations.Alchemist.DelayTrap());
            skillHandlers.Add(2389, new SkillDefinations.Alchemist.DustExplosion());
            skillHandlers.Add(2214, new SkillDefinations.Alchemist.PlantAnalysis());
            skillHandlers.Add(954, new SkillDefinations.Alchemist.FoodThrow());
            skillHandlers.Add(909, new SkillDefinations.Alchemist.PotionFighter());
            skillHandlers.Add(406, new SkillDefinations.Alchemist.PlaDamUp());
            skillHandlers.Add(407, new SkillDefinations.Alchemist.PlaHitUp());
            skillHandlers.Add(408, new SkillDefinations.Alchemist.PlaAvoUp());
            skillHandlers.Add(6202, new SkillDefinations.Global.PetCastSkill(6203, "PLANT"));
            skillHandlers.Add(6203, new SkillDefinations.Alchemist.PetPlantAtk());
            skillHandlers.Add(6206, new SkillDefinations.Global.PetCastSkill(6207, "PLANT"));
            skillHandlers.Add(6207, new SkillDefinations.Alchemist.PetPlantDefupSelf());
            skillHandlers.Add(6204, new SkillDefinations.Global.PetCastSkill(6205, "PLANT"));
            skillHandlers.Add(6205, new SkillDefinations.Alchemist.PetPlantHealing());
            skillHandlers.Add(2263, new SkillDefinations.Alchemist.SupportPlantInfo());
            skillHandlers.Add(943, new SkillDefinations.Alchemist.BoostMagic());
            skillHandlers.Add(2390, new SkillDefinations.Alchemist.ThrowChemical());
            skillHandlers.Add(3343, new SkillDefinations.Alchemist.SumChemicalPlant());
            skillHandlers.Add(3344, new SkillDefinations.Alchemist.SumChemicalPlant2());
            skillHandlers.Add(6200, new SkillDefinations.Global.PetCastSkill(6201, "PLANT"));
            skillHandlers.Add(6201, new SkillDefinations.Alchemist.PetPlantPoison());
            skillHandlers.Add(2211, new SkillDefinations.Alchemist.TreeAnalysis());
            #endregion

            #region Marionest
            skillHandlers.Add(2038, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(133, new SkillDefinations.Marionest.MarioDamUp());
            skillHandlers.Add(2328, new SkillDefinations.Marionest.MarioCtrl());
            skillHandlers.Add(967, new SkillDefinations.Marionest.MarioCtrlMove());
            skillHandlers.Add(2329, new SkillDefinations.Marionest.MarioOver());
            skillHandlers.Add(2331, new SkillDefinations.Marionest.EnemyCharming());
            skillHandlers.Add(2335, new SkillDefinations.Marionest.MarioEarthWater());
            skillHandlers.Add(2334, new SkillDefinations.Marionest.MarioWindEarth());
            skillHandlers.Add(2333, new SkillDefinations.Marionest.MarioFireWind());
            skillHandlers.Add(2332, new SkillDefinations.Marionest.MarioWaterFire());
            skillHandlers.Add(2371, new SkillDefinations.Marionest.Puppet());
            skillHandlers.Add(976, new SkillDefinations.Marionest.MarioTimeUp());
            skillHandlers.Add(2370, new SkillDefinations.Marionest.MarionetteHarmony());
            skillHandlers.Add(980, new SkillDefinations.Marionest.ChangeMarionette());
            skillHandlers.Add(3333, new SkillDefinations.Marionest.MarioCancel());
            skillHandlers.Add(981, new SkillDefinations.Marionest.MariostateUp());
            skillHandlers.Add(3334, new SkillDefinations.Marionest.SumMario(26040001, 3335));
            skillHandlers.Add(3335, new SkillDefinations.Marionest.SumMarioCont(Elements.Fire));
            skillHandlers.Add(3336, new SkillDefinations.Marionest.SumMario(26100009, 3337));
            skillHandlers.Add(3337, new SkillDefinations.Marionest.SumMarioCont(Elements.Water));
            skillHandlers.Add(3338, new SkillDefinations.Marionest.SumMario(26100009, 3339));
            skillHandlers.Add(3339, new SkillDefinations.Marionest.SumMarioCont(Elements.Wind));
            skillHandlers.Add(3340, new SkillDefinations.Marionest.SumMario(26070003, 3341));
            skillHandlers.Add(3341, new SkillDefinations.Marionest.SumMarioCont(Elements.Earth));
            #endregion

            #region Ranger
            skillHandlers.Add(2088, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(713, new SkillDefinations.Ranger.Bivouac());
            skillHandlers.Add(805, new SkillDefinations.Ranger.InsectInfo());
            skillHandlers.Add(806, new SkillDefinations.Ranger.BirdInfo());
            skillHandlers.Add(808, new SkillDefinations.Ranger.AnimalInfo());
            skillHandlers.Add(812, new SkillDefinations.Ranger.WataniInfo());
            skillHandlers.Add(816, new SkillDefinations.Ranger.TreasureInfo());
            skillHandlers.Add(2197, new SkillDefinations.Ranger.CswarSleep());
            skillHandlers.Add(2103, new SkillDefinations.Ranger.Unlock());
            skillHandlers.Add(403, new SkillDefinations.Ranger.AniDamUp());
            skillHandlers.Add(404, new SkillDefinations.Ranger.AniHitUp());
            skillHandlers.Add(405, new SkillDefinations.Ranger.AniAvoUp());
            skillHandlers.Add(415, new SkillDefinations.Ranger.WanDamUp());
            skillHandlers.Add(416, new SkillDefinations.Ranger.WanHitUp());
            skillHandlers.Add(417, new SkillDefinations.Ranger.WanAvoUp());
            #endregion

            #region Druid
            skillHandlers.Add(3146, new SkillDefinations.Druid.CureAll());
            skillHandlers.Add(3307, new SkillDefinations.Druid.RegiAllUp());
            skillHandlers.Add(3308, new SkillDefinations.Druid.AreaHeal());
            skillHandlers.Add(3257, new SkillDefinations.Druid.STR_VIT_AGI_UP());
            skillHandlers.Add(3258, new SkillDefinations.Druid.MAG_INT_DEX_UP());
            skillHandlers.Add(3056, new SkillDefinations.Druid.HolyFeather());
            skillHandlers.Add(3164, new SkillDefinations.Druid.FlashLight());
            //skillHandlers.Add(3080, new SkillDefinations.Enchanter.ElementCircle(Elements.Holy));//熾天使之翼（ホーリーパワーサークル
            skillHandlers.Add(3163, new SkillDefinations.Druid.SunLightShower());
            skillHandlers.Add(3268, new SkillDefinations.Druid.CriAvoDownOne());
            skillHandlers.Add(3266, new SkillDefinations.Druid.LightHigeCircle());
            skillHandlers.Add(3118, new SkillDefinations.Druid.Seal());
            skillHandlers.Add(2210, new SkillDefinations.Druid.UndeadAnalysis());
            skillHandlers.Add(3267, new SkillDefinations.Druid.UndeadMdefDownOne());
            skillHandlers.Add(3077, new SkillDefinations.Druid.ClairvoYance());
            skillHandlers.Add(3119, new SkillDefinations.Druid.SealMagic());
            skillHandlers.Add(950, new SkillDefinations.Druid.TranceSpdUp());
            skillHandlers.Add(940, new SkillDefinations.Global.ElementLimitUp(Elements.Holy));//光明守護
            skillHandlers.Add(509, new SkillDefinations.Druid.UndDamUp());
            skillHandlers.Add(510, new SkillDefinations.Druid.UndHitUp());
            skillHandlers.Add(511, new SkillDefinations.Druid.UndAvoUp());
            skillHandlers.Add(3345, new SkillDefinations.Druid.AllHealing());
            #endregion

            #region Bard
            skillHandlers.Add(2310, new SkillDefinations.Bard.DefUpCircle());
            skillHandlers.Add(3323, new SkillDefinations.Bard.DeadMarch());
            skillHandlers.Add(2313, new SkillDefinations.Bard.HPSPMPUPCircle());
            skillHandlers.Add(2311, new SkillDefinations.Bard.AVOUPCircle());
            skillHandlers.Add(2312, new SkillDefinations.Bard.CRIUPCircle());
            skillHandlers.Add(2309, new SkillDefinations.Bard.ATKUPCircle());
            skillHandlers.Add(2367, new SkillDefinations.Bard.MusicalBlow());
            skillHandlers.Add(2366, new SkillDefinations.Bard.Shout());
            skillHandlers.Add(2365, new SkillDefinations.Bard.HMSPRateUp());
            skillHandlers.Add(2315, new SkillDefinations.Bard.BardSession());
            skillHandlers.Add(3321, new SkillDefinations.Bard.ORaToRiO());
            skillHandlers.Add(2308, new SkillDefinations.Bard.MAGINTDEXUPCircle());
            skillHandlers.Add(2307, new SkillDefinations.Bard.STRVITAGIUPCircle());
            skillHandlers.Add(2306, new SkillDefinations.Bard.ChangeMusic());
            skillHandlers.Add(131, new SkillDefinations.Bard.MusicalDamUp());
            skillHandlers.Add(2314, new SkillDefinations.Bard.Requiem());
            skillHandlers.Add(3322, new SkillDefinations.Bard.AttractMarch());
            skillHandlers.Add(3320, new SkillDefinations.Bard.LoudSong());
            #endregion

            #region Sage
            skillHandlers.Add(2030, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2031, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2032, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2033, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2296, new SkillDefinations.Sage.IntelRides());
            skillHandlers.Add(3169, new SkillDefinations.Sage.EnergyStorm());
            skillHandlers.Add(2330, new SkillDefinations.Sage.EnergyFreak());
            skillHandlers.Add(3291, new SkillDefinations.Sage.EnergyFlare());
            skillHandlers.Add(3292, new SkillDefinations.Sage.ChgstBlock());
            skillHandlers.Add(3312, new SkillDefinations.Sage.LuminaryNova());
            skillHandlers.Add(3315, new SkillDefinations.Sage.LastInQuest());
            skillHandlers.Add(3313, new SkillDefinations.Sage.AReflection());
            skillHandlers.Add(130, new SkillDefinations.Sage.ReadDamup());
            skillHandlers.Add(2295, new SkillDefinations.Sage.StaffCtrl());
            skillHandlers.Add(2297, new SkillDefinations.Sage.Provide());
            skillHandlers.Add(2294, new SkillDefinations.Sage.MonsterSketch());
            skillHandlers.Add(3293, new SkillDefinations.Sage.MagHitUpCircle());
            skillHandlers.Add(3314, new SkillDefinations.Sage.SumDop());
            #endregion

            #region Necromancer
            skillHandlers.Add(3331, new SkillDefinations.Necromancer.Dejion());
            skillHandlers.Add(2316, new SkillDefinations.Necromancer.AtkMp());
            skillHandlers.Add(2317, new SkillDefinations.Necromancer.AtkSp());
            skillHandlers.Add(3288, new SkillDefinations.Necromancer.DarkLight());
            skillHandlers.Add(3330, new SkillDefinations.Necromancer.EvilSoul());
            skillHandlers.Add(3332, new SkillDefinations.Necromancer.ChaosGait());
            skillHandlers.Add(2318, new SkillDefinations.Necromancer.AbsorbHpWeapon());
            skillHandlers.Add(2320, new SkillDefinations.Necromancer.SummobLemures());
            skillHandlers.Add(961, new SkillDefinations.Necromancer.LemuresHpUp());
            skillHandlers.Add(963, new SkillDefinations.Necromancer.LemuresMatkUp());
            skillHandlers.Add(962, new SkillDefinations.Necromancer.LemuresAtkUp());
            skillHandlers.Add(2321, new SkillDefinations.Necromancer.HealLemures());
            skillHandlers.Add(2319, new SkillDefinations.Necromancer.Rebone());
            skillHandlers.Add(3121, new SkillDefinations.Necromancer.NeKuRoMaNShi());
            skillHandlers.Add(315, new SkillDefinations.Necromancer.ChgstDamUp());
            skillHandlers.Add(3122, new SkillDefinations.Necromancer.TrDrop2());
            skillHandlers.Add(3297, new SkillDefinations.Necromancer.Terror());
            skillHandlers.Add(3324, new SkillDefinations.Necromancer.SumDeath());
            skillHandlers.Add(3325, new SkillDefinations.Necromancer.SumDeath2());
            skillHandlers.Add(3326, new SkillDefinations.Necromancer.SumDeath3());
            skillHandlers.Add(3327, new SkillDefinations.Necromancer.SumDeath4());
            skillHandlers.Add(3328, new SkillDefinations.Necromancer.SumDeath5());
            skillHandlers.Add(3329, new SkillDefinations.Necromancer.SumDeath6());
            #endregion

            #region Soul
            skillHandlers.Add(4450, new SkillDefinations.Global.Soul());//SKILL_P_T_SWORDMAN,光戰士之魂
            skillHandlers.Add(4451, new SkillDefinations.Global.Soul());//SKILL_P_T_KINGHT,聖騎士之魂
            skillHandlers.Add(4452, new SkillDefinations.Global.Soul());//SKILL_P_T_ASSASSIN,暗殺者之魂
            skillHandlers.Add(4453, new SkillDefinations.Global.Soul());//SKILL_P_T_ARCHER,弓手之魂
            skillHandlers.Add(4454, new SkillDefinations.Global.Soul());//SKILL_P_T_MAGIC,魔導士之魂
            skillHandlers.Add(4455, new SkillDefinations.Global.Soul());//SKILL_P_T_ELEMENTAL,元素術師之魂
            skillHandlers.Add(4460, new SkillDefinations.Global.Soul());//SKILL_P_T_DRUID,神官之魂
            skillHandlers.Add(4461, new SkillDefinations.Global.Soul());//SKILL_P_T_KABBALIST,暗黑神官之魂
            skillHandlers.Add(4462, new SkillDefinations.Global.Soul());//SKILL_P_T_BLACKSMITH,鐵匠之魂
            skillHandlers.Add(4463, new SkillDefinations.Global.Soul());//SKILL_P_T_ALCHEMIST,鍊金術師之魂
            skillHandlers.Add(4464, new SkillDefinations.Global.Soul());//SKILL_P_T_EXPLORER,探險家之魂
            skillHandlers.Add(4465, new SkillDefinations.Global.Soul());//SKILL_P_T_TRADER,拜金使之魂
            #endregion

            #region DarkStalker

            skillHandlers.Add(2357, new SkillDefinations.DarkStalker.DarkMist());
            skillHandlers.Add(2356, new SkillDefinations.DarkStalker.LifeSteal());
            skillHandlers.Add(3289, new SkillDefinations.DarkStalker.DegradetionDarkFlare());
            skillHandlers.Add(3290, new SkillDefinations.DarkStalker.DegradetionDarkFlare());
            skillHandlers.Add(2403, new SkillDefinations.DarkStalker.FlareSting());
            skillHandlers.Add(2404, new SkillDefinations.DarkStalker.FlareSting2());
            skillHandlers.Add(2405, new SkillDefinations.DarkStalker.BloodAbsrd());
            skillHandlers.Add(3120, new SkillDefinations.DarkStalker.Spell());
            skillHandlers.Add(957, new SkillDefinations.DarkStalker.NecroResu());
            skillHandlers.Add(314, new SkillDefinations.DarkStalker.ChgstSwoDamUp());
            skillHandlers.Add(2277, new SkillDefinations.DarkStalker.CancelLightCircle());
            skillHandlers.Add(958, new SkillDefinations.DarkStalker.DarkProtect());
            skillHandlers.Add(935, new SkillDefinations.Global.ElementAddUp(Elements.Dark, "DarkAddUp"));
            skillHandlers.Add(2278, new SkillDefinations.DarkStalker.LightSeal());
            skillHandlers.Add(2279, new SkillDefinations.DarkStalker.BradStigma());
            skillHandlers.Add(2358, new SkillDefinations.DarkStalker.HpLostDamUp());
            skillHandlers.Add(979, new SkillDefinations.DarkStalker.HpDownToDamUp());
            skillHandlers.Add(2406, new SkillDefinations.DarkStalker.DarknessOfNight());
            skillHandlers.Add(2407, new SkillDefinations.DarkStalker.DarknessOfNight2());
            skillHandlers.Add(500, new SkillDefinations.DarkStalker.EleDamUp());
            skillHandlers.Add(501, new SkillDefinations.DarkStalker.EleHitUp());
            skillHandlers.Add(502, new SkillDefinations.DarkStalker.EleAvoUp());
            #endregion

            #region Striker
            skillHandlers.Add(2149, new SkillDefinations.Striker.ElementArrow(Elements.Fire));
            skillHandlers.Add(2150, new SkillDefinations.Striker.ElementArrow(Elements.Water));
            skillHandlers.Add(2151, new SkillDefinations.Striker.ElementArrow(Elements.Earth));
            skillHandlers.Add(2152, new SkillDefinations.Striker.ElementArrow(Elements.Wind));
            skillHandlers.Add(2220, new SkillDefinations.Striker.PotionArrow());
            skillHandlers.Add(2190, new SkillDefinations.Striker.LightDarkArrow(Elements.Holy));
            skillHandlers.Add(2191, new SkillDefinations.Striker.LightDarkArrow(Elements.Dark));
            skillHandlers.Add(313, new SkillDefinations.Striker.HuntingTactics());
            skillHandlers.Add(2267, new SkillDefinations.Striker.BowCastCancelOne());
            skillHandlers.Add(310, new SkillDefinations.Striker.ChgstArrDamUp());
            skillHandlers.Add(2385, new SkillDefinations.Striker.ArmBreak());
            skillHandlers.Add(6500, new SkillDefinations.Striker.PetBirdAtkRowCircle());
            skillHandlers.Add(6501, new SkillDefinations.Striker.PetBirdAtkRowCircle2());
            skillHandlers.Add(6306, new SkillDefinations.Striker.DogHateUpCircle());
            skillHandlers.Add(6307, new SkillDefinations.Striker.PetDogHateUpCircle());
            skillHandlers.Add(6502, new SkillDefinations.Striker.BirdAtk());
            skillHandlers.Add(6503, new SkillDefinations.Striker.PetBirdAtk());
            skillHandlers.Add(6308, new SkillDefinations.Global.PetCastSkill(6309, "ANIMAL"));
            skillHandlers.Add(6309, new SkillDefinations.Striker.PetDogAtkCircle());
            skillHandlers.Add(6550, new SkillDefinations.Striker.BirdDamUp());
            skillHandlers.Add(6350, new SkillDefinations.Striker.DogHpUp());
            skillHandlers.Add(6310, new SkillDefinations.Global.PetCastSkill(6311, "ANIMAL"));
            skillHandlers.Add(6311, new SkillDefinations.Striker.PetDogDefUp());
            #endregion

            #region Gunner
            skillHandlers.Add(2285, new SkillDefinations.Gunner.FastDraw());
            skillHandlers.Add(2286, new SkillDefinations.Gunner.PluralityShot());
            skillHandlers.Add(2287, new SkillDefinations.Gunner.ChargeShot());
            skillHandlers.Add(2289, new SkillDefinations.Gunner.GrenadeShot());
            skillHandlers.Add(2291, new SkillDefinations.Gunner.GrenadeSlow());
            skillHandlers.Add(2292, new SkillDefinations.Gunner.GrenadeStan());
            skillHandlers.Add(2163, new SkillDefinations.Gunner.StunShot());
            skillHandlers.Add(2288, new SkillDefinations.Gunner.BurstShot());
            skillHandlers.Add(974, new SkillDefinations.Gunner.CQB());
            skillHandlers.Add(2364, new SkillDefinations.Gunner.GunCancel());
            skillHandlers.Add(2418, new SkillDefinations.Gunner.VitalShot());
            skillHandlers.Add(2419, new SkillDefinations.Gunner.ClothCrest());
            skillHandlers.Add(126, new SkillDefinations.Gunner.RifleGunDamUp());
            skillHandlers.Add(2290, new SkillDefinations.Gunner.ApiBullet());
            skillHandlers.Add(210, new SkillDefinations.Gunner.GunHitUp());
            skillHandlers.Add(2293, new SkillDefinations.Gunner.OverRange());
            skillHandlers.Add(2363, new SkillDefinations.Gunner.BulletDance());
            skillHandlers.Add(2420, new SkillDefinations.Gunner.PrecisionFire());
            skillHandlers.Add(2421, new SkillDefinations.Gunner.CanisterShot());
            #endregion

            #region Explorer
            skillHandlers.Add(2222, new SkillDefinations.Explorer.CaveHiding());
            skillHandlers.Add(2392, new SkillDefinations.Explorer.Blinding());
            skillHandlers.Add(2221, new SkillDefinations.Explorer.CaveBivouac());
            skillHandlers.Add(2212, new SkillDefinations.Explorer.InsectAnalysis());
            skillHandlers.Add(2213, new SkillDefinations.Explorer.BirdAnalysis());
            skillHandlers.Add(2215, new SkillDefinations.Explorer.AnimalAnalysis());
            skillHandlers.Add(2264, new SkillDefinations.Explorer.SupportInfo());
            skillHandlers.Add(953, new SkillDefinations.Explorer.BaitTrap());
            skillHandlers.Add(311, new SkillDefinations.Explorer.TrapDamUp());
            skillHandlers.Add(944, new SkillDefinations.Explorer.BoostHp());
            skillHandlers.Add(2391, new SkillDefinations.Explorer.FakeDeath());
            skillHandlers.Add(6300, new SkillDefinations.Global.PetCastSkill(6301, "ANIMAL"));
            skillHandlers.Add(6301, new SkillDefinations.Explorer.PetDogSlash());
            skillHandlers.Add(6302, new SkillDefinations.Global.PetCastSkill(6303, "ANIMAL"));
            skillHandlers.Add(6303, new SkillDefinations.Explorer.PetDogStan());
            skillHandlers.Add(6304, new SkillDefinations.Global.PetCastSkill(6305, "ANIMAL"));
            skillHandlers.Add(6305, new SkillDefinations.Explorer.PetDogLineatk());
            skillHandlers.Add(2171, new SkillDefinations.Explorer.BarbedTrap());
            skillHandlers.Add(2172, new SkillDefinations.Explorer.Bungestac());
            skillHandlers.Add(412, new SkillDefinations.Explorer.InsDamUp());
            skillHandlers.Add(413, new SkillDefinations.Explorer.InsHitUp());
            skillHandlers.Add(414, new SkillDefinations.Explorer.InsAvoUp());
            #endregion

            #region TreasureHunter
            skillHandlers.Add(2336, new SkillDefinations.TreasureHunter.BackRush());
            skillHandlers.Add(2337, new SkillDefinations.TreasureHunter.Catch());
            skillHandlers.Add(2340, new SkillDefinations.TreasureHunter.ConthWhip());
            skillHandlers.Add(2373, new SkillDefinations.TreasureHunter.WhipFlourish());
            skillHandlers.Add(2426, new SkillDefinations.TreasureHunter.Caution());
            skillHandlers.Add(2372, new SkillDefinations.TreasureHunter.Snatch());
            skillHandlers.Add(2427, new SkillDefinations.TreasureHunter.PullWhip());
            skillHandlers.Add(2430, new SkillDefinations.TreasureHunter.SonicWhip());
            skillHandlers.Add(129, new SkillDefinations.TreasureHunter.RopeDamUp());
            skillHandlers.Add(2341, new SkillDefinations.TreasureHunter.WeaponRemove());
            skillHandlers.Add(2342, new SkillDefinations.TreasureHunter.ArmorRemove());
            skillHandlers.Add(134, new SkillDefinations.TreasureHunter.UnlockDamUp());
            skillHandlers.Add(2429, new SkillDefinations.TreasureHunter.Escape());
            #endregion

            #region Merchant
            skillHandlers.Add(702, new SkillDefinations.Merchant.Packing());
            skillHandlers.Add(703, new SkillDefinations.Merchant.BuyRateDown());
            skillHandlers.Add(704, new SkillDefinations.Merchant.SellRateUp());
            skillHandlers.Add(2173, new SkillDefinations.Merchant.AtkUpOne());
            skillHandlers.Add(2180, new SkillDefinations.Merchant.SunSofbley());
            skillHandlers.Add(2186, new SkillDefinations.Merchant.Magrow());
            #endregion

            #region Trader
            skillHandlers.Add(2394, new SkillDefinations.Trader.BugRand());
            skillHandlers.Add(705, new SkillDefinations.Trader.Trust());
            skillHandlers.Add(906, new SkillDefinations.Trader.BagDamUp());
            skillHandlers.Add(811, new SkillDefinations.Trader.HumanInfo());
            skillHandlers.Add(2223, new SkillDefinations.Trader.Shift());
            skillHandlers.Add(2225, new SkillDefinations.Trader.AgiDexUpOne());
            skillHandlers.Add(948, new SkillDefinations.Trader.BagCapDamup());
            skillHandlers.Add(2227, new SkillDefinations.Trader.Abetment());
            skillHandlers.Add(706, new SkillDefinations.Trader.Connection());
            skillHandlers.Add(2218, new SkillDefinations.Trader.HumanAnalysis());
            skillHandlers.Add(945, new SkillDefinations.Trader.BoostCritical());
            skillHandlers.Add(6400, new SkillDefinations.Trader.HumCustomary());
            skillHandlers.Add(6401, new SkillDefinations.Trader.PetHumCustomary());
            skillHandlers.Add(6404, new SkillDefinations.Trader.PetAtkupSelf());
            skillHandlers.Add(6405, new SkillDefinations.Trader.PetHitupSelf());
            skillHandlers.Add(6406, new SkillDefinations.Trader.PetDefupSelf());
            skillHandlers.Add(6402, new SkillDefinations.Trader.HumAdditional());
            skillHandlers.Add(6403, new SkillDefinations.Trader.PetHumAdditional());
            skillHandlers.Add(6407, new SkillDefinations.Trader.PetSlash());
            skillHandlers.Add(6408, new SkillDefinations.Trader.PetIai());
            skillHandlers.Add(6409, new SkillDefinations.Trader.PetProvocation());
            skillHandlers.Add(6410, new SkillDefinations.Trader.PetSennpuuken());
            skillHandlers.Add(6411, new SkillDefinations.Trader.PetMeditation());
            skillHandlers.Add(6450, new SkillDefinations.Trader.HumHealRateUp());
            #endregion

            #region Gambler
            skillHandlers.Add(3286, new SkillDefinations.Gambler.RandHeal());
            skillHandlers.Add(3287, new SkillDefinations.Gambler.RouletteHeal());
            skillHandlers.Add(2348, new SkillDefinations.Gambler.AtkDownOne());
            skillHandlers.Add(2374, new SkillDefinations.Gambler.CardBoomEran());
            skillHandlers.Add(2350, new SkillDefinations.Gambler.RandDamOne());
            skillHandlers.Add(2377, new SkillDefinations.Gambler.DoubleUp());
            skillHandlers.Add(2375, new SkillDefinations.Gambler.CoinShot());
            skillHandlers.Add(972, new SkillDefinations.Gambler.Blackleg());
            skillHandlers.Add(2347, new SkillDefinations.Gambler.Slot());
            skillHandlers.Add(973, new SkillDefinations.Gambler.BadLucky());
            skillHandlers.Add(2376, new SkillDefinations.Gambler.SkillBreak());
            skillHandlers.Add(2436, new SkillDefinations.Gambler.TrickDice());
            skillHandlers.Add(2433, new SkillDefinations.Gambler.SumArcanaCard());
            skillHandlers.Add(2432, new SkillDefinations.Gambler.SumArcanaCard2());
            skillHandlers.Add(2431, new SkillDefinations.Gambler.SumArcanaCard3());
            skillHandlers.Add(2434, new SkillDefinations.Gambler.SumArcanaCard4());
            skillHandlers.Add(2435, new SkillDefinations.Gambler.SumArcanaCard5());
            skillHandlers.Add(2351, new SkillDefinations.Gambler.RandChgstateCircle());
            skillHandlers.Add(2439, new SkillDefinations.Gambler.FlowerCard());
            skillHandlers.Add(2440, new SkillDefinations.Gambler.FlowerCardSEQ());
            skillHandlers.Add(2441, new SkillDefinations.Gambler.FlowerCardSEQ2());
            #endregion

            #region Pet
            skillHandlers.Add(6424, new SkillDefinations.Knight.Revive(2));
            skillHandlers.Add(6425, new SkillDefinations.Knight.Revive(5));
            #endregion

            #region Breeder
            skillHandlers.Add(1000, new SkillDefinations.Breeder.GrowUp());
            skillHandlers.Add(1001, new SkillDefinations.Breeder.Biology());
            skillHandlers.Add(1002, new SkillDefinations.Breeder.LionPower());
            skillHandlers.Add(1003, new SkillDefinations.Breeder.Reins());
            skillHandlers.Add(2442, new SkillDefinations.Breeder.TheTrust());
            skillHandlers.Add(2443, new SkillDefinations.Breeder.Metamorphosis());
            skillHandlers.Add(2444, new SkillDefinations.Breeder.PetDelayCancel());
            skillHandlers.Add(2445, new SkillDefinations.Breeder.Akurobattoibeijon());
            skillHandlers.Add(2446, new SkillDefinations.Breeder.HealFire());
            skillHandlers.Add(2447, new SkillDefinations.Breeder.Encouragement());
            #endregion

            #region Gardener
            skillHandlers.Add(2453, new SkillDefinations.Gardener.IAmTree());
            skillHandlers.Add(2449, new SkillDefinations.Gardener.GardenerSkill());
            skillHandlers.Add(1006, new SkillDefinations.Gardener.MoogCoalUp());
            skillHandlers.Add(2025, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(2455, new SkillDefinations.Gardener.Cabin());
            skillHandlers.Add(1004, new SkillDefinations.Gardener.GadenMaster());
            skillHandlers.Add(1005, new SkillDefinations.Gardener.Topiary());
            skillHandlers.Add(1007, new SkillDefinations.Gardener.Gardening());
            skillHandlers.Add(2452, new SkillDefinations.Gardener.WeatherControl());
            skillHandlers.Add(2451, new SkillDefinations.Gardener.HeavenlyControl());
            skillHandlers.Add(2454, new SkillDefinations.Gardener.Gathering());
            #endregion
            #region 新Boss
            //skillHandlers.Add(22000, new SkillDefinations.NewBoss.B1());
            skillHandlers.Add(22008, new SkillDefinations.NewBoss.WaterBall());
            #endregion

            #region 旅者
            skillHandlers.Add(23000, new SkillDefinations.Traveler.ChainLightning());
            skillHandlers.Add(23001, new SkillDefinations.Traveler.HartHeal());
            skillHandlers.Add(23002, new SkillDefinations.Traveler.ThunderFall());
            skillHandlers.Add(23003, new SkillDefinations.Traveler.ThunderFall_Effect());
            skillHandlers.Add(23004, new SkillDefinations.Traveler.Silva());
            skillHandlers.Add(23005, new SkillDefinations.Traveler.EarthQuake());
            skillHandlers.Add(23006, new SkillDefinations.Traveler.EarthQuake_Effect());
            #endregion

            #region 武器技能
            skillHandlers.Add(24000, new SkillDefinations.Item.WA_Neutral());
            #endregion

            #region FL1
            skillHandlers.Add(100, new SkillDefinations.Global.MaxHPUp());
            skillHandlers.Add(107, new SkillDefinations.Global.SwordMastery());
            skillHandlers.Add(109, new SkillDefinations.Global.SpearMastery());
            skillHandlers.Add(122, new SkillDefinations.Global.TwoAxeMastery());
            skillHandlers.Add(116, new SkillDefinations.Fencer.ShieldGuardUp());
            skillHandlers.Add(2112, new SkillDefinations.Swordman.ConfuseBlow());
            skillHandlers.Add(2138, new SkillDefinations.Fencer.LightningSpear());
            skillHandlers.Add(2121, new SkillDefinations.Fencer.ChargeStrike());
            skillHandlers.Add(2115, new SkillDefinations.Swordman.Iai());
            skillHandlers.Add(2201, new SkillDefinations.Swordman.Iai2());
            #endregion

            #region FL2-1
            skillHandlers.Add(2354, new SkillDefinations.BountyHunter.Gravity());
            skillHandlers.Add(2124, new SkillDefinations.BladeMaster.Sinkuha());
            skillHandlers.Add(119, new SkillDefinations.Global.TwoHandMastery());
            skillHandlers.Add(2202, new SkillDefinations.Swordman.Iai3());
            skillHandlers.Add(2116, new SkillDefinations.BladeMaster.aStormSword());
            skillHandlers.Add(2002, new SkillDefinations.Swordman.ATKUp());
            skillHandlers.Add(2239, new SkillDefinations.BladeMaster.Transporter());
            //skillHandlers.Add(25010, new SkillDefinations.FL2_1.FireSlash());
            //skillHandlers.Add(25011, new SkillDefinations.FL2_1.ArmorBreaker());
            
            #endregion

            #region FL2-2
            skillHandlers.Add(2228, new SkillDefinations.Knight.HolyBlade());
            skillHandlers.Add(2276, new SkillDefinations.DarkStalker.DarkVacuum());
            skillHandlers.Add(123, new SkillDefinations.Global.RapierMastery());
            skillHandlers.Add(3170, new SkillDefinations.Knight.Healing());
            skillHandlers.Add(120, new SkillDefinations.Global.TwoSpearMastery());
            skillHandlers.Add(2383, new SkillDefinations.Knight.Appeal());
            skillHandlers.Add(2249, new SkillDefinations.Knight.StrikeSpear());
            skillHandlers.Add(25020, new SkillDefinations.FL2_2.IceStab());
            skillHandlers.Add(25021, new SkillDefinations.FL2_2.ShieldDefence());
            skillHandlers.Add(25022, new SkillDefinations.FL2_2.ShieldBash());
            #endregion

            #region FR1
            skillHandlers.Add(2042, new SkillDefinations.Scout.Hiding());
            skillHandlers.Add(102, new SkillDefinations.Global.MaxSPUp());
            skillHandlers.Add(2000, new SkillDefinations.Swordman.HitMeleeUp());
            skillHandlers.Add(2110, new SkillDefinations.Swordman.Blow());
            skillHandlers.Add(2004, new SkillDefinations.Scout.AvoidMeleeUp());
            skillHandlers.Add(2035, new SkillDefinations.Global.Synthese());
            skillHandlers.Add(110, new SkillDefinations.Global.ShortSwordMastery());
            skillHandlers.Add(2126, new SkillDefinations.Scout.VitalAttack());
            skillHandlers.Add(2119, new SkillDefinations.Scout.Brandish());
            skillHandlers.Add(114, new SkillDefinations.Archer.LAvoUp());
            skillHandlers.Add(2129, new SkillDefinations.Archer.ChargeArrow());
            skillHandlers.Add(2148, new SkillDefinations.Striker.PluralityArrow());
            skillHandlers.Add(112, new SkillDefinations.Global.ThrowMastery());
            skillHandlers.Add(2127, new SkillDefinations.Scout.ConThrow());
            skillHandlers.Add(908, new SkillDefinations.Assassin.ThrowRangeUp());
            #endregion

            #region FR2-1
            skillHandlers.Add(2113, new SkillDefinations.Swordman.StunBlow());
            skillHandlers.Add(2043, new SkillDefinations.Assassin.Cloaking());
            skillHandlers.Add(2384, new SkillDefinations.Assassin.Raid());
            skillHandlers.Add(977, new SkillDefinations.Assassin.AvoidUp());
            skillHandlers.Add(2068, new SkillDefinations.Assassin.BackAtk());
            skillHandlers.Add(312, new SkillDefinations.Assassin.CriDamUp());
            skillHandlers.Add(118, new SkillDefinations.Assassin.ClawMastery());
            skillHandlers.Add(2360, new SkillDefinations.Command.CyclOne());
            skillHandlers.Add(2140, new SkillDefinations.Assassin.PosionNeedle());
            skillHandlers.Add(26010, new SkillDefinations.FR2_1.ThrowThrowThrow());
            #endregion

            #region FR2-2
            skillHandlers.Add(951, new SkillDefinations.Striker.ShotStance());
            skillHandlers.Add(2049, new SkillDefinations.Archer.LHitUp());
            skillHandlers.Add(2130, new SkillDefinations.Striker.BlastArrow());
            skillHandlers.Add(2386, new SkillDefinations.Striker.ArrowGroove());
            skillHandlers.Add(2144, new SkillDefinations.FR2_2.FireArrow());
            skillHandlers.Add(2145, new SkillDefinations.FR2_2.WaterArrow());
            skillHandlers.Add(2146, new SkillDefinations.FR2_2.EarthArrow());
            skillHandlers.Add(2147, new SkillDefinations.FR2_2.WindArrow());
            skillHandlers.Add(2206, new SkillDefinations.Archer.DistanceArrow());
            skillHandlers.Add(26020, new SkillDefinations.FR2_2.SlowArrow());
            #endregion
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;
using SagaMap.Network.Client;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap
{
    public class AtCommand:Singleton<AtCommand>
    {
        private delegate void ProcessCommandFunc( MapClient client, string args );

        private struct CommandInfo
        {
            public ProcessCommandFunc func;
            public uint level;

			public CommandInfo(ProcessCommandFunc func, uint lvl)
			{
				this.func = func;
				this.level = lvl;
			}
        }

        private Dictionary<string, CommandInfo> commandTable;
        private static string _MasterName = "Saga";

        public AtCommand()
        {
            this.commandTable = new Dictionary<string, CommandInfo>();

            #region "Prefixes"
            string OpenCommandPrefix = "/";
            string GMCommandPrefix = "!";
            string RemoteCommandPrefix = "~";
            #endregion

            #region "Public Commands"
            // public commands
            this.commandTable.Add(OpenCommandPrefix + "where", new CommandInfo( new ProcessCommandFunc( this.ProcessWhere ), 1 ) );
            this.commandTable.Add(OpenCommandPrefix + "who", new CommandInfo(new ProcessCommandFunc(this.ProcessWho2), 1));
            #endregion

            #region "GM Commands"
            // gm commands
            //this.commandTable.Add(GMCommandPrefix + "wlevel", new CommandInfo(new ProcessCommandFunc(this.ProcessWlevel), 2));
            this.commandTable.Add(GMCommandPrefix + "broadcast", new CommandInfo( new ProcessCommandFunc( this.ProcessBroadcast ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "jump", new CommandInfo( new ProcessCommandFunc( this.ProcessJump ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "npc", new CommandInfo( new ProcessCommandFunc( this.ProcessNPC ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "item", new CommandInfo( new ProcessCommandFunc( this.ProcessItem ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "info", new CommandInfo( new ProcessCommandFunc( this.ProcessInfo ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "who", new CommandInfo( new ProcessCommandFunc( this.ProcessWho ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "pjump", new CommandInfo( new ProcessCommandFunc( this.ProcessPJump ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "pcall", new CommandInfo( new ProcessCommandFunc( this.ProcessPCall ), 20 ) );
            this.commandTable.Add(GMCommandPrefix + "level", new CommandInfo( new ProcessCommandFunc( this.ProcessLevel ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "cash", new CommandInfo( new ProcessCommandFunc( this.ProcessCash ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "commandlist", new CommandInfo( new ProcessCommandFunc( this.ProcessCommandList ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "speed", new CommandInfo(new ProcessCommandFunc(this.ProcessSpeed), 60));
            this.commandTable.Add(GMCommandPrefix + "reloadscript", new CommandInfo(new ProcessCommandFunc(this.ProcessReloadScript), 60));
            this.commandTable.Add(GMCommandPrefix + "reloadconfig", new CommandInfo(new ProcessCommandFunc(this.ProcessReloadConfig), 60));
            this.commandTable.Add(GMCommandPrefix + "kick", new CommandInfo(new ProcessCommandFunc(this.ProcessKick), 20));
            this.commandTable.Add(GMCommandPrefix + "kickall", new CommandInfo(new ProcessCommandFunc(this.ProcessKickAll), 99));
            this.commandTable.Add(GMCommandPrefix + "raw", new CommandInfo(new ProcessCommandFunc(this.ProcessRaw), 100));
            
            #endregion

            #region "Remote Commands"
            // remote commands
            //this.commandTable.Add( RemoteCommandPrefix + "jump", new CommandInfo( new ProcessCommandFunc( this.ProcessRJump ), 60 ) );
            //this.commandTable.Add( RemoteCommandPrefix + "cash", new CommandInfo( new ProcessCommandFunc( this.ProcessRCash ), 60 ) );
            //this.commandTable.Add( RemoteCommandPrefix + "info", new CommandInfo( new ProcessCommandFunc( this.ProcessRInfo ), 60 ) );
            //this.commandTable.Add( RemoteCommandPrefix + "res", new CommandInfo( new ProcessCommandFunc(this.ProcessRRes), 60));
            //this.commandTable.Add(RemoteCommandPrefix + "die", new CommandInfo(new ProcessCommandFunc(this.ProcessRDie), 60));
            //this.commandTable.Add(RemoteCommandPrefix + "heal", new CommandInfo(new ProcessCommandFunc(this.ProcessRHeal), 60));
            #endregion


            #region "Aliases"
            // Aliases
            //this.commandTable.Add(GMCommandPrefix + "kill", new CommandInfo(new ProcessCommandFunc(this.ProcessDie), 60));
            //this.commandTable.Add(RemoteCommandPrefix + "kill", new CommandInfo(new ProcessCommandFunc(this.ProcessRDie), 60));
            //this.commandTable.Add(GMCommandPrefix + "b", new CommandInfo(new ProcessCommandFunc(this.ProcessBroadcast), 60));
            //this.commandTable.Add(GMCommandPrefix + "gm", new CommandInfo(new ProcessCommandFunc(this.ProcessGMChat), 60));
            #endregion
        }

        public bool ProcessCommand( MapClient client, string command )
        {
            try
            {
                string[] args = command.Split( " ".ToCharArray(), 2 );
                args[0] = args[0].ToLower();

                if( this.commandTable.ContainsKey( args[0] ) )
                {
                    CommandInfo cInfo = this.commandTable[args[0]];

                    if (client.Character.Account.GMLevel >= cInfo.level)
                    {

                        if (args.Length == 2)
                            cInfo.func(client, args[1]);
                        else cInfo.func(client, "");
                    }
                    else
                        client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_NO_ACCESS);

                    return true;
                }
            }
            catch( Exception e ) { Logger.ShowError( e, null ); }

            return false;
        }

        #region "Command Processing"


        #region "Public Commands"
        private void ProcessWho2(MapClient client, string args)
        {
        
        }

        private void ProcessWhere(MapClient client, string args)
        {
        
        }

        private void ProcessGetHeight(MapClient client, string args)
        {
        }
        #endregion

        public void ProcessCommandList(MapClient client, string args)
        {
            int CommandCounter = 0;
            foreach (KeyValuePair<string, CommandInfo> kvp in this.commandTable)
            {
                if (kvp.Value.level <= client.Character.Account.GMLevel)
                {
                    CommandCounter++;
                    //client.SendMessage(_MasterName, "Command " + CommandCounter + ": " + kvp.Key);
                }
            }
        }

        private void ProcessSpeed(MapClient client, string args)
        {
           
        }

        private void ProcessReloadConfig(MapClient client, string args)
        {
            
        }

        private void ProcessReloadScript(MapClient client, string args)
        {
           
        }

        private void ProcessMap( MapClient client, string args )
        {
            
        }

        private void ProcessInfo( MapClient client, string args )
        {
           
        }

       
        private void ProcessJump( MapClient client, string args )
        {
            
        }

        
        

        private void ProcessNPC( MapClient client, string args )
        {
            
        }

        private void ProcessItem( MapClient client, string args )
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            else
            {
                try
                {
                    uint id = uint.Parse(args);
                    Item item = ItemFactory.Instance.GetItem(id);
                    if (item != null)
                    {
                        InventoryAddResult result = client.Character.Inventory.AddItem(ContainerType.BODY, item);
                        client.SendItemAdd(item, ContainerType.BODY, result, 1);
                    }
                    else
                    {
                        client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_NO_SUCH_ITEM);
                    }
                }
                catch (Exception) { }
            }
            
        }

        private void ProcessWho( MapClient client, string args )
        {
            
        }      

        private void ProcessPJump( MapClient client, string args )
        {
            
        }

        private void ProcessPCall( MapClient client, string args )
        {
            
        }

        
        private void ProcessBroadcast( MapClient client, string args )
        {
            
        }

        private void ProcessCash( MapClient client, string args )
        {
            
        }

        private void ProcessLevel( MapClient client, string args )
        {
            
        }
       
        private void ProcessHeal(MapClient client, string args)
        {
        
        }

        
        #region "Admin commands"

        private void ProcessKickAll(MapClient client, string playername)
        {
            
        }


        private void ProcessKick(MapClient client, string playername)
        {
           
        }

        private void ProcessSpawn(MapClient client, string args)
        {
           
        }

        private void ProcessCallMap(MapClient client, string args)
        {
              
        }

        //Be careful with this command
        private void ProcessCallAll(MapClient client, string args)
        {
            
        }


        #endregion

        #region "Dev commands"
        
        private void ProcessRaw(MapClient client, string args)
        {
            
        }

        

        #endregion

        #endregion

        
    }
}
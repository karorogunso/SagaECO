using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using SagaLib;
using static SagaAPI.Process;
namespace SagaAPI
{
    public class WebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;
        string stoken = Configuration.Instance.APIKey;
        bool success = false;

        public WebServer(string[] prefixes, Func<HttpListenerRequest, string> method)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Cannot Start Server, APIServer requires at lease Windows XP SP2 or Windows Server 2003.");
            
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("Error: No prefixes set.");
            
            if (method == null)
                throw new ArgumentException("Error: No method is set");

            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            _responderMethod = method;
            _listener.Start();
        }

        public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
            : this(prefixes, method) { }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            if (ctx.Request.HttpMethod == "POST")
                            {
                                //Allow POST to connect
                                Logger.ShowInfo("Client connected:"+ctx.Request.UserHostAddress);
    
                                string token = ctx.Request.Headers.Get("token");
              
                                if(token == Configuration.Instance.APIKey)
                                {
                                    if (ctx.Request.Headers.Get("char_id") == null)
                                    {
                                        Logger.ShowWarning("No char_id received");
                                        ctx.Response.OutputStream.Close();
                                        
                                    }
                                    uint charid = uint.Parse(ctx.Request.Headers.Get("char_id"));
                                    uint itemid = uint.Parse(ctx.Request.Headers.Get("item_id"));
                                    ushort qty = ushort.Parse(ctx.Request.Headers.Get("qty"));

                                    switch (ctx.Request.Headers.Get("action"))
                                    {
                                        case "vshop_buy":
                                            Process p = new Process("additem");
                                            p.Action(charid, itemid, qty);
                                            success = p.Load();
                                            break;
                                        default:
                                            Logger.ShowWarning("Action is empty or not exists.");

                                            break;
                                    }
                                    




                                }else
                                {
                                    Logger.ShowWarning("Token access deined.");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Dropped.");
                                    ctx.Response.OutputStream.Close();
                                }
                                

                            }
                            else
                            {
                                //Not allow to GET
                                Logger.ShowWarning("Method disallowed from:" + ctx.Request.UserHostAddress);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Dropped.");
                                ctx.Response.OutputStream.Close();
                            }


                            try
                            {
                                string rstr;
                                if (success == true)
                                {
                                    rstr = "{\"success\":1,\"created_time\":\"" + DateTime.Now + "\"}";
                                }
                                else
                                {
                                     rstr = "{\"success\":0,\"created_time\":\"" + DateTime.Now + "\"}";
                                }
                            
                                
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch { }
                            finally
                            {
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch { }
            });
        }
        
    }
}

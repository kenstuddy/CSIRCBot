/*
 * Created by SharpDevelop.
 * User: Ken
 * Date: 10/10/2010
 * Time: 3:51 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System;
using System.Reflection;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
namespace ircbot
{
    public class Program
    {
        public static string owner = "ken";
        static string server = "irc.swiftirc.net";
        static string nick = "ON-csbotIRC";
        static string address = "wat"; //not quite sure what this does yet
        static string channel = "#test";
        static int port = 6667;
           static string currLine = null;
        static TcpClient irc = new TcpClient(server, port);
        static NetworkStream stream = irc.GetStream(); // defines the sockets
        static StreamReader Reader = new StreamReader(stream);
        static StreamWriter Writer = new StreamWriter(stream) {AutoFlush = true};
        static Bot csBot = new Bot(Writer, channel);
        public Program()
        {
            csBot.login(nick, address);
            //Writer.AutoFlush = true;
            //Console.WriteLine(currLine);
            //The csBot object needs to be passed as an argument because we HAVE to use the same streamwriter for the Bot class,
            //and also it can't be static or it won't work for some reason except now it can be... weird.
            Thread thrd = new Thread(run);
            thrd.Start();
            //old code before threading:
            //par.parseText(currLine, csBot);
            /* Code to test multithreading to prove that this really has multiple threads:
            while (true)
                Console.WriteLine("GG");
            */
        }
        public static void Main(string[] args)
        {
            new Program();
        }
        static void  run() {
            Parser par = new Parser();
            while((currLine = Reader.ReadLine()) != null)
            {
                par.parseText(currLine,csBot);
              }
        }
    }
}

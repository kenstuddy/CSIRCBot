/*
 * Created by SharpDevelop.
 * User: Ken
 * Date: 10/10/2010
 * Time: 3:59 PM
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
    /// <summary>
    /// Description of Bot.
    /// </summary>
    public class Bot
    {
        StreamWriter sw;
        public string channel;
        public Bot(StreamWriter writer, string chan )
        {
            sw = writer;
            channel = chan;
            sw.AutoFlush = true;
        }
        public void sendMessage(string message)
        {
            sw.Write(message);
            Console.Write(message);
            Logger.log(message);
        }
        public void login(string nick, string address)
        {
            sendMessage("NICK " + nick + "\n");
            sendMessage("USER " + nick + " " + address + ": cs Bot\n");
            sendMessage("JOIN " + channel + "\n");
            say("#test","hi everyone");
        }
        public void say(string channel, string message)
        {
            sendMessage("PRIVMSG " + channel + " :" + message + "\n");
        }
        public void act(string channel, string message)
        {
            sendMessage("PRIVMSG " + channel + " \u0001ACTION " + message + "\u0001");
        }
        public void notice(string channel, string message)
        {
            sendMessage("NOTICE " + channel + " :" + message + "\n");
        }
        public void pong(string currLine)
        {
            sendMessage("PONG :{0}", currLine.Substring(currLine.IndexOf(" :") + 2) + "\n");

        }
        public void mode(string user, string mode)
        {
            sendMessage("MODE " + channel + " " + mode + " " + user + "\n");
        }
        public void join(string chann)
        {
            part();
            sendMessage("JOIN " + chann + "\n");
            channel = chann;
        }
        public void part()
        {
            sendMessage("PART " + channel + "\n");
        }
        public void exit()
        {
            say(channel,"I'm out. Peace!");
            Environment.Exit(0);
        }
    }
}

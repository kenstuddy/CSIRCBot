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
	    public void login(String nick, String address)
    	{
	        sw.Write("NICK " + nick + "\n");
	        sw.Write("USER " + nick + " " + address + ": cs Bot\n");
	        sw.Write("JOIN " + channel + "\n");
	        sw.Write("PRIVMSG NICKSERV : IDENTIFY big360" + "\n");
	        Console.WriteLine("NICK " + nick);
	        Console.WriteLine("USER " + nick + " " + address + ": cs Bot");
	        Console.WriteLine("PRIVMSG NICKSERV :id big360");
	        Console.WriteLine("JOIN " + channel);
	        Logger.log("NICK " + nick);
	        Logger.log("USER " + nick + " " + address + ": cs Bot");
	        Logger.log("PRIVMSG NICKSERV :IDENTIFY big360");
	        Logger.log("JOIN " + channel);
	        say("#test","hi every1");
    	}
	    public void say(string channel, string message) 
	    {
	        sw.Write("PRIVMSG " + channel + " :" + message + "\n");
	        Console.Write("PRIVMSG " + channel + " :" + message + "\n");
	        Logger.log("PRIVMSG " + channel + " :" + message + "\n");
		}
		public void act(string channel, string message) 
		{
            sw.WriteLine("PRIVMSG " + channel + " \u0001ACTION " + message + "\u0001");
		    Console.WriteLine("PRIVMSG " + channel + " \u0001ACTION " + message + "\u0001");
		    Logger.log("PRIVMSG " + channel + " \u0001ACTION " + message + "\u0001");
		}
		public void notice(string channel, string message) 
		{
		    sw.Write("NOTICE " + channel + " :" + message + "\n");
		    Console.Write("NOTICE " + channel + " :" + message + "\n");
		    Logger.log("NOTICE " + channel + " :" + message + "\n");
		}
		public void pong(string currLine) 
		{
		    sw.Write("PONG :{0}", currLine.Substring(currLine.IndexOf(" :") + 2) + "\n");
		    Console.Write("PONG :{0}", currLine.Substring(currLine.IndexOf(" :") + 2) + "\n");
		    Logger.log("PONG :{0}", currLine.Substring(currLine.IndexOf(" :") + 2) + "\n");
		}
        public void mode(string user, string mode)
        {
            sw.Write("MODE " + channel + " " + mode + " " + user + "\n");
            Console.Write("MODE " + channel + " " + mode + " " + user + "\n");
            Logger.log("MODE " + channel + " " + mode + " " + user);
        }
		public void join(string chann)
		{
		    part();
		    sw.Write("JOIN " + chann + "\n");
		    Logger.log("JOIN " + chann + "\n");
		    channel = chann;
		}
		public void part()
		{
		    sw.Write("PART " + channel + "\n");
		    Logger.log("PART " + channel + "\n");
		}
		public void exit()
		{
		    say(channel,"I'm out. Peace!");
		    Logger.log(channel,"I'm out. Peace!");
		    Environment.Exit(0);
		}
	}
}

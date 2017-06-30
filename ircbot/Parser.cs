/*
 * Created by SharpDevelop.
 * User: Ken
 * Date: 10/10/2010
 * Time: 4:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;

namespace ircbot
{
	/// <summary>
	/// Description of Parser.
	/// </summary>
	public class Parser
	{
		string nickn;
		string usern;
		string hostn;
        double firstNumber = 0;
        double secondNumber = 0;
        string exit = "";
        string operation = "";
        double Total = 0;
        bool badInput = false;
	    private string[] dinput;
		public Parser()
		{
		}
        public void calc(string currLine, Bot csBot, double firstNum, string myOperation, double secondNum)
		{
            if (dinput.Length > 6)
            {
                firstNumber = firstNum;
                secondNumber = secondNum;
                operation = myOperation;
                switch (operation)
                {
                    case "+":
                        Total = firstNumber + secondNumber;
                        break;
                    case "-":
                        Total = firstNumber - secondNumber;
                        break;
                    case "*":
                        Total = firstNumber * secondNumber;
                        break;
                    case "/":
                        Total = firstNumber / secondNumber;
                        break;
                    default:
                        badInput = true;
                        break;
                }
            }
            if (badInput == false)
            {
                csBot.say(csBot.channel, "The resulting number is " + Total);
            }
            else
            {
                //csBot.say(csBot.channel, "The resulting number is " + Total);
                csBot.say(csBot.channel, "You entered a bad operation sign, you have to start over again.");
                csBot.say(csBot.channel, "The syntax of the command is: .calc fistnumber operation secondnumber");
            }
		}
		public string parseText(string currLine, Bot csBot)
		{
			try {
				Console.WriteLine(currLine);
				Logger.log(currLine);
				dinput = currLine.Split(' ');
                if (dinput[0].StartsWith(":") && dinput[0].IndexOf("!") > 0 && dinput[0].IndexOf("@") > 0)
	            {
	                usern = dinput[0].Substring(1); //users username
	                nickn = usern.Substring(0, usern.IndexOf("!")); //users nick
	                hostn = usern.Substring(usern.IndexOf("@") + 1); //users host
	            }
                string p4 = "";
                if (dinput.Length > 4)
                {
                    p4 = dinput[4];
                }
				if (currLine.StartsWith("PING"))
				{
					csBot.pong(currLine);
				}
                //Check the bot has a command sent from a channel and not from a user for every nonping command, check each command for owner seperately if needed.
                if (currLine.Contains("PRIVMSG " + csBot.channel + " :"))
                {

                    if (currLine.Contains(".sup"))
                    {
                        csBot.say(csBot.channel, "sup " + nickn);
                    }
                        //old way of authing: pass the name of the nick through to bot, then bot checks if it's the owner
                        //else if (currLine.Contains("PRIVMSG " + csBot.channel + " :.exit"))
                        //{
                        //	csBot.exit(nickn);
                        //}
                    else if (currLine.Contains(".exit"))
                    {
                        if (nickn.Equals(Program.owner))
                        {
                            csBot.exit();
                        }
                        else
                        {
                            csBot.notice(nickn, "You aren't allowed to do that.");
                        }
                    }
                    else if (currLine.Contains(".op") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "+o");
                        }
                        else
                        {
                            csBot.mode(p4, "+o");
                        }
                    }
                    else if (currLine.Contains(".deop") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "-o");
                        }
                        else
                        {
                            csBot.mode(p4, "-o");
                        }
                    }
                    else if (currLine.Contains(".halfop") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "+h");
                        }
                        else
                        {
                            csBot.mode(p4, "+h");
                        }
                    }
                    else if (currLine.Contains(".dehalfop") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "-h");
                        }
                        else
                        {
                            csBot.mode(p4, "-h");
                        }
                    }
                    else if (currLine.Contains(".voice") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "+v");
                        }
                        else
                        {
                            csBot.mode(p4, "+v");
                        }
                    }
                    else if (currLine.Contains(".devoice") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "-v");
                        }
                        else
                        {
                            csBot.mode(p4, "-v");
                        }
                    }
                    else if (currLine.Contains(".protect") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "+a");
                        }
                        else
                        {
                            csBot.mode(p4, "+a");
                        }
                    }
                    else if (currLine.Contains(".deprotect") && nickn.Equals(Program.owner))
                    {
                        if (p4.Equals(null) || p4.Length < 1)
                        {
                            csBot.mode(nickn, "-a");
                        }
                        else
                        {
                            csBot.mode(p4, "-a");
                        }
                    }
                    else if (currLine.Contains(".act"))
                    {
                        string message = "";
                        for (int x = 4; x < dinput.Length; x++)
                        {
                            message += dinput[x] + " ";
                        }
                        csBot.act(csBot.channel, message);
                    }
                    else if ((currLine.Contains(".say")) && (!currLine.Contains(".sayhi")) && nickn.Equals(Program.owner))
                    {
                        if (dinput.Length < 5)
                        {
                            csBot.say(csBot.channel, "You must type something for me to say!");
                        }
                        else
                        {
                            string message = "";
                            for (int x = 4; x < dinput.Length; x++)
                            {
                                message += dinput[x] + " ";
                            }
                            csBot.say(csBot.channel, message);
                        }
                    }
                    else if (currLine.Contains(".calc"))
                    {
                        try
                        {
                            if (dinput.Length > 6)
                            {
                                calc(currLine, csBot, double.Parse(dinput[4]), dinput[5], double.Parse(dinput[6]));
                            }
                        }
                        catch (Exception e)
                        {
                            csBot.say(csBot.channel, e.ToString());
                            csBot.say(csBot.channel, "The syntax of the command is: .calc firstnumber operation secondnumber");
                            //throw;
                        }
                        
                        //csBot.say(csBot.channel, ""); 
                    }
                    else if (currLine.Contains(".sayhi"))
                    {
                        csBot.say(csBot.channel, "Hello everyone, I am a bot!");
                    }
                    else if (currLine.Contains(".join") && nickn.Equals(Program.owner))
                    {
                        string chan = dinput[4];
                        csBot.part();
                        if (chan.StartsWith("#"))
                        {
                            csBot.join(chan);
                        }
                        else
                        {
                            csBot.join("#" + chan);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Someone tried hacking me I think.");
                    //this shouldn't happen.
                }
			} catch (Exception e) {
				Console.WriteLine("Exception: " + e);
			}
			return null;
		}
	}
}

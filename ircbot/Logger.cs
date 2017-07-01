/*
 * Created by SharpDevelop.
 * User: Ken
 * Date: 10/10/2010
 * Time: 7:49 PM
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
    /// Description of Log.
    /// </summary>
    public class Logger
    {
        public Logger()
        {
        }
        public static void log(string @message)
        {
            try {
                if (@message == null) throw new ArgumentNullException("string");
                string currentTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                var objFile = new System.IO.StreamWriter(
                System.AppDomain.CurrentDomain.BaseDirectory +
                @"\botlogs.txt", true);
                objFile.WriteLine("(" + currentTime + ") " + @message);
                objFile.Close();
                objFile.Dispose();
            } catch (Exception e){
                Console.WriteLine("Exception: " + e);
            }
        }
        public static void log(string @message1, string @message2)
        {
            string @message = @message1 + @message2;
            try {
                if (@message == null) throw new ArgumentNullException("string");
                string currentTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                var objFile = new System.IO.StreamWriter(
                System.AppDomain.CurrentDomain.BaseDirectory +
                @"\botlogs.txt", true);
                objFile.WriteLine("(" + currentTime + ") " + @message);
                objFile.Close();
                objFile.Dispose();
            } catch (Exception e){
                Console.WriteLine("Exception: " + e);
            }
        }
    }
}

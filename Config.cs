using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Test.FileProcess;

namespace Test
{
    public class Ghost
    {
        public string Port_Name { get; set; }
        public string Baud_Rate { get; set; }
        public string Stop_Bit { get; set; }
        public string Data_Bit { get; set; }
        public string Parity { get; set; }
        public string Port_Switch { get; set; }
        public string Msg_Send { get; set; }
        public string[] Baud { get; set; }
        public bool Check_Time { get; set; }
        public bool Receive_Format { get; set; }
        public bool Send_Entered { get; set; }
        public double jogwidth { get; set; }
        public string fixedSpeedOne { get; set; }
        public string fixedSpeedTwo { get; set; }
        public string fixedSpeedthree { get; set; }
    }

    public class Config : FileBase
    {
        public static string Port_Name = "COM1";
        public static string Baud_Rate = "9600";
        public static string Stop_Bit = "One";
        public static string Data_Bit = "8";
        public static string Parity = "None";
        public static string Port_Switch = "打开串口";
        public static string Msg_Send = "123";
        public static string[] Baud = { "1200", "2400", "4800", "9600", "14400", "19200", "38400" };
        public static bool Check_Time = true;
        public static bool Receive_Format = false;
        public static bool Send_Entered = false;
        public static double jogwidth;
        public static string fixedSpeedOne = "2";
        public static string fixedSpeedTwo = "8";
        public static string fixedSpeedthree = "20";

        public void ReadFile()
        {
            if (File.Exists("Ghost.json"))
            {
                try
                {
                    var Ghostconfig = JsonConvert.DeserializeObject<Ghost>(File.ReadAllText("Ghost.json"));
                    Port_Name = Ghostconfig.Port_Name;
                    Baud_Rate = Ghostconfig.Baud_Rate;
                    Stop_Bit = Ghostconfig.Stop_Bit;
                    Data_Bit = Ghostconfig.Data_Bit;
                    Parity = Ghostconfig.Parity;
                    Port_Switch = Ghostconfig.Port_Switch;
                    Msg_Send = Ghostconfig.Msg_Send;
                    Baud = Ghostconfig.Baud;
                    Check_Time = Ghostconfig.Check_Time;
                    Receive_Format = Ghostconfig.Receive_Format;
                    Send_Entered = Ghostconfig.Send_Entered;
                    jogwidth = Ghostconfig.jogwidth;
                    fixedSpeedOne = Ghostconfig.fixedSpeedOne;
                    fixedSpeedTwo  = Ghostconfig.fixedSpeedTwo;
                    fixedSpeedthree = Ghostconfig.fixedSpeedthree;
                }
                catch
                {

                    WriteFile();
                }
            }
            else
                WriteFile();
        }

        public void WriteFile()
        {
            File.WriteAllText("Ghost.Json", JsonConvert.SerializeObject(new Ghost()
            {
                Port_Name = Port_Name,
                Baud_Rate = Baud_Rate,
                Stop_Bit = Stop_Bit,
                Data_Bit = Data_Bit,
                Parity = Parity,
                Port_Switch = Port_Switch,
                Msg_Send = Msg_Send,
                Baud = Baud,
                Check_Time = Check_Time,
                Receive_Format = Receive_Format,
                Send_Entered = Send_Entered,
                jogwidth = jogwidth,
                fixedSpeedOne= fixedSpeedOne,
                fixedSpeedTwo= fixedSpeedTwo,
                fixedSpeedthree= fixedSpeedthree,
            }), Encoding.UTF8);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Test.Nlog
{
    /// <summary>
    /// 写日志类
    /// </summary>
    public class WriteLog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string filebasepath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志内容</param>
        public void WriteComLog(string log)
        {
            //记录警告信息
            logger.Warn(log);
        }
        public void WriteComLog(string log, string logname)
        {
            Logger loggerByName = LogManager.GetLogger(logname);
            //记录一般信息
            loggerByName.Info(log);
        }
        /// <summary>
        /// 将信息写入文件
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="filename">文件名</param>
        /// <param name="filefix">文件后缀名</param>
        public void WriteFile(string info, string filename, string filefix)
        {
            try
            {
                //文件夹路径
                string _filebasepath = GetTodyRecordPath();
                string filePath = string.Empty;

                if (string.IsNullOrEmpty(filefix))
                {
                    //日志文件路径
                    filePath = _filebasepath + "\\" + filename + ".log";
                }
                else
                {
                    filePath = _filebasepath + "\\" + filename + filefix;
                }
                //如果file文件夹不存在创建file文件夹
                if (!System.IO.Directory.Exists(_filebasepath))
                {
                    Directory.CreateDirectory(_filebasepath);
                }
                //如果文件不存在创建该文件
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                StreamWriter sw = File.AppendText(filePath);
                sw.Write(info);
                sw.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将信息写入文件
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="filename">文件名</param>
        /// <param name="filefix">文件后缀名</param>
        /// <param name="childpath"></param>
        public void WriteFile(string info, string filename, string filefix, string childpath)
        {
            try
            {
                string _filebasepath = GetTodyRecordPath() + childpath;
                string filePath = string.Empty;

                if (string.IsNullOrEmpty(filefix))
                {
                    filePath = _filebasepath + "\\" + filename + ".log";
                }
                else
                {
                    filePath = _filebasepath + "\\" + filename + filefix;
                }

                if (!System.IO.Directory.Exists(_filebasepath))
                {
                    Directory.CreateDirectory(_filebasepath);
                }
                //如果文件不存在创建该文件
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                //将日志文件追加到日志文件夹下
                StreamWriter sw = File.AppendText(filePath);
                //将日志的内容写入日志文件中
                sw.Write(info);
                sw.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取当前日期文件夹
        /// </summary>
        /// <returns>createPath文件夹路径</returns>
        private string GetTodyRecordPath()
        {
            string createPath = string.Empty;
            //如果不存在就创建file文件夹
            if (Directory.Exists(filebasepath + DateTime.Now.Year.ToString()) == false)
            {
                //创建文件夹
                Directory.CreateDirectory(filebasepath + DateTime.Now.Year.ToString());
                //获取文件夹路径
                createPath = DateTime.Now.Year.ToString() + @"\";
            }

            if (Directory.Exists(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()) == false)
            {
                Directory.CreateDirectory(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString());
                createPath = DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\";
            }

            if (Directory.Exists(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyyMMdd")) == false)
            {
                Directory.CreateDirectory(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("dd"));
                createPath = DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.ToString("dd") + "/";
            }
            createPath = filebasepath + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.ToString("dd") + @"\";
            return createPath;
        }
    }

}

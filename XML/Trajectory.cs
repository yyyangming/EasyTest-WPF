using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using Test.FileProcess;
using System.Windows.Media.Animation;

namespace Test
{
    /// <summary>
    /// 定义全局静态值
    /// </summary>
    public static class trajectoryPath
    {
        public static string trajectParXmlPath;
    }

    /// <summary>
    /// 两轴轨迹类
    /// </summary>
    public class BaseTrejectory2D
    {
        FileBase fileBase = new FileBase();
        private double pointX;
        private double pointY;
        /// <summary>
        /// X轴坐标
        /// </summary>
        [CategoryAttribute("状态"),
              DefaultValueAttribute(0.00)]
        public double PointX { get => pointX; set => pointX = value; }
        /// <summary>
        /// Y轴坐标
        /// </summary>
        [CategoryAttribute("状态"),
             DefaultValueAttribute(0.00)]
        public double PointY { get => pointY; set => pointY = value; }
    }

    /// <summary>
    /// 3轴轨迹类
    /// </summary>
    public class Trajectory3D : BaseTrejectory2D
    {
        private double pointZ;
        /// <summary>
        /// Z轴坐标
        /// </summary>
        [CategoryAttribute("状态"),
             DefaultValueAttribute(0.00)]
        public double PointZ { get => pointZ; set => pointZ = value; }


    }
    /// <summary>
    /// 四轴轨迹类
    /// </summary>
    public class Trajectory4D : Trajectory3D
    {

        private double pointW;

        /// <summary>
        /// 第四轴偏转轴
        /// </summary>
        [CategoryAttribute("状态"),
             DefaultValueAttribute(0.00)]
        public double PointW { get => pointW; set => pointW = value; }

    }
    /// <summary>
    /// 第五轴偏转轴
    /// </summary>
    public class Trajectory5D : Trajectory4D
    {
        private bool pointU;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("位置"),
             DefaultValueAttribute(false)]
        public bool PointU { get => pointU; set => pointU = value; }
    }
    /// <summary>
    /// 点的轨迹类
    /// </summary>
    public class TrajectoryPoint
    {
        [CategoryAttribute("开始点"),
            DefaultValueAttribute("0")]
        public string stratPoint;

        public Trajectory5D StratPoint = new Trajectory5D();
        public Point PointStrat;

        private string _sort;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("序号"),
             DefaultValueAttribute("??")]
        public string Sort { get => _sort; set => _sort = value; }

        private double _inAOpenTime;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("状态"),
             DefaultValueAttribute(3.00)]
        public double InAOpenTime { get => _inAOpenTime; set => _inAOpenTime = value; }

        private double _inACloseTime;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("状态"),
             DefaultValueAttribute(4.00)]
        public double InACloseTime { get => _inACloseTime; set => _inACloseTime = value; }

        private double _delayOpenTime;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("状态"),
             DefaultValueAttribute(0.00)]
        public double DelayOpenTime { get => _delayOpenTime; set => _delayOpenTime = value; }

        private double _delayCloseTime;
        /// <summary>
        /// 关胶延迟
        /// </summary>
        [CategoryAttribute("状态"),
             DefaultValueAttribute(0.00)]
        public double DelayClodeTime { get => _delayCloseTime; set => _delayCloseTime = value; }


        private bool lift;
        /// <summary>
        /// 胶阀升降
        /// </summary>
        [CategoryAttribute("状态"),
            DefaultValueAttribute(false)]
        public bool Lift { set => lift = value; get => lift; }

        private bool _open;
        /// <summary>
        /// 胶阀开关
        /// </summary>
        [CategoryAttribute("状态"),
            DefaultValueAttribute(false)]
        public bool Open { set => _open = value; get => _open; }

        public string Type = "Point";
    }

    /// <summary>
    /// 线的轨迹类
    /// </summary>
    public class TrajectoryLine : TrajectoryPoint
    {
        public Trajectory5D EndPoint = new Trajectory5D();
        public Point PointEnd;

        [CategoryAttribute("结束点"),
            DefaultValueAttribute("0")]
        public string endPoint;

        public string Type = "Line";
    }

    /// <summary>
    /// 圆的轨迹类
    /// </summary>
    public class TrajectoryRound : TrajectoryLine
    {
        public Trajectory5D MidPoint = new Trajectory5D();
        public string Type = "Round";
        public Point PointMid;
        public double RoundR;
        public Point point1;
    }


    /// <summary>
    /// 处理轨迹方法类
    /// </summary>
    public class Trajectory
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration xmldecl;
        string XMLPath;

        /// <summary>
        /// 在使用下面方法前，先使用此方法Load需要的XML文件
        /// </summary>
        /// <param name="xMLPath"></param>
        public void trajectoryLoaded(string xMLPath)
        {
            XMLPath = xMLPath;
            doc.Load(XMLPath);
        }

        /// <summary>
        /// 在修改文件后必须调用此方法对xml文件进行保存
        /// </summary>
        /// <returns></returns>
        public bool trajectorySave()
        {
            try
            {
                doc.Save(XMLPath);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public ObservableCollection<object> collection = new ObservableCollection<object>();

        /// <summary>
        /// 创建xml文件
        /// </summary>
        public bool newTarjectory(string name)
        {
            xmldecl = doc.CreateXmlDeclaration("1.0", "utf-8", null);  // 加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlElement xle = doc.CreateElement("Trajectory");    //定义根结点Trajectory
            doc.AppendChild(xle);
            doc.Save(XMLPath);
            XmlNode xmlnode;
            return true;
        }

        /// <summary>
        /// 打开一个XML文件并返回一个TrajectoryLine类型的集合
        /// </summary>
        /// <param name="XMLPath1"></param>
        /// <returns></returns>
        public ObservableCollection<object> OpenTarjectory2()
        {

            //找到根节点
            XmlNode xn = doc.SelectSingleNode("Tarjectory");
            XmlNodeList xnl = xn.ChildNodes;
            ObservableCollection<object> TrajectoryParList = new ObservableCollection<object>();

            //获取所有子节点
            foreach (XmlNode xn1 in xnl)
            {
                XmlElement xe = (XmlElement)xn1;  //将节点转化为元素
                XmlNodeList xnl0 = xe.ChildNodes; //xe.GetAttribute获得的类型为string，转化为int
                string TypeCategory = xe.Attributes["Type"].Value;
                if (TypeCategory == "线" || TypeCategory == "Line")
                {
                    TrajectoryLine trajectoryline = new TrajectoryLine();
                    trajectoryline.Sort = xe.Attributes["Sort"].Value;
                    trajectoryline.StratPoint.PointX = double.Parse(xnl0.Item(0).InnerText);
                    trajectoryline.StratPoint.PointY = double.Parse(xnl0.Item(1).InnerText);
                    trajectoryline.StratPoint.PointZ = double.Parse(xnl0.Item(2).InnerText);
                    trajectoryline.StratPoint.PointU = bool.Parse(xnl0.Item(3).InnerText);
                    trajectoryline.StratPoint.PointW = double.Parse(xnl0.Item(4).InnerText);
                    trajectoryline.Open = bool.Parse(xnl0.Item(5).InnerText);
                    trajectoryline.EndPoint.PointX = double.Parse(xnl0.Item(6).InnerText);
                    trajectoryline.EndPoint.PointY = double.Parse(xnl0.Item(7).InnerText);
                    trajectoryline.EndPoint.PointZ = double.Parse(xnl0.Item(8).InnerText);
                    trajectoryline.EndPoint.PointU = bool.Parse(xnl0.Item(9).InnerText);
                    trajectoryline.EndPoint.PointW = double.Parse(xnl0.Item(10).InnerText);
                    trajectoryline.Lift = bool.Parse(xnl0.Item(11).InnerText);
                    trajectoryline.stratPoint = trajectoryline.StratPoint.PointX.ToString() + " " + trajectoryline.StratPoint.PointY.ToString();
                    trajectoryline.endPoint = trajectoryline.EndPoint.PointX.ToString() + " " + trajectoryline.EndPoint.PointY.ToString();
                    TrajectoryParList.Add(trajectoryline);
                }
                else if (TypeCategory == "点")
                {
                    TrajectoryPoint trajectoryPoint = new TrajectoryPoint();
                    trajectoryPoint.Sort = xe.Attributes["Sort"].Value;
                    trajectoryPoint.StratPoint.PointX = double.Parse(xnl0.Item(0).InnerText);
                    trajectoryPoint.StratPoint.PointY = double.Parse(xnl0.Item(1).InnerText);
                    trajectoryPoint.StratPoint.PointZ = double.Parse(xnl0.Item(2).InnerText);
                    trajectoryPoint.StratPoint.PointU = bool.Parse(xnl0.Item(3).InnerText);
                    trajectoryPoint.StratPoint.PointW = double.Parse(xnl0.Item(4).InnerText);
                    trajectoryPoint.Open = bool.Parse(xnl0.Item(5).InnerText);
                    trajectoryPoint.Lift = bool.Parse(xnl0.Item(6).InnerText);
                    trajectoryPoint.stratPoint = trajectoryPoint.StratPoint.PointX.ToString() + " " + trajectoryPoint.StratPoint.PointY.ToString();
                    TrajectoryParList.Add(trajectoryPoint);
                }
                else if (TypeCategory == "Round")
                {
                    TrajectoryRound trajectoryRound = new TrajectoryRound();
                    trajectoryRound.Sort = xe.Attributes["Sort"].Value;
                    trajectoryRound.StratPoint.PointX = double.Parse(xnl0.Item(0).InnerText);
                    trajectoryRound.StratPoint.PointY = double.Parse(xnl0.Item(1).InnerText);
                    trajectoryRound.StratPoint.PointZ = double.Parse(xnl0.Item(2).InnerText);
                    trajectoryRound.StratPoint.PointU = bool.Parse(xnl0.Item(3).InnerText);
                    trajectoryRound.StratPoint.PointW = double.Parse(xnl0.Item(4).InnerText);
                    trajectoryRound.Open = bool.Parse(xnl0.Item(5).InnerText);
                    trajectoryRound.EndPoint.PointX = double.Parse(xnl0.Item(6).InnerText);
                    trajectoryRound.EndPoint.PointY = double.Parse(xnl0.Item(7).InnerText);
                    trajectoryRound.EndPoint.PointZ = double.Parse(xnl0.Item(8).InnerText);
                    trajectoryRound.EndPoint.PointU = bool.Parse(xnl0.Item(9).InnerText);
                    trajectoryRound.EndPoint.PointW = double.Parse(xnl0.Item(10).InnerText);
                    trajectoryRound.Lift = bool.Parse(xnl0.Item(11).InnerText);
                    trajectoryRound.MidPoint.PointX = double.Parse(xnl0.Item(12).InnerText);
                    trajectoryRound.MidPoint.PointY = double.Parse(xnl0.Item(13).InnerText);
                    trajectoryRound.MidPoint.PointZ = double.Parse(xnl0.Item(14).InnerText);
                    trajectoryRound.MidPoint.PointU = bool.Parse(xnl0.Item(15).InnerText);
                    trajectoryRound.MidPoint.PointW = double.Parse(xnl0.Item(16).InnerText);
                    trajectoryRound.PointStrat.X = trajectoryRound.StratPoint.PointX;
                    trajectoryRound.PointStrat.Y = trajectoryRound.StratPoint.PointY;
                    trajectoryRound.PointMid.X = trajectoryRound.MidPoint.PointX;
                    trajectoryRound.PointMid.Y = trajectoryRound.MidPoint.PointY;
                    trajectoryRound.PointEnd.X = trajectoryRound.EndPoint.PointX;
                    trajectoryRound.PointEnd.Y = trajectoryRound.EndPoint.PointY;
                    //在此确认圆心，半径
                    TrajectoryParList.Add(trajectoryRound);
                }
            }
            return TrajectoryParList;
        }

        /// <summary>
        /// 添加XML文件
        /// </summary>
        /// <param name="trajectoryLine"></param>
        /// <param name="xmlpath"></param>
        public bool AddTrajectory(TrajectoryLine trajectoryLine)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode("Tarjectory"); ///  载入根节点
                XmlNodeList xnl = xn.ChildNodes;                             ///  载入其所有子节点的集合
                int max = 0;
                foreach (XmlNode item in xnl)
                {
                    XmlElement xe = (XmlElement)item;  //将节点转化为元素
                    if (int.Parse(xe.Attributes["Sort"].Value) > max)
                        max = int.Parse(xe.Attributes["Sort"].Value);
                }
                max += 1;

                XmlElement xml = doc.CreateElement("tarjectory");
                xml.SetAttribute("Sort", max.ToString());
                xml.SetAttribute("Type", trajectoryLine.Type);
                xn.AppendChild(xml);

                XmlElement xesub1 = doc.CreateElement("StartPointX");
                xesub1.InnerText = trajectoryLine.StratPoint.PointX.ToString();//设置文本节点
                xml.AppendChild(xesub1);// 添加到<Node>节点中

                XmlElement xesub2 = doc.CreateElement("StartPointY");
                xesub2.InnerText = trajectoryLine.StratPoint.PointY.ToString();//设置文本节点
                xml.AppendChild(xesub2);// 添加到<Node>节点中

                XmlElement xesub3 = doc.CreateElement("StartPointZ");
                xesub3.InnerText = trajectoryLine.StratPoint.PointZ.ToString();//设置文本节点
                xml.AppendChild(xesub3);// 添加到<Node>节点中

                XmlElement xesub4 = doc.CreateElement("StartPointU");
                xesub4.InnerText = trajectoryLine.StratPoint.PointU.ToString();//设置文本节点
                xml.AppendChild(xesub4);// 添加到<Node>节点中

                XmlElement xesub5 = doc.CreateElement("StartPointW");
                xesub5.InnerText = trajectoryLine.StratPoint.PointW.ToString();//设置文本节点
                xml.AppendChild(xesub5);// 添加到<Node>节点中

                XmlElement xesub6 = doc.CreateElement("Open");
                xesub6.InnerText = trajectoryLine.Open.ToString();//设置文本节点
                xml.AppendChild(xesub6);// 添加到<Node>节点中

                XmlElement xesub7 = doc.CreateElement("EndPointX");
                xesub7.InnerText = trajectoryLine.EndPoint.PointX.ToString();//设置文本节点
                xml.AppendChild(xesub7);// 添加到<Node>节点中

                XmlElement xesub8 = doc.CreateElement("EndPointY");
                xesub8.InnerText = trajectoryLine.EndPoint.PointY.ToString();//设置文本节点
                xml.AppendChild(xesub8);// 添加到<Node>节点中

                XmlElement xesub9 = doc.CreateElement("EndPointZ");
                xesub9.InnerText = trajectoryLine.EndPoint.PointZ.ToString();//设置文本节点
                xml.AppendChild(xesub9);// 添加到<Node>节点中

                XmlElement xesub10 = doc.CreateElement("EndPointU");
                xesub10.InnerText = trajectoryLine.EndPoint.PointU.ToString();//设置文本节点
                xml.AppendChild(xesub10);// 添加到<Node>节点中

                XmlElement xesub11 = doc.CreateElement("EndPointW");
                xesub11.InnerText = trajectoryLine.EndPoint.PointW.ToString();//设置文本节点
                xml.AppendChild(xesub11);// 添加到<Node>节点中


                XmlElement xesub12 = doc.CreateElement("Lift");
                xesub12.InnerText = trajectoryLine.Lift.ToString();//设置文本节点
                xml.AppendChild(xesub12);//添加到<Node>节点中
                //doc.Save(XMLPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 圆的添加入XMl文件方法
        /// </summary>
        /// <param name="trajectoryLine">导入的圆的名称</param>
        /// <param name="xmlpath">需要写入的XML位置</param>
        /// <returns></returns>
        public bool AddTrajectory(TrajectoryRound trajectoryLine)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode("Tarjectory"); ///  载入根节点
                XmlNodeList xnl = xn.ChildNodes;                             ///  载入其所有子节点的集合
                int max = 0;
                foreach (XmlNode item in xnl)
                {
                    XmlElement xe = (XmlElement)item;  //将节点转化为元素
                    if (int.Parse(xe.Attributes["Sort"].Value) > max)
                        max = int.Parse(xe.Attributes["Sort"].Value);
                }
                max += 1;

                XmlElement xml = doc.CreateElement("tarjectory");
                xml.SetAttribute("Sort", max.ToString());
                xml.SetAttribute("Type", trajectoryLine.Type);
                xn.AppendChild(xml);

                XmlElement xesub1 = doc.CreateElement("StartPointX");
                xesub1.InnerText = trajectoryLine.StratPoint.PointX.ToString();//设置文本节点
                xml.AppendChild(xesub1);// 添加到<Node>节点中

                XmlElement xesub2 = doc.CreateElement("StartPointY");
                xesub2.InnerText = trajectoryLine.StratPoint.PointY.ToString();//设置文本节点
                xml.AppendChild(xesub2);// 添加到<Node>节点中

                XmlElement xesub3 = doc.CreateElement("StartPointZ");
                xesub3.InnerText = trajectoryLine.StratPoint.PointZ.ToString();//设置文本节点
                xml.AppendChild(xesub3);// 添加到<Node>节点中

                XmlElement xesub4 = doc.CreateElement("StartPointU");
                xesub4.InnerText = trajectoryLine.StratPoint.PointU.ToString();//设置文本节点
                xml.AppendChild(xesub4);// 添加到<Node>节点中

                XmlElement xesub5 = doc.CreateElement("StartPointW");
                xesub5.InnerText = trajectoryLine.StratPoint.PointW.ToString();//设置文本节点
                xml.AppendChild(xesub5);// 添加到<Node>节点中

                XmlElement xesub6 = doc.CreateElement("Open");
                xesub6.InnerText = trajectoryLine.Open.ToString();//设置文本节点
                xml.AppendChild(xesub6);// 添加到<Node>节点中

                XmlElement xesub7 = doc.CreateElement("EndPointX");
                xesub7.InnerText = trajectoryLine.EndPoint.PointX.ToString();//设置文本节点
                xml.AppendChild(xesub7);// 添加到<Node>节点中

                XmlElement xesub8 = doc.CreateElement("EndPointY");
                xesub8.InnerText = trajectoryLine.EndPoint.PointY.ToString();//设置文本节点
                xml.AppendChild(xesub8);// 添加到<Node>节点中

                XmlElement xesub9 = doc.CreateElement("EndPointZ");
                xesub9.InnerText = trajectoryLine.EndPoint.PointZ.ToString();//设置文本节点
                xml.AppendChild(xesub9);// 添加到<Node>节点中

                XmlElement xesub10 = doc.CreateElement("EndPointU");
                xesub10.InnerText = trajectoryLine.EndPoint.PointU.ToString();//设置文本节点
                xml.AppendChild(xesub10);// 添加到<Node>节点中

                XmlElement xesub11 = doc.CreateElement("EndPointW");
                xesub11.InnerText = trajectoryLine.EndPoint.PointW.ToString();//设置文本节点
                xml.AppendChild(xesub11);// 添加到<Node>节点中

                XmlElement xesub12 = doc.CreateElement("Lift");
                xesub12.InnerText = trajectoryLine.Lift.ToString();//设置文本节点
                xml.AppendChild(xesub12);//添加到<Node>节点中

                XmlElement xesub13 = doc.CreateElement("Lift");
                xesub13.InnerText = trajectoryLine.MidPoint.PointX.ToString();//设置文本节点
                xml.AppendChild(xesub13);//添加到<Node>节点中

                XmlElement xesub14 = doc.CreateElement("Lift");
                xesub14.InnerText = trajectoryLine.MidPoint.PointY.ToString();//设置文本节点
                xml.AppendChild(xesub14);//添加到<Node>节点中

                XmlElement xesub15 = doc.CreateElement("Lift");
                xesub15.InnerText = trajectoryLine.MidPoint.PointZ.ToString();//设置文本节点
                xml.AppendChild(xesub15);//添加到<Node>节点中

                XmlElement xesub16 = doc.CreateElement("EndPointU");
                xesub16.InnerText = trajectoryLine.MidPoint.PointU.ToString();//设置文本节点
                xml.AppendChild(xesub16);// 添加到<Node>节点中

                XmlElement xesub17 = doc.CreateElement("EndPointW");
                xesub17.InnerText = trajectoryLine.MidPoint.PointW.ToString();//设置文本节点
                xml.AppendChild(xesub17);// 添加到<Node>节点中

                //doc.Save(XMLPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <param name="sort"></param>
        /// <param name="BatchTrajectoryPar"></param>
        /// <returns></returns>
        public TrajectoryLine ReviseTrajectory(int sort, TrajectoryLine BatchTrajectoryPar)
        {
            XmlNode Tarjectory = doc.SelectSingleNode("Tarjectory");//在xml文件中找到Teajectorypar节点，并将其命名为Tarjectory节点
            XmlNodeList tarjectory = Tarjectory.ChildNodes;//将Teajectorypar节点的的集合命名为trajectory集合
            foreach (XmlNode xn1 in tarjectory)//遍历
            {
                XmlElement xle = (XmlElement)xn1;
                if (xle.GetAttribute("Sort") == sort.ToString())
                {
                    XmlNodeList nls = xle.ChildNodes;
                    foreach (XmlNode nls1 in nls)
                    {
                        XmlElement xe2 = (XmlElement)nls1;
                        if (xe2.Name == "StartPointX") { xe2.InnerText = BatchTrajectoryPar.StratPoint.PointX.ToString(); break; }
                        if (xe2.Name == "StartPointY") { xe2.InnerText = BatchTrajectoryPar.StratPoint.PointY.ToString(); break; }
                        if (xe2.Name == "StartPointZ") { xe2.InnerText = BatchTrajectoryPar.StratPoint.PointZ.ToString(); break; }
                        if (xe2.Name == "StartPointU") xe2.InnerText = BatchTrajectoryPar.StratPoint.PointU.ToString();
                        if (xe2.Name == "StartPointW") xe2.InnerText = BatchTrajectoryPar.StratPoint.PointW.ToString();
                        if (xe2.Name == "Type") xe2.InnerText = BatchTrajectoryPar.Type.ToString();
                        if (xe2.Name == "Open") xe2.InnerText = BatchTrajectoryPar.Open.ToString();
                        if (xe2.Name == "EndPointX") xe2.InnerText = BatchTrajectoryPar.EndPoint.PointX.ToString();
                        if (xe2.Name == "EndPointY") xe2.InnerText = BatchTrajectoryPar.EndPoint.PointY.ToString();
                        if (xe2.Name == "EndPointZ") xe2.InnerText = BatchTrajectoryPar.EndPoint.PointZ.ToString();
                        if (xe2.Name == "EndPointU") xe2.InnerText = BatchTrajectoryPar.EndPoint.PointU.ToString();
                        if (xe2.Name == "EndPointW") xe2.InnerText = BatchTrajectoryPar.EndPoint.PointW.ToString();
                        if (xe2.Name == "Lift") xe2.InnerText = BatchTrajectoryPar.Lift.ToString();
                    }
                }
            }
            doc.Save(XMLPath);
            return BatchTrajectoryPar;
        }


        /// <summary>
        /// 暂定，暂未修改，需要重新修改
        /// </summary>
        /// <param name="XMLPath1"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public void OpenTarjectory2(int Order)
        {
            //找到根节点
            XmlNode xn = doc.SelectSingleNode("Tarjectory");
            //节点集合
            XmlNodeList xnl = xn.ChildNodes;
            //获取所有子节点
            foreach (XmlNode xn1 in xnl)
            {
                int inOrder = 0;
                //将节点转化为元素
                XmlElement xe = (XmlElement)xn1;
                //xe.GetAttribute获得的类型为string，转化为int

                XmlNodeList xnl0 = xe.ChildNodes;
                if (xe.Attributes["Sort"].Value == "线")
                {
                    TrajectoryLine trajectoryLine = new TrajectoryLine();
                    trajectoryLine.Sort = xe.Attributes["Sort"].Value;
                    trajectoryLine.StratPoint.PointX = double.Parse(xnl0.Item(0).InnerText);
                    trajectoryLine.StratPoint.PointY = double.Parse(xnl0.Item(1).InnerText);
                    trajectoryLine.StratPoint.PointZ = double.Parse(xnl0.Item(2).InnerText);
                    trajectoryLine.StratPoint.PointU = bool.Parse(xnl0.Item(3).InnerText);
                    trajectoryLine.StratPoint.PointW = double.Parse(xnl0.Item(4).InnerText);
                    trajectoryLine.Open = bool.Parse(xnl0.Item(5).InnerText);
                    trajectoryLine.EndPoint.PointX = double.Parse(xnl0.Item(6).InnerText);
                    trajectoryLine.EndPoint.PointY = double.Parse(xnl0.Item(7).InnerText);
                    trajectoryLine.EndPoint.PointZ = double.Parse(xnl0.Item(8).InnerText);
                    trajectoryLine.EndPoint.PointU = bool.Parse(xnl0.Item(9).InnerText);
                    trajectoryLine.EndPoint.PointW = double.Parse(xnl0.Item(10).InnerText);
                    trajectoryLine.Lift = bool.Parse(xnl0.Item(11).InnerText);
                    trajectoryLine.stratPoint = trajectoryLine.StratPoint.PointX.ToString() + " " + trajectoryLine.StratPoint.PointY.ToString();
                    trajectoryLine.endPoint = trajectoryLine.EndPoint.PointX.ToString() + " " + trajectoryLine.EndPoint.PointY.ToString();
                    collection.Add(trajectoryLine);
                }
                else if (xe.Attributes["Sort"].Value == "点")
                {
                    TrajectoryPoint trajectoryPoint = new TrajectoryPoint();
                    trajectoryPoint.Sort = xe.Attributes["Sort"].Value;
                    trajectoryPoint.StratPoint.PointX = double.Parse(xnl0.Item(0).InnerText);
                    trajectoryPoint.StratPoint.PointY = double.Parse(xnl0.Item(1).InnerText);
                    trajectoryPoint.StratPoint.PointZ = double.Parse(xnl0.Item(2).InnerText);
                    trajectoryPoint.StratPoint.PointU = bool.Parse(xnl0.Item(3).InnerText);
                    trajectoryPoint.StratPoint.PointW = double.Parse(xnl0.Item(4).InnerText);
                    trajectoryPoint.Open = bool.Parse(xnl0.Item(5).InnerText);
                    trajectoryPoint.Lift = bool.Parse(xnl0.Item(6).InnerText);
                    trajectoryPoint.stratPoint = trajectoryPoint.StratPoint.PointX.ToString() + " " + trajectoryPoint.StratPoint.PointY.ToString();
                    collection.Add(trajectoryPoint);
                }
                if (inOrder == Order)
                {
                    break;
                }
                else
                    inOrder += 1;
            }
        }

        /// <summary>
        /// 将软件坐标转化为canvas坐标或者将canvas坐标转化为软件坐标
        /// </summary>
        /// <param name="X">传入软件写入坐标或者canvas坐标</param>
        /// <returns></returns>
        public double X_PointG_TO_PointC(double X)
        {
            double truePointX = 500 - X;
            return truePointX;
        }
        /// <summary>
        /// 将软件X坐标转化为canvas的X坐标或者将canvas的X坐标转化为软件X坐标
        /// </summary>
        /// <param name="X">传入软件写入坐标或者canvas坐标</param>
        /// <returns></returns>
        public double Y_PointG_TO_PointC(double X)
        {
            double truePointX = 300 - X;
            return truePointX;
        }

        public Line trajectoryLine(Trajectory3D trajectory3D1, Trajectory3D trajectory3D2)
        {
            Trajectory3D StartPoint = trajectory3D1;
            Trajectory3D EndPoint = trajectory3D2;
            double StartTruePointX = X_PointG_TO_PointC(StartPoint.PointX);
            double StartTruePointY = Y_PointG_TO_PointC(StartPoint.PointY);
            double EndTruePointX = X_PointG_TO_PointC(EndPoint.PointX);
            double EndTruePointY = Y_PointG_TO_PointC(EndPoint.PointY);
            Line LinePath = new Line();
            LinePath.Stroke = Brushes.Black;
            LinePath.StrokeThickness = 1;
            LinePath.X1 = StartTruePointX;
            LinePath.Y1 = StartTruePointY;
            LinePath.X2 = EndTruePointX;
            LinePath.Y2 = EndTruePointY;
            return LinePath;
        }

        /// <summary>
        /// 用户控件是的动画
        /// </summary>
        /// <param name="element">控件名</param>
        /// <param name="point">元素开始动画的位置</param>
        /// <param name="from">元素开始的大小</param>
        /// <param name="to">元素到达的大小</param>
        public void ScaleEasingAnimationShow(FrameworkElement element, Point point, double from, double to)
        {
            lock (element)
            {
                ScaleTransform scale = new ScaleTransform();
                element.RenderTransform = scale;
                element.RenderTransformOrigin = point;//定义圆心位置        
                EasingFunctionBase easeFunction = new PowerEase()
                {
                    EasingMode = EasingMode.EaseOut,
                    Power = 5
                };
                DoubleAnimation scaleAnimation = new DoubleAnimation()
                {
                    From = from,                                   //起始值
                    To = from + to,                                     //结束值

                    EasingFunction = easeFunction,                    //缓动函数
                    Duration = new TimeSpan(0, 0, 0, 1, 0)  //动画播放时间
                };
                AnimationClock clock = scaleAnimation.CreateClock();
                scale.ApplyAnimationClock(ScaleTransform.ScaleXProperty, clock);
                scale.ApplyAnimationClock(ScaleTransform.ScaleYProperty, clock);
            }

        }
        public void ScaleEasingAnimationShow2(FrameworkElement element, Point point, double from, double to)
        {
            lock (element)
            {
                ScaleTransform scale = new ScaleTransform();
                element.RenderTransform = scale;
                element.RenderTransformOrigin = point;//定义圆心位置        
                EasingFunctionBase easeFunction = new PowerEase()
                {
                    EasingMode = EasingMode.EaseOut,
                    Power = 5
                };
                DoubleAnimation scaleAnimation = new DoubleAnimation()
                {
                    From = from,                                   //起始值
                    To = from - to,                                     //结束值
                    EasingFunction = easeFunction,                    //缓动函数
                    Duration = new TimeSpan(0, 0, 0, 1, 0)  //动画播放时间
                };
                AnimationClock clock = scaleAnimation.CreateClock();
                scale.ApplyAnimationClock(ScaleTransform.ScaleXProperty, clock);
                scale.ApplyAnimationClock(ScaleTransform.ScaleYProperty, clock);
            }
        }

        /// <summary>
        /// 透明度动画
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="to"></param>
        public void FloatElement(UIElement elem, double to)
        {
            lock (elem)
            {
                if (to == 1)
                {
                    elem.Visibility = Visibility.Visible;
                }
                DoubleAnimation opacity = new DoubleAnimation()
                {
                    To = to - 0.01,
                    Duration = new TimeSpan(0, 0, 0, 1, 0)
                };
                EventHandler handler = null;
                opacity.Completed += handler = (s, e) =>
                {
                    opacity.Completed -= handler;
                    if (to == 0)
                    {
                        elem.Visibility = Visibility.Collapsed;
                    }
                    opacity = null;
                };
                elem.BeginAnimation(UIElement.OpacityProperty, opacity);
            }
        }

        /// <summary>
        /// 透明度动画
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="to"></param>
        public void FloatElement2(UIElement elem, double to)
        {
            lock (elem)
            {
                if (to == 1)
                {
                    elem.Visibility = Visibility.Visible;
                }
                DoubleAnimation opacity = new DoubleAnimation()
                {
                    To = to + 0.01,
                    Duration = new TimeSpan(0, 0, 0, 1, 0)
                };
                EventHandler handler = null;
                opacity.Completed += handler = (s, e) =>
                {
                    opacity.Completed -= handler;
                    if (to == 0)
                    {
                        elem.Visibility = Visibility.Collapsed;
                    }
                    opacity = null;
                };
                elem.BeginAnimation(UIElement.OpacityProperty, opacity);
            }
        }


    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using Test.FileProcess;

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
        [CategoryAttribute("B开始点"),
            DefaultValueAttribute("0")]
        public string stratPoint;

        public Trajectory5D StratPoint = new Trajectory5D();
        public Point PointStrat;

        private string _sort;
        /// <summary>
        /// 开胶延迟
        /// </summary>  
        [CategoryAttribute("A序号"),
             DefaultValueAttribute("??"),]
        public string Sort { get => _sort; set => _sort = value; }

        private double _inAOpenTime;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("E状态"),
             DefaultValueAttribute(3.00)]
        public double InAOpenTime { get => _inAOpenTime; set => _inAOpenTime = value; }

        private double _inACloseTime;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("E状态"),
             DefaultValueAttribute(4.00)]
        public double InACloseTime { get => _inACloseTime; set => _inACloseTime = value; }

        private double _delayOpenTime;
        /// <summary>
        /// 开胶延迟
        /// </summary>
        [CategoryAttribute("E状态"),
             DefaultValueAttribute(0.00)]
        public double DelayOpenTime { get => _delayOpenTime; set => _delayOpenTime = value; }

        private double _delayCloseTime;
        /// <summary>
        /// 关胶延迟
        /// </summary>
        [CategoryAttribute("E状态"),
             DefaultValueAttribute(0.00)]
        public double DelayClodeTime { get => _delayCloseTime; set => _delayCloseTime = value; }


        private bool lift;
        /// <summary>
        /// 胶阀升降
        /// </summary>
        [CategoryAttribute("E状态"),
            DefaultValueAttribute(false)]
        public bool Lift { set => lift = value; get => lift; }

        private bool _open;
        /// <summary>
        /// 胶阀开关
        /// </summary>
        [CategoryAttribute("E状态"),
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

        [CategoryAttribute("B开始点"),
            DefaultValueAttribute("0")]
        public double StratX { set => StratPoint.PointX = value; get => StratPoint.PointX; }
        [CategoryAttribute("B开始点"),
            DefaultValueAttribute("0")]
        public double StratY { set => StratPoint.PointY = value; get => StratPoint.PointY; }
        [CategoryAttribute("B开始点"),
            DefaultValueAttribute("0")]
        public double StratZ { set => StratPoint.PointZ = value; get => StratPoint.PointZ; }
        [CategoryAttribute("B开始点"),
            DefaultValueAttribute("0")]
        public double StratW { set => StratPoint.PointW = value; get => StratPoint.PointW; }
        [CategoryAttribute("B开始点"),
            DefaultValueAttribute("0")]
        public bool StratU { set => StratPoint.PointU = value; get => StratPoint.PointU; }

        [CategoryAttribute("C结束点"),
            DefaultValueAttribute("0")]
        public bool EndU { set => EndPoint.PointU = value; get => EndPoint.PointU; }
        [CategoryAttribute("C结束点"),
            DefaultValueAttribute("0")]
        public double EndW { set => EndPoint.PointW = value; get => EndPoint.PointW; }
        [CategoryAttribute("C结束点"),
            DefaultValueAttribute("0")]
        public double EndZ { set => EndPoint.PointZ = value; get => EndPoint.PointZ; }
        [CategoryAttribute("C结束点"),
            DefaultValueAttribute("0")]
        public double EndY { set => EndPoint.PointY = value; get => EndPoint.PointY; }
        [CategoryAttribute("C结束点"),
            DefaultValueAttribute("0")]
        public double EndX { set => EndPoint.PointX = value; get => EndPoint.PointX; }


        [CategoryAttribute("C结束点"),
            DefaultValueAttribute("0")]
        public string endPoint;
        public string Type = "Line";
    }

    /// <summary>
    /// 圆的轨迹类
    /// </summary>
    public class TrajectoryRound : TrajectoryLine
    {
        /// <summary>
        /// 中间点的对象
        /// </summary>
        public Trajectory5D MidPoint = new Trajectory5D();
        public string Type = "Round";
        public bool ForWardRatation;

        [CategoryAttribute("D中间点"),
            DefaultValueAttribute("0")]
        public double MidW { set => MidPoint.PointW = value; get => MidPoint.PointW; }
        [CategoryAttribute("D中间点"),
            DefaultValueAttribute("0")]
        public bool MidU { set => MidPoint.PointU = value; get => MidPoint.PointU; }
        [CategoryAttribute("D中间点"),
            DefaultValueAttribute("0")]
        public double MidZ { set => MidPoint.PointZ = value; get => MidPoint.PointZ; }
        [CategoryAttribute("D中间点"),
            DefaultValueAttribute("0")]
        public double MidY { set => MidPoint.PointY = value; get => MidPoint.PointY; }
        [CategoryAttribute("D中间点"),
            DefaultValueAttribute("0")]
        public double MidX { set => MidPoint.PointX = value; get => MidPoint.PointX; }


        public Point PointMid;
        public double RoundR;
        public Point point1;
    }
    /// <summary>
    /// 弧的轨迹类
    /// </summary>
    public class TrajectoryArc : TrajectoryRound
    {
        public bool Superior;
    }

    /// <summary>
    /// 处理轨迹方法类
    /// </summary>
    public class Trajectory
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration xmldecl;
        string XMLPath;
        ObservableCollection<object> TrajectoryParList = new ObservableCollection<object>();

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
        /// 打开一个XML文件并返回一个Object类型的集合
        /// </summary>
        /// <param name="XMLPath1"></param>
        /// <returns></returns>
        public ObservableCollection<object> OpenTarjectory2()
        {

            //找到根节点
            XmlNode xn = doc.SelectSingleNode("Tarjectory");
            XmlNodeList xnl = xn.ChildNodes;
            

            //获取所有子节点
            foreach (XmlNode xn1 in xnl)
            {
                XmlElement xe = (XmlElement)xn1; //将节点转化为元素
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
                    //借助三点向量成绩，判断他是正向还是反向
                    if ((trajectoryRound.MidPoint.PointX - trajectoryRound.StratPoint.PointX) * (trajectoryRound.EndPoint.PointY - trajectoryRound.MidPoint.PointY) - (trajectoryRound.MidPoint.PointY - trajectoryRound.StratPoint.PointY) * (trajectoryRound.EndPoint.PointX - trajectoryRound.MidPoint.PointX) > 0)
                    {
                        trajectoryRound.ForWardRatation = true;
                    }
                    else
                        trajectoryRound.ForWardRatation = false;
                    //在此确认圆心，半径
                    TrajectoryParList.Add(trajectoryRound);
                }
                else if (TypeCategory == "Arc")
                {
                    TrajectoryArc trajectoryArc = new TrajectoryArc();
                    trajectoryArc.Sort = xe.Attributes["Sort"].Value;
                    trajectoryArc.StratPoint.PointX = double.Parse(xnl0.Item(0).InnerText);
                    trajectoryArc.StratPoint.PointY = double.Parse(xnl0.Item(1).InnerText);
                    trajectoryArc.StratPoint.PointZ = double.Parse(xnl0.Item(2).InnerText);
                    trajectoryArc.StratPoint.PointU = bool.Parse(xnl0.Item(3).InnerText);
                    trajectoryArc.StratPoint.PointW = double.Parse(xnl0.Item(4).InnerText);
                    trajectoryArc.Open = bool.Parse(xnl0.Item(5).InnerText);
                    trajectoryArc.EndPoint.PointX = double.Parse(xnl0.Item(6).InnerText);
                    trajectoryArc.EndPoint.PointY = double.Parse(xnl0.Item(7).InnerText);
                    trajectoryArc.EndPoint.PointZ = double.Parse(xnl0.Item(8).InnerText);
                    trajectoryArc.EndPoint.PointU = bool.Parse(xnl0.Item(9).InnerText);
                    trajectoryArc.EndPoint.PointW = double.Parse(xnl0.Item(10).InnerText);
                    trajectoryArc.Lift = bool.Parse(xnl0.Item(11).InnerText);
                    trajectoryArc.MidPoint.PointX = double.Parse(xnl0.Item(12).InnerText);
                    trajectoryArc.MidPoint.PointY = double.Parse(xnl0.Item(13).InnerText);
                    trajectoryArc.MidPoint.PointZ = double.Parse(xnl0.Item(14).InnerText);
                    trajectoryArc.MidPoint.PointU = bool.Parse(xnl0.Item(15).InnerText);
                    trajectoryArc.MidPoint.PointW = double.Parse(xnl0.Item(16).InnerText);
                    trajectoryArc.PointStrat.X = trajectoryArc.StratPoint.PointX;
                    trajectoryArc.PointStrat.Y = trajectoryArc.StratPoint.PointY;
                    trajectoryArc.PointMid.X = trajectoryArc.MidPoint.PointX;
                    trajectoryArc.PointMid.Y = trajectoryArc.MidPoint.PointY;
                    trajectoryArc.PointEnd.X = trajectoryArc.EndPoint.PointX;
                    trajectoryArc.PointEnd.Y = trajectoryArc.EndPoint.PointY;
                    //借助三点向量成绩，判断他是正向还是反向
                    if ((trajectoryArc.MidPoint.PointX - trajectoryArc.StratPoint.PointX) * (trajectoryArc.EndPoint.PointY - trajectoryArc.MidPoint.PointY) - (trajectoryArc.MidPoint.PointY - trajectoryArc.StratPoint.PointY) * (trajectoryArc.EndPoint.PointX - trajectoryArc.MidPoint.PointX) > 0)
                    {
                        trajectoryArc.ForWardRatation = true;
                    }
                    else
                        trajectoryArc.ForWardRatation = false;
                    //在此确认圆心，半径
                    TrajectoryParList.Add(trajectoryArc);
                }
            }
            return TrajectoryParList;
        }

        public void writeXmlFile(TrajectoryLine trajectoryLine)
        {
            XmlNode xn = doc.SelectSingleNode("Tarjectory"); ///  载入根节点


        }

        /// <summary>
        /// 在指定节点添加序列号为指定数字的线的基类，注意需要使用save方法才能保存到文件中
        /// </summary>
        /// <param name="trajectoryLine">需要保存的线的类</param>
        /// <param name="Sort">在文件中的序号</param>
        /// <param name="xmlNode">指定的节点</param>
        public void AddLine(TrajectoryLine trajectoryLine,int Sort, XmlNode xmlNode) 
        {
            XmlElement xml = doc.CreateElement("tarjectory"); // 创建单独的一个轨迹节点
            xml.SetAttribute("Sort", Sort.ToString());
            xml.SetAttribute("Type", trajectoryLine.Type);

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
            xmlNode.AppendChild(xml);
        }

        /// <summary>
        /// 在指定节点添加序列号为指定数字的圆的基类，注意需要使用save方法才能保存到文件中
        /// </summary>
        /// <param name="trajectoryLine">需要保存的线的类</param>
        /// <param name="Sort">在文件中的序号</param>
        /// <param name="xmlNode">指定的节点</param>
        public void AddRound(TrajectoryRound trajectoryLine, int Sort, XmlNode xmlNode) 
        {
            XmlElement xml = doc.CreateElement("tarjectory"); // 创建单独的一个轨迹节点
            xml.SetAttribute("Sort", Sort.ToString());
            xml.SetAttribute("Type", trajectoryLine.Type);

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

            XmlElement xesub13 = doc.CreateElement("MidPointX");
            xesub13.InnerText = trajectoryLine.MidPoint.PointX.ToString();//设置文本节点
            xml.AppendChild(xesub13);//添加到<Node>节点中

            XmlElement xesub14 = doc.CreateElement("MidPointY");
            xesub14.InnerText = trajectoryLine.MidPoint.PointY.ToString();//设置文本节点
            xml.AppendChild(xesub14);//添加到<Node>节点中

            XmlElement xesub15 = doc.CreateElement("MidPointZ");
            xesub15.InnerText = trajectoryLine.MidPoint.PointZ.ToString();//设置文本节点
            xml.AppendChild(xesub15);//添加到<Node>节点中

            XmlElement xesub16 = doc.CreateElement("MidPointU");
            xesub16.InnerText = trajectoryLine.MidPoint.PointU.ToString();//设置文本节点
            xml.AppendChild(xesub16);// 添加到<Node>节点中

            XmlElement xesub17 = doc.CreateElement("MidPointW");
            xesub17.InnerText = trajectoryLine.MidPoint.PointW.ToString();//设置文本节点
            xml.AppendChild(xesub17);// 添加到<Node>节点中

            xmlNode.AppendChild(xml);
        }

        /// <summary>
        /// 在指定节点添加序列号为指定数字的弧的基类，注意需要使用save方法才能保存到文件中
        /// </summary>
        /// <param name="trajectoryLine">需要保存的弧的类</param>
        /// <param name="Sort">在文件中的序号</param>
        /// <param name="xmlNode">指定的节点</param>
        public void AddArc(TrajectoryArc trajectoryLine, int Sort, XmlNode xmlNode)
        {
            XmlElement xml = doc.CreateElement("tarjectory"); // 创建单独的一个轨迹节点
            xml.SetAttribute("Sort", Sort.ToString());
            xml.SetAttribute("Type", trajectoryLine.Type);

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

            XmlElement xesub13 = doc.CreateElement("MidPointX");
            xesub13.InnerText = trajectoryLine.MidPoint.PointX.ToString();//设置文本节点
            xml.AppendChild(xesub13);//添加到<Node>节点中

            XmlElement xesub14 = doc.CreateElement("MidPointY");
            xesub14.InnerText = trajectoryLine.MidPoint.PointY.ToString();//设置文本节点
            xml.AppendChild(xesub14);//添加到<Node>节点中

            XmlElement xesub15 = doc.CreateElement("MidPointZ");
            xesub15.InnerText = trajectoryLine.MidPoint.PointZ.ToString();//设置文本节点
            xml.AppendChild(xesub15);//添加到<Node>节点中

            XmlElement xesub16 = doc.CreateElement("MidPointU");
            xesub16.InnerText = trajectoryLine.MidPoint.PointU.ToString();//设置文本节点
            xml.AppendChild(xesub16);// 添加到<Node>节点中

            XmlElement xesub17 = doc.CreateElement("MidPointW");
            xesub17.InnerText = trajectoryLine.MidPoint.PointW.ToString();//设置文本节点
            xml.AppendChild(xesub17);// 添加到<Node>节点中

            xmlNode.AppendChild(xml);
        }

        /// <summary>
        /// 添加XML文件:线
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

                AddLine(trajectoryLine,max, xn);
                //doc.Save(XMLPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        /// <summary>
        /// 添加XMl文件:圆
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
                AddRound(trajectoryLine,max,xn);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        /// <summary>
        /// 添加XMl文件:弧
        /// </summary>
        /// <param name="trajectoryLine">导入的圆的名称</param>
        /// <param name="xmlpath">需要写入的XML位置</param>
        /// <returns></returns>
        public bool AddTrajectory(TrajectoryArc trajectoryLine)
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
                AddArc(trajectoryLine,max,xn);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 修改XML文件:线
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <param name="sort">需要修改的XML文件的序号</param>
        /// <param name="BatchTrajecstoryPar">已经修改完成的XML的对应的修改好的部分</param>
        /// <returns></returns>
        public TrajectoryLine ReviseTrajectory(int sort, TrajectoryLine trajectoryLine)
        {
            XmlNode Tarjectory = doc.SelectSingleNode("Tarjectory");//在xml文件中找到Teajectorypar节点，并将其命名为Tarjectory节点
            XmlNodeList tarjectorys = Tarjectory.ChildNodes;//将Teajectorypar节点的的集合命名为trajectory集合
            foreach (XmlNode xn1 in tarjectorys)//遍历
            {
                XmlElement xle = (XmlElement)xn1;
                if (xle.GetAttribute("Sort") == sort.ToString())
                {
                    XmlNodeList nls = xle.ChildNodes;
                    nls.Item(0).InnerText = trajectoryLine.StratPoint.PointX.ToString();
                    nls.Item(1).InnerText = trajectoryLine.StratPoint.PointY.ToString();
                    nls.Item(2).InnerText = trajectoryLine.StratPoint.PointZ.ToString();
                    nls.Item(3).InnerText = trajectoryLine.StratPoint.PointU.ToString();
                    nls.Item(4).InnerText = trajectoryLine.StratPoint.PointW.ToString();
                    nls.Item(5).InnerText = trajectoryLine.Open.ToString();
                    nls.Item(6).InnerText = trajectoryLine.EndPoint.PointX.ToString();
                    nls.Item(7).InnerText = trajectoryLine.EndPoint.PointY.ToString();
                    nls.Item(8).InnerText = trajectoryLine.EndPoint.PointZ.ToString();
                    nls.Item(9).InnerText = trajectoryLine.EndPoint.PointU.ToString();
                    nls.Item(10).InnerText = trajectoryLine.EndPoint.PointW.ToString();
                    nls.Item(11).InnerText = trajectoryLine.Lift.ToString();
                    xle.SetAttribute("Type", trajectoryLine.Type.ToString()); 
                }
                doc.Save(XMLPath);
            }
            return trajectoryLine;
        }

        public void SaveTrojectory2() 
        {

        }


        public void ReviseTrajectory2(ObservableCollection<object> TrajectoryPars)
        {
            XmlNode xn = doc.SelectSingleNode("Tarjectory"); ///  载入根节点
            xn.RemoveAll();
            int Sort = 1;
            TrajectoryPoint trajectoryPointWrite = new TrajectoryPoint();
            TrajectoryLine trajectoryLineWrite = new TrajectoryLine();
            TrajectoryRound trajectoryRoundWrite = new TrajectoryRound();
            TrajectoryArc trajectoryArcWrite = new TrajectoryArc();
            ///遍历传进来的参数中的数据，准备写入
            foreach (var item in TrajectoryPars)
            {
                if (item.GetType().ToString() == "Test.TrajectoryPoint")
                {
                    trajectoryPointWrite = (TrajectoryPoint)item;
                    
                }
                if (item.GetType().ToString() == "Test.TrajectoryLine")
                {
                    trajectoryLineWrite = (TrajectoryLine)item;
                    AddLine(trajectoryLineWrite, Sort++, xn);
                }
                if (item.GetType().ToString() == "Test.TrajectoryRound")
                {
                    trajectoryRoundWrite = (TrajectoryRound)item;
                    AddRound(trajectoryRoundWrite, Sort++, xn);
                }
                if (item.GetType().ToString() == "Test.TrajectoryArc")
                {
                    trajectoryArcWrite = (TrajectoryArc)item;
                    AddArc(trajectoryArcWrite,Sort++,xn);
                }
            }

        }

        ObservableCollection<object> collection = new ObservableCollection<object>();
        /// <summary>
        /// 暂定，暂未修改，需要重新修改
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public object OpenTarjectory2(int Order)
        {
            TrajectoryLine trajectoryLine = new TrajectoryLine();
            TrajectoryPoint trajectoryPoint = new TrajectoryPoint();
            TrajectoryRound trajectoryRound = new TrajectoryRound();

            //找到根节点
            XmlNode xn = doc.SelectSingleNode("Tarjectory");
            //节点集合
            XmlNodeList xnl = xn.ChildNodes;
            //获取所有子节点
            foreach (object xn1 in xnl)
            {
                int inOrder = 0;
                //将节点转化为元素

                //xe.GetAttribute获得的类型为string，转化为int
                string Name = xn1.GetType().ToString();

                if (Name == "Test.TrajectoryLine")
                {
                    trajectoryLine = (TrajectoryLine)xn1;
                    if (int.Parse(trajectoryLine.Sort) == Order)
                    {
                        return trajectoryLine;
                    }
                }
                else if (Name == "Test.TrajectoryRound")
                {
                    trajectoryRound = (TrajectoryRound)xn1;
                    if (int.Parse(trajectoryRound.Sort) == Order)
                    {
                        return trajectoryRound;
                    }
                }
            }
            return "超过最大长度";
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

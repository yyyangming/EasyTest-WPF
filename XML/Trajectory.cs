using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace Test
{
    /// <summary>
    /// 轨迹的实体类
    /// </summary>
    public  class TrajectoryPar
    {
        private  int sort;
        private  double startPointX;
        private  double startPointY;
        private  double startPointZ;
        private  bool startPointU;
        private  double startPointW;
        private  double endPointX;
        private  double endPointY;
        private  double endPointZ;
        private  bool endPointU;
        private  double endPointW;
        private  double speed;
        private  double quantity;
        private  string type;
        private bool lift;
        private  bool open;
        private string startPoint;
        private string endPoint;

        /// <summary>
        /// 轨迹的序号
        /// </summary>
        public  int Sort
        {
            get { return sort; }
            set { sort = value; }
        }
        /// <summary>
        /// 轨迹开始点的X轴坐标
        /// </summary>
        public  double StartPointX { get => startPointX; set => startPointX = value; }
        /// <summary>
        /// 轨迹开始点的Y轴坐标
        /// </summary>
        public  double StartPointY { get => startPointY; set => startPointY = value; }
        /// <summary>
        /// 轨迹开始点的Z轴坐标
        /// </summary>
        public  double StartPointZ { get => startPointZ; set => startPointZ = value; }
        /// <summary>
        /// 轨迹开始点的U轴坐标
        /// </summary>
        public  bool StartPointU { get => startPointU; set => startPointU = value; }
        /// <summary>
        /// 轨迹开始点的W轴坐标
        /// </summary>
        public  double StartPointW { get => startPointW; set => startPointW = value; }
        /// <summary>
        /// 轨迹结束点的X轴坐标
        /// </summary>
        public  double EndPointX { get => endPointX; set => endPointX = value; }
        /// <summary>
        /// 轨迹结束点的Y轴坐标
        /// </summary>
        public  double EndPointY { get => endPointY; set => endPointY = value; }
        /// <summary>
        /// 轨迹结束点的Z轴坐标
        /// </summary>
        public  double EndPointZ { get => endPointZ; set => endPointZ = value; }
        /// <summary>
        /// 轨迹结束点的U轴坐标
        /// </summary>
        public  bool EndPointU { get => endPointU; set => endPointU = value; }
        /// <summary>
        /// 轨迹结束点的W轴坐标
        /// </summary>
        public  double EndPointW { get => endPointW; set => endPointW = value; }
        /// <summary>
        /// 运行该轨道时的速度
        /// </summary>
        public  double Speed { get => speed; set => speed = value; }
        /// <summary>
        /// 运行轨道的胶压
        /// </summary>
        public  double Quantity { get => quantity; set => quantity = value; }
        /// <summary>
        /// 轨迹的类型
        /// </summary>
        public  string Type { get => type; set => type = value; }
        /// <summary>
        /// 胶阀的升降
        /// </summary>
        public bool Lift { get => lift; set => lift = value; }
        /// <summary>
        /// 运行中是否开胶
        /// </summary>
        /// 默认开启
        public  bool Open { get => open; set => open = value; }
        /// <summary>
        /// 开始点的字符串
        /// </summary>
        public string StartPoint { get => startPoint; set => endPoint = value; }
        /// <summary>
        /// 结束点的字符串
        /// </summary>
        public string EndPoint {  get => endPoint;set => endPoint = value; }
    }

    public class Trajectory { 
        /// <summary>
        /// 创建新文件时，使用构造函数自动创建新的XML轨迹文件
        /// </summary>
        public  void Tarjectory() 
        {

        }
        /// <summary>
        /// 创建xml文件
        /// </summary>
        public void newTarjectory()
        {
            XmlDocument xn = new XmlDocument();
            XmlNode xmlnode;
            XmlElement xmlelem;
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
        }

        public ObservableCollection<TrajectoryPar> OpenTarjectory(string XMLPath)
        {
            TrajectoryPar trajectoryPar = new TrajectoryPar();
            //后期输入打开的文件的地址
            XMLPath = "E:\\desket\\GitDevelop\\MySelf\\EasyCoat-WPF\\XML\\Test.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(XMLPath);
            //找到根节点
            XmlNode xn = doc.SelectSingleNode("Tarjectory");
            XmlNodeList xnl = xn.ChildNodes;
            ObservableCollection<TrajectoryPar> TrajectoryParList = new ObservableCollection<TrajectoryPar>();

            //获取所有子节点
            foreach (XmlNode xn1 in xnl)
            {
                //将节点转化为元素
                XmlElement xe = (XmlElement)xn1;
                //xe.GetAttribute获得的类型为string，转化为int
                XmlNodeList xnl0 = xe.ChildNodes;
                trajectoryPar.Sort              =       int.Parse(xnl0.Item(0).InnerText);
                trajectoryPar.StartPointX  = double.Parse(xnl0.Item(1).InnerText);
                trajectoryPar.StartPointY  = double.Parse(xnl0.Item(2).InnerText);
                trajectoryPar.StartPointZ  =  double.Parse(xnl0.Item(3).InnerText);
                trajectoryPar.StartPointU  =     bool.Parse(xnl0.Item(4).InnerText);
                trajectoryPar.StartPointW  = double.Parse(xnl0.Item(5).InnerText);
                trajectoryPar.Type             =                        xnl0.Item(6).InnerText;
                trajectoryPar.Open             =     bool.Parse(xnl0.Item(7).InnerText);
                trajectoryPar.EndPointX     = double.Parse(xnl0.Item(8).InnerText);
                trajectoryPar.EndPointY    =  double.Parse(xnl0.Item(9).InnerText);
                trajectoryPar.EndPointZ    =double.Parse(xnl0.Item(10).InnerText);
                trajectoryPar.EndPointU    =     bool.Parse(xnl0.Item(11).InnerText);
                trajectoryPar.EndPointW    = double.Parse(xnl0.Item(8).InnerText);
                trajectoryPar.Lift                 = bool.Parse(xnl0.Item(13).InnerText);
                trajectoryPar.StartPoint = trajectoryPar.StartPointX.ToString() + " " + trajectoryPar.StartPointY.ToString();
                trajectoryPar.EndPoint = trajectoryPar.EndPointX.ToString() + " " + trajectoryPar.EndPointY.ToString();
                TrajectoryParList.Add(trajectoryPar);
            }
            return TrajectoryParList;
        }
    }
    /// <summary>
    /// 轨迹起点属性类
    /// </summary>
    public class StartPoint 
    {
        public StartPoint(double X,double Y) 
        {
        }
        
        

    }
    /// <summary>
    /// 轨迹终点属性类
    /// </summary>
    public class EndPoint 
    {
        public EndPoint(double X, double Y) 
        {
        }
        
    }
    /// <summary>
    /// 轨迹宽高属性类
    /// </summary>
    public class sizeF_Property
    {
        public sizeF_Property(double Speed,double Quantity) 
        {

        }
        
    }
    /// <summary>
    /// 读取XML文件
    /// </summary>
    public class fileXmlread 
    {
        
    }
}

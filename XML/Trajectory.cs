using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace Test
{
    [DefaultPropertyAttribute("SaveOnClose")]
    /// <summary>
    /// 轨迹的实体类
    /// </summary>
    public  class TrajectoryPar
    {
        private  string sort;
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
        private bool saveOnClose = true;


        [CategoryAttribute("文档设置"),
            DefaultValueAttribute(true)]
        public bool SaveOnClose
        {
            get { return saveOnClose; }
            set { saveOnClose = value; }
        }
        
        /// <summary>
        /// 胶阀的升降
        /// </summary>
        [CategoryAttribute("状态"),
            DefaultValueAttribute(4.00)]
        public bool Lift { get => lift; set => lift = value; }

        /// <summary>
        /// 运行中是否开胶
        /// </summary>
        /// 默认开启
        [CategoryAttribute("状态"),
            DefaultValueAttribute(4.00)]
        public bool Open { get => open; set => open = value; }
        /// <summary>
        /// 轨迹开始点的X轴坐标
        /// </summary>
        [CategoryAttribute("开始点"),
            DefaultValueAttribute(4.00)]
        public  double StartPointX { get => startPointX; set => startPointX = value; }


        /// <summary>
        /// 轨迹开始点的Y轴坐标
        /// </summary>
        [CategoryAttribute("开始点"),
             DefaultValueAttribute(4)]
        public  double StartPointY { get => startPointY; set => startPointY = value; }
        /// <summary>
        /// 轨迹开始点的Z轴坐标
        /// </summary>
        /// [CategoryAttribute("开始点"),
        [CategoryAttribute("开始点"),
             DefaultValueAttribute(4)]
        public  double StartPointZ { get => startPointZ; set => startPointZ = value; }
        /// <summary>
        /// 轨迹开始点的U轴坐标
        /// </summary>
        [CategoryAttribute("开始点"),
             DefaultValueAttribute(true)]
        public  bool StartPointU { get => startPointU; set => startPointU = value; }
        /// <summary>
        /// 轨迹开始点的W轴坐标
        /// </summary>
        [CategoryAttribute("开始点"),
             DefaultValueAttribute(4)]
        public  double StartPointW { get => startPointW; set => startPointW = value; }
        /// <summary>
        /// 轨迹结束点的X轴坐标
        /// </summary>
        [CategoryAttribute("结束点"),
             DefaultValueAttribute(4)]
        public  double EndPointX { get => endPointX; set => endPointX = value; }
        /// <summary>
        /// 轨迹结束点的Y轴坐标
        /// </summary>
        [CategoryAttribute("结束点"),
             DefaultValueAttribute(4)]
        public  double EndPointY { get => endPointY; set => endPointY = value; }
        /// <summary>
        /// 轨迹结束点的Z轴坐标
        /// </summary>
        [CategoryAttribute("结束点"),
             DefaultValueAttribute(4)]
        public  double EndPointZ { get => endPointZ; set => endPointZ = value; }
        /// <summary>
        /// 轨迹结束点的U轴坐标
        /// </summary>
        [CategoryAttribute("结束点"),
             DefaultValueAttribute(true)]
        public  bool EndPointU { get => endPointU; set => endPointU = value; }
        /// <summary>
        /// 轨迹结束点的W轴坐标
        /// </summary>
        [CategoryAttribute("结束点"),
             DefaultValueAttribute(4)]
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
        /// 轨迹的序号
        /// </summary>
        public string Sort
        {
            get { return sort; }
            set { sort = value; }
        }

        /// <summary>
        /// 开始点的字符串
        /// </summary>
        public string StartPoint { get => startPoint; set => startPoint = value; }
        /// <summary>
        /// 结束点的字符串
        /// </summary>
        public string EndPoint {  get => endPoint;set => endPoint = value; }
    }

    public class Trajectory {
        string XMLPath = "E:\\desket\\GitDevelop\\MySelf\\EasyCoat-WPF\\XML\\Test.xml";
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
            
            XmlDocument doc = new XmlDocument();
            doc.Load(XMLPath);
            XmlElement xle = doc.CreateElement("trajectory");
            XmlElement Open = doc.CreateElement("Open");
            Open.InnerText = "true";
            xle.AppendChild(Open);
            doc.Save(XMLPath);


            XmlNode xmlnode;
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
        }
        /// <summary>
        /// 打开一个XML文件并返回一个Trajectorypar类型的集合
        /// </summary>
        /// <param name="XMLPath1"></param>
        /// <returns></returns>
        public ObservableCollection<TrajectoryPar> OpenTarjectory(string XMLPath1)
        {
            XMLPath1 = XMLPath;
            //后期输入打开的文件的地址
            XmlDocument doc = new XmlDocument();
            doc.Load(XMLPath);
            //找到根节点
            XmlNode xn = doc.SelectSingleNode("Tarjectory");
            XmlNodeList xnl = xn.ChildNodes;
            ObservableCollection<TrajectoryPar> TrajectoryParList = new ObservableCollection<TrajectoryPar>();

            foreach (XmlNode xn2 in xnl)
            {

            }
            //获取所有子节点
            foreach (XmlNode xn1 in xnl)
            {
                TrajectoryPar trajectoryPar = new TrajectoryPar();
                
                XmlElement xe = (XmlElement)xn1;  //将节点转化为元素
                
                XmlNodeList xnl0 = xe.ChildNodes; //xe.GetAttribute获得的类型为string，转化为int
                trajectoryPar.Sort              = xe.Attributes["Sort"].Value;
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
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <param name="sort"></param>
        /// <param name="BatchTrajectoryPar"></param>
        /// <returns></returns>
        public TrajectoryPar ReviseTrajectory(string xmlpath, int sort, TrajectoryPar BatchTrajectoryPar)
        {
            TrajectoryPar trajectoryPar = BatchTrajectoryPar;//将带入的trajectorypar类赋值给trajectoryPar
            string XMLPath = xmlpath;//将带入的xmlpath赋值给XMLPath
            XmlDocument doc = new XmlDocument();//创建xmldocument类操作xml类
            doc.Load(XMLPath);//载入xml文件
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
                        if (xe2.Name == "StartPointX") { xe2.InnerText = BatchTrajectoryPar.StartPointX.ToString(); break; }
                        if (xe2.Name == "StartPointY") { xe2.InnerText = BatchTrajectoryPar.StartPointY.ToString(); break; }
                        if (xe2.Name == "StartPointZ") { xe2.InnerText = BatchTrajectoryPar.StartPointZ.ToString(); break; }
                        if (xe2.Name == "StartPointU") xe2.InnerText = BatchTrajectoryPar.StartPointU.ToString();
                        if (xe2.Name == "StartPointW") xe2.InnerText = BatchTrajectoryPar.StartPointW.ToString();
                        if (xe2.Name == "Type") xe2.InnerText = BatchTrajectoryPar.Type.ToString();
                        if (xe2.Name == "Open") xe2.InnerText = BatchTrajectoryPar.Open.ToString();
                        if (xe2.Name == "EndPointX") xe2.InnerText = BatchTrajectoryPar.EndPointX.ToString();
                        if (xe2.Name == "EndPointY") xe2.InnerText = BatchTrajectoryPar.EndPointY.ToString();
                        if (xe2.Name == "EndPointZ") xe2.InnerText = BatchTrajectoryPar.EndPointZ.ToString();
                        if (xe2.Name == "EndPointU") xe2.InnerText = BatchTrajectoryPar.EndPointU.ToString();
                        if (xe2.Name == "EndPointW") xe2.InnerText = BatchTrajectoryPar.EndPointW.ToString();
                        if (xe2.Name == "Lift") xe2.InnerText = BatchTrajectoryPar.Lift.ToString();
                    }
                }
            }
            doc.Save(XMLPath);
            return trajectoryPar;
        }

        /// <summary>
        /// 打开XML文件，返回trajectory类型的对象
        /// </summary>
        /// <param name="XMLPath1"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public TrajectoryPar OpenTarjectory(string XMLPath1,int Order)
        {

            //后期输入打开的文件的地址
            XMLPath1 = XMLPath;
            XmlDocument doc = new XmlDocument();
            doc.Load(XMLPath);
            //找到根节点
            XmlNode xn = doc.SelectSingleNode("Tarjectory");
            //节点集合
            XmlNodeList xnl = xn.ChildNodes;
            ObservableCollection<TrajectoryPar> TrajectoryParList = new ObservableCollection<TrajectoryPar>();
            TrajectoryPar trajectoryPar = new TrajectoryPar();
            //获取所有子节点
            foreach (XmlNode xn1 in xnl)
            {
                int inOrder = 0;
                //将节点转化为元素
                XmlElement xe = (XmlElement)xn1;
                //xe.GetAttribute获得的类型为string，转化为int

                XmlNodeList xnl0 = xe.ChildNodes;
                trajectoryPar.Sort = xe.Attributes["Sort"].Value;
                trajectoryPar.StartPointX = double.Parse(xnl0.Item(1).InnerText);
                trajectoryPar.StartPointY = double.Parse(xnl0.Item(2).InnerText);
                trajectoryPar.StartPointZ = double.Parse(xnl0.Item(3).InnerText);
                trajectoryPar.StartPointU = bool.Parse(xnl0.Item(4).InnerText);
                trajectoryPar.StartPointW = double.Parse(xnl0.Item(5).InnerText);
                trajectoryPar.Type = xnl0.Item(6).InnerText;
                trajectoryPar.Open = bool.Parse(xnl0.Item(7).InnerText);
                trajectoryPar.EndPointX = double.Parse(xnl0.Item(8).InnerText);
                trajectoryPar.EndPointY = double.Parse(xnl0.Item(9).InnerText);
                trajectoryPar.EndPointZ = double.Parse(xnl0.Item(10).InnerText);
                trajectoryPar.EndPointU = bool.Parse(xnl0.Item(11).InnerText);
                trajectoryPar.EndPointW = double.Parse(xnl0.Item(12).InnerText);
                trajectoryPar.Lift = bool.Parse(xnl0.Item(13).InnerText);
                trajectoryPar.StartPoint = trajectoryPar.StartPointX.ToString() + " " + trajectoryPar.StartPointY.ToString();
                trajectoryPar.EndPoint = trajectoryPar.EndPointX.ToString() + " " + trajectoryPar.EndPointY.ToString();

                if (inOrder == Order)
                {
                    break;
                }
                else
                    inOrder += 1;
            }
            return trajectoryPar;
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

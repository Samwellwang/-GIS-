using System;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;

namespace 中华美食查询系统
{
    //固定格式
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string str_url = Application.StartupPath + "\\BaiduAPI.html";
           // Uri url = new Uri(str_url);
            webBrowser1.Url = url;
            //屏蔽webBrowser浏览器右键菜单
            //webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //修改webbrowser的属性使c#可以调用js方法：
            webBrowser1.ObjectForScripting = this;
            timer1.Enabled = true;
            comboBox1.DataSource = new string[] { "黑龙江", "吉林", "辽宁", "内蒙古自治区", "河北", "河南", "山东", "山西", "陕西", "甘肃", "青海", "江苏", "浙江", "湖北", "湖南", "安徽", "江西",
                                                  "福建", "台湾", "广东", "海南", "四川", "云南", "贵州", "北京", "上海", "天津", "重庆", "香港", "澳门", "西藏", "广西", "宁夏", "新疆" };
            comboBox1.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //获得当前鼠标所在的经纬度，然后显示在框体左下角
                string tag_lng = webBrowser1.Document.GetElementById("mouselng").InnerText;
                string tag_lat = webBrowser1.Document.GetElementById("mouselat").InnerText;
                double dou_lng, dou_lat;
                if (double.TryParse(tag_lng, out dou_lng) && double.TryParse(tag_lat, out dou_lat))
                {
                    this.toolStripStatusLabel1.Text = "当前坐标：" + dou_lng.ToString("F5") + "," + dou_lat.ToString("F5");
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message); 
            }
        }
        //数字转字符串 
        public string setWhichCar(int num)
        {
            return num.ToString();
        }
        class jwd
        {
            string Lng { get; set; }
            string Lat { get; set; }
        }
        public void LocateInfo(string msg)
        {
            string get = msg;
        }

        private void distant_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("openGetDistance");
        }


        private void mark_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("PutInMarker");
            MessageBox.Show("点击鼠标右键添加标注！");
        }

        private void clear_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("ClearAllMarkers");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //116.380967,39.913285
            object[] objects = new object[2];
            //当前经度
            objects[0] = Convert.ToDouble(textBox1.Text);
            //当前纬度
            objects[1] = Convert.ToDouble(textBox2.Text);
            //传值给html中的FindPosition函数
            object bb = webBrowser1.Document.InvokeScript("FindPosition", objects);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            //全国各省的地级行政单位
            string[][] str ={new string[]{ "哈尔滨市", "齐齐哈尔市", "牡丹江市", "佳木斯市", "大庆市", "鸡西市", "双鸭山市", "七台河市", "伊春市", "鹤岗市", "黑河市", "绥化市" },
                             new string[]{ "长春市", "吉林市", "延边朝鲜族自治州", "四平市", "通化市", "白城市", "辽源市", "松原市", "白山市"},//9
                             new string[]{ "沈阳市", "大连市", "鞍山市", "抚顺市", "本溪市", "丹东市", "锦州市", "营口市", "阜新市", "辽阳市", "盘锦市", "铁岭市", "朝阳市", "葫芦岛市" },
                             new string[]{ "呼和浩特市", "包头市", "乌海市", "赤峰市", "通辽市", "鄂尔多斯市", "呼伦贝尔市", "巴彦淖尔市", "乌兰察布市", "兴安盟", "锡林郭勒盟", "阿拉善盟" },
                             new string[]{ "石家庄市", "唐山市", "秦皇岛市", "邯郸市", "邢台市", "保定市", "张家口市", "承德市", "沧州市", "廊坊市", "衡水市"},
                             new string[]{ "郑州市", "开封市", "洛阳市", "平顶山市", "安阳市", "鹤壁市", "新乡市", "焦作市", "濮阳市", "许昌市", "漯河市", "三门峡市", "商丘市", "周口市", "驻马店市", "南阳市", "信阳市", "济源市"},
                             new string[]{ "济南市", "青岛市", "淄博市市", "枣庄市", "东营市", "烟台市", "潍坊市", "济宁市", "泰安市", "威海市", "日照市", "滨州市", "德州市", "聊城市", "临沂市", "菏泽市"},//16
                             new string[]{ "太原市", "大同市", "朔州市", "沂州市", "阳泉市", "吕梁市", "晋中市", "长治市", "晋城市", "临汾市", "运城市"},//11
                             new string[]{ "西安市", "宝鸡市", "咸阳市", "铜川市", "渭南市", "延安市", "榆林市", "汉中市", "安康市", "商洛市"},//10
                             new string[]{ "兰州市", "嘉峪关市", "金昌市", "白银市", "天水市", "武威市", "张掖市", "平凉市", "酒泉市", "庆阳市", "定西市", "陇南市", "临夏回族自治州", "甘南藏族自治州"},//14
                             new string[]{ "西宁市", "海东市", "海北藏族自治州", "黄南藏族自治州", "海南藏族自治区州", "果洛藏族自治州", "玉树藏族自治州", "海西蒙古藏族自治州"},//8
                             new string[]{ "南京市", "无锡市", "徐州市", "常州市", "苏州市", "南通市", "连云港市", "淮安市", "盐城市", "扬州市", "镇江市", "泰州市", "宿迁市"},//13
                             new string[]{ "杭州市", "宁波市", "温州市", "绍兴市", "湖州市", "嘉兴市", "金华市", "衢州市", "台州市", "丽水市", "舟山市"},
                             new string[]{ "武汉市", "黄石市", "十堰市", "宜昌市", "襄阳市", "鄂州市", "荆门市", "孝感市", "荆州市", "黄冈市", "咸宁市", "随州市", "恩施市"},
                             new string[]{ "长沙", "株洲市", "湘潭市", "衡阳市", "邵阳市", "岳阳市", "常德市", "张家界市", "益阳市", "娄底市", "郴州市", "永州市", "怀化市", "湘西土家族苗族自治州"},
                             new string[]{ "合肥市", "芜湖市", "蚌埠市", "淮南市", "马鞍山市", "淮北市", "铜陵市", "安庆市", "黄山市", "阜阳市", "宿州市", "滁州市", "六安市", "宣城市", "池州市", "亳州市"},
                             new string[]{ "南昌市", "九江市", "上饶市", "抚州市", "宜春市", "吉安市", "赣州市", "景德镇市", "萍乡市", "新余市", "鹰潭市"},
                             new string[]{ "福州市", "厦门市", "漳州市", "泉州市", "三明市", "莆田市", "南平市", "龙岩市", "宁德市"},
                             new string[]{ "台北", "新北市", "桃园", "台中市", "台南市", "高雄市", "基隆市", "新竹市", "嘉义市"},
                             new string[]{ "广州市", "深圳市", "珠海市", "汕头市", "佛山市", "韶关市", "湛江市", "肇庆市", "江门市", "茂名市","惠州市", "梅州市", "汕尾市", "河源市", "阳江市", "清远市", "东莞市", "中山市", "潮州市", "揭阳市", "云浮市"},//21
                             new string[]{ "海口", "三亚市", "三沙市", "儋州市"},
                             new string[]{ "成都市", "绵阳市", "自贡市", "攀枝花市", "泸州市", "德阳市", "广元市", "遂宁市", "内江市", "乐山市","资阳市", "宜宾市", "南充市", "达州市", "雅安市", "阿坝藏族羌族自治州", "广安市", "巴中市", "甘孜藏族自治州", "凉山彝族自治州", "眉山市"},
                             new string[]{ "昆明市", "曲靖市", "玉溪市", "邵通市", "保山市", "丽江市", "普洱市", "临沧市", "德宏傣族景颇族自治州", "怒江傈僳族自治州", "迪庆藏族自治州", "大理白族自治州", "楚雄彝族自治州", "红河哈尼族彝族自治州", "文山壮族苗族自治州", "西双版纳傣族自治州"},
                             new string[]{ "贵阳市", "遵义市", "六盘水市", "安顺市", "毕节市", "铜仁市", "黔东南苗族侗族自治州", "黔西南布依族苗族自治州", "黔南布衣族苗族自治州"},
                             new string[] {"北京"},
                             new string[] {"上海"},
                             new string[] {"天津"},
                             new string[] {"重庆"},
                             new string[] {"香港"},
                             new string[] {"澳门"},
                             new string[] {"拉萨市","日喀则市","昌都市","林芝市","山南市","那曲市","阿里地区"},
                             new string[]{ "南宁市", "柳州市", "桂林市", "梧州市", "北海市", "防城港市", "钦州市", "贵港市", "玉林市", "百色市", "贺州市", "河池市", "来宾市", "崇左市"},
                             new string[]{ "银川市", "石嘴山市", "吴忠市", "固原市","中卫市"},
                             new string[]{ "乌鲁木齐市", "克拉玛依市", "吐鲁番市", "哈密市", "阿克苏地区", "喀什地区", "和田地区", "昌吉回族自治州", "博尔塔拉蒙古自治州", "巴音郭楞蒙古自治州", "克孜勒苏柯尔克孜自治州", "伊犁哈萨克自治州", "塔城地区", "阿勒泰地区"}
            };
            // switch语句实现combobox1和2的省市连接
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 1:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 2:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 3:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 4:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 5:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 6:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 7:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 8:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 9:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 10:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 11:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 12:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 13:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 14:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 15:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 16:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 17:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 18:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 19:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 20:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 21:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 22:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 23:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 24:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 25:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 26:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 27:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 28:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 29:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 30:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 31:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 32:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
                case 33:
                    comboBox2.DataSource = str[comboBox1.SelectedIndex]; break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            //传递给前台cityname数据
            webBrowser1.Document.GetElementById("cityName").InnerText= comboBox2.Text;
            //webBrowser1.Document.GetElementById(comboBox2.Text).InnerText;
            webBrowser1.Document.InvokeScript("theLocation");
           
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //City = comboBox2.Text;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}

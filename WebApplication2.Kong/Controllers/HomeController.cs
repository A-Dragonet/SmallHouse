using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Kong.Controllers
{
    public class HomeController : Controller
    {
/*示例
        public ActionResult Index()
        {
            return View();
        }
        //文件上传方式一

        public ActionResult UpLoad()
        {

            System.Diagnostics.Debug.WriteLine("16546s1");
            Request.Files["File"].SaveAs(Request.MapPath("~/upload/") + Request.Files["File"].FileName);
            int fileCount = Request.Files.Count; //上传数量
            double fileSize = Request.Files["File"].ContentLength; //文件大小（字节）
            string fileName = Request.Files["File"].FileName; //文件名
            string fileType = Request.Files["File"].ContentType;//文件类型
            string fileExt = System.IO.Path.GetExtension(fileName); //文件扩展后缀名
            return Content($"上传数量:{fileCount} 文件名:{fileName} 文件类型：{fileType} 文件格式:{fileExt}");
        }
        //文件上传方式二
        public ActionResult UploadFile(HttpPostedFileBase fileName)
        {
            fileName.SaveAs(Request.MapPath("~/upload/") + fileName);
            System.Diagnostics.Debug.WriteLine("165461");
            return Content("OK");
        }
*/

        //常用的内置对象
        //      Request     Response      Session       Cookie                  Application     Server
        //      请求         响应          会话           缓存客户端数据的存储       当前网站对象       服务器对象


        //Request：服务器接收客户端数据的       //在地址栏输入localhost:（这个服务器的端口号），请求
        //Request.QueryString["name"]：请求.查询字符串["name参数的数据"]   对应着Request里的get方法，在地址后面接 ？name = value
        //$"{Request.QueryString["name"]}- {Request.QueryString["Age"]}-{Request.QueryString["id"]}"：get连接多个添加 &

        //Request.Form["loginname"]：对应post请求

        //Request.MapPath()：将虚拟路径转换为物理路径    
        //虚拟路径：相对路径，相对这个文件的路径       物理路径：绝对路径，C盘符，D盘符等，在那个盘符下的那个文件夹的路径

        //Request.Files["file"]：post请求的文件，俗称文件上传

        public ActionResult Index()//get
        {
            return Content($"{Request.QueryString["name"]}- {Request.QueryString["Age"]}-{Request.QueryString["id"]}");
        }

        public ActionResult PostData()//post
        {
            return Content(Request.Form["loginname"]);
        }

        public ActionResult FileData()//file
        {
            //SavaAs需要物理路径
            Request.Files["file"].SaveAs(Request.MapPath("~/uploads/" + Request.Files["file"].FileName));
            return Content("ok");
        }

        //服务器给客户端的结果称之为响应
        // Response.Write("窗帘插嘴"); ：向客户端输出内容

        // Response.Redirect("www.baidu.com");  ：重定向，重新请求另外一个路径，地址会变成定向外部地址
        public ActionResult ResponseData()
        {
            //Response.Write("窗帘插嘴");
            Response.Redirect("http://www.baidu.com");
            return Content("");
        }
        //headers：请求头，可以添加很多隐藏信息
        public ActionResult ResponseHeader()
        {
            Response.Headers["Hello"] = "word";
            return Content(Response.Headers["token"]);
        }
        //从浏览器进入网站开始，就启动了一个Session，每个人的Session是独立的，Session是所有人自己的存贮空间
        //存储时间为20分钟，如果二十分钟之内没有进行任何操作（可自己设置时间），Session消失。Session数据保存在服务器中 Session 90% 是用来做身份识别，存储少量重要数据
        public ActionResult SessionData()
        {
            //Session   会话  数据保存在服务器中，保存少量并且重要的数据，比如账号
            //Session   是一个键值对，读取数据的
            //Session   销毁使用Abandon/Clear
            Session["user"] = Request.Form["user"]; //页面通过post方式传出一个post数据，存成了会话中的内容，名字叫user
            return Content("会话中的数据是："+Session["user"]);
        }

        public ActionResult GetSession()
        {
            return Content("当前会话的数据是"+Session["user"]);
        }

        //Cookie
        public ActionResult CookieSave()
        {
            //这个Cookie值具有时效性
            //给客户端的cookie显示值
            Response.Cookies.Add(new HttpCookie("token") { 
                Value = "123asd",
                Expires = DateTime.Now.AddDays(1)//AddDays(1)让这个值保存在浏览器第几天，   AddHours(5)这就是保存五小时
            });
            return Content("ok");
        }

        public ActionResult CookieGet()
        {
            //获取cookie值
            return Content(Request.Cookies["token"].Value);
        }

        public ActionResult CookieClear()
        {
            //清除cookie值，使用过期的方式
            Response.Cookies.Add(new HttpCookie("token"){ 
                Expires = DateTime.Now.AddDays(0)
            });
            return Content("ok");
        }
        //Application：存储的是一个全局的
        public ActionResult ApplicationData()//存
        {
            HttpContext.Application["user"] = "abc";
            return Content("");
        }

        public ActionResult ApplicationGet()//取
        {
            return Content(HttpContext.Application["user"].ToString());
        }

        //Server.Transfer：转发
        //Server.MapPath：返回与Web服务器上的指定虚拟路径相对应的物理文件路径，其参数path为Web 服务器的虚拟路径，
                        //返回结果是与path相对应的物理文件路径。但有时参数并非为虚拟路径，而是用户自定义的文件名
        //Server.HttplEncode：一些转码行为
        //Server.HttplDecode：一些转码行为
        //Server.UrlEncode：一些转码行为
        //Server.UrlDecode：一些转码行为

        public ActionResult ServerDemo()
        {
            //Transfer:转发，路径不变，内容发生变化，不支持外部路径，只能转发自己有的，地址栏地址不变  （可参考Response.Redirect重定向，76行）
            Server.Transfer("/WebForm1.aspx");
            return Content("ok");
        }

        public ActionResult ShowDemo()
        {
            return Content("这是内容");
        }
    }
}
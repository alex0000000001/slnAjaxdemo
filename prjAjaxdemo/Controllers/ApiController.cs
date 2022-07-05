using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjAjaxdemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace prjAjaxdemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApiController(DemoContext demoContext , IWebHostEnvironment webHost)
        {
            db = demoContext;
            _webHostEnvironment = webHost;
        }

        public IActionResult Index(User user) 
        {
            //System.Threading.Thread.Sleep(5000); //停止5秒鐘

            //return Content("Ajax, 你好!!","text/plain", System.Text.Encoding.UTF8);
            if (String.IsNullOrEmpty(user.name))
            {
                user.name = "Ajax";
            }
            return Content($"{user.name}你好,你的年紀是{user.age}!!", "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Register(Member member , IFormFile file)
        {
            string pName = Guid.NewGuid().ToString() + ".jpg";

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", pName);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(filestream);
            }

            byte[] imgByte = null;
            using (var memoryStrem = new MemoryStream()) 
            {
                file.CopyTo(memoryStrem);
                imgByte = memoryStrem.ToArray();
            }

            member.FileName = pName;
            member.FileData = imgByte;

            db.Members.Add(member);
            db.SaveChanges();


            string info = $"{file.FileName}-{file.ContentType}-{file.Length}";

            return Content(info, "text/plain", System.Text.Encoding.UTF8);
        }


        public IActionResult Create(Member member)
        {
            var data = db.Members.FirstOrDefault(m => m.Name == member.Name);
            if (data != null)
            {
                return Content($"{data.Name}已經被註冊過。");
            }
            else
            {
                //todo
                //無法寫入資料庫? 

                //data.Name = member.Name;
                //data.Age = member.Age;
                //data.Email = member.Email;
                //data.FileName = member.FileName;
                //data.FileData = member.FileData;
                
                //db.SaveChanges();

                if(string.IsNullOrEmpty(member.Name))
                    return Content($"會員姓名不可為空白!");
                else
                    return Content($"{member.Name}可以使用!");
            }
        }

        //========================老師===========================
        public IActionResult CheckAccount(string name)
        {
            var exists = db.Members.Any(m => m.Name == name);
            return Content(exists.ToString(), "text/plain");

            // true / false
        }

        // =========== View <script>==========
        //  
        //if (txtName.value.trim() === "") {
        //        //顯示錯誤訊息 (前端 先判斷 是否空白)
        //    } else {
        //        //Ajax   //  回傳 true / false 再以前端選擇回傳的內容

        //        if (data == "True") {
        //        }
        //        else
        //
        //    }
        //====================================
        //=======================================================

        // 讀出所有城市的資料
        //public IActionResult City()
        //{
        //    var cities = db.Addresses.Select(a => a.City).Distinct();
        //    return Json(cities);
        //}
        public IActionResult City()
        {
            var cities = db.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }



        // 根據城市名稱讀出鄉鎮區的資料
        public IActionResult District(string city)
        {
            var districts = db.Addresses.Where(a => a.City==city).Select(a=>a.SiteId).Distinct();
            return Json(districts);
        }
        // 根據鄉鎮區的資料讀出路名
        public IActionResult Road(string district)
        {
            var roads = db.Addresses.Where(a => a.SiteId==district).Select(a=>a.Road).Distinct();
            return Json(roads);
        }

        public IActionResult GetImageBytes(int id = 1)
        {
            Member member = db.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
        }

        public IActionResult FirstAjax()
        {
                return View();
        }
        public IActionResult Index2(User user)
        {
                //System.Threading.Thread.Sleep(1000);

                if (String.IsNullOrEmpty(user.name))
                {
                    user.name = "Ajax";
                }
                return Content($"{user.name}你好,你的年紀是{user.age}!!", "text/plain", System.Text.Encoding.UTF8);
                //return Content($"{user.name},你是{user.age}歲!,Email是{user.email},電話號碼是{user.phone} ", "text/plain", System.Text.Encoding.UTF8);
        }
        public IActionResult Members()
        {
            var members = db.Members.ToList();

            return Json(members);
        }
    }
}

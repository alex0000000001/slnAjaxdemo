using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjAjaxdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAjaxdemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext db;
        public ApiController(DemoContext demoContext)
        {
            db = demoContext;
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
    }
}

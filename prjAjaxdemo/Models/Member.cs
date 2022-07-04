using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace prjAjaxdemo.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        [DisplayName("會員姓名")]
        public string Name { get; set; }
        [DisplayName("會員電子信箱")]
        public string Email { get; set; }
        [DisplayName("會員年齡")]
        public int? Age { get; set; }
        [DisplayName("照片名稱")]
        public string FileName { get; set; }
        [DisplayName("會員照片")]
        public byte[] FileData { get; set; }
    }
}

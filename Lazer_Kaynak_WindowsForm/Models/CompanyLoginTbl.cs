using System;
using System.Collections.Generic;
using System.Collections;

namespace Lazer_Kaynak_WindowsForm.Models
{
    public partial class CompanyLoginTbl
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Guid { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public BitArray? Type { get; set; }
        public BitArray? IsActive { get; set; }
    }
}

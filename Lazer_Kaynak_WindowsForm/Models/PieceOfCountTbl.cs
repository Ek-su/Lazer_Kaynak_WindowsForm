using System;
using System.Collections.Generic;

namespace Lazer_Kaynak_WindowsForm.Models
{
    public partial class PieceOfCountTbl
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? PieceOfCount { get; set; }
        public int? DeviceNo { get; set; }
        public string? DeviceName { get; set; }
    }
}

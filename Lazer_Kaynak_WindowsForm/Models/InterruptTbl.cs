using System;
using System.Collections.Generic;

namespace Lazer_Kaynak_WindowsForm.Models
{
    public partial class InterruptTbl
    {
        public string? OperatorName { get; set; }
        public string? MachineName { get; set; }
        public string? InterruptTime { get; set; }
        public string? RecordType { get; set; }
        public string? InterruptType { get; set; }
        public string? InterruptNote { get; set; }
        public DateTime? InterruptStartDate { get; set; }
        public DateTime? InterruptEndDate { get; set; }
        public int Id { get; set; }
        public string? DeviceNo { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Lazer_Kaynak_WindowsForm.Models
{
    public partial class ProgramTbl
    {
        public string? MaterialType { get; set; }
        public int? MaterialThickness { get; set; }
        public int? LaserPower { get; set; }
        public int? WeldingSpeed { get; set; }
        public string? ShieldingGas { get; set; }
        public int? GasFlowRate { get; set; }
        public string? GasDirection { get; set; }
        public int? FocalLength { get; set; }
        public bool? MaximumPenetration { get; set; }
        public int? Wavelength { get; set; }
        public bool? LaserMode { get; set; }
        public int Id { get; set; }
        public string? DeviceNo { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Assembly<T>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? Likes { get; set; }
        public DateTime? CreationTime { get; set; }
        public int Power { get; set; }
        public int ComputerCaseId { get; set; }
        public int CoolerId { get; set; }
        public int MotherBoardId { get; set; }
        public int PowerUnitId { get; set; }
        public int ProcessorId { get; set; }
        public int RamId { get; set; }
        public int SsdId { get; set; }
        public int VideoCardId { get; set; }
        public T Image { get; set; }
    }
}

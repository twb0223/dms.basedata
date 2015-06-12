using System;
using System.ComponentModel.DataAnnotations;

namespace Base.Model
{
    public class Station
    {
        [Key]
        public string StationID { get; set; }

        [Required]
        [MaxLength(50)]
        public string StationName { get; set; }

        [MaxLength(150)]
        public string StationDes { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }

    }
}

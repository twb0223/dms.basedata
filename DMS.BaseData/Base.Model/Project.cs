using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Model
{
    public class Project
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        [MaxLength(50)]
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public int ProjectTypeID { get; set; }

        public virtual ProjectType ProjectType { get; set; }
    }
}

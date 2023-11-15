using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name{ get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]  
        public string email { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public int contact { get; set; }
        [Required]
        public string Designation { get; set; }
    }
}

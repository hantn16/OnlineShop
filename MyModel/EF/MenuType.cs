using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("MenuType")]
    public class MenuType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Tên kiểu menu")]
        [Required(ErrorMessage = "Not allow to be null or empty")]
        [StringLength(250)]
        public string Name { get; set; }
    }
}

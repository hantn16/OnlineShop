using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Footer")]
    public class Footer
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Display(Name = "Tình trạng")]
        public bool? Status { get; set; }
    }
}

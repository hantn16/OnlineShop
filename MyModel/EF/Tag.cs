using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Tag")]
    public class Tag
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Required(ErrorMessage = "Tên tag không được phép để trống")]
        [StringLength(50)]
        public string Name { get; set; }

        //Navigation properties
        public virtual ICollection<ContentTag> ContentTags { get; set; }
    }
}

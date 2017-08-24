using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Tên Menu")]
        [Required(ErrorMessage = "Tên Menu không được phép để trống")]
        [StringLength(50)]
        public string Text { get; set; }

        [Display(Name = "Đường dẫn")]
        [StringLength(250)]
        public string Link { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }

        [ForeignKey("MenuType")]
        [Display(Name = "Kiểu menu")]
        public int? TypeID { get; set; }

        //Thuộc tính navigation
        public virtual MenuType MenuType { get; set; }
    }
}

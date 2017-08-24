using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("FeedBack")]
    public class FeedBack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Họ tên không được phép để trống")]
        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Điện thoại")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(50)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Nội dung không được phép để trống")]
        [Display(Name = "Nội dung")]
        [StringLength(250)]
        public string Content { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Tình trạng")] //1-chưa xử lý, 0-đã xử lý
        public bool? Status { get; set; }
    }
}

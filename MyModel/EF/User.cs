using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required(ErrorMessage = "Tên tài khoản không được phép để trống")]
        [Display(Name = "Tài khoản")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được phép để trống")]
        [Display(Name = "Mật khẩu")]
        [StringLength(32)]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Họ tên không được phép để trống")]
        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Người tạo")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Người cập nhật")]
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Required(ErrorMessage = "Tình trạng kích hoạt không được phép để trống")]
        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}

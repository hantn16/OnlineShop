using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("About")]
    public class About
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Display(Name = "Tên")]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Tiêu đề SEO")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Display(Name = "Chi tiết")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Người tạo")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Người sửa")]
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Từ khóa SEO")]
        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [Display(Name = "Mô tả SEO")]
        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}

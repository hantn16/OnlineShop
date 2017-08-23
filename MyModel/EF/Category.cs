using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được phép bỏ trống")]
        [Display(Name = "Tên danh mục")]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Tiêu đề Meta")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [ForeignKey("ParentCategory")]
        [Display(Name = "Danh mục cha")]
        public long? ParentID { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public long? DisplayOrder { get; set; }

        [Display(Name = "Tiêu đề SEO")]
        [StringLength(250)]
        public string SeoTitle { get; set; }

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

        [Display(Name = "Từ khóa Meta")]
        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [Display(Name = "Mô tả Meta")]
        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }

        [Display(Name = "Hiện trên trang chủ")]
        public bool ShowOnHome { get; set; }

        //Thuộc tính navigation
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Content> Contents { get; set; }
    }
}

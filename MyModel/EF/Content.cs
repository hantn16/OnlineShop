using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Content")]
    public class Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required(ErrorMessage = "Not Allow to be null or empty")]
        [Display(Name = "Tiêu đề")]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Danh mục")]
        public long? CategoryID { get; set; }

        //[Required(ErrorMessage = "Not Allow to be null or empty")]
        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Người đăng")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Người sửa")]
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Từ khóa meta")]
        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [Display(Name = "Mô tả meta")]
        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        [Display(Name = "Số lượt xem")]
        public int? ViewCount { get; set; }

        [Display(Name = "Tags")]
        [StringLength(500)]
        public string Tagstring { get; set; }

        //Thuộc tính navigation
        public virtual Category Category { get; set; }
        public virtual ICollection<ContentTag> ContentTags { get; set; }
    }
}

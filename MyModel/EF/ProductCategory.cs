using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Display(Name = "Tên danh mục sản phẩm")]
        [Required(ErrorMessage = "Tên danh mục không được phép để trống")]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [ForeignKey("ParentProductCat")]
        [Display(Name = "Danh mục cha")]
        public long? ParentID { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public long? DisplayOrder { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }

        [Display(Name = "Hiện trên trang chủ")]
        public bool ShowOnHome { get; set; }

        //Thuộc tính navigation
        public virtual ICollection<Product> Products { get; set; }

        public virtual ProductCategory ParentProductCat { get; set; }
    }
}

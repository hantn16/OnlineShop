using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được phép để trống")]
        [StringLength(20)]
        public string Code { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được phép để trống")]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Code")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Display(Name = "Galery ảnh")]
        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Display(Name = "Giá bán")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mại")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Bao gồm VAT")]
        public bool IncludeVAT { get; set; }

        [Display(Name = "Số lượng")]
        public long? Quantity { get; set; }

        [ForeignKey("ProductCategory")]
        [Display(Name = "Mã danh mục sản phẩm")]
        public long? CategoryID { get; set; }

        [Display(Name = "Chi tiết")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Display(Name = "Số tháng bảo hành")]
        public int? Waranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Code")]
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Từ khóa meta")]
        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [Display(Name = "Mô tả meta")]
        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Display(Name = "Tình trạng")]
        [Required(ErrorMessage = "Tình trạng không được phép để trống")]
        public bool Status { get; set; }

        [Display(Name = "Ngày bắt đầu cho tophot")]
        public DateTime? TopHot { get; set; }

        [Display(Name = "Số lượt xem")]
        public int? ViewCount { get; set; }

        //Thuộc tính navigation
        public virtual ProductCategory ProductCategory { get; set; }
    }
}

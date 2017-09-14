using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [ForeignKey("Order")]
        public long OrderID { get; set; }

        [ForeignKey("Product")]
        [Display(Name = "Mã sản phẩm")]
        public long ProductID { get; set; }

        [Display(Name = "Số lượng")]
        public long? Quantity { get; set; }

        [Display(Name = "Đơn giá")]
        public decimal? Price { get; set; }

        //Thuộc tính navigation
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

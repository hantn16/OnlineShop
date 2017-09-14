using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("Customer")]
        public long CustomerID { get; set; }
        [Display(Name = "Tên ship")]
        public string ShipName { get; set; }
        [Display(Name = "Điện thoại ship")]
        public string ShipMobile { get; set; }
        [Display(Name = "Địa chỉ ship")]
        public string ShipAddress { get; set; }
        [Display(Name = "Email")]
        public string ShipEmail { get; set; }
        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
        //Thuộc tính navigation
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User Customer { get; set; }
    }
}

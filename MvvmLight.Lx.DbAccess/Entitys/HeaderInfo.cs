using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.DbAccess.Entitys
{
    [Table("headerinfo")]
    public class HeaderInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        public string HeaderName {  get; set; }
        public string NavigatToView {  get; set; }
    }
}

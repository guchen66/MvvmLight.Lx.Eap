using MvvmLight.Lx.DbAccess.Entitys.BaseEntitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.DbAccess.Entitys
{
    [Table("userinfo")]
    public class UserInfo : BaseEntity, IDeletionWare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdministrator {  get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDeleted {  get; set; }
    }
}

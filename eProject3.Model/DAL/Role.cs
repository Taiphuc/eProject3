namespace eProject3.Model.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Role")]
    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            UserGroup = new HashSet<UserGroup>();
        }

        [StringLength(50)]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Bạn chưa nhập Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mô tả")]
        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGroup> UserGroup { get; set; }
    }
}

using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BS_Zoom_Demo.Teachers
{
    [Table("StsTeacher")]
    public class Person : Entity<int>
    {
        /// <summary>
        /// A property (database field) for a Person to store his/her name.
        /// NOTE: NHibernate requires that all members of an entity must be virtual (for proxying purposes)!
        /// </summary>
        public virtual string Name { get; set; }

        public virtual string ZoomId { get; set; }
    }
}

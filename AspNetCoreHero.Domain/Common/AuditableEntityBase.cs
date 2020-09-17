using System;

namespace AspNetCoreHero.Domain.Common
{
    public abstract class AuditableEntityBase
    {
        public virtual int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Domain.Common
{
    public abstract class AuditableEntity
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

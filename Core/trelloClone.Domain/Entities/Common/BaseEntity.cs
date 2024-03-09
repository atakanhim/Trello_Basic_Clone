using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trelloClone.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual DateTime UpdatedTime { get; set; }
    }
}

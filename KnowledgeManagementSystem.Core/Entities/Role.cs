using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<User> Users { get; set; }
    }
}

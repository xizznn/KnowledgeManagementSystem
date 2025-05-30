using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserAuthor { get; set; }
        public ICollection<TestInCourse> Tests { get; set; }
        public ICollection<UserOnCourse> Users { get; set; }
    }
}
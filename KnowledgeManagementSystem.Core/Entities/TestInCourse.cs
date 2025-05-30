using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Entities
{
    public class TestInCourse
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}

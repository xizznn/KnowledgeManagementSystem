using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<QuestionsInTest> Questions { get; set; }
        public ICollection<TestInCourse> Courses { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public ICollection<QuestionsInTest> Tests { get; set; }
    }
}
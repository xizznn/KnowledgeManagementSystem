﻿using System.Text.Json;

namespace KnowledgeManagementSystem.Core.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);
    }
}

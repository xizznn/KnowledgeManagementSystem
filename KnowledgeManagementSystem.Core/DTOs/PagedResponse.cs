﻿namespace KnowledgeManagementSystem.Core.DTOs
{
    public class PagedResponse<T>
    {
        public required IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
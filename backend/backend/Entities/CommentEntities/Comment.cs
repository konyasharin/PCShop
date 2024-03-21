﻿namespace backend.Entities.CommentEntities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = String.Empty;
        public DateTime CommentDate { get; set; }
        public int ProductId { get; set; }
    }
}

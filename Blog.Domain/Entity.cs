﻿namespace Blog.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
    public abstract class Entity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

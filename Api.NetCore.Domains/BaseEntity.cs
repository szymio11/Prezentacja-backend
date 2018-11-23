using System;

namespace Api.NetCore.Domains
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public BaseEntity()
        {
            Id = new Guid();
            IsActive = true;
        }
    }
}
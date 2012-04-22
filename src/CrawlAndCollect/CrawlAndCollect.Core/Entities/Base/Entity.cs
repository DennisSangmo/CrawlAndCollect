using System;

namespace CrawlAndCollect.Core.Entities.Base {
    public class Entity {
        public Guid Id { get; set; }

        public Entity() {
            Id = Guid.NewGuid();
        }
    }
}
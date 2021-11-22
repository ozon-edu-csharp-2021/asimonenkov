using System.Collections.Concurrent;
using System.Collections.Generic;
using Route256.MerchandiseService.Domain.Models;
using Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Route256.MerchandiseService.Infrastructure.Repositories
{
    public class ChangeTracker : IChangeTracker
    {
        public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();
        
        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField;

        public ChangeTracker()
        {
            _usedEntitiesBackingField = new ConcurrentBag<Entity>();
        }
        
        public void Track(Entity entity)
        {
            _usedEntitiesBackingField.Add(entity);
        }
    }
}
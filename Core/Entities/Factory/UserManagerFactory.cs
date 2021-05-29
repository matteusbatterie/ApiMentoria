using Core.Entities.Factory.Abstractions;
using Core.Entities.Strategy;
using Core.Entities.Strategy.Interfaces;

namespace Core.Entities.Factory
{
    public class UserManagerFactory : UserStrategyFactory
    {
        public UserManagerFactory() { }

        
        public override IUserStrategy Create() => new UserStrategyManager();
    }
} 

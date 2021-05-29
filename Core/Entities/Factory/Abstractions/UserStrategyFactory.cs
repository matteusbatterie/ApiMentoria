using Core.Entities.Strategy.Interfaces;

namespace Core.Entities.Factory.Abstractions
{
    public abstract class UserStrategyFactory
    {
        public abstract IUserStrategy Create();
    }
}
using Core.Entities.Factory.Abstractions;
using Core.Entities.Strategy;
using Core.Entities.Strategy.Interfaces;

namespace Core.Entities.Factory
{
    public class UserStrategyCommonFactory : UserStrategyFactory
    {
        public UserStrategyCommonFactory() { }


        public override IUserStrategy Create() => new UserStrategyCommon();
    }
}
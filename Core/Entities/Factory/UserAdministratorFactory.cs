using Core.Entities.Factory.Abstractions;
using Core.Entities.Strategy;
using Core.Entities.Strategy.Interfaces;

namespace Core.Entities.Factory
{
    public class UserAdministratorFactory : UserStrategyFactory
    {
        public UserAdministratorFactory() { }


        public override IUserStrategy Create() => new UserStrategyAdministrator();
    }
}
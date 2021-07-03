using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Enums;
using Core.Services.Strategies.Employees.Reports;

namespace Core.Services.Factories.Employees.Reports
{
    public class EmployeeReportStrategyFactory
    {
        public UserRole Role { get; set; }
        private static readonly IReadOnlyDictionary<UserRole, EmployeeReportStrategy> _strategies = 
            typeof(EmployeeReportStrategy).Assembly.ExportedTypes
                .Where(t => typeof(EmployeeReportStrategy).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t)) // !!
                .Cast<EmployeeReportStrategy>()
                .ToImmutableDictionary(t => t.UserRole, t => t);

        public EmployeeReportStrategyFactory(UserRole role)
        {
            Role = role;

        }

        public static EmployeeReportStrategy GetStrategy(UserRole role)
        {
            _strategies.TryGetValue(role, out EmployeeReportStrategy entity);
            return entity ?? throw new Exception("The specified role whether was not implementer or doesn't have a Strategy implementation");
        }
    }
}

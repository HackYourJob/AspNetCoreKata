using System.Reflection;
using Microsoft.Extensions.Localization;

namespace aspnetcore5.Resources
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResources);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(nameof(SharedResources), assemblyName.Name);
        }
        public LocalizedString this[string name] => _localizer[name];
    }
}
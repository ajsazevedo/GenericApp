using GenericApp.Infra.CC.Localization.Interfaces;
using Microsoft.Extensions.Localization;

namespace GenericApp.Infra.CC.Localization
{
    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}

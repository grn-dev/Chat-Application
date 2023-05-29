using Chat.Application.Configuration.Data;
using Chat.Language.Services;
using Microsoft.Extensions.Localization;

namespace Chat.Language
{
    public class LangUtility : ILangUtility
    {
        private readonly EFStringLocalizerFactory LocalizerFactory;
        private IStringLocalizer _e;

        public LangUtility(EFStringLocalizerFactory localizerFactory)
        {
            LocalizerFactory = localizerFactory;
            _e = localizerFactory.Create(null);
        }

        public string Translate(string key)
        {
            return _e[key];
        }
    }
}
using ICities;
using ThemeEditorMaps.Redirection;

namespace ThemeEditorMaps
{
    public class UserMod : IUserMod
    {
        private bool deployed;

        public UserMod()
        {
            if (deployed) return;
            Redirector<LoadThemePanelDetour>.Deploy();
            Redirector<NewThemePanelDetour>.Deploy();
            deployed = true;
        }

        public string Name => "Theme Editor Maps";
        public string Description
        {
            get
            {
                Initializer.Initialize();
                return "Load custom maps into the theme editor.";
            }
        }
    }
}

using UnityEngine;

namespace ThemeEditorMaps
{
    public class Initializer : MonoBehaviour
    {
        private static bool initialized;

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }
            new GameObject("ThemeEditorMaps").AddComponent<Initializer>();
        }

        public void Awake()
        {
            if (!initialized)
            {
                
                if (ToolsModifierControl.toolController == null || ToolsModifierControl.toolController.m_mode == ItemClass.Availability.None || ToolsModifierControl.toolController.m_mode == ItemClass.Availability.ThemeEditor)
                {
                    LoadThemeUI.Initialize(this.gameObject);
                    NewThemeUI.Initialize(this.gameObject);
                }
                initialized = true;
            }
        }

        public void OnDestroy()
        {            
            initialized = false;
        }
    }
}

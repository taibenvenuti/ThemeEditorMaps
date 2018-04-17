using ColossalFramework.UI;
using UnityEngine;

namespace ThemeEditorMaps
{
    public class LoadThemeUI : AbstractUI<MapThemeMetaData>
    {
        internal static UILabel Anchor;

        public static void Initialize(GameObject gameObject)
        {
            gameObject.AddComponent<LoadThemeUI>();
        }

        public void Update()
        {
            if (Anchor == null)
            {
                var panelObject = GameObject.Find("(Library) LoadThemePanel");
                if (panelObject == null)
                {
                    return;
                }
                basePanel = panelObject.GetComponent<LoadThemePanel>();
                var panel = panelObject.GetComponent<UIPanel>();
                
                Anchor = panel.Find<UILabel>("MapThemeLabel");
               
                dropDown = UIUtil.CreateDropDown(Anchor.parent);
                dropDown.name = "MapDropDown";
                dropDown.relativePosition = new Vector2(Anchor.relativePosition.x, Anchor.relativePosition.y + 74);
                dropDown.eventSelectedIndexChanged += OnDropDownIndexChanged;
                SetupDropdown();

                label = UIUtil.CreateLabel(Anchor.parent);
                label.text = "Select Map:";
                label.relativePosition = new Vector2(Anchor.relativePosition.x, Anchor.relativePosition.y + 55);
            }
        }
    }
}

using ColossalFramework.UI;
using UnityEngine;

namespace ThemeEditorMaps
{
    public class NewThemeUI : AbstractUI<SystemMapMetaData>
    {
        internal static UIButton Anchor;

        public static void Initialize(GameObject gameObject)
        {
            gameObject.AddComponent<NewThemeUI>();
        }

        public void Update()
        {
            if (Anchor == null)
            {
                var panelObject = GameObject.Find("(Library) NewThemePanel");
                if (panelObject == null) return;
                basePanel = panelObject.GetComponent<NewThemePanel>();
                var panel = panelObject.GetComponent<UIPanel>();

                Anchor = panel.Find<UIButton>("Create"); 

                dropDown = UIUtil.CreateDropDown(Anchor.parent);
                label = UIUtil.CreateLabel(Anchor.parent);
                
                label.text = "Select Map:";
                label.relativePosition = new Vector2(34f, Anchor.relativePosition.y + ((Anchor.height - dropDown.height) / 2) + ((dropDown.height - label.height) / 2));

                dropDown.name = "MapDropDown";
                dropDown.relativePosition = new Vector2(label.relativePosition.x + label.width + 10, Anchor.relativePosition.y + ((Anchor.height - dropDown.height) / 2));
                dropDown.eventSelectedIndexChanged += OnDropDownIndexChanged;

                SetupDropdown();
            }
        }
    }
}

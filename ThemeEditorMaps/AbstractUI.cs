using ColossalFramework.UI;

namespace ThemeEditorMaps
{
    public class AbstractUI<T> : LoadSavePanelBase<T> where T : MetaData
    {
        protected UIDropDown dropDown;
        protected UILabel label;
        protected LoadSavePanelBase<T> basePanel;

        protected override void Awake()
        {

        }

        public override void OnClosed()
        {

        }

        protected override void OnLocaleChanged()
        {

        }

        protected void OnDropDownIndexChanged(UIComponent component, int index)
        {
            ThemeEditorMaps.Settings.SelectedOption = ThemeEditorMaps.MapList[index];
            ThemeEditorMaps.Settings.Save();
        }

        protected void SetupDropdown()
        {
            dropDown.items = ThemeEditorMaps.MapList;
            dropDown.selectedIndex = ThemeEditorMaps.SelectedMapIndex;
        }
    }
}

using ColossalFramework.UI;
using UnityEngine;

namespace ThemeEditorMaps
{
    public class UIUtil
    {
        public static UILabel CreateLabel(UIComponent parent)
        {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.autoSize = true;
            label.textColor = new Color32(185, 221, 254, 255);
            label.textScale = 1;
            return label;
        }

        public static UIDropDown CreateDropDown(UIComponent parent)
        {
            UIDropDown dropDown = parent.AddUIComponent<UIDropDown>();
            dropDown.size = new Vector2(196f, 27f);
            dropDown.textScale = 1;
            dropDown.listBackground = "StylesDropboxListbox";
            dropDown.itemHeight = 22;
            dropDown.itemHover = "ListItemHover";
            dropDown.itemHighlight = "ListItemHighlight";
            dropDown.normalBgSprite = "CMStylesDropbox";
            dropDown.disabledBgSprite = "";
            dropDown.hoveredBgSprite = "";
            dropDown.focusedBgSprite = "";
            dropDown.listWidth = 196;
            dropDown.listHeight = 200;
            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            dropDown.popupColor = Color.white;
            dropDown.popupTextColor = new Color32(170, 170, 170, 255);
            dropDown.zOrder = 1;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;
            dropDown.horizontalAlignment = UIHorizontalAlignment.Left;
            dropDown.selectedIndex = 0;
            dropDown.textFieldPadding = new RectOffset(7, 28, 4, 0);
            dropDown.itemPadding = new RectOffset(14, 14, 4, 0);
            dropDown.listPadding = new RectOffset(4, 4, 4, 4);
            dropDown.listPosition = UIDropDown.PopupListPosition.Below;
            dropDown.triggerButton = dropDown;
            dropDown.eventSizeChanged += (c, t) =>
            {
                dropDown.listWidth = (int)t.x;
            };            
            return dropDown;
        }
    }
}

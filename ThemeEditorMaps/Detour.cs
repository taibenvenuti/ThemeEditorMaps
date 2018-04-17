using ColossalFramework;
using ColossalFramework.Packaging;
using ColossalFramework.PlatformServices;
using ColossalFramework.UI;
using System;
using ThemeEditorMaps.Redirection;
using static ThemeEditorMaps.ReflectionUtil;

namespace ThemeEditorMaps
{
    [TargetType(typeof(NewThemePanel))]
    public class NewThemePanelDetour : NewThemePanel
    {
        [RedirectMethod]
        public new void OnCreate()
        {
            if (Singleton<ToolManager>.exists && Singleton<ToolManager>.instance.m_properties != null && Singleton<ToolManager>.instance.m_properties.CurrentTool != null)
            {
                UITabstrip uitabstrip = UIView.Find<UITabstrip>("MainToolstrip");
                if (uitabstrip != null)
                {
                    ThemeEditorMainToolbar component = uitabstrip.GetComponent<ThemeEditorMainToolbar>();
                    if (component != null)
                    {
                        component.ResetToDefaults();
                    }
                }
            }
            SimulationMetaData simulationMetaData = new SimulationMetaData()
            {
                m_gameInstanceIdentifier = Guid.NewGuid().ToString(),
                m_WorkshopPublishedFileId = PublishedFileId.invalid,
                m_newMapAppVersion = 172482832u,
                m_updateMode = SimulationManager.UpdateMode.NewTheme
            };
            Package.Asset asset = PackageManager.FindAssetByName(ThemeEditorMaps.MapHash[ThemeEditorMaps.Settings.SelectedOption]);
            if (asset != null)
            {
                SystemMapMetaData listingMetaData = base.GetListingMetaData(this.selectedIndex);
                simulationMetaData.m_environment = listingMetaData.environment;
                Singleton<LoadingManager>.instance.LoadLevel(asset, "ThemeEditor", "InThemeEditor", simulationMetaData);
            }
            else
            {
                Singleton<LoadingManager>.instance.LoadLevel(base.GetListingData(this.selectedIndex), "ThemeEditor", "InThemeEditor", simulationMetaData);
            }
            UIView.library.Hide(base.GetType().Name, 1);
        }
    }

    [TargetType(typeof(LoadThemePanel))]
    public class LoadThemePanelDetour : LoadThemePanel
    {
        private string LastSaveName
        {
            get { return GetField<string>(typeof(LoadThemePanel), "m_LastSaveName"); }
            set { SetField(typeof(LoadThemePanel), "m_LastSaveName", value); }
        }

        [RedirectMethod]
        public new void OnLoad()
        {
            var m_SaveList = GetField<UIListBox>(this, "m_SaveList");
            
            base.CloseEverything();
            LastSaveName = base.GetListingName(m_SaveList.selectedIndex);
            SaveMapPanel.lastLoadedName = LastSaveName;
            MapThemeMetaData listingMetaData = base.GetListingMetaData(m_SaveList.selectedIndex);
            base.PrintModsInfo(listingMetaData.mods);
            SaveMapPanel.lastLoadedAsset = listingMetaData.name;
            SaveMapPanel.lastPublished = listingMetaData.isPublished;
            string listingPackageName = base.GetListingPackageName(m_SaveList.selectedIndex);
            PublishedFileId invalid = PublishedFileId.invalid;
            if (ulong.TryParse(listingPackageName, out ulong value))
            {
                invalid = new PublishedFileId(value);
            }
            SimulationMetaData simulationMetaData = new SimulationMetaData()
            {
                m_WorkshopPublishedFileId = invalid,
                m_updateMode = SimulationManager.UpdateMode.LoadMap,
                m_MapThemeMetaData = listingMetaData,
                m_environment = listingMetaData.environment
            };
            if (!string.IsNullOrEmpty(this.m_forceEnvironment))
            {
                simulationMetaData.m_environment = this.m_forceEnvironment;
            }
            Package.Asset asset = PackageManager.FindAssetByName(ThemeEditorMaps.MapHash[ThemeEditorMaps.Settings.SelectedOption]);
            if (asset != null)
            {
                Singleton<LoadingManager>.instance.LoadLevel(asset, "ThemeEditor", "InThemeEditor", simulationMetaData);
            }
            else
            {
                Package.Asset asset2 = PackageManager.FindAssetByName("System.BuiltinTerrainMap-" + listingMetaData.environment);
                SystemMapMetaData systemMapMetaData = asset2.Instantiate<SystemMapMetaData>();
                Singleton<LoadingManager>.instance.LoadLevel(systemMapMetaData.assetRef, "ThemeEditor", "InThemeEditor", simulationMetaData);
            }
            UIView.library.Hide(base.GetType().Name, 1);
        }
    }
}

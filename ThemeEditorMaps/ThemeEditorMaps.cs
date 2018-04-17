using ColossalFramework.Packaging;
using System;
using System.Collections.Generic;

namespace ThemeEditorMaps
{
    public class ThemeEditorMaps
    {
        private static ThemeEditorMapsSettings settings;

        public static ThemeEditorMapsSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = ThemeEditorMapsSettings.Load();
                    if (settings == null)
                    {
                        settings = new ThemeEditorMapsSettings();
                        settings.Save();
                    }
                }
                return settings;
            }
            set
            {
                settings = value;
            }
        }

        public static string[] MapList => GetMapList().ToArray();

        public static Dictionary<string, string> MapHash;

        public static int SelectedMapIndex => GetMapList().FindIndex(option => option == Settings.SelectedOption);

        private static List<string> GetMapList()
        {
            MapHash = new Dictionary<string, string>();
            var mapList = new List<string>();
            bool sfOwned = SteamHelper.IsDLCOwned(SteamHelper.DLC.SnowFallDLC);
            bool ndOwned = SteamHelper.IsDLCOwned(SteamHelper.DLC.NaturalDisastersDLC);
            bool mtOwned = SteamHelper.IsDLCOwned(SteamHelper.DLC.InMotionDLC);
            bool gcOwned = SteamHelper.IsDLCOwned(SteamHelper.DLC.GreenCitiesDLC);
            foreach (Package.Asset asset in PackageManager.FilterAssets(new Package.AssetType[] { UserAssetType.MapMetaData }))
            {
                if (asset != null && asset.isEnabled)
                {
                    try
                    {
                        MapMetaData mapMetaData = asset.Instantiate<MapMetaData>();
                        if ((mapMetaData.environment != "Winter" || sfOwned) && mapMetaData.isPublished)
                        {
                            string text = mapMetaData.mapName;
                            bool flag5 = NewGamePanel.IsNDScenarioMap(text);
                            bool flag6 = NewGamePanel.IsMTScenarioMap(text);
                            bool flag7 = NewGamePanel.IsGCScenarioMap(text);
                            bool flag8 = mapMetaData.environment == "Winter" && mapMetaData.IsBuiltinMap(asset.package.packagePath);
                            if ((mtOwned || !mapMetaData.IsBuiltinMap(asset.package.packagePath) || !flag6) && (ndOwned || !mapMetaData.IsBuiltinMap(asset.package.packagePath) || !flag5) && (gcOwned || !mapMetaData.IsBuiltinMap(asset.package.packagePath) || !flag7) && !NewGamePanel.IsUnplayableNDScenarioMap(text))
                            {
                                mapList.Add(text);
                                MapHash.Add(text, mapMetaData.assetRef.fullName);
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            mapList.Sort();
            return mapList;
        }
    }
}

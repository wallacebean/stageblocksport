using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using LLHandlers;
using BepInEx.Logging;
using BepInEx.Configuration;
using LLBML.Utils;


namespace StageBlocks
{
    [BepInPlugin(PluginInfos.PLUGIN_ID, PluginInfos.PLUGIN_NAME, PluginInfos.PLUGIN_VERSION)]
    [BepInDependency(LLBML.PluginInfos.PLUGIN_ID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("no.mrgentle.plugins.llb.modmenu", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInProcess("LLBlaze.exe")]

    public class StageBlocks : BaseUnityPlugin

    {
        public static ManualLogSource Log { get; private set; } = null;
        public static StageBlocks Instance { get; private set; }
        public static ConfigEntry<bool> customStageBlocks;


        public string BlocksConfigPath => Path.Combine(ModdingFolder.GetModSubFolder(this.Info).FullName, "BlockConfig.txt");
        public Dictionary<Stage, List<StageBlock>> stageBlocks = new Dictionary<Stage, List<StageBlock>>();


        void Awake()
        {
            Log = this.Logger;
            Instance = this;



            Config.Bind("Use Custom Stage Blocks", "mm_header_qol", "Use Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            customStageBlocks = Config.Bind<bool>("StageBlocksToggle", "enableCustomStageBlocks", true);


            Logger.LogDebug("liuberal poop hippie pronoun fortnite be farting pooping bacon epic, i love ivermectin flouride apartmentism");

            var harmony = new Harmony(this.Info.Metadata.GUID);
            harmony.PatchAll(typeof(WorldSetStageDimensionsPatch));
            harmony.PatchAll(typeof(NitroStuckFix));

            Logger.LogDebug($"{this.Info.Metadata.Name} is loaded");
        }

        void Start()
        {
            ModDependenciesUtils.RegisterToModMenu(this.Info);
            LoadConfig();
        }

        private void CreateConfig()
        {
            var defaultStageBlocks = new Dictionary<Stage, List<StageBlock>>
            {
                {
                    Stage.OUTSKIRTS,
                    new List<StageBlock>
                    {
                        new Rect(new Vector2(-500,232), new Vector2(250,54)),
                        new Rect(new Vector2(500,232), new Vector2(250,54)),
                        new Rect(new Vector2(0,414), new Vector2(250,105)),
                        new Rect(new Vector2(0,0), new Vector2(250,54)),
                    }
                },
                {
                    Stage.SEWERS,
                    new List<StageBlock>
                    {
                        new Rect(new Vector2(-500,232), new Vector2(250,54)),
                        new Rect(new Vector2(500,232), new Vector2(250,54)),
                        new Rect(new Vector2(0,414), new Vector2(250,105)),
                        new Rect(new Vector2(0,0), new Vector2(250,54)),
                    }
                }
            };
            using (TextWriter configFile = File.CreateText(BlocksConfigPath))
            {
                WriteConfig(configFile, defaultStageBlocks);
            }
        }

        public void LoadConfig()
        {
            if (!File.Exists(BlocksConfigPath))
            {
                CreateConfig();
            }
            using (TextReader config = File.OpenText(BlocksConfigPath))
            {
                ReadConfig(config, stageBlocks);
            }
        }

        private static readonly Dictionary<Stage, string> allStagesMapping = StringUtils.regularStagesNames.Union(StringUtils.retroStagesNames).ToDictionary(x => x.Key, x => x.Value);
        public void WriteConfig(TextWriter writer, Dictionary<Stage, List<StageBlock>> config)
        {
            writer.WriteLine("# The format is <stage name>: <pos x>, <pos y>, <size x>, <size y>");
            writer.WriteLine("# One block per line, '#' is a comment line.");
            writer.WriteLine("# stage xy origin is bottom center of the stage");
            writer.WriteLine("# block xy origin is bottom left of the block");
            writer.WriteLine("# Refer to readme for stage names (case sensitive)");
            foreach (Stage stage in config.Keys)
            {
                foreach (StageBlock block in config[stage])
                {
                    writer.WriteLine(allStagesMapping[stage] + ": " + block.ToString());
                }
            }
        }

        private static readonly Dictionary<string, Stage> reverseStageMapping = allStagesMapping.ToDictionary(x => x.Value, x => x.Key);
        public void ReadConfig(TextReader reader, Dictionary<Stage, List<StageBlock>> config)
        {
            config.Clear();
            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();
                if (line[0] == '#')
                {
                    continue;
                }
                string[] splits = line.Split(':');
                Stage stage = reverseStageMapping[splits[0]];
                if (!config.ContainsKey(stage))
                {
                    config.Add(stage, new List<StageBlock>());
                }
                try
                {
                    config[stage].Add(StageBlock.FromString(splits[1]));
                }
                catch (FormatException e)
                {
                    this.Logger.LogWarning(e);
                }
            }
        }

    }

}



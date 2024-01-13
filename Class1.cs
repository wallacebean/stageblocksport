using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using LLHandlers;
using GameplayEntities;
using StageBackground;
using LLGUI;
using Abilities;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx.Logging;
using BepInEx.Configuration;
using LLBML;
using LLBML.States;
using LLBML.Utils;
using LLBML.Math;
using LLBML.Players;
using LLBML.Networking;



namespace StageBlocks
{
    [BepInPlugin("us.wallace.plugins.llb.stageBlocksPorted", "StageBlocks", "1.0.3")]
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

            var harmony = new Harmony("us.wallace.plugins.llb.stageBlocksPorted");
            harmony.PatchAll(typeof(WorldSetStageDimensionsPatch));
            harmony.PatchAll(typeof(NitroStuckFix));

            Logger.LogDebug("allUnlockedPorted is loaded");
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

        class NitroStuckFix
        {

            [HarmonyTranspiler]
            [HarmonyPatch(typeof(BallEntity), nameof(BallEntity.UpdateState), new Type[] { })]
            public static IEnumerable<CodeInstruction> UpdateState_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator iL)
            {
                CodeMatcher cm = new CodeMatcher(instructions, iL);

                cm.MatchForward(true, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldloc_3),
                        new CodeMatch(OpCodes.Callvirt),
                        new CodeMatch(OpCodes.Br));


                try
                {
                    cm.Insert(
                        new CodeInstruction(OpCodes.Ldarg_0),
                        Transpilers.EmitDelegate<Action<BallEntity>>((BallEntity __instance) =>
                        {
                            CopPlayer copPlayer = PlayerHandler.instance.GetPlayerEntity(__instance.ballData.cuffBallOwner) as CopPlayer;
                            if (copPlayer.abilityData.abilityState == "CUFF_HOOKSHOT" && HHBCPNCDNDH.HPLPMEAOJPM(HHBCPNCDNDH.NPDCPLFLLIG(copPlayer.moveableData.velocity.GCPKPHMKLBN), HHBCPNCDNDH.NKKIFJJEPOL(0.5m)) && HHBCPNCDNDH.HPLPMEAOJPM(HHBCPNCDNDH.NPDCPLFLLIG(copPlayer.moveableData.velocity.CGJJEHPPOAN), HHBCPNCDNDH.NKKIFJJEPOL(0.5m)))
                            {
                                copPlayer.SetAbilityState("CUFF_NEUTRAL_STAY");
                                copPlayer.PlayAnim("cuffPull", "main");
                                copPlayer.abilityData.specialHeading = Side.NONE;
                                copPlayer.playerData.velocity = IBGCBLLKIHA.DBOMOJGKIFI;
                                copPlayer.GiveBall(BallState.STICK_TO_PLAYER_CUFFED, __instance, false);
                                return;
                            }
                        })
                    );
                }
                catch (Exception e)
                {
                    StageBlocks.Log.LogInfo(e);
                }
                return cm.InstructionEnumeration();

            }
        }

        class WorldSetStageDimensionsPatch
        {
            [HarmonyPatch(typeof(World), nameof(World.SetStageDimensions))]
            [HarmonyPostfix]
            public static void SetStageDimensions_Prefix(World __instance)
            {
                if (StageBlocks.customStageBlocks.Value && !__instance.useStageBlocks)
                {
                    StageBlocks.Instance.LoadConfig();
                    if (StageBlocks.Instance.stageBlocks.ContainsKey(StageHandler.curStage))
                    {
                        foreach (StageBlock stageBlock in StageBlocks.Instance.stageBlocks[StageHandler.curStage])
                        {
                            Rect box = stageBlock.box;
                            GameObject blockGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            blockGameObject.transform.rotation = Quaternion.identity;
                            blockGameObject.transform.position = new Vector3((Floatf)box.center.x * World.FPIXEL_SIZE, (Floatf)box.center.y * World.FPIXEL_SIZE);
                            blockGameObject.transform.localScale = new Vector3((Floatf)box.size.x * World.FPIXEL_SIZE, (Floatf)box.size.y * World.FPIXEL_SIZE, 1f);
                            blockGameObject.GetComponent<Renderer>().material.color = stageBlock.color;
                            __instance.stageBlockList.Add(new Boundsf(new Vector2f(box.center.x, box.center.y) * World.FPIXEL_SIZE, new Vector2f(box.size.x, box.size.y) * World.FPIXEL_SIZE));
                        }
                    }
                }
                return;
            }
        }


        public struct StageBlock
        {
            public Rect box;
            public Color color;

            public StageBlock(Rect block, Color? color = null)
            {
                this.box = block;
                this.color = color ?? Color.clear;
            }
            public static implicit operator StageBlock(Rect a) => new StageBlock(a);

            public override string ToString()
            {
                string res = "";
                res += box.position.x + ", " + box.position.y + ", " + box.size.x + ", " + box.size.y;
                if ((int) color.a == 0)
                {
                    res += " ; " + ColorUtility.ToHtmlStringRGB(color);
                }
                return res;
            }
            public static StageBlock FromString(string data)
            {

                string[] splits = data.Split(';');
                string[] boxStrings = splits[0].Split(',');
                Rect box = new Rect(
                    float.Parse(boxStrings[1]),
                    float.Parse(boxStrings[2]),
                    float.Parse(boxStrings[3]),
                    float.Parse(boxStrings[4])
                );
                if (splits.Length > 1)
                {
                    string htmlColor = splits[1][0] == '#' ? splits[1] : $"#{splits[1]}";
                    if (ColorUtility.TryParseHtmlString(htmlColor, out Color color) == false)
                    {
                        StageBlocks.Log.LogWarning("Failed to load colour. Is it in the right format? e.g. #1a1a22");
                        return new StageBlock(box);
                    }
                    return new StageBlock(box, color);
                }
                return new StageBlock(box);
            }
        }
    }
}



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
using System.Xml;
using System.Xml.Serialization;



namespace StageBlocks
{
    [BepInPlugin("us.wallace.plugins.llb.stageBlocksPorted", "stageBlocksPorted Plug-In", "1.0.1.0")]
    [BepInDependency(LLBML.PluginInfos.PLUGIN_ID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("no.mrgentle.plugins.llb.modmenu", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInProcess("LLBlaze.exe")]

    public class Plugin : BaseUnityPlugin

    {
        public static ManualLogSource Log { get; private set; } = null;

        public static ConfigEntry<bool> custromStageBlocks;

        //outskirts
        public static ConfigEntry<int> outskirtsBlockAmount;
        public static ConfigEntry<int> outskirtsBlockSizeX;
        public static ConfigEntry<int> outskirtsBlockSizeY;
        public static ConfigEntry<int> outskirtsBlockLocationX;
        public static ConfigEntry<int> outskirtsBlockLocationY;
        public static ConfigEntry<int> outskirtsBlockSizeX2;
        public static ConfigEntry<int> outskirtsBlockSizeY2;
        public static ConfigEntry<int> outskirtsBlockLocationX2;
        public static ConfigEntry<int> outskirtsBlockLocationY2;
        public static ConfigEntry<int> outskirtsBlockSizeX3;
        public static ConfigEntry<int> outskirtsBlockSizeY3;
        public static ConfigEntry<int> outskirtsBlockLocationX3;
        public static ConfigEntry<int> outskirtsBlockLocationY3;
        public static ConfigEntry<int> outskirtsBlockSizeX4;
        public static ConfigEntry<int> outskirtsBlockSizeY4;
        public static ConfigEntry<int> outskirtsBlockLocationX4;
        public static ConfigEntry<int> outskirtsBlockLocationY4;
        //sewers
        public static ConfigEntry<int> sewersBlockSizeX;
        public static ConfigEntry<int> sewersBlockSizeY;
        public static ConfigEntry<int> sewersBlockLocationX;
        public static ConfigEntry<int> sewersBlockLocationY;
        public static ConfigEntry<int> sewersBlockSizeX2;
        public static ConfigEntry<int> sewersBlockSizeY2;
        public static ConfigEntry<int> sewersBlockLocationX2;
        public static ConfigEntry<int> sewersBlockLocationY2;
        public static ConfigEntry<int> sewersBlockSizeX3;
        public static ConfigEntry<int> sewersBlockSizeY3;
        public static ConfigEntry<int> sewersBlockLocationX3;
        public static ConfigEntry<int> sewersBlockLocationY3;
        public static ConfigEntry<int> sewersBlockSizeX4;
        public static ConfigEntry<int> sewersBlockSizeY4;
        public static ConfigEntry<int> sewersBlockLocationX4;
        public static ConfigEntry<int> sewersBlockLocationY4;
        //desert
        public static ConfigEntry<int> desertBlockSizeX;
        public static ConfigEntry<int> desertBlockSizeY;
        public static ConfigEntry<int> desertBlockLocationX;
        public static ConfigEntry<int> desertBlockLocationY;
        public static ConfigEntry<int> desertBlockSizeX2;
        public static ConfigEntry<int> desertBlockSizeY2;
        public static ConfigEntry<int> desertBlockLocationX2;
        public static ConfigEntry<int> desertBlockLocationY2;
        public static ConfigEntry<int> desertBlockSizeX3;
        public static ConfigEntry<int> desertBlockSizeY3;
        public static ConfigEntry<int> desertBlockLocationX3;
        public static ConfigEntry<int> desertBlockLocationY3;
        public static ConfigEntry<int> desertBlockSizeX4;
        public static ConfigEntry<int> desertBlockSizeY4;
        public static ConfigEntry<int> desertBlockLocationX4;
        public static ConfigEntry<int> desertBlockLocationY4;
        //elevator
        public static ConfigEntry<int> elevatorBlockSizeX;
        public static ConfigEntry<int> elevatorBlockSizeY;
        public static ConfigEntry<int> elevatorBlockLocationX;
        public static ConfigEntry<int> elevatorBlockLocationY;
        public static ConfigEntry<int> elevatorBlockSizeX2;
        public static ConfigEntry<int> elevatorBlockSizeY2;
        public static ConfigEntry<int> elevatorBlockLocationX2;
        public static ConfigEntry<int> elevatorBlockLocationY2;
        public static ConfigEntry<int> elevatorBlockSizeX3;
        public static ConfigEntry<int> elevatorBlockSizeY3;
        public static ConfigEntry<int> elevatorBlockLocationX3;
        public static ConfigEntry<int> elevatorBlockLocationY3;
        public static ConfigEntry<int> elevatorBlockSizeX4;
        public static ConfigEntry<int> elevatorBlockSizeY4;
        public static ConfigEntry<int> elevatorBlockLocationX4;
        public static ConfigEntry<int> elevatorBlockLocationY4;
        //factory
        public static ConfigEntry<int> factoryBlockSizeX;
        public static ConfigEntry<int> factoryBlockSizeY;
        public static ConfigEntry<int> factoryBlockLocationX;
        public static ConfigEntry<int> factoryBlockLocationY;
        public static ConfigEntry<int> factoryBlockSizeX2;
        public static ConfigEntry<int> factoryBlockSizeY2;
        public static ConfigEntry<int> factoryBlockLocationX2;
        public static ConfigEntry<int> factoryBlockLocationY2;
        public static ConfigEntry<int> factoryBlockSizeX3;
        public static ConfigEntry<int> factoryBlockSizeY3;
        public static ConfigEntry<int> factoryBlockLocationX3;
        public static ConfigEntry<int> factoryBlockLocationY3;
        public static ConfigEntry<int> factoryBlockSizeX4;
        public static ConfigEntry<int> factoryBlockSizeY4;
        public static ConfigEntry<int> factoryBlockLocationX4;
        public static ConfigEntry<int> factoryBlockLocationY4;
        //subway
        public static ConfigEntry<int> subwayBlockSizeX;
        public static ConfigEntry<int> subwayBlockSizeY;
        public static ConfigEntry<int> subwayBlockLocationX;
        public static ConfigEntry<int> subwayBlockLocationY;
        public static ConfigEntry<int> subwayBlockSizeX2;
        public static ConfigEntry<int> subwayBlockSizeY2;
        public static ConfigEntry<int> subwayBlockLocationX2;
        public static ConfigEntry<int> subwayBlockLocationY2;
        public static ConfigEntry<int> subwayBlockSizeX3;
        public static ConfigEntry<int> subwayBlockSizeY3;
        public static ConfigEntry<int> subwayBlockLocationX3;
        public static ConfigEntry<int> subwayBlockLocationY3;
        public static ConfigEntry<int> subwayBlockSizeX4;
        public static ConfigEntry<int> subwayBlockSizeY4;
        public static ConfigEntry<int> subwayBlockLocationX4;
        public static ConfigEntry<int> subwayBlockLocationY4;
        //stadium
        public static ConfigEntry<int> stadiumBlockSizeX;
        public static ConfigEntry<int> stadiumBlockSizeY;
        public static ConfigEntry<int> stadiumBlockLocationX;
        public static ConfigEntry<int> stadiumBlockLocationY;
        public static ConfigEntry<int> stadiumBlockSizeX2;
        public static ConfigEntry<int> stadiumBlockSizeY2;
        public static ConfigEntry<int> stadiumBlockLocationX2;
        public static ConfigEntry<int> stadiumBlockLocationY2;
        public static ConfigEntry<int> stadiumBlockSizeX3;
        public static ConfigEntry<int> stadiumBlockSizeY3;
        public static ConfigEntry<int> stadiumBlockLocationX3;
        public static ConfigEntry<int> stadiumBlockLocationY3;
        public static ConfigEntry<int> stadiumBlockSizeX4;
        public static ConfigEntry<int> stadiumBlockSizeY4;
        public static ConfigEntry<int> stadiumBlockLocationX4;
        public static ConfigEntry<int> stadiumBlockLocationY4;
        //streets
        public static ConfigEntry<int> streetsBlockSizeX;
        public static ConfigEntry<int> streetsBlockSizeY;
        public static ConfigEntry<int> streetsBlockLocationX;
        public static ConfigEntry<int> streetsBlockLocationY;
        public static ConfigEntry<int> streetsBlockSizeX2;
        public static ConfigEntry<int> streetsBlockSizeY2;
        public static ConfigEntry<int> streetsBlockLocationX2;
        public static ConfigEntry<int> streetsBlockLocationY2;
        public static ConfigEntry<int> streetsBlockSizeX3;
        public static ConfigEntry<int> streetsBlockSizeY3;
        public static ConfigEntry<int> streetsBlockLocationX3;
        public static ConfigEntry<int> streetsBlockLocationY3;
        public static ConfigEntry<int> streetsBlockSizeX4;
        public static ConfigEntry<int> streetsBlockSizeY4;
        public static ConfigEntry<int> streetsBlockLocationX4;
        public static ConfigEntry<int> streetsBlockLocationY4;
        //pool
        public static ConfigEntry<int> poolBlockSizeX;
        public static ConfigEntry<int> poolBlockSizeY;
        public static ConfigEntry<int> poolBlockLocationX;
        public static ConfigEntry<int> poolBlockLocationY;
        public static ConfigEntry<int> poolBlockSizeX2;
        public static ConfigEntry<int> poolBlockSizeY2;
        public static ConfigEntry<int> poolBlockLocationX2;
        public static ConfigEntry<int> poolBlockLocationY2;
        public static ConfigEntry<int> poolBlockSizeX3;
        public static ConfigEntry<int> poolBlockSizeY3;
        public static ConfigEntry<int> poolBlockLocationX3;
        public static ConfigEntry<int> poolBlockLocationY3;
        public static ConfigEntry<int> poolBlockSizeX4;
        public static ConfigEntry<int> poolBlockSizeY4;
        public static ConfigEntry<int> poolBlockLocationX4;
        public static ConfigEntry<int> poolBlockLocationY4;
        //room21
        public static ConfigEntry<int> room21BlockSizeX;
        public static ConfigEntry<int> room21BlockSizeY;
        public static ConfigEntry<int> room21BlockLocationX;
        public static ConfigEntry<int> room21BlockLocationY;
        public static ConfigEntry<int> room21BlockSizeX2;
        public static ConfigEntry<int> room21BlockSizeY2;
        public static ConfigEntry<int> room21BlockLocationX2;
        public static ConfigEntry<int> room21BlockLocationY2;
        public static ConfigEntry<int> room21BlockSizeX3;
        public static ConfigEntry<int> room21BlockSizeY3;
        public static ConfigEntry<int> room21BlockLocationX3;
        public static ConfigEntry<int> room21BlockLocationY3;
        public static ConfigEntry<int> room21BlockSizeX4;
        public static ConfigEntry<int> room21BlockSizeY4;
        public static ConfigEntry<int> room21BlockLocationX4;
        public static ConfigEntry<int> room21BlockLocationY4;
        //retro factrory


        void Awake()
        {
            Config.Bind("Use Custom Stage Blocks", "mm_header_qol", "Use Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            custromStageBlocks = Config.Bind<bool>("StageBlocksToggle", "enableCustomStageBlocks", false);

            Config.Bind("gap2", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            outskirtsBlockAmount = Config.Bind("1. Outskirts Values", "Number Of Blocks", 2, new BepInEx.Configuration.ConfigDescription("", new BepInEx.Configuration.AcceptableValueRange<int>(0, 4), new object[0]));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Stage Block 1", new ConfigDescription("", null, "modmenu_header"));
            outskirtsBlockSizeX = Config.Bind("1. Outskirts Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            outskirtsBlockSizeY = Config.Bind("1. Outskirts Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            outskirtsBlockLocationX = Config.Bind("1. Outskirts Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            outskirtsBlockLocationY = Config.Bind("1. Outskirts Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Stage Block 2", new ConfigDescription("", null, "modmenu_header"));
            outskirtsBlockSizeX2 = Config.Bind("1. Outskirts Values", "X Size of Stage Block 2", 250, new ConfigDescription("Controls The Width of a Stage Block 2"));
            outskirtsBlockSizeY2 = Config.Bind("1. Outskirts Values", "Y Size of Stage Block 2", 54, new ConfigDescription("Controls The Width of a Stage Block 2"));
            outskirtsBlockLocationX2 = Config.Bind("1. Outskirts Values", "X Location of Stage Block 2", 500, new ConfigDescription("Controls The Width of a Stage Block 2"));
            outskirtsBlockLocationY2 = Config.Bind("1. Outskirts Values", "Y Location of Stage Block 2", 232, new ConfigDescription("Controls The Width of a Stage Block 2"));

            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Stage Block 3", new ConfigDescription("", null, "modmenu_header"));
            outskirtsBlockSizeX3 = Config.Bind("1. Outskirts Values", "X Size of Stage Block 3", 250, new ConfigDescription("Controls The Width of a Stage Block 3"));
            outskirtsBlockSizeY3 = Config.Bind("1. Outskirts Values", "Y Size of Stage Block 3", 105, new ConfigDescription("Controls The Width of a Stage Block 3"));
            outskirtsBlockLocationX3 = Config.Bind("1. Outskirts Values", "X Location of Stage Block 3", 0, new ConfigDescription("Controls The Width of a Stage Block 3"));
            outskirtsBlockLocationY3 = Config.Bind("1. Outskirts Values", "Y Location of Stage Block 3", 414, new ConfigDescription("Controls The Width of a Stage Block 3"));

            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Stage Block 4", new ConfigDescription("", null, "modmenu_header"));
            outskirtsBlockSizeX4 = Config.Bind("1. Outskirts Values", "X Size of Stage Block 4", 250, new ConfigDescription("Controls The Width of a Stage Block 4"));
            outskirtsBlockSizeY4 = Config.Bind("1. Outskirts Values", "Y Size of Stage Block 4", 54, new ConfigDescription("Controls The Width of a Stage Block 4"));
            outskirtsBlockLocationX4 = Config.Bind("1. Outskirts Values", "X Location of Stage Block 4", 0, new ConfigDescription("Controls The Width of a Stage Block 4"));
            outskirtsBlockLocationY4 = Config.Bind("1. Outskirts Values", "Y Location of Stage Block 4", 0, new ConfigDescription("Controls The Width of a Stage Block 4"));


            Config.Bind("gap3", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Sewers Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            sewersBlockSizeX = Config.Bind("2. Sewers Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            sewersBlockSizeY = Config.Bind("2. Sewers Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            sewersBlockLocationX = Config.Bind("2. Sewers Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            sewersBlockLocationY = Config.Bind("2. Sewers Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap4", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Desert Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            desertBlockSizeX = Config.Bind("3. Desert Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            desertBlockSizeY = Config.Bind("3. Desert Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            desertBlockLocationX = Config.Bind("3. Desert Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            desertBlockLocationY = Config.Bind("3. Desert Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap5", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            elevatorBlockSizeX = Config.Bind("4. Elevator Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            elevatorBlockSizeY = Config.Bind("4. Elevator Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            elevatorBlockLocationX = Config.Bind("4. Elevator Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            elevatorBlockLocationY = Config.Bind("4. Elevator Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap6", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            factoryBlockSizeX = Config.Bind("5. Factory Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            factoryBlockSizeY = Config.Bind("5. Factory Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            factoryBlockLocationX = Config.Bind("5. Factory Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            factoryBlockLocationY = Config.Bind("5. Factory Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap7", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            subwayBlockSizeX = Config.Bind("6. Subway Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            subwayBlockSizeY = Config.Bind("6. Subway Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            subwayBlockLocationX = Config.Bind("6. Subway Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            subwayBlockLocationY = Config.Bind("6. Subway Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap8", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            stadiumBlockSizeX = Config.Bind("7. Stadium Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            stadiumBlockSizeY = Config.Bind("7. Stadium Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            stadiumBlockLocationX = Config.Bind("7. Stadium Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            stadiumBlockLocationY = Config.Bind("7. Stadium Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap9", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            streetsBlockSizeX = Config.Bind("8. Streets Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            streetsBlockSizeY = Config.Bind("8. Streets Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            streetsBlockLocationX = Config.Bind("8. Streets Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            streetsBlockLocationY = Config.Bind("8. Streets Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap10", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            poolBlockSizeX = Config.Bind("9. Pool Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            poolBlockSizeY = Config.Bind("9. Pool Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            poolBlockLocationX = Config.Bind("9. Pool Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            poolBlockLocationY = Config.Bind("9. Pool Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Config.Bind("gap11", "mm_header_gap", 50, new ConfigDescription("", null, "modmenu_gap"));
            Config.Bind("Outskirts Stage Block Values", "mm_header_qol", "Custom Stage Blocks", new ConfigDescription("", null, "modmenu_header"));
            room21BlockSizeX = Config.Bind("10. Room 21 Values", "X Size of Stage Block", 250, new ConfigDescription("Controls The Width of a Stage Block"));
            room21BlockSizeY = Config.Bind("10. Room 21 Values", "Y Size of Stage Block", 54, new ConfigDescription("Controls The Width of a Stage Block"));
            room21BlockLocationX = Config.Bind("10. Room 21 Values", "X Location of Stage Block", -500, new ConfigDescription("Controls The Width of a Stage Block"));
            room21BlockLocationY = Config.Bind("10. Room 21 Values", "Y Location of Stage Block", 232, new ConfigDescription("Controls The Width of a Stage Block"));

            Log = this.Logger;

            Logger.LogDebug("liuberal poop hippie pronoun fortnite be farting pooping bacon epic, i love ivermectin flouride apartmentism");

            var harmony = new Harmony("us.wallace.plugins.llb.stageBlocksPorted");

            {
                harmony.PatchAll(typeof(WorldSetStageDimensionsPatch));
                harmony.PatchAll(typeof(NitroStuckFix));
            }

            Logger.LogDebug("allUnlockedPorted is loaded");
        }

        void Start()
        {
            ModDependenciesUtils.RegisterToModMenu(this.Info);

        }

        void Update()
        {

        }

        class NitroStuckFix
        {

            [HarmonyTranspiler]
            [HarmonyPatch(typeof(BallEntity), nameof(BallEntity.UpdateState), new Type[] { })]
            public static IEnumerable<CodeInstruction> UpdateState_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator iL)
            {
                PatchUtils.LogInstructions(instructions, 350, 370);
                CodeMatcher cm = new CodeMatcher(instructions, iL);

                cm.MatchForward(true, // false = move at the start of the match, true = move at the end of the match
                        new CodeMatch(OpCodes.Ldloc_3),
                        new CodeMatch(OpCodes.Callvirt),
                        new CodeMatch(OpCodes.Br));

                Plugin.Log.LogDebug(cm.Pos);
                PatchUtils.LogInstruction(cm.Instruction);

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
                    Plugin.Log.LogInfo(e);
                }
                PatchUtils.LogInstructions(cm.InstructionEnumeration(), 350, 370);
                Plugin.Log.LogInfo("---");
                return cm.InstructionEnumeration();

            }
        }

        class WorldSetStageDimensionsPatch
        {
            [HarmonyPatch(typeof(World), nameof(World.SetStageDimensions))]
            [HarmonyPostfix]
            public static void SetStageDimensions_Prefix(World __instance)
            {
                global::IBGCBLLKIHA acihfibjnkm = new global::IBGCBLLKIHA(global::StageBackground.BG.instance.stageOffsetInPixels[0], global::StageBackground.BG.instance.stageOffsetInPixels[1]);
                global::IBGCBLLKIHA acihfibjnkm2 = new global::IBGCBLLKIHA(global::StageBackground.BG.instance.stageSizeInPixels[0], global::StageBackground.BG.instance.stageSizeInPixels[1]);
                __instance.stageSize = global::IBGCBLLKIHA.AJOCFFLIIIH(acihfibjnkm2, global::World.FPIXEL_SIZE);
                __instance.stageMin = global::IBGCBLLKIHA.DBOMOJGKIFI;
                __instance.stageMax = __instance.stageSize;
                __instance.stageMin = global::IBGCBLLKIHA.GAFCIOAEGKD(__instance.stageMin, global::IBGCBLLKIHA.AJOCFFLIIIH(acihfibjnkm, global::World.FPIXEL_SIZE));
                __instance.stageMax = global::IBGCBLLKIHA.GAFCIOAEGKD(__instance.stageMax, global::IBGCBLLKIHA.AJOCFFLIIIH(acihfibjnkm, global::World.FPIXEL_SIZE));
                __instance.stageMin.GCPKPHMKLBN = global::HHBCPNCDNDH.FCKBPDNEAOG(__instance.stageMin.GCPKPHMKLBN, global::HHBCPNCDNDH.AJOCFFLIIIH(__instance.stageMax.GCPKPHMKLBN, global::HHBCPNCDNDH.GMEDDLALMGA));
                __instance.stageMax.GCPKPHMKLBN = global::HHBCPNCDNDH.FCKBPDNEAOG(__instance.stageMax.GCPKPHMKLBN, global::HHBCPNCDNDH.AJOCFFLIIIH(__instance.stageMax.GCPKPHMKLBN, global::HHBCPNCDNDH.GMEDDLALMGA));
                __instance.stageBlockList = new global::System.Collections.Generic.List<global::JEPKNLONCHD>();



                if (!(bool)Plugin.custromStageBlocks.Value)
                {
                    foreach (global::StageBackground.StageBlock stageBlock in global::StageBackground.BG.instance.stageBlocksArrangements[0].stageBlocks)
                    {
                        global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                        if (gameObject == null)
                        {
                            gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                        }

                        gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                        gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(stageBlock.centerX), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(stageBlock.centerY), global::World.FPIXEL_SIZE)));
                        gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(stageBlock.sizeX), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(stageBlock.sizeY), global::World.FPIXEL_SIZE)), 1f);
                        __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(stageBlock.centerX, stageBlock.centerY), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(stageBlock.sizeX, stageBlock.sizeY), global::World.FPIXEL_SIZE)));
                    }
                }

                else
                {
                    foreach (global::StageBackground.StageBlock stageBlock in global::StageBackground.BG.instance.stageBlocksArrangements[0].stageBlocks)
                    {

                        if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.OUTSKIRTS)
                        {
                            if (outskirtsBlockAmount.Value >= 1)
                            {
                                global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                                if (gameObject == null)
                                {
                                    gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                                }
                                gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                                gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                                gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                                __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                                if (outskirtsBlockAmount.Value >= 2)
                                {
                                    global::UnityEngine.GameObject gameObject2 = stageBlock.gameObject;
                                    if (gameObject2 == null)
                                    {
                                        gameObject2 = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                                    }
                                    gameObject2.transform.rotation = global::UnityEngine.Quaternion.identity;
                                    gameObject2.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX2.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY2.Value), global::World.FPIXEL_SIZE)));
                                    gameObject2.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX2.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY2.Value), global::World.FPIXEL_SIZE)), 1f);
                                    __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX2.Value, Plugin.outskirtsBlockLocationY2.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX2.Value, Plugin.outskirtsBlockSizeY2.Value), global::World.FPIXEL_SIZE)));
                                    if (outskirtsBlockAmount.Value >= 3)
                                    {
                                        global::UnityEngine.GameObject gameObject3 = stageBlock.gameObject;
                                        if (gameObject3 == null)
                                        {
                                            gameObject3 = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                                        }
                                        gameObject3.transform.rotation = global::UnityEngine.Quaternion.identity;
                                        gameObject3.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX3.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY3.Value), global::World.FPIXEL_SIZE)));
                                        gameObject3.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX3.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY3.Value), global::World.FPIXEL_SIZE)), 1f);
                                        __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX3.Value, Plugin.outskirtsBlockLocationY3.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX3.Value, Plugin.outskirtsBlockSizeY3.Value), global::World.FPIXEL_SIZE)));
                                        if (outskirtsBlockAmount.Value >= 4)
                                        {
                                            global::UnityEngine.GameObject gameObject4 = stageBlock.gameObject;
                                            if (gameObject4 == null)
                                            {
                                                gameObject4 = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                                            }
                                            gameObject4.transform.rotation = global::UnityEngine.Quaternion.identity;
                                            gameObject4.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX4.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY4.Value), global::World.FPIXEL_SIZE)));
                                            gameObject4.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX4.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY4.Value), global::World.FPIXEL_SIZE)), 1f);
                                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX4.Value, Plugin.outskirtsBlockLocationY4.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX4.Value, Plugin.outskirtsBlockSizeY4.Value), global::World.FPIXEL_SIZE)));
                                        }
                                    }
                                }
                            }
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.SEWERS)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.JUNKTOWN)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.CONSTRUCTION)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.FACTORY)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.SUBWAY)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.STADIUM)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.STREETS)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.POOL)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                        else if (LLHandlers.StageHandler.curStage == LLHandlers.Stage.ROOM21)
                        {
                            global::UnityEngine.GameObject gameObject = stageBlock.gameObject;
                            if (gameObject == null)
                            {
                                gameObject = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Cube);
                            }
                            gameObject.transform.rotation = global::UnityEngine.Quaternion.identity;
                            gameObject.transform.position = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE)));
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeX.Value), global::World.FPIXEL_SIZE)), global::HHBCPNCDNDH.GEIMAIJIPKI(global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)), 1f);
                            __instance.stageBlockList.Add(new global::JEPKNLONCHD(global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockLocationX.Value, Plugin.outskirtsBlockLocationY.Value), global::World.FPIXEL_SIZE), global::IBGCBLLKIHA.AJOCFFLIIIH(new global::IBGCBLLKIHA(Plugin.outskirtsBlockSizeX.Value, Plugin.outskirtsBlockSizeY.Value), global::World.FPIXEL_SIZE)));
                        }
                    }
                }

                return;
            }
        }


    }


}



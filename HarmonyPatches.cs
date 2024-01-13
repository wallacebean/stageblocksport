using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using GameplayEntities;
using LLHandlers;
using LLBML.Math;

namespace StageBlocks
{

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


}

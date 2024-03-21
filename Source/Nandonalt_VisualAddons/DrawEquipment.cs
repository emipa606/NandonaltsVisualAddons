using HarmonyLib;
using UnityEngine;
using Verse;

namespace Nandonalt_VisualAddons;

[HarmonyPatch(typeof(PawnRenderUtility))]
[HarmonyPatch("DrawEquipmentAndApparelExtras")]
internal class DrawEquipment
{
    private static void Prefix(Vector3 drawPos, Pawn pawn)
    {
        if (!Nandonalt_VisualAddonsMod.instance.Settings.BilliardsCue)
        {
            return;
        }

        if (pawn == null || pawn.Downed || pawn.Dead || !pawn.Spawned)
        {
            return;
        }

        if (pawn.CurJob == null || pawn.CurJob.def != DefDatabase<JobDef>.GetNamed("Play_Billiards"))
        {
            return;
        }

        if (pawn.Rotation == Rot4.South)
        {
            var drawLoc = drawPos + new Vector3(0f, 0f, -0.22f);
            drawLoc.y += 0.04f;
            DrawEquipmentAiming(drawLoc, 143f);
            return;
        }

        if (pawn.Rotation == Rot4.North)
        {
            var vector = drawPos + new Vector3(0f, 0f, -0.11f);
            vector.y += 0.04f;
            DrawEquipmentAiming(vector, 143f);
            return;
        }

        if (pawn.Rotation == Rot4.East)
        {
            var drawLoc2 = drawPos + new Vector3(0.2f, 0f, -0.22f);
            drawLoc2.y += 0.04f;
            DrawEquipmentAiming(drawLoc2, 143f);
            return;
        }

        if (pawn.Rotation != Rot4.West)
        {
            return;
        }

        var drawLoc3 = drawPos + new Vector3(-0.2f, 0f, -0.22f);
        drawLoc3.y += 0.04f;
        DrawEquipmentAiming(drawLoc3, 217f);
    }

    public static void DrawEquipmentAiming(Vector3 drawLoc, float aimAngle)
    {
        var num = aimAngle - 90f;
        var num2 = -85f;
        Mesh mesh;
        switch (aimAngle)
        {
            case > 20f and < 160f:
                mesh = MeshPool.plane10;
                num += num2;
                break;
            case > 200f and < 340f:
                mesh = MeshPool.plane10Flip;
                num -= 180f;
                num -= num2;
                break;
            default:
                mesh = MeshPool.plane10;
                num += num2;
                break;
        }

        num %= 360f;
        var matSingle = HarmonyPatches.poolCue.MatSingle;
        Graphics.DrawMesh(mesh, drawLoc, Quaternion.AngleAxis(num, Vector3.up), matSingle, 0);
    }
}
﻿// ******************************************************************
//       /\ /|       @file       ThinkNodeConditionalSatisfyTouchAnimal.cs
//       \ V/        @brief      行为树条件节点 尝试满足抚摸动物需求
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-04 00:23:32
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchAnimal;

[UsedImplicitly]
public class ThinkNodeConditionalSatisfyTouchAnimal : ThinkNode_Conditional
{
    /// <summary>
    ///     是否满足条件
    /// </summary>
    /// <param name="pawn"></param>
    /// <returns></returns>
    protected override bool Satisfied(Pawn pawn)
    {
        var touchNeed = pawn.needs?.TryGetNeed<NeedTouchAnimal>();
        if (touchNeed == null)
        {
            return false;
        }

        return touchNeed.CurCategory <= DrugDesireCategory.Desire;
    }
}
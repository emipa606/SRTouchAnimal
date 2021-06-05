﻿// ******************************************************************
//       /\ /|       @file       JobGiverTouchAnimalAddiction.cs
//       \ V/        @brief      行为树action节点 成瘾需求触摸动物
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-05 09:48:06
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using JetBrains.Annotations;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
    [UsedImplicitly]
    public class JobGiverTouchAnimalAddiction : JobGiverTouchAnimal
    {
        protected override float MaxDistanceToTouch => 200f;

        protected override Pawn FindAnimal(Pawn pawn)
        {
            //获取成瘾物种名称
            var kindDefName = GetAddictionPawnKindDefName(pawn);
            //成瘾物种不存在
            if (kindDefName.Equals(string.Empty))
            {
                return null;
            }

            //尝试在附近寻找指定动物
            var currentMap = pawn.Map;
            //最大距离内 最近的 满足驯兽等级的 名单内的(非玩家角色没有此限制) 是成瘾物种的
            Pawn targetAnimal = null;
            //缓存当前物种与角色的距离
            var cacheDistance = -1f;
            foreach (var animal in currentMap.mapPawns.AllPawnsSpawned)
            {
                //不是成瘾物种
                if (!animal.kindDef.defName.Equals(kindDefName))
                {
                    continue;
                }

                var distance = animal.Position.DistanceTo(pawn.Position);
                //距离超过上限
                if (distance > MaxDistanceToTouch)
                {
                    continue;
                }

                //选中设置检测
                if (!CheckSelected(pawn.Faction, animal.kindDef.defName))
                {
                    continue;
                }

                //todo 驯兽等级满足
                //距离没有之前缓存过的近
                if (distance >= cacheDistance && Math.Abs(cacheDistance + 1) >= 0.001f)
                {
                    continue;
                }

                cacheDistance = distance;
                targetAnimal = animal;
            }

            return targetAnimal;
        }

        /// <summary>
        /// 获取引起成瘾的物种定义名称
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        private static string GetAddictionPawnKindDefName(Pawn pawn)
        {
            var hediffs = pawn.health.hediffSet.hediffs;
            foreach (var hdf in hediffs)
            {
                if (hdf is HediffAddictionTouchAnimal hediffAddiction)
                {
                    return hediffAddiction.AddictionKindDefName;
                }
            }

            return string.Empty;
        }
    }
}
﻿// ******************************************************************
//       /\ /|       @file       UIModWindowMain.cs
//       \ V/        @brief      Mod主窗口的View+Controller
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 13:34:51
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using System.Linq;
using HugsLib;
using HugsLib.Settings;
using JetBrains.Annotations;
using UnityEngine;
using Verse;

namespace SR.ModRimWorldTouchAnimal;

[UsedImplicitly]
[StaticConstructorOnStartup]
public class UIModWindowMain : ModBase
{
    private SettingHandle<int> _currentSkillLevelAnimal; //当前驯兽等级面板
    private SettingHandle<string> _handleChanceToJoinList; //加入概率面板
    private UIModelMain _model; //页面数据模型层 都被handle分走了 剩余的数据不多
    private SettingHandle<SettingHandleSelectedAnimalDefs> _settingHandleSelectedAnimalDefs; //选择面板

    public UIModWindowMain()
    {
        Instance = this;
    }

    public static UIModWindowMain Instance { get; private set; } //静态实例

    /// <summary>
    ///     动物种类定义选择数据 不包含未操作过种类
    /// </summary>
    public Dictionary<string, bool> MapPawnKindDefSelectedData
    {
        get
        {
            if (_settingHandleSelectedAnimalDefs.Value != null)
            {
                return _settingHandleSelectedAnimalDefs.Value.MapPawnKindDefSelectedData;
            }

            _settingHandleSelectedAnimalDefs.Value = new SettingHandleSelectedAnimalDefs();
            RefreshSelectedListHeight();

            return _settingHandleSelectedAnimalDefs.Value.MapPawnKindDefSelectedData;
        }
    }

    /// <summary>
    ///     窗口标题
    /// </summary>
    /// <returns></returns>
    public override string ModIdentifier => "TouchAnimal";

    /// <summary>
    ///     定义文件加载后回调
    /// </summary>
    public override void DefsLoaded()
    {
        InitSetting();
    }

    /// <summary>
    ///     初始化设置数据
    /// </summary>
    private void InitSetting()
    {
        _model = new UIModelMain();
        //选择列表
        _settingHandleSelectedAnimalDefs =
            Settings.GetHandle<SettingHandleSelectedAnimalDefs>("settingHandleSelectedAnimalDefs",
                "TitleAnimalsWantToTouch".Translate(), string.Empty);
        //GetHandle<T>会把默认值赋给Value,默认值必须为null，因为重置时默认字典与当前字典公用同一个指针，无法达到重置效果
        //刷新列表绘制高度
        RefreshSelectedListHeight();
        _settingHandleSelectedAnimalDefs.CustomDrawer = DrawAnimalSelectionList;
        //假定驯兽等级
        _currentSkillLevelAnimal = Settings.GetHandle("handleCurrentSkillLevelAnimal",
            "TitleAssumedSkillLevel".Translate(), string.Empty, 12, Validators.IntRangeValidator(1, 20));
        //信息列表
        _handleChanceToJoinList =
            Settings.GetHandle("handleChanceToJoinList", "TitleTouchBehaviorInformation".Translate(),
                string.Empty, string.Empty);
        _handleChanceToJoinList.CustomDrawerHeight = (_model.listAllAnimalDef.Count + 1) * ModDef.RowHeight;
        _handleChanceToJoinList.CustomDrawer = DrawBehaviourTouchInfoList;
    }

    /// <summary>
    ///     绘制触摸信息
    /// </summary>
    /// <param name="rect"></param>
    /// <returns>是否触发序列化</returns>
    private bool DrawBehaviourTouchInfoList(Rect rect)
    {
        //背景
        DrawUtil.DrawBg(rect, Color.white);
        DrawUtil.DrawTouchInfoRow(rect, "LabelAnimalKind".Translate(), "LabelAnimalSkillRequire".Translate(),
            "LabelChanceToJoin".Translate());
        //绘制信息
        foreach (var pawnKindDef in _model.listAllAnimalDef)
        {
            rect.y += ModDef.RowHeight;
            DrawUtil.DrawTouchInfoRow(rect, pawnKindDef.label,
                $"{CalcUtil.CalcRequireSkillLevel(pawnKindDef.RaceProps.baseHealthScale)}",
                $"{CalcUtil.CalcChanceToJoin(_currentSkillLevelAnimal.Value, pawnKindDef.RaceProps.baseHealthScale)}");
        }

        //不需要序列化
        return false;
    }

    /// <summary>
    ///     绘制
    /// </summary>
    /// <param name="rect"></param>
    /// <returns>是否触发序列化</returns>
    private bool DrawAnimalSelectionList(Rect rect)
    {
        //是否需要序列化
        var isNeedToSave = false;
        //背景
        DrawUtil.DrawBg(rect, Color.white);
        //标题
        var leftRect = new Rect(rect) { width = rect.width / 2 };
        DrawUtil.DrawLabel("LabelWillTouch".Translate(), leftRect, ModDef.BilibiliPink, 0, Color.black);
        var rightRect = new Rect(rect) { width = rect.width / 2, x = rect.x + (rect.width / 2) };
        DrawUtil.DrawLabel("LabelWontTouch".Translate(), rightRect, ModDef.BilibiliBlue, 0, Color.black);
        var selectedIndex = 0;
        var unSelectedIndex = 0;
        for (var i = 0; i < _model.listAllAnimalDef.Count; i++)
        {
            var pawnKindDef = _model.listAllAnimalDef[i];
            //存档中不存在当前种类名称
            var isSelected = MapPawnKindDefSelectedData.GetValueOrDefault(pawnKindDef.defName, true);

            //默认选中
            //当前种类在左还是右 顺便索引++
            var currentIndex = isSelected ? ++selectedIndex : ++unSelectedIndex;
            //当前要绘制按钮的位置
            var buttonRect = new Rect(rect)
            {
                y = rect.y + (currentIndex * ModDef.RowHeight),
                x = rect.x + (isSelected ? 0 : rect.width / 2),
                width = rect.width / 2,
                height = ModDef.RowHeight
            };
            //是否点击了按钮
            var interacted = DrawUtil.DrawSelectionButton(buttonRect, pawnKindDef.label,
                isSelected ? ModDef.BilibiliPink : ModDef.BilibiliBlue, i);
            //没有点击
            if (!interacted)
            {
                continue;
            }

            //存档中有当前物种数据 修改
            if (!MapPawnKindDefSelectedData.TryAdd(pawnKindDef.defName, false))
            {
                MapPawnKindDefSelectedData[pawnKindDef.defName] = !MapPawnKindDefSelectedData[pawnKindDef.defName];
                isNeedToSave = true;
                continue;
            }

            //存档中没有当前物种数据 添加 因为默认是true 所以第一次点击后一定是false
            isNeedToSave = true;
        }

        if (!isNeedToSave)
        {
            return false;
        }

        RefreshSelectedListHeight();
        return true;
    }

    /// <summary>
    ///     刷新列表绘制高度
    /// </summary>
    private void RefreshSelectedListHeight()
    {
        var unSelectedCount = MapPawnKindDefSelectedData.Values.Count(isSelected => !isSelected);
        _settingHandleSelectedAnimalDefs.CustomDrawerHeight = _model.GetDrawHeight(unSelectedCount);
    }
}
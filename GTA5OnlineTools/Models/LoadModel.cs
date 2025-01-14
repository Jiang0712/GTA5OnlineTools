﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public class LoadModel : ObservableObject
{
    private string loadState;
    /// <summary>
    /// 程序加载状态
    /// </summary>
    public string LoadState
    {
        get => loadState;
        set => SetProperty(ref loadState, value);
    }

    private Version versionInfo;
    /// <summary>
    /// 程序版本号
    /// </summary>
    public Version VersionInfo
    {
        get => versionInfo;
        set => SetProperty(ref versionInfo, value);
    }

    private DateTime buildDate;
    /// <summary>
    /// 程序最后编译时间
    /// </summary>
    public DateTime BuildDate
    {
        get => buildDate;
        set => SetProperty(ref buildDate, value);
    }

    private bool isInitError;
    /// <summary>
    /// 是否初始化发生错误
    /// </summary>
    public bool IsInitError
    {
        get => isInitError;
        set => SetProperty(ref isInitError, value);
    }
}

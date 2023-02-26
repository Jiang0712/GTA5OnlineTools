﻿using GTA5OnlineTools.Utils;

using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Windows;

/// <summary>
/// KiddionWindow.xaml 的交互逻辑
/// </summary>
public partial class KiddionWindow
{
    public KiddionWindow()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    private void Window_Kiddion_Loaded(object sender, RoutedEventArgs e)
    {
        var isClearScriptDir = IniHelper.ReadValue("Kiddion", "IsClearScriptDir");
        if (!string.IsNullOrEmpty(isClearScriptDir))
            CheckBox_IsClearScriptDir.IsChecked = isClearScriptDir == "1";
    }

    private void Window_Kiddion_Closing(object sender, CancelEventArgs e)
    {
        IniHelper.WriteValue("Kiddion", "IsClearScriptDir", $"{Convert.ToInt32(CheckBox_IsClearScriptDir.IsChecked == true)}");
    }

    /// <summary>
    /// Lua脚本按钮点击
    /// </summary>
    /// <param name="Name"></param>
    [RelayCommand]
    private void LuaScriptClick(string Name)
    {
        try
        {
            AudioHelper.PlayClickSound();

            switch (Name)
            {
                case "LuaScriptDirectory":
                    ProcessHelper.OpenLink(FileHelper.Dir_Kiddion_Scripts);
                    break;
                case "AliceLua":
                    AliceLuaClick();
                    break;
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 是否清空脚本目录
    /// </summary>
    private void ClearScriptDir()
    {
        ProcessHelper.CloseProcess("Kiddion");

        if (CheckBox_IsClearScriptDir.IsChecked == true)
            FileHelper.ClearDirectory(FileHelper.Dir_Kiddion_Scripts);
    }

    /// <summary>
    /// 释放 Alice Lua脚本
    /// </summary>
    private void AliceLuaClick()
    {
        ClearScriptDir();

        var lua = $"{FileHelper.ResFiles}.Kiddion.scripts.AliceLua.zip";
        var file = $"{FileHelper.Dir_Kiddion_Scripts}\\AliceLua.zip";

        FileHelper.ExtractResFile(lua, file);
        FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Scripts_Readme, FileHelper.File_Kiddion_Scripts_Readme);

        using var archive = ZipFile.OpenRead(file);
        archive.ExtractToDirectory(FileHelper.Dir_Kiddion_Scripts);
        archive.Dispose();

        File.Delete(file);
        NotifierHelper.Show(NotifierType.Success, "Alice Lua脚本替换成功，请重新启动Kiddion查看");
    }
}

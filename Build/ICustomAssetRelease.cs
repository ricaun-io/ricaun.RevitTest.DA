using Nuke.Common;
using ricaun.Nuke.Components;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using ricaun.Nuke.Extensions;
using ricaun.Nuke.IO;
using System;

/// <summary>
/// ICustomAssetRelease
/// </summary>
public interface ICustomAssetRelease : IHazRelease, IRelease, INukeBuild, IHazAssetRelease, IHazChangelog
{
    /// <summary>
    /// <see cref="IHazAssetRelease.AssetRelease"/> executes inside <see cref="IGitPreRelease"/> and <see cref="IGitRelease"/> before release.
    /// </summary>
    IAssetRelease IHazAssetRelease.AssetRelease => new AssetRelease(this);
    /// <summary>
    /// AssetsUploadFilter (default: *.zip)
    /// </summary>
    [Parameter] string AssetsUploadFilter => TryGetValue(() => AssetsUploadFilter) ?? "*Console.zip";
    /// <summary>
    /// ASSETS_UPLOAD_ADDRESS
    /// </summary>
    [Parameter] public string AssetsUploadAddress => TryGetValue(() => AssetsUploadAddress);
    /// <summary>
    /// ASSETS_UPLOAD_AUTHORIZATION
    /// </summary>
    [Parameter] public string AssetsUploadAuthorization => TryGetValue(() => AssetsUploadAuthorization);
    public void UploadFile(AbsolutePath file)
    {
        Serilog.Log.Information($"Upload File: {file.Name}");
        if (AssetsUploadAddress.SkipEmpty())
        {
            try
            {
                var result = HttpAuthTasks.HttpPostFile(AssetsUploadAddress, file, AssetsUploadAuthorization);
                ReportSummary(_ => _.AddPair("File", file.Name));
                Serilog.Log.Warning($"Upload File: {file.Name} => {result}");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error($"Upload Error: {file.Name} => {ex}");
            }
            return;
        }

        Serilog.Log.Warning($"Upload File Skipped: AssetsUploadAddress empty.");
    }
}

public class AssetRelease : IAssetRelease
{
    private ICustomAssetRelease custom;

    public AssetRelease(ICustomAssetRelease custom)
    {
        this.custom = custom;
    }
    public void ReleaseAsset(ReleaseAssets releaseAssets)
    {
        foreach (var asset in releaseAssets.Assets)
        {
            custom.UploadFile(asset);
        }
    }
}


param(
    [Parameter(Mandatory = $true)]
    [string]$ManifestPath,

    [Parameter(Mandatory = $true)]
    [string]$OutputPath,

    [Parameter(Mandatory = $true)]
    [string]$DownloadUrl
)

$manifest = Get-Content -LiteralPath $ManifestPath -Raw | ConvertFrom-Json
$lastUpdate = [DateTimeOffset]::UtcNow.ToUnixTimeSeconds()

$entry = [ordered]@{
    Author                 = $manifest.Author
    Name                   = $manifest.Name
    Punchline              = $manifest.Punchline
    Description            = $manifest.Description
    InternalName           = $manifest.InternalName
    AssemblyVersion        = $manifest.AssemblyVersion
    RepoUrl                = $manifest.RepoUrl
    ApplicableVersion      = $manifest.ApplicableVersion
    DalamudApiLevel        = $manifest.DalamudApiLevel
    TestingDalamudApiLevel = $manifest.DalamudApiLevel
    DownloadLinkInstall    = $DownloadUrl
    DownloadLinkUpdate     = $DownloadUrl
    LastUpdate             = $lastUpdate
    IconUrl                = $manifest.IconUrl
    Tags                   = $manifest.Tags
    CategoryTags           = $manifest.CategoryTags
    AcceptsFeedback        = $false
    IsHide                 = $false
    IsTestingExclusive     = $false
}

ConvertTo-Json -InputObject @($entry) -Depth 10 | Set-Content -LiteralPath $OutputPath -Encoding utf8

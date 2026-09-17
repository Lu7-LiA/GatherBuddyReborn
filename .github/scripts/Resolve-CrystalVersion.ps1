param(
    [Parameter(Mandatory = $true)]
    [string]$UpstreamVersion,

    [Parameter(Mandatory = $true)]
    [string]$StatePath,

    [Parameter(Mandatory = $true)]
    [string]$RepositoryManifestPath
)

$parsedUpstreamVersion = [Version]$UpstreamVersion.TrimStart("v")
$upstreamBuild = [Math]::Max(0, $parsedUpstreamVersion.Build)
$upstreamRevision = [Math]::Max(0, $parsedUpstreamVersion.Revision)
$normalizedUpstreamVersion = "{0}.{1}.{2}.{3}" -f `
    $parsedUpstreamVersion.Major, `
    $parsedUpstreamVersion.Minor, `
    $upstreamBuild, `
    $upstreamRevision

$ignoredPaths = @(
    "pluginmaster.json",
    ".github/crystal-version.json",
    "README.md"
)
$sourceTree = @(
    git ls-tree -r HEAD | Where-Object {
        $path = ($_ -split "`t", 2)[1]
        $path -and $path -notin $ignoredPaths -and -not $path.StartsWith("docs/")
    }
)
if ($LASTEXITCODE -ne 0) {
    throw "Failed to read the Git source tree."
}

$sourceBytes = [Text.Encoding]::UTF8.GetBytes(($sourceTree -join "`n"))
$sourceFingerprint = [Convert]::ToHexString(
    [Security.Cryptography.SHA256]::HashData($sourceBytes)
).ToLowerInvariant()

$state = $null
if (Test-Path -LiteralPath $StatePath) {
    $state = Get-Content -LiteralPath $StatePath -Raw | ConvertFrom-Json
}

$crystalRevision = 1
$shouldPublish = $true
$reason = "initial crystal version"

if ($null -ne $state) {
    $previousRevision = [int]$state.CrystalRevision
    if ($state.UpstreamVersion -ne $normalizedUpstreamVersion) {
        $crystalRevision = 1
        $reason = "upstream release changed"
    } elseif ($state.SourceFingerprint -ne $sourceFingerprint) {
        $crystalRevision = $previousRevision + 1
        $reason = "crystal source changed"
    } else {
        $crystalRevision = $previousRevision
        $shouldPublish = $false
        $reason = "source is unchanged"
    }
}

if ($crystalRevision -lt 1 -or $crystalRevision -gt 99) {
    throw "Crystal revision '$crystalRevision' is outside the supported range 1-99."
}

$encodedRevision = ($upstreamRevision * 100) + $crystalRevision
if ($encodedRevision -gt 65535) {
    throw "The encoded assembly revision '$encodedRevision' exceeds the .NET version limit."
}

$releaseVersion = "{0}.{1}.{2}.{3}" -f `
    $parsedUpstreamVersion.Major, `
    $parsedUpstreamVersion.Minor, `
    $upstreamBuild, `
    $encodedRevision
$friendlyVersion = "GBR {0} + Crystal r{1:D2}" -f $normalizedUpstreamVersion, $crystalRevision
$releaseTag = "crystal-v$($releaseVersion.Replace('.', '-'))"

if (-not $shouldPublish -and -not (Test-Path -LiteralPath $RepositoryManifestPath)) {
    $shouldPublish = $true
    $reason = "repository manifest is missing"
}

if (-not $shouldPublish) {
    $repository = Get-Content -LiteralPath $RepositoryManifestPath -Raw | ConvertFrom-Json
    if ($repository.Count -ne 1 -or $repository[0].AssemblyVersion -ne $releaseVersion) {
        $shouldPublish = $true
        $reason = "repository manifest does not match resolved version"
    }
}

[pscustomobject]@{
    UpstreamVersion  = $normalizedUpstreamVersion
    CrystalRevision = $crystalRevision
    ReleaseVersion  = $releaseVersion
    FriendlyVersion = $friendlyVersion
    ReleaseTag      = $releaseTag
    SourceFingerprint = $sourceFingerprint
    ShouldPublish   = $shouldPublish
    Reason          = $reason
}

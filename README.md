# [![](https://raw.githubusercontent.com/FFXIV-CombatReborn/RebornAssets/main/IconAssets/GBR_Icon.png)](https://github.com/FFXIV-CombatReborn/GatherBuddyReborn)

**GatherBuddy Reborn + Crystal**

> [!WARNING]
> **非公式の個人改変版です。** このリポジトリは
> [GatherBuddyReborn](https://github.com/FFXIV-CombatReborn/GatherBuddyReborn) を個人用途向けに改変したものであり、
> オリジナル版・公式配布版ではありません。この改変版に関するサポートを本家の開発者・コミュニティへ求めないでください。
>
> **This is an unofficial personal modification.** It is not an original or official release of
> [GatherBuddyReborn](https://github.com/FFXIV-CombatReborn/GatherBuddyReborn). The upstream maintainers and community
> do not provide support for this modified build. Do not report issues specific to this fork to the upstream project.

![Github Latest Releases](https://img.shields.io/github/downloads/FFXIV-CombatReborn/GatherBuddyReborn/latest/total.svg?style=for-the-badge)
![Github All Releases](https://img.shields.io/github/downloads/FFXIV-CombatReborn/GatherBuddyReborn/total.svg?style=for-the-badge)
![Github License](https://img.shields.io/github/license/FFXIV-CombatReborn/GatherBuddyReborn.svg?label=License&style=for-the-badge)
[![](https://dcbadge.limes.pink/api/server/p54TZMPnC9)](https://discord.gg/p54TZMPnC9)

GatherBuddyReborn is a community-made fork of the original GatherBuddy plugin for Final Fantasy XIV. This tool is designed to enhance your gameplay experience by assisting with all things gathering, now with automated routes via vnavmesh.

## Features

- **AutoGather**: Automated pathing for gathering up to 10 full stacks (999 each) of a resource.
- **Resource Queueing**: Create a list of resources you want and GBR will gather up to 10 stacks of each!
- **Full BTN/MIN Automation**: GBR can find any BTN/MIN item in the world and gather it for you fully automatically, no user input required beyond initial setup

**NOTE**: vnavmesh plugin is *required* for full automation. Please see the links section of this README for more information on vnavmesh.
  
## Installing

This personal fork is distributed through a separate Dalamud custom repository and is not submitted to the official plugin list.
Its custom-repository URL is intentionally not published in this README. The upstream Combat Reborn repository installs the
original build, not this modified build.

## Versioning

Releases show both the upstream GatherBuddyReborn version and this fork's revision, for example:

```text
GBR 7.5.5.3 + Crystal r01
```

The numeric Dalamud assembly version encodes the same information as `7.5.5.301`: upstream revision `3` plus Crystal revision
`01`. The Crystal revision advances only when upstream or fork source content changes; rebuilding identical source does not
create a new version.

## Want to contribute?

- Create a fork
- Make your changes
- Test the changes
- Create a PR and point it to main

## Links

[vnavmesh](https://github.com/awgil/ffxiv_navmesh) Required for automated navigation

## Attribution and Acknowledgements

GatherBuddyReborn and its various functions relies heavily on the original foundations of various individuals whom without their prior works GatherBuddyReborn would not be the utility it is today and in the future. These attributions are NOT implied as endorsements of GatherBuddyReborn. In alphabetical order:

    - All of the contributors to Dalamud and FFXIVLauncher
    - [Artisan](https://github.com/PunishXIV/Artisan): Taurenkey, pksage, Limiana, et al.
    - [GatherBuddy](https://github.com/Ottermandias/GatherBuddy): Ottermandias, et al.
    - [vnavmesh](https://github.com/awgil/ffxiv_navmesh): awgil, xanderscore, et al.

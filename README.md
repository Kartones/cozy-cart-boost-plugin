# Cozy Tool Boost

[BepInEx](https://github.com/BepInEx/BepInEx) v5 plugin for [Cozy Game Restoration](https://store.steampowered.com/app/3581230/Cozy_Game_Restoration/). It makes the cleaning tools stronger and raises the sale price of fully cleaned cartridges. No game file is modified.

Tested with BepInEx version `5.4.23.6`.

## Tool Changes

These are the default values set by the plugin, you can tweak them (see below the **Configure** section).

- Cleaning radius of every tool: `x2.5`
- Dirt a tool absorbs before it is too dirty: `x6`
- UV light reveal duration: `x3`
- Airdrier strength: `x1.5`
- Tweezers strength: `x2`
- Sale price of a 100% cleaned (rank S) cartridge: `x1.75` (the game default is `x1.15`)

## Install

- Download the DLL from the [releases page of this repository](https://github.com/Kartones/cozy-cart-boost-plugin/releases/latest/).
- With the game closed, copy `CozyToolBoost.dll` to `<game-folder>\BepInEx\plugins\`.
- Start the game once. The plugin creates `<game-folder>\BepInEx\config\local.cozygame.toolboost.cfg`.
- Check that `<game-folder>\BepInEx\LogOutput.log` contains `Cozy Tool Boost loaded`.
- Uninstall: delete `CozyToolBoost.dll`.

## Configure

- Edit `<game-folder>\BepInEx\config\local.cozygame.toolboost.cfg`.
- Each value accepts a number from `0.1` to `20`.
- Restart the game to apply the changes.

## Limitations

- The radius covers an area, so `x2.5` radius cleans about `x6.25` pixels per stroke. Tools get dirty from the pixels they clean, so they get dirty faster the bigger the radius is. You can tweak the "dirt absorption" value to compensate.

## Build

- Needs the .NET SDK and the game installed.
- Tests (not included): `cd tests; dotnet test`
- Build: `cd src; dotnet build -c Release`
- Other game location: add `-p:GameDir="<path to Cozy Game Restoration>"` (default `C:\Games\Steam\steamapps\common\Cozy Game Restoration`).
- Output: `src\bin\Release\netstandard2.1\CozyToolBoost.dll`

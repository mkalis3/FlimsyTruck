# Architecture

FlimsyTruck is a Unity mobile driving game. The gameplay controller is organized as a Unity partial class so scene state, gameplay flow, menu actions, rewards, and cloud save logic can be reviewed in focused files.

## Current Runtime Flow

1. Unity loads the scene and starts the `Bike` MonoBehaviour.
2. `BikeStartup` resolves scene objects and initializes saved progress.
3. `BikeGameLoop` runs the frame update and vehicle state changes.
4. UI button callbacks call public methods grouped in `BikeInput`, `BikeLevelFlow`, `BikeMenusAndShop`, and `Purchaser`.
5. `Purchaser` delegates product lookup and wallet updates to small service classes.

## Engineering Direction

- `Bike.cs` keeps component identity and shared scene state.
- Gameplay behavior is grouped by runtime responsibility under `BikeParts`.
- Pure business rules live in small services when they can be tested independently.
- EditMode tests cover services that do not require a loaded scene.

## Main Components

- `Bike.cs` - Unity component identity and shared scene state.
- `BikeStartup` - scene lookup and initialization.
- `BikeGameLoop` - per-frame gameplay state.
- `BikeInput` - mobile controls and movement commands.
- `BikeLevelFlow` - level selection, scoring, replay, and finish flow.
- `BikeMenusAndShop` - menu navigation and inventory purchases.
- `BikeRewards` - free coin timing and network reward helpers.
- `BikeCloudSave` - Google Play saved game integration.
- `BikeSettingsAndCamera` - audio, quality, and camera sizing.
- `Purchaser` - Unity IAP adapter for coin purchases.
- `PurchaseCatalog` - product IDs, coin rewards, and confirmation UI names.
- `CoinWallet` - PlayerPrefs-backed coin balance operations.
- `BetterObjectPool` / `IcosphereObjectPool` - reusable pooling utilities.

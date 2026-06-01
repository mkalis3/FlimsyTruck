# FlimsyTruck

Unity mobile driving game focused on truck handling, level progression, hazards, pickups, and upgrade-style inventory.

The repository includes gameplay scripts, Unity project settings, the package manifest, EditMode tests, and shader code needed to inspect and continue the implementation. Large art and store assets are intentionally kept outside the repository.

## Features

- Side-scrolling driving physics with level-based objectives
- Mine, wind, speed, and no-brake challenge modifiers
- Star scoring and saved level progress
- Coin and power-up inventory with a small tested wallet service
- Camera and resolution helpers for mobile layouts
- Unity IAP wrapper backed by a tested purchase catalog

## Tech Stack

- Unity 2019.x project format
- C#
- Unity UI
- Unity Purchasing
- Google Play Games integration

## Project Structure

```text
Assets/
├── Scripts/
│   ├── Bike.cs
│   ├── Around.cs
│   ├── BetterObjectPool.cs
│   ├── Purchaser.cs
│   ├── BikeParts/
│   ├── Services/
│   └── UI_Other/
├── Tests/
│   └── EditMode/
├── Shaders/
docs/
Packages/
ProjectSettings/
```

## Architecture

The gameplay controller is organized with Unity partial classes. `Bike.cs` contains the component identity and shared state, while focused files under `Assets/Scripts/BikeParts` group gameplay, levels, shop flows, rewards, input, settings, and cloud save code for easier review.

- `PurchaseCatalog` owns product IDs and coin rewards.
- `CoinWallet` owns PlayerPrefs-backed coin balance operations.
- `Purchaser` adapts Unity IAP callbacks to the catalog and wallet.
- `Bike.cs` keeps the Unity component identity and shared scene state.
- `BikeParts/*` groups scene behavior by responsibility.

More detail is in [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md).

## Running Locally

1. Clone the repository.
2. Open the folder in Unity Hub with a compatible Unity 2019 editor.
3. Let Unity restore packages from `Packages/manifest.json`.
4. Open the main scene used by the project.
5. Press Play in the editor.

## Tests and Checks

- Run EditMode tests from Unity Test Runner.
- Run `python scripts/check_project.py` for repository hygiene checks.
- GitHub Actions runs the repository checks on push and pull request.

Manual QA steps are in [`docs/QA.md`](docs/QA.md).

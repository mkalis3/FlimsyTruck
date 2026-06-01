# QA Notes

## Automated Checks

- GitHub Actions validates the Unity project layout.
- The repository check script blocks Unity build output, missing docs, and missing test files.
- EditMode tests cover the purchase catalog and wallet behavior.

## Manual Smoke Test

Run these checks in Unity 2019.2.2f1:

1. Open the project from Unity Hub.
2. Confirm packages restore without errors.
3. Open the main gameplay scene.
4. Press Play.
5. Start a level, accelerate, brake, finish or fail a level, return to the menu.
6. Open the shop, select a coin product, cancel the purchase flow, and verify the UI remains responsive.
7. Change audio and quality settings, exit Play Mode, enter Play Mode again, and verify saved settings load.

## Production Considerations

- Store builds should add server-side receipt validation for consumable purchases.
- Reward timing should keep a local fallback path when the network endpoint is unavailable.
- PlayMode coverage should be added around level completion, replay, and shop flows.

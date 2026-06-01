from pathlib import Path
import sys

ROOT = Path(__file__).resolve().parents[1]

REQUIRED_PATHS = [
    "README.md",
    "LICENSE",
    ".editorconfig",
    ".gitattributes",
    "docs/ARCHITECTURE.md",
    "docs/QA.md",
    "Packages/manifest.json",
    "ProjectSettings/ProjectVersion.txt",
    "Assets/Scripts/Bike.cs",
    "Assets/Scripts/BikeParts/BikeStartup.cs",
    "Assets/Scripts/BikeParts/BikeGameLoop.cs",
    "Assets/Scripts/BikeParts/BikeInput.cs",
    "Assets/Scripts/BikeParts/BikeLevelFlow.cs",
    "Assets/Scripts/BikeParts/BikeMenusAndShop.cs",
    "Assets/Scripts/BikeParts/BikeRewards.cs",
    "Assets/Scripts/BikeParts/BikeCloudSave.cs",
    "Assets/Scripts/BikeParts/BikeSettingsAndCamera.cs",
    "Assets/Scripts/Purchaser.cs",
    "Assets/Scripts/Services/CoinWallet.cs",
    "Assets/Scripts/Services/PurchaseCatalog.cs",
    "Assets/Tests/EditMode/CoinWalletTests.cs",
    "Assets/Tests/EditMode/PurchaseCatalogTests.cs",
]

FORBIDDEN_PATHS = [
    "Library",
    "Temp",
    "Obj",
    "Build",
    "Builds",
    "Logs",
    "UserSettings",
]

LINE_LIMITS = {
    "Assets/Scripts/Bike.cs": 120,
    "Assets/Scripts/BikeParts/BikeStartup.cs": 700,
    "Assets/Scripts/BikeParts/BikeGameLoop.cs": 1700,
    "Assets/Scripts/BikeParts/BikeInput.cs": 250,
    "Assets/Scripts/BikeParts/BikeLevelFlow.cs": 1600,
    "Assets/Scripts/BikeParts/BikeMenusAndShop.cs": 750,
    "Assets/Scripts/BikeParts/BikeRewards.cs": 450,
    "Assets/Scripts/BikeParts/BikeCloudSave.cs": 300,
    "Assets/Scripts/BikeParts/BikeSettingsAndCamera.cs": 300,
}


def fail(message):
    print(message)
    return 1


def has_comment_syntax(text):
    in_string = False
    in_char = False
    in_verbatim = False
    i = 0
    while i < len(text) - 1:
        current = text[i]
        next_char = text[i + 1]

        if in_string:
            if current == "\\":
                i += 2
                continue
            if current == '"':
                in_string = False
        elif in_char:
            if current == "\\":
                i += 2
                continue
            if current == "'":
                in_char = False
        elif in_verbatim:
            if current == '"' and next_char == '"':
                i += 2
                continue
            if current == '"':
                in_verbatim = False
        else:
            if current == "@" and next_char == '"':
                in_verbatim = True
                i += 2
                continue
            if current == '"':
                in_string = True
            elif current == "'":
                in_char = True
            elif current == "/" and next_char in {"/", "*"}:
                return True

        i += 1

    return False


def main():
    for relative_path in REQUIRED_PATHS:
        if not (ROOT / relative_path).exists():
            return fail(f"Missing required path: {relative_path}")

    for relative_path in FORBIDDEN_PATHS:
        if (ROOT / relative_path).exists():
            return fail(f"Generated Unity folder should not be committed: {relative_path}")

    for relative_path, max_lines in LINE_LIMITS.items():
        path = ROOT / relative_path
        line_count = len(path.read_text(encoding="utf-8", errors="ignore").splitlines())
        if line_count > max_lines:
            return fail(f"{relative_path} has {line_count} lines; limit is {max_lines}")

    for relative_path in LINE_LIMITS:
        text = (ROOT / relative_path).read_text(encoding="utf-8", errors="ignore")
        if has_comment_syntax(text):
            return fail(f"Commented-out code or inline comments found in {relative_path}")

    return 0


if __name__ == "__main__":
    sys.exit(main())

# Unity Screenshot

[English](README.md) | [한국어](README_KO.md)

Capture the Unity Game view with **F12**, just like taking a screenshot on Steam.

## Features

- Capture the current Game view as a PNG in Edit Mode or Play Mode
- Use F12 by default
- Rebind the shortcut with Unity's Shortcut Manager
- Choose the screenshot output folder
- Open the output folder from the Unity menu
- No runtime component or scene setup required

## Requirements

- Unity 2021.3 or newer

## Installation

In Unity, open **Window > Package Manager**, click the **+** button, and select
**Add package from git URL...**. Enter:

```text
https://github.com/leesiuuuu/Unity-Screenshot.git
```

## Usage

1. Focus any Unity editor window.
2. Press **F12** in Edit Mode or Play Mode.
3. Find the PNG in the `Screenshots` folder at the root of your Unity project.

You can also capture or open the output folder from **Tools > Unity Screenshot**.

## Settings

Open **Edit > Project Settings > Unity Screenshot** to choose the output folder.

To change F12, open **Edit > Shortcuts**, search for
`Unity Screenshot/Capture Game View`, and assign another shortcut.

## Notes

- Screenshots use the current Game view resolution.
- The tool captures the final rendered Game view, including UI and post-processing.
- The Game view is focused automatically before capture.
- Capture is available in both Edit Mode and Play Mode.

## License

[MIT](LICENSE.md)

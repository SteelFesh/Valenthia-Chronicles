/*---------------------------------------------------------------------------------------------
 *  Copyright (c) SteelFesh. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using UnityEditor;
using UnityEngine;

public class MaximizeGameViewShortcutMenu
{
    [MenuItem("Tools/Toggle Game View")]
    private static void ToggleGameViewMaximization()
    {
        var gameViewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        if (gameViewType == null)
        {
            Debug.LogError("Error: Unable to retrieve the 'UnityEditor.GameView' type. Please ensure the context is valid.");
            return;
        }

        var gameView = EditorWindow.GetWindow(gameViewType);
        if (gameView == null)
        {
            Debug.LogError("Error: Unable to get the Game View window. Please ensure that the Game View window is open.");
            return;
        }

        gameView.maximized = !gameView.maximized;
    }
}

/*---------------------------------------------------------------------------------------------
 *  Copyright (c) SteelFesh. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using UnityEditor;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class VSCodeContextMenu
{
    // Adds a context menu item when right-clicking on a MonoBehaviour script or any asset
    [MenuItem("Assets/Open in VSCode", false, 1000)]
    private static void OpenInVSCode()
    {
        // Get the selected object in the project view
        var selectedObject = Selection.activeObject;

        // Check if we have something selected
        if (selectedObject == null)
        {
            Debug.LogError("Error: No file selected. Please select a script or file to open in VSCode.");
            return;
        }

        // Get the path to the selected script or asset
        string assetPath = AssetDatabase.GetAssetPath(selectedObject);

        // Ensure the selected asset is a valid file (not a folder)
        if (string.IsNullOrEmpty(assetPath) || !assetPath.EndsWith(".cs"))
        {
            Debug.LogError("Error: The selected file is not a C# script. Please select a valid '.cs' file.");
            return;
        }

        // Absolute path to the file on the disk
        string fullPath = System.IO.Path.GetFullPath(assetPath);

        try
        {
            // Path to VSCode on your machine (update this path if necessary)
            string vscodePath = "/Applications/Visual Studio Code.app/Contents/Resources/app/bin/code";

            // Start VSCode with the selected file
            Process.Start(vscodePath, $"\"{fullPath}\"");

            // Log a success message
            Debug.Log($"Opening file in VSCode: {fullPath}");
        }
        catch (System.Exception ex)
        {
            // Log any errors that occur
            Debug.LogError($"Error: Failed to open the file in VSCode. Details: {ex.Message}");
        }
    }

    // Validation function to only show the context menu for supported file types
    [MenuItem("Assets/Open in VSCode", true)]
    private static bool ValidateOpenInVSCode()
    {
        // Check if the selected object is a valid C# script
        var selectedObject = Selection.activeObject;
        if (selectedObject == null) return false;

        string assetPath = AssetDatabase.GetAssetPath(selectedObject);
        return !string.IsNullOrEmpty(assetPath) && assetPath.EndsWith(".cs");
    }
}

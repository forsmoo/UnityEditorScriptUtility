using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;

namespace ScriptGenerator
{
    public class PathUtility
    {
        static string EditorPath = "Editor";
        
        public static string CreateEditorScriptPath(MonoScript script)
        {
            string path = AssetDatabase.GetAssetPath(script);
            string scriptFolder = Path.GetDirectoryName(path);
            string editorScriptFolder = scriptFolder + Path.AltDirectorySeparatorChar + EditorPath;
           
            if (!Directory.Exists(editorScriptFolder) )
            {
                AssetDatabase.CreateFolder(scriptFolder, EditorPath);
            }

            return editorScriptFolder;
        }

        public static bool PathIsEditorFolder(string path)
        {
            string[] dirs = path.Split(Path.DirectorySeparatorChar);
            string[] altDirs = path.Split(Path.AltDirectorySeparatorChar);
            bool match = dirs.Concat(altDirs).Where(x => x == EditorPath).Any();
            return match;
        }

        public static bool ValidateScriptPath(string newPath)
        {
            if (string.IsNullOrEmpty(newPath))
            {
                Debug.LogError("Path is null or empty");
                return false;
            }

            string ext = Path.GetExtension(newPath);
            if (ext != ".cs")
            {
                Debug.LogError("File extension is not .cs");
                return false;
            }

            return true;
        }
    }

}
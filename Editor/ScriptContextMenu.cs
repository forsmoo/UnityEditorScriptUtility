using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using ScriptGenerator;
using System.IO;
using Microsoft.CSharp;

public class ScriptContextMenu : MonoBehaviour {

    [MenuItem("Assets/Create/CSharp Class")]
    private static void CreateClassScript()
    {
        string path = ExtractPath();
        string newPath = EditorUtility.SaveFilePanelInProject("New CSharp Class", "MyClass.cs", "cs", "Save", path);

        if (!PathUtility.ValidateScriptPath(newPath))
            return;

        path = Path.GetDirectoryName(newPath);
        string className = Path.GetFileNameWithoutExtension(newPath);
        Debug.Log("Creating " + className + " at path " + newPath);
        IScriptGenerator generator = new SimpleClassStubGenerator(className);
        ScriptSaver.GenerateScript(path, className + ".cs", generator);
    }

    [MenuItem("Assets/Create/EditorScript", true)]
    private static bool ValidateCreateEditorScript()
    {
        return PathUtility.PathIsEditorFolder(ExtractPath());
    }

    [MenuItem("Assets/Create/EditorScript")]
    private static void CreateEditorScript()
    {
        string path = ExtractPath();
        string newPath = EditorUtility.SaveFilePanelInProject("New Editor Class", "EditorClass.cs", "cs", "Save", path);
        if( !PathUtility.PathIsEditorFolder(newPath))
        {
            Debug.LogError("Path has to be in Editor folder");
            return;
        }

        if (!PathUtility.ValidateScriptPath(newPath))
            return;
        
        path = Path.GetDirectoryName(newPath);
        string className = Path.GetFileNameWithoutExtension(newPath);
        Debug.Log("Creating "+ className +" at path " + newPath);
        IScriptGenerator generator = new SimpleEditorStubGenerator(className);
        ScriptSaver.GenerateScript(path,className+".cs", generator);
    }
    
    [MenuItem("Assets/Create/EditorWindowScript", true)]
    private static bool ValidateCreateEditorWindowScript()
    {
        return PathUtility.PathIsEditorFolder(ExtractPath());
    }

    [MenuItem("Assets/Create/EditorWindowScript")]
    private static void CreateEditorWindowScript()
    {
        string path = ExtractPath();
        CreateEditorWindowWizard.Open(path);
    }
    
    [MenuItem("Assets/Create/Custom Inspector", true)]
    private static bool CreateInspectorInProject()
    {
        if (EditorApplication.isCompiling)
        {
            return false;
        }
        if (Selection.activeObject == null)
            return false;
        return Selection.activeObject.GetType() == typeof(MonoScript);
    }

    [MenuItem("Assets/Create/Custom Inspector")]
    private static void CreateInspectorProject(MenuCommand menuCommand)
    {
        var script = Selection.activeObject as MonoScript;
        Create(script);
    }

    private static void Create(MonoScript script)
    {
        if (EditorApplication.isCompiling)
        {
            Debug.LogError("Busy compiling");
            return;
        }
        Type type = script.GetClass();
        if( type == null )
        {
            Debug.LogError(script.name + " is not a valid MonoBehvaiour. (Make sure the class name match the filename)");
            return;
        }
        string folderPath = PathUtility.CreateEditorScriptPath(script);
        string filename = type.Name + "Editor.cs";
        IScriptGenerator generator = new SimpleInspectorGenerator(type.Name,type.Namespace);
        ScriptSaver.GenerateScript(folderPath,filename, generator);
    }

    private static string ExtractPath()
    {
        string path = "Assets" + Path.AltDirectorySeparatorChar;
        if (Selection.activeObject != null)
        {
            path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (Selection.activeObject.GetType() != typeof(UnityEditor.DefaultAsset))
            {
                path = Path.GetDirectoryName(path);
            }
        }

        path += Path.AltDirectorySeparatorChar;
        return path;
    }
}


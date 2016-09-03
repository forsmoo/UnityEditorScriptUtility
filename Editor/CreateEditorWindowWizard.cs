using UnityEngine;
using UnityEditor;
using ScriptGenerator;
using System;

public class CreateEditorWindowWizard : EditorWindow
{
    static WindowStubSettings settings = new WindowStubSettings();
    string message;
    string path = "";
    static Vector2 windowSize = new Vector2(400, 200);
    static Vector2 windowPos= new Vector2(300, 200);
    

    public static void Open(string path)
    {
        //CreateEditorWindowWizard window = (CreateEditorWindowWizard)EditorWindow.GetWindow(typeof(CreateEditorWindowWizard));
        var window = EditorWindow.GetWindowWithRect<CreateEditorWindowWizard>(new Rect(windowPos,windowSize),true,"Create Window Class",true);
        window.Construct(path);
        window.Show();
    }

    public void Construct(string path)
    {
        this.path = path;
    }
    
    void Generate()
    {
        IScriptGenerator generator = new SimpleWindowStubGenerator(settings);
        ScriptSaver.GenerateScript(path,settings.ClassName+".cs", generator);
    }

    void OnGUI()
    {
        GUILayout.Label("Window Class Settings", EditorStyles.boldLabel);
        settings.ClassName = EditorGUILayout.TextField("Class Name", settings.ClassName);
        settings.Title = EditorGUILayout.TextField("Window Title", settings.Title);
        settings.Utility = EditorGUILayout.Toggle("Utility window", settings.Utility);
        EditorGUILayout.LabelField(path);
        settings.AddContextMenu = EditorGUILayout.Toggle("Add Menu Item", settings.AddContextMenu);
        if (settings.AddContextMenu)
        {
            settings.MenuPath = EditorGUILayout.TextField("MenuPath", settings.MenuPath);
        }

        if( !String.IsNullOrEmpty(message) )
            GUILayout.Label(message);

        bool valid = true;
        if (settings.AddContextMenu)
        {
            valid = false;
            if (string.IsNullOrEmpty(settings.MenuPath))
                GUILayout.Label("* MenuPath can not be null");
            else if (!settings.MenuPath.Contains("/"))
                GUILayout.Label("* MenuPath must be in a submenu");
            else
                valid = true;
        }

        if (string.IsNullOrEmpty(settings.ClassName))
        {
            valid = false;
            GUILayout.Label("* Enter valid Class Name");
        }
        
        if (valid && GUILayout.Button("Generate"))
        {
            try
            {
                message = "";
                Generate();
                Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                message = e.Message;
            }
        }
    }
}

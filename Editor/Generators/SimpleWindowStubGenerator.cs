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
    public class WindowStubSettings
    {
        public bool AddContextMenu { get; set; }
        public string MenuPath { get; set; }
        public string ClassName { get; set; }
        public bool Utility { get; set; }
        public string Title { get; set; }
    }

    public class SimpleWindowStubGenerator : IScriptGenerator
    {
        WindowStubSettings settings;
        public SimpleWindowStubGenerator(WindowStubSettings settings)
        {
            this.settings = settings;
        }

        public bool Generate(StreamWriter writer)
        {
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using UnityEditor;");
            writer.WriteLine();
            writer.WriteLine("public class "+ settings.ClassName+ " : EditorWindow");
            writer.WriteLine("{");
            if (settings.AddContextMenu)
            {
                writer.WriteLine("\t[MenuItem(\"" + settings.MenuPath + "\")]");
            }
            writer.WriteLine("\tstatic void Open()");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\tvar win = EditorWindow.GetWindow<" + settings.ClassName + ">("+settings.Utility.ToString().ToLower()+",\""+settings.Title+"\");");
            writer.WriteLine("\t\twin.Show();");
            writer.WriteLine("\t}");
            
            writer.WriteLine();
            writer.WriteLine("\tprivate void OnGUI()");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\tGUILayout.Label(\"Window text\");");
            writer.WriteLine("\t}");
            writer.WriteLine("}");
            
            return true;
        }
    }

}
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

    public class SimpleInspectorGenerator : IScriptGenerator
    {
        string className;
        public SimpleInspectorGenerator(string className)
        {
            this.className = className;
        }

        public bool Generate(StreamWriter writer)
        {

            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using UnityEditor;");
            writer.WriteLine();
            writer.WriteLine("[CustomEditor(typeof("+className+"))]");
            writer.WriteLine("public class " + className + "Editor : Editor");
            writer.WriteLine("{");

            writer.WriteLine("\tpublic override void OnInspectorGUI()");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\tbase.OnInspectorGUI();");
            writer.WriteLine("\t\tvar instance = target as " + className+";");
            writer.WriteLine("\t}");
            writer.WriteLine("}");
            
            return true;
        }
    }

}
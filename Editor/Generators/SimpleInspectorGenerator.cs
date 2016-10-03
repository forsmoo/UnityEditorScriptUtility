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
        readonly string className;
        readonly string classNamespace;

        public SimpleInspectorGenerator(string className, string classNamespace)
        {
            this.className = className;
            this.classNamespace = classNamespace;
        }

        public bool Generate(StreamWriter writer)
        {

            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using UnityEditor;");
            writer.WriteLine();

            string prefix = "";
            if (!string.IsNullOrEmpty(classNamespace))
            {
                prefix = "\t";
                writer.WriteLine("namespace " + classNamespace);
                writer.WriteLine("{");
            }

            writer.WriteLine(prefix + "[CustomEditor(typeof(" + className + "))]");
            writer.WriteLine(prefix + "public class " + className + "Editor : Editor");
            writer.WriteLine(prefix + "{");

            writer.WriteLine(prefix + "\tpublic override void OnInspectorGUI()");
            writer.WriteLine(prefix + "\t{");
            writer.WriteLine(prefix + "\t\tbase.OnInspectorGUI();");
            writer.WriteLine(prefix + "\t\tvar instance = target as " + className + ";");
            writer.WriteLine(prefix + "\t}");
            writer.WriteLine(prefix + "}");

            if (!string.IsNullOrEmpty(classNamespace))
                writer.WriteLine("}");
            return true;
        }
    }

}
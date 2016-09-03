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

    public class SimpleClassStubGenerator : IScriptGenerator
    {
        string className;

        public SimpleClassStubGenerator(string name)
        {
            this.className = name;
        }

        public bool Generate(StreamWriter writer)
        {
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using System.Collections;");
            writer.WriteLine();
            writer.WriteLine("public class " + className);
            writer.WriteLine("{");
            writer.WriteLine("\tpublic " + className + "()");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\t");
            writer.WriteLine("\t}");
            writer.WriteLine("}");

            return true;
        }
    }

}
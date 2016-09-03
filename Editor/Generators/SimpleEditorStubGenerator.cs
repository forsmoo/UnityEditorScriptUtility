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

    public class SimpleEditorStubGenerator : IScriptGenerator
    {
        string className;

        public SimpleEditorStubGenerator(string name)
        {
            this.className = name;
        }

        public bool Generate(StreamWriter writer)
        {
            string code = @"
using UnityEngine;
using UnityEditor;

public class ClassName
{
    public ClassName()
    {
    }
}";
            code = code.Replace("ClassName",className);
            writer.WriteLine(code);
            return true;
        }
    }

}
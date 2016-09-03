using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;
using System.Text;

namespace ScriptGenerator
{
    public interface IScriptGenerator
    {
        bool Generate(StreamWriter writer);
    }
    
}
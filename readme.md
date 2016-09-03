#UnityEditorScriptUtility
##Simple Editor extensions for creating Editor classes

The package adds four new commands to the project view context menu to speed up workflow. 
I tend to create a lot of custom inspectors when prototyping code and these tools will create the boilerplate code for you.  

#Custom inspector
Right click a MonoBehvaiour in the project view and navigate to **[MonoScript]->Create->Custom Inspector**

This is the code it generates for a MonoBehaviour called GameBehvaiour.
*This command is disabled while the solution is compiling*


```CSharp
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameBehaviour))]
public class GameBehaviourEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var instance = target as GameBehaviour;
	}
}
```

#EditorWindowScript
Enter some settings for the window and click generate
#EditorScript
A simple EditorScript with using UnityEditor namespace
#CSharp Class
A simple CSharp class

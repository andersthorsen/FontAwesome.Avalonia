using CodeGeneration.Roslyn;
using System;
using System.Diagnostics;

namespace FontAwesome.Generate
{

    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    [CodeGenerationAttribute(typeof(IconDatabaseGenerator))]
    [Conditional("CodeGeneration")]

    public class IconDatabaseGenerateAttribute : Attribute
    {
    }
}

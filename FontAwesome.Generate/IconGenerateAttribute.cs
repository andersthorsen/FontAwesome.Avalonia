using CodeGeneration.Roslyn;
using System;
using System.Diagnostics;
using Validation;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
[CodeGenerationAttribute(typeof(IconDBGenerator))]
[Conditional("CodeGeneration")]
public class IconGenerateAttribute : Attribute
{ 
}
using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading;
using System.Threading.Tasks;
using Validation;
namespace FontAwesome.Generate
{
    public class IconDatabaseGenerator : ICodeGenerator
    {

        public IconDatabaseGenerator(AttributeData attributeData)
        {
            Requires.NotNull(attributeData, nameof(attributeData));
        }
        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();

            var src = @" 
                ///<summary>
                ///	FontAwesome v4.7.0 by Dave Gandy (@davegandy)
                ///	The iconic font and CSS toolkit
                ///	License http://fontawesome.io/license (Font: v4.7.0, C#: MIT License)
                ///</summary>
                ///<see href=""http://fontawesome.io"" />
                ///<seealso href=""https://github.com/FortAwesome/Font-Awesome"" />
                ///<seealso href=""https://github.com/jmacato/FontAwesome.Avalonia"" />
                public enum FontAwesomeIcon {
                    
                    ///<summary>Set this value to show no icon.</summary>
                    None = 0x0,

                    ///<summary>Glass (created: 1.0)</summary>
                    ///<see href=""http://fontawesome.io/icon/glass/"" />
                    [Description(""Glass""),IconId(""glass""),IconCategory(""Web Application Icons"")]
                    Glass = 0xf000,
                }            
            ";

            var tree = SyntaxFactory.ParseSyntaxTree(src);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var mem = (EnumDeclarationSyntax)root.Members[0];

          //  Return our modified copy. It will be added to the user's project for compilation.
            results = results.Add(mem);
            return Task.FromResult<SyntaxList<MemberDeclarationSyntax>>(results);
        }
    }
}
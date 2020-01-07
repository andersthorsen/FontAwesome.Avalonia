using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Text;
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
            var fa = new FAConfigYamlParser("../Font-Awesome/metadata/icons.yml", "../Font-Awesome/metadata/categories.yml");
            var baseUrl = String.Format("{0}/{1}/{{0}}/", "http://fontawesome.io", "icon");

            var items = "";

            foreach (var item in fa.Items)
            {
                if (!(item.Value.Styles.Contains("regular") || item.Value.Styles.Contains("solid")))
                    continue;

                items += $"///<summary>{item.Value.Label}</summary>\n";
                items += $"///<see href=\"{String.Format(baseUrl, item.Key)}\" />\n";

                var sb = new StringBuilder();

                sb.AppendFormat("Description(\"{0}\"),", item.Value.Label);
                sb.AppendFormat("IconId(\"{0}\"),", item.Key);

                if (item.Value.Categories != null && item.Value.Categories.Count > 0)
                {
                    foreach (var cat in item.Value.Categories)
                    {
                        sb.AppendFormat("IconCategory(\"{0}\"),", cat);
                    }
                }

                sb.Remove(sb.Length - 1, 1);
                items += $"[{sb.ToString()}]\n";
                items += $"{item.Value.Safe(item.Key)} = 0x{item.Value.Unicode},\n";

                /*
                if (item.Aliases != null)
                {
                    foreach (var alias in item.Aliases)
                    {
                        var safeAlias = item.Safe(alias);
                        if (String.Equals(safeAlias, item.SafeName, StringComparison.InvariantCultureIgnoreCase)) continue;
                        items += $"///<summary>Alias of: {item.SafeName}</summary>\n";
                        items += $"///<see cref=\"F:FontAwesome.Avalonia.FontAwesomeIcon.{item.SafeName}\" />\n";
                        items += $"[IconAlias]\n";
                        items += $"{safeAlias} = {item.SafeName},\n";
                    }
                }*/
            }

            var src = $@" 
                ///<summary>
                ///	FontAwesome 5.x by Dave Gandy (davegandy)
                ///	The iconic font and CSS toolkit
                ///</summary>
                ///<see href=""http://fontawesome.io"" />
                ///<seealso href=""https://github.com/FortAwesome/Font-Awesome/"" />
                ///<seealso href=""https://github.com/jmacato/FontAwesome.Avalonia"" />
                public enum FontAwesomeIcon {{
                    
                    ///<summary>Set this value to show no icon.</summary>
                    None = 0x0,

                    {items}
                }}            
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
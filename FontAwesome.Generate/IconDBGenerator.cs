using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading;
using System.Threading.Tasks;
using Validation;
using FontAwesome.Generate;

public class IconDBGenerator : ICodeGenerator
{
    public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
    {
        try
        { 
            return GenerateAsyncInternal(context);
        }
        catch (Exception ex)
        {
            progress?.Report(Diagnostic.Create("GEN001", "FontAwesome.Generate", ex.Message, DiagnosticSeverity.Error, DiagnosticSeverity.Error, true, 0));
            throw;
        }
    }

    private Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsyncInternal(TransformationContext context)
    {

        var results = SyntaxFactory.List<MemberDeclarationSyntax>();

        // Our generator is applied to any class that our attribute is applied to.
        var applyToClass = (ClassDeclarationSyntax)context.ProcessingMember;

        // Apply a suffix to the name of a copy of the class.
        var copy = applyToClass
            .WithIdentifier(SyntaxFactory.Identifier(applyToClass.Identifier.ValueText + "_AutoGen"));

        // Return our modified copy. It will be added to the user's project for compilation.
        results = results.Add(copy);
        return Task.FromResult<SyntaxList<MemberDeclarationSyntax>>(results);
    }
}
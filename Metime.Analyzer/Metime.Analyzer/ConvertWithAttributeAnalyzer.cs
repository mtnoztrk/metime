using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Metime.Analyzer
{
    public static class InjectionRules
    {
        public static readonly DiagnosticDescriptor InjectionMustBeAttribute =
            new DiagnosticDescriptor(
                    id: "MT1001",
                    title: "Attribute usage error",
                    messageFormat: "Used type must implement ICanGetOffset",
                    category: "Metime.Design",
                    defaultSeverity: DiagnosticSeverity.Error,
                    isEnabledByDefault: true);
    }

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConvertWithAttributeAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(InjectionRules.InjectionMustBeAttribute);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(AnalyzeAttribute, SyntaxKind.Attribute);
        }

        private static void AnalyzeAttribute(SyntaxNodeAnalysisContext context)
        {
            //var attr = context.ContainingSymbol.GetAttributes().FirstOrDefault(a => a.ApplicationSyntaxReference.Span == context.Node.Span);

            //var location = context.Node.GetLocation();

            //if (attr.AttributeConstructor == null)
            //    return;

            //if (attr.ConstructorArguments[0].Value is INamedTypeSymbol arg)
            //{
            //    if (arg.TypeKind == TypeKind.Error)
            //        return;

            //    if (!arg.GetAttributes().Any(a => a.AttributeClass.ToDisplayString() == "ICanGetOffset"))
            //        context.ReportDiagnostic(Diagnostic.Create(InjectionRules.InjectionMustBeAttribute, location, arg.Name));
            //}
        }
    }
}

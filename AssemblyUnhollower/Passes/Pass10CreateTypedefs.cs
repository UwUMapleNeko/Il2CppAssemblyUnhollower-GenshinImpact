using AssemblyUnhollower.Contexts;
using AssemblyUnhollower.Extensions;
using Mono.Cecil;

namespace AssemblyUnhollower.Passes
{
    public static class Pass10CreateTypedefs
    {
        public static void DoPass(RewriteGlobalContext context)
        {
            foreach (var assemblyContext in context.Assemblies)
            {
                foreach (var type in assemblyContext.OriginalAssembly.MainModule.Types)
                    ProcessType(type, assemblyContext, null);
            }
        }

        private static void ProcessType(TypeDefinition type, AssemblyRewriteContext assemblyContext, TypeDefinition? parentType)
        {
            var convertedTypeName = GetConvertedTypeName(assemblyContext.GlobalContext, type, parentType);
            var newType = new TypeDefinition(convertedTypeName.Namespace ?? type.Namespace.UnSystemify(assemblyContext.GlobalContext.Options), convertedTypeName.Name, AdjustAttributes(type.Attributes));

            if (type.IsSealed && type.IsAbstract) // is static
            {
                newType.IsSealed = newType.IsAbstract = true;
            }
            
            if(parentType == null)
                assemblyContext.NewAssembly.MainModule.Types.Add(newType);
            else
            {
                parentType.NestedTypes.Add(newType);
                newType.DeclaringType = parentType;
            }
            
            foreach (var typeNestedType in type.NestedTypes) 
                ProcessType(typeNestedType, assemblyContext, newType);

            assemblyContext.RegisterTypeRewrite(new TypeRewriteContext(assemblyContext, type, newType));
        }

        internal static (string? Namespace, string Name) GetConvertedTypeName(RewriteGlobalContext assemblyContextGlobalContext, TypeDefinition type, TypeDefinition? enclosingType)
        {
            if (assemblyContextGlobalContext.Options.PassthroughNames)
                return (null, type.Name);

            if (type.Name.IsObfuscated(assemblyContextGlobalContext.Options))
            {
                var newNameBase = assemblyContextGlobalContext.RenamedTypes[type];
                var genericParametersCount = type.GenericParameters.Count;
                var renameGroup =
                    assemblyContextGlobalContext.RenameGroups[((object) type.DeclaringType ?? type.Namespace, newNameBase, genericParametersCount)];
                var genericSuffix = genericParametersCount == 0 ? "" : "`" + genericParametersCount;
                var convertedTypeName = newNameBase + (renameGroup.Count == 1 ? "Unique" : renameGroup.IndexOf(type).ToString()) + genericSuffix;

                var fullName = enclosingType == null
                    ? type.Namespace
                    : (enclosingType.GetNamespacePrefix() + "." + enclosingType.Name);

                if (assemblyContextGlobalContext.Options.RenameMap.TryGetValue(fullName + "." + convertedTypeName, out var newName))
                {
                    var lastDotPosition = newName.LastIndexOf(".");
                    if (lastDotPosition >= 0)
                    {
                        var ns = newName.Substring(0, lastDotPosition);
                        var name = newName.Substring(lastDotPosition + 1);
                        return (ns, name);
                    } else 
                        convertedTypeName = newName;
                }

                if (assemblyContextGlobalContext.Options.RenameMap.TryGetValue(type.Name, out var newName2)) {
                    var lastSlashPosition = newName2.LastIndexOf("/");
                    if (lastSlashPosition >= 0) {
                        var name = newName2.Substring(lastSlashPosition + 1);
                        return (null, name);
                    }

                    var lastDotPosition = newName2.LastIndexOf(".");
                    if (lastDotPosition >= 0) {
                        var ns = newName2.Substring(0, lastDotPosition);
                        var name = newName2.Substring(lastDotPosition + 1);
                        name = name.Replace(ns, "");
                        return (ns, name);
                    }
                    else
                        convertedTypeName = newName2;
                }

                return (null, convertedTypeName);
            }

            if (type.Name.IsInvalidInSource())
                return (null, type.Name.FilterInvalidInSourceChars());

            return (null, type.Name);
        }

        private static TypeAttributes AdjustAttributes(TypeAttributes typeAttributes)
        {
            typeAttributes |= TypeAttributes.BeforeFieldInit;
            typeAttributes &= ~(TypeAttributes.Abstract | TypeAttributes.Interface);
            
            var visibility = typeAttributes & TypeAttributes.VisibilityMask;
            if (visibility == 0 || visibility == TypeAttributes.Public)
                return typeAttributes | TypeAttributes.Public;

            return typeAttributes & ~(TypeAttributes.VisibilityMask) | TypeAttributes.NestedPublic;
        }
    }
}
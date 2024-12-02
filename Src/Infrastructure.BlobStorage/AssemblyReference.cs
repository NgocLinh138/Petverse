using System.Reflection;

namespace Infrastructure.BlobStorage;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}


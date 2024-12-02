using System.Reflection;

namespace Infrastructure.PayOS;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}


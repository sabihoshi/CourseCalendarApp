using System;

namespace UVACanvasAccess.Util
{
    /// <summary>
    ///     Indicates that the return value of this method is paginated, and must be accumulated or otherwise handled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class PaginatedResponse : Attribute { }
}
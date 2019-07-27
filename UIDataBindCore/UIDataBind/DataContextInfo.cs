using System;
using System.Reflection;
using UIDataBindCore.Attributes;

namespace UIDataBindCore
{
    public struct DataContextInfo
    {
        /// <summary>
        /// Marks if a context is inherited <see cref="IInitializable"/> interface
        /// </summary>
        public bool IsInitializable;

        /// <summary>
        /// Name of a context
        /// </summary>
        public string Name;

        /// <summary>
        /// <see cref="IDataContext"/> members that can be bound: all members with <see cref="BindAttribute"/> on them.
        /// </summary>
        public MemberInfo[] Members;

        /// <summary>
        /// An ID associated with this type
        /// </summary>
        public Guid Guid;
    }
}
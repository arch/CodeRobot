// Copyright (c) yingtingxu(徐应庭). All rights reserved

namespace Arch.CodeRobot
{
    /// <summary>
    /// Defines the interface for code reviewer.
    /// </summary>
    public interface ICodeReviewer
    {
        /// <summary>
        /// Ensures using whether order system first.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        bool EnsureUsingAreOrderedSystemFirst();
        /// <summary>
        /// Ensures source code doesn't contains tabs.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        bool EnsureSourceCodeDoesNotContainTabs();
        /// <summary>
        /// Ensures interfaces should be prefixed with capital I.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        bool EnsureInterfacesShouldBePrefixedWithI();
        /// <summary>
        /// Ensures #region are not allowed.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        bool EnsureRegionAreNotAllowed();
    }
}

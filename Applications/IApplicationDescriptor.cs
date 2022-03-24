using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace MiliOS.Applications;

/// <summary>
/// Set of values used to describe the application to the system and the user.
/// </summary>
public interface IApplicationDescriptor
{
    public bool CanInstall();
    public void Install();

    public Type Describes { get; }

    public LocalizedText Name { get; }
    public Asset<Texture2D> Icon { get; }

    /// <summary><c>true</c> to have this application display in the AppBrowser application; otherwise <c>false</c>.</summary>
    public bool InAppBrowser { get; }

    /// <summary><c>true</c> if multiple instances of this application can be launched; otherwise <c>false</c>.</summary>
    public bool SingleInstance { get; }
    
    /// <summary>
    /// The index in which to add the application in the repository. A value of <c>0</c> means to simply add it at the end while anything else will result in a specific position.
    /// This position is not guaranteed if two or more applications share the same AppIndex.
    /// </summary>
    public int AppIndex { get; }
}
using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;
using WebmilioCommons;

namespace MiliOS.Applications;

/// <inheritdoc cref="IApplicationDescriptor"/>
[Skip]
public class ApplicationDescriptor : IApplicationDescriptor
{
    public static readonly Asset<Texture2D> DefaultIcon =
        MiliOS.Instance.Assets.Request<Texture2D>("Applications/Default/icon");

    private readonly string _nameKey;

    /// <summary></summary>
    /// <param name="describes"></param>
    /// <param name="nameKey"></param>
    /// <param name="asset">Passing a null asset will default to the "no icon" icon. Icon must be 64x64.</param>
    public ApplicationDescriptor(Type describes, string nameKey, Asset<Texture2D> asset)
    {
        Describes = describes;

        _nameKey = nameKey;
        asset ??= DefaultIcon;

        Icon = asset;
    }

    public virtual bool CanInstall() => true;
    public virtual void Install() { }

    public Type Describes { get; }

    public virtual LocalizedText Name => Language.GetText(_nameKey);
    public Asset<Texture2D> Icon { get; set; }

    public virtual bool InAppBrowser { get; init; } = true;
    public virtual bool SingleInstance { get; init; } = true;

    public virtual int AppIndex { get; init; }
}

[Skip]
public class ApplicationDescriptor<T> : ApplicationDescriptor where T : IApplication
{
    public ApplicationDescriptor(string nameKey, Asset<Texture2D> asset) : base(typeof(T), nameKey, asset) { }
}
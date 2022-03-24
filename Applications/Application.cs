using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace MiliOS.Applications;

public abstract class Application : IApplication
{
    public abstract bool Launch(Explorer.Explorer explorer, Player caller, object[] args);

    public virtual bool Exit()
    {
        State = 0;
        return true;
    }

    public virtual void Minimize() { }
    public virtual void Maximize() { }

    public virtual bool NetSync(Player player) => false;

    public virtual void PostUpdateEverything() { }
    public virtual void PostUpdateInput() { }

    public virtual void PreUpdateEntities() { }

    public virtual void UpdateUI(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }

    public int State { get; protected set; }
    public bool Maximized { get; set; } = true;

    public Mod Mod { get; set; }
}
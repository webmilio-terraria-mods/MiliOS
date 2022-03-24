using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using WebmilioCommons.Commons;

namespace MiliOS.Applications;

public interface IApplication : IModLinked
{
    public bool Launch(Explorer.Explorer explorer, Player caller, object[] args);
    public bool Exit();

    public void Minimize();
    public void Maximize();

    public bool NetSync(Player player);

    public void PostUpdateEverything();
    public void PostUpdateInput();

    public void PreUpdateEntities();

    public void UpdateUI(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch) { }

    /// <summary>
    /// The state of the application. A value of zero means not running/OK.
    /// Any number greater than 0 means running/OK.
    /// A number in the negative means not running/ERROR.
    /// Any value that falls under 'not-running' will result in any associated UIState to be removed from Explorer.
    /// </summary>
    public int State { get; }

    public bool Maximized { get; set; }
}
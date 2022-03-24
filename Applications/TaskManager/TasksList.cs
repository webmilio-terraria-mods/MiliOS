using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using WebmilioCommons;
using WebmilioCommons.Extensions;
using WebmilioCommons.UI;

namespace MiliOS.Applications.TaskManager;

public class TasksList : UIList
{
    private readonly Explorer.Explorer _explorer;
    private int _updateTimer;

    public TasksList(Explorer.Explorer explorer)
    {
        _explorer = explorer;

        SetPadding(10);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (_updateTimer == 0)
        {
            BuildTaskList(_explorer.RunningApplications);
        }

        _updateTimer = (_updateTimer + 1) % (Constants.TicksPerSecond * 3);
    }

    private void BuildTaskList(IEnumerable<IApplication> applications)
    {
        Clear();

        applications.Do(delegate(IApplication application)
        {
            var descriptor = _explorer.Descriptors.Get(application);
            var dimensions = FontAssets.MouseText.Value.MeasureString(descriptor.Name.Value);

            UIPanel container = new()
            {
                Width = new(0, .99f),
                Height = new(dimensions.Y, 0),

                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };
            container.SetPadding(0);
            Add(container);

            container.Append(new UIText(descriptor.Name)
            {
                Width = new(dimensions.X, 0),
                Height = new(dimensions.Y, 0),

                Left = new(10, 0),
                
                TextOriginY = .5f,
                VAlign = .5f
            });

            container.Append(new KillButton(_explorer, application)
            {
                Height = StyleDimension.Fill,

                HAlign = 1
            });
        });
    }
}
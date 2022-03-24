using WebmilioCommons.DependencyInjection;
using WebmilioCommons.Loaders;

namespace MiliOS.Applications;

[Service]
public class DescriptorsLoader : PrototypeLoader<IApplicationDescriptor>
{
    private readonly SimpleServices _services;

    public DescriptorsLoader(SimpleServices services)
    {
        _services = services;

        Load();
    }

    public IApplicationDescriptor Get(IApplication application)
    {
        var type = application.GetType();

        foreach (var descriptor in Generics)
            if (descriptor.Describes == type)
                return descriptor;

        return null;
    }
}
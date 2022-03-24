using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebmilioCommons.DependencyInjection;
using WebmilioCommons.Extensions;

namespace MiliOS.Applications;

[Service]
public class ApplicationsRepository
{
    private List<IApplicationDescriptor> _descriptors = new();

    public ApplicationsRepository()
    {
        Installed = _descriptors.AsReadOnly();
    }

    public void Install(IEnumerable<IApplicationDescriptor> applications) => applications.Do(app => Install(app));
    public bool Install(IApplicationDescriptor descriptor)
    {
        if (!descriptor.CanInstall())
            return false;

        if (descriptor.AppIndex == 0)
            _descriptors.Add(descriptor);
        else
        {
            if (_descriptors.Count == 0)
                _descriptors.Add(descriptor);
            else
                for (int i = 0; i < _descriptors.Count && i > -1; i++)
                {
                    if (_descriptors[i].AppIndex > descriptor.AppIndex)
                    {
                        _descriptors.Insert(i, descriptor);
                        i = -2;
                    }
                }
        }

        descriptor.Install();
        return true;
    }

    public void Clear()
    {
        _descriptors.Clear();
    }

    public void For(Action<IApplicationDescriptor> action) => _descriptors.Do(action);

    public ReadOnlyCollection<IApplicationDescriptor> Installed { get; }
}
using System.Collections.Generic;
using Utills.domain;

namespace Utills.observer;

public interface IObserver
{
    void schimbareMeciuri(IEnumerable<MeciL> meciuri);
}

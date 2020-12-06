using System.Collections;
using System.Threading.Tasks;

namespace TicketReSail.Core.Interface
{
    public interface ILocalizationService
    {
        IEnumerable GetLocalization();
        Task AddLocalization();
    }
}

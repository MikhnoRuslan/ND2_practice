using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly TicketsContext _context;

        private readonly List<Localization> _localizations = new List<Localization>
        {
            new Localization{ Name = "ru" },
            new Localization{ Name = "ru-BY"},
            new Localization{ Name = "en"}
        };


        public LocalizationService(TicketsContext context)
        {
            _context = context;
        }

        public  IEnumerable GetLocalization()
        {
            return  _context.Localizations.ToArray();
        }

        public async Task AddLocalization()
        {
            if (! _context.Localizations.Any())
            {
                await _context.Localizations.AddRangeAsync(_localizations);
            }

            await _context.SaveChangesAsync();
        }
    }
}

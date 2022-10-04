using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Utenti;
using PhotoSi_IT_Demo_New.Services.Utenti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Utenti
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UtenteModel, UtenteEntity>()
                .ReverseMap();
        }       
    }
}

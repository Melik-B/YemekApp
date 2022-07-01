using AppCore.Business.Services.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services.Bases
{
    public class IMalzemeService : IService<MalzemeModel, Malzeme, YemekAppContext>
    {
        Task<List<MalzemeModel>> MalzemeleriGetirAsync();
    }
}

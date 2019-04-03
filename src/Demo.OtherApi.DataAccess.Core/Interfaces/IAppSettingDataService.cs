using Demo.OtherApi.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.OtherApi.DataAccess.Core.Interfaces
{
    public interface IAppSettingDataService
    {
        Task SaveChanges();
        Task Add(AppSetting entity);
        Task Update(AppSetting entity);
        Task<AppSetting> GetById(string id);
        Task<List<AppSetting>> GetAll();
    }
}

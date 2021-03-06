using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.Common.ViewModel;
using WebScraper.DataAccess.Interface;
using WebScraper.DataAccess.Model;
using WebScraper.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.BusinessLogic
{
    public class ProvinsiFacade
    {
        #region Important
        private ljgbContext db;
        private IProvinsi dep;

        public ProvinsiFacade()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection").ToString();

            var optionsBuilder = new DbContextOptionsBuilder<ljgbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            db = new ljgbContext(optionsBuilder.Options);
            this.dep = new ProvinsiRepository(db);
        }
        #endregion
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<ProvinsiResponse> GetAll(string search, string order, string orderDir, int startRec, int pageSize, int draw)
        {
            var models = await dep.GetAll(search, order, orderDir, startRec, pageSize, draw);
            if (models == null)
            {
                return null;
            }
            return models;
        }

        //public async Task<ProvinsiResponse> GetAllWithoutFilter()
        //{
        //    var models = await dep.GetAll();
        //    if (models == null)
        //    {
        //        return null;
        //    }
        //    return models;
        //}
        
        public async Task<List<Dropdown>> GetAllForDropdown()
        {
            List<Provinsi> ListProvinsi = await dep.GetAllForDropdown();
            List<Dropdown> ListDropdown = ListProvinsi.Select(x => new Dropdown() { ID = x.Id, Text = x.Name }).ToList();
            if (ListDropdown == null)
            {
                return null;
            }
            return ListDropdown;
        }        


        public async Task<ProvinsiResponse> GetPost(ProvinsiRequest req)
        {
            var model = await dep.GetPost(req);

            if (model == null)
            {
                return null;
            }
            return model;

        }

        public async Task<ProvinsiResponse> AddPost(ProvinsiRequest req)
        {
            ProvinsiResponse response = new ProvinsiResponse();
            long result = 0;
            try
            {
                Provinsi getModel = await dep.GetByName(req.Name);
                if (getModel == null)
                {
                    Provinsi provinsi = new Provinsi();
                    provinsi.Name = req.Name;
                    provinsi.Description = req.Description;
                    provinsi.Created = DateTime.Now;
                    provinsi.CreatedBy = req.UserName;
                    provinsi.RowStatus = true;
                    result = await dep.AddPost(provinsi);

                    if (result > 0)
                    {
                        response.Message = "Data Already Saved";
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.Message = "Save data failed";
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    response.Message = "Duplicate with Existing Data";
                    response.IsSuccess = false;
                }
              

            }
            catch (Exception ex)
            {
                log.Error(ex);
                response.Message = ex.ToString();
                response.IsSuccess = false;
            }

            return response;
        }

        public async Task<ProvinsiResponse> DeletePost(ProvinsiRequest req)
        {

            return await dep.DeletePost(req);

        }

        //public async Task<ProvinsiResponse> GetByName(ProvinsiRequest req)
        //{
        //    ProvinsiResponse response = new ProvinsiResponse();
        //    return response;
        //}

        public async Task<ProvinsiResponse> UpdatePost(ProvinsiRequest req)
        {

            ProvinsiResponse response = new ProvinsiResponse();
            long result = 0;
            try
            {

                Provinsi getModel = await dep.GetByName(req.Name);
                if (getModel == null)
                {
                    Provinsi provinsi = await dep.GetPostByID(req.ID);
                    provinsi.Name = req.Name;
                    provinsi.Description = req.Description;
                    provinsi.Modified = DateTime.Now;
                    provinsi.ModifiedBy = req.UserName;

                    result = await dep.UpdatePost(provinsi);

                    if (result > 0)
                    {
                        response.Message = "Data Already Saved";
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.Message = "Save data failed";
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (req.ID == getModel.Id)
                    {
                        Provinsi provinsi = await dep.GetPostByID(req.ID);
                        provinsi.Name = req.Name;
                        provinsi.Description = req.Description;
                        provinsi.Modified = DateTime.Now;
                        provinsi.ModifiedBy = req.UserName;

                        result = await dep.UpdatePost(provinsi);

                        if (result > 0)
                        {
                            response.Message = "Data Already Saved";
                            response.IsSuccess = true;
                        }
                        else
                        {
                            response.Message = "Save data failed";
                            response.IsSuccess = false;
                        }
                    }
                    else
                    {
                        response.Message = "Duplicate With Existing Data";
                        response.IsSuccess = false;
                    }
                }
                

            }
            catch (Exception ex)
            {
                log.Error(ex);
                response.Message = ex.ToString();
                response.IsSuccess = false;
            }

            return response;
        }
    }
}

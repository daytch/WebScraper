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
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.BusinessLogic
{
    public class MerkFacade
    {
        #region Important
        private ljgbContext db;
        private IMerk dep;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MerkFacade()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection").ToString();

            var optionsBuilder = new DbContextOptionsBuilder<ljgbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            db = new ljgbContext(optionsBuilder.Options);
            this.dep = new MerkRepository(db);
        }
        #endregion


        public async Task<MerkResponse> GetAll(string search, string order, string orderDir, int startRec, int pageSize, int draw)
        {
            var models = await dep.GetAll(search, order, orderDir, startRec, pageSize, draw);
            if (models == null)
            {
                return null;
            }
            return models;
        }

        public async Task<MerkResponse> GetAllWithoutFilter()
        {
            return await dep.GetAllWithoutFilter();
        }

        public async Task<MerkResponse> GetPost(long ID)
        {
            var model = await dep.GetPost(ID);

            if (model == null)
            {
                return null;
            }
            return model;

        }

        public async Task<MerkResponse> AddPost(MerkRequest model)
        {
            MerkResponse response = new MerkResponse();
            try
            {

                if (await dep.GetMerkByName(model.Name) != null)
                {
                    response.Message = "Data is Duplicate with Existing Data";
                    response.IsSuccess = false;
                }
                else
                {
                    response = await dep.AddPost(model);
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

        public async Task<MerkResponse> DeletePost(long ID, string username)
        {

            return await dep.DeletePost(ID, username);
           
        }

        public async Task<MerkResponse> UpdatePost(MerkRequest model)
        {
            MerkResponse response = new MerkResponse();
            try
            {
                Merk item = await dep.GetMerkByName(model.Name);
                if (item != null)
                {
                    if (item.Id == model.ID)
                    {
                        response = await dep.UpdatePost(model);
                    }
                    else
                    {
                        response.Message = "Data is Duplicate with Existing Data";
                        response.IsSuccess = false;
                    }
                    
                }
                else
                {
                    response = await dep.UpdatePost(model);
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

        public async Task<MerkResponse> UpdateMerkRank(MerkRequest model, string username)
        {
            MerkResponse response = new MerkResponse();
            try
            {
                MerkRank item = await dep.GetMerkRankByMerkID(model.ID);
                if (item != null)
                {
                    if (item.Id == model.ID)
                    {
                        item.MerkId = model.ID;
                        item.Rank = model.MerkRank;
                        item.Modified = DateTime.Now;
                        item.ModifiedBy = username;
                        item.RowStatus = true;

                        if (await dep.UpdateMerkRank(item) > 0)
                        {
                            response.Message = "Data Already Save!";
                            response.IsSuccess = true;
                        }
                        else
                        {
                            response.Message = "There is something wrong in our system";
                        }
                    }
                    else
                    {
                        response.Message = "Data is Duplicate with Existing Data";
                        response.IsSuccess = false;
                    }

                }
                else
                {
                    MerkRank merkRank = new MerkRank();
                    merkRank.MerkId = model.ID;
                    merkRank.Rank = model.MerkRank;
                    merkRank.Created = DateTime.Now;
                    merkRank.CreatedBy = username;
                    merkRank.RowStatus = true;
                    if (await dep.AddMerkRank(merkRank) > 0)
                    {
                        response.Message = "Data Already Save!";
                        response.IsSuccess = true;
                    } else{
                        response.Message = "There is something wrong in our system";
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
        public MerkResponse GetMerkByKotaID(long KotaID)
        {
            MerkResponse response = new MerkResponse();
            try
            {
                response.ListSP_MerkByKotaID = dep.GetMerkByKotaID(KotaID);
                response.IsSuccess = true;
                response.Message = "Success Get Merk";
            }
            catch (Exception ex)
            {
                log.Error(ex);
                response.IsSuccess = false;
                response.Message = "Something Error with System";
            }
           

            return response;
        }

        public MerkResponse GetMerkRank(string search, int draw, int startRec, int pageSize)
        {
            MerkResponse response = new MerkResponse();
            try
            {
                response.ListSP_MerkRank = dep.GetMerkRank(search, draw, startRec, pageSize).Result;
                response.IsSuccess = true;
                response.Message = "Succes";
                response.recordsTotal = response.ListSP_MerkRank[0].Total;
            }
            catch (Exception ex)
            {

                log.Error(ex);
                response.IsSuccess = false;
                response.Message = "Something Error wtih System";
            }
            return response;
        }
    }
}

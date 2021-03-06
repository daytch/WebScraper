using WebScraper.Common.ViewModel;
using WebScraper.DataAccess.Interface;
using WebScraper.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebScraper.Common.Responses;
using WebScraper.Common.Requests;
using System.Data.SqlClient;

namespace WebScraper.DataAccess.Repository
{
    public class BarangRepository : IBarang
    {
        ljgbContext db;

        public BarangRepository(ljgbContext _db)
        {
            db = _db;
        }

        public async Task<List<Barang>> GetAllBarang()
        {
            List<Barang> Result = new List<Barang>();
            if (db != null)
            {
                Result = await db.Barang.Where(x => x.RowStatus == true).ToListAsync();
            }
            return Result;
        }

        public async Task<long> AddPost(Barang model)
        {
            if (db != null)
            {
                await db.Barang.AddAsync(model);
                await db.SaveChangesAsync();

                return model.Id;
            }

            return 0;
        }

        public async Task<long> DeletePost(long ID, string username)
        {

            int result = 0;

            if (db != null)
            {
                //Find the warna for specific warna id
                Barang barang = await db.Barang.FirstOrDefaultAsync(x => x.Id == ID);

                if (barang != null)
                {
                    barang.Modified = DateTime.Now;
                    barang.ModifiedBy = username;
                    barang.RowStatus = false;
                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public List<Car> GetLowestAsk(string kota, int total)
        {
            if (db != null)
            {
                try
                {
                    return db.Set<Car>().FromSqlRaw("EXEC sp_GetLowestAsk {0}, {1}", kota, total).AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return null;
        }

        public List<Car> GetRelatedProducts(int id)
        {
            if (db != null)
            {
                try
                {
                    return db.Set<Car>().FromSqlRaw("EXEC sp_GetRelatedProductByID {0}", id).AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return null;
        }

        public async Task<Position> GetAskPosition(int id, int nominal)
        {
            if (db != null)
            {
                try
                {
                    return await db.Set<Position>().FromSqlRaw("EXEC sp_GetAskPosition {0}, {1}", id, nominal).FirstOrDefaultAsync();//.AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return null;
        }

        public async Task<Position> GetBidPosition(int id, int nominal)
        {
            if (db != null)
            {
                try
                {
                    return await db.Set<Position>().FromSqlRaw("EXEC sp_GetBidPosition {0}, {1}", id, nominal).FirstOrDefaultAsync();//.AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return null;
        }

        public List<Car> GetListNormal(string kota, int total)
        {
            if (db != null)
            {
                try
                {
                    return db.Set<Car>().FromSqlRaw("EXEC sp_GetListNormal {0}, {1}", kota, total).AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return null;
        }

        public List<Car> GetHighestBid(string kota, int total)
        {

            if (db != null)
            {
                try
                {
                    return db.Set<Car>().FromSqlRaw("EXEC sp_GetHighestBid {0}, {1}", kota, total).AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return null;
        }

        public List<CarAsks> GetAllAsksById(BarangRequest req)
        {
            if (db != null)
            {
                try
                {
                    int result = 0;
                    return db.Set<CarAsks>().FromSqlRaw("EXEC sp_GetAllAsksByID_With_Paging {0},{1},{2},{3},{4},{5},{6}",
                        req.ID, req.start, req.limit, null, null, req.max, result).AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return null;
        }

        public async Task<CarDetail> GetBarangDetail(int id)
        {

            if (db != null)
            {
                try
                {
                    return await db.Set<CarDetail>().FromSqlRaw("EXEC sp_GetDetailBarang {0}", id).AsNoTracking().FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return null;
        }

        public async Task<BarangResponse> GetAll(string search, string order, string orderDir, int startRec, int pageSize, int draw)
        {
            BarangResponse response = new BarangResponse();
            try
            {
                if (db != null)
                {
                    var query = (from brg in db.Barang
                                 join warna in db.Warna
                                 on brg.WarnaId equals warna.Id
                                 join type in db.TypeBarang
                                 on brg.TypeBarangId equals type.Id
                                 join model in db.ModelBarang
                                 on type.ModelBarangId equals model.Id
                                 join merk in db.Merk
                                 on model.MerkId equals merk.Id
                                 join kota in db.Kota
                                 on brg.KotaId equals kota.Id
                                 where merk.RowStatus == true
                                 && model.RowStatus == true
                                 select new
                                 {
                                     brg.Id,
                                     brg.Name,
                                     brg.HargaOtr,
                                     namaType = type.Name,
                                     namaModelBarang = model.Name,
                                     NamaMerk = merk.Name,
                                     brg.TypeBarangId,
                                     type.ModelBarangId,
                                     model.MerkId,
                                     brg.WarnaId,
                                     namaWarna = warna.Name,
                                     brg.Created,
                                     brg.CreatedBy,
                                     brg.Modified,
                                     brg.ModifiedBy,
                                     brg.RowStatus,
                                     brg.PhotoPath,
                                     kotaID = kota.Id,
                                     brg.Year

                                 }
                           );

                    int totalRecords = query.Count();
                    if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                    {
                        query = query.Where(p => p.Name.ToString().ToLower().Contains(search.ToLower()) ||
                                            p.namaType.ToString().ToLower().Contains(search.ToLower()) ||
                                            p.namaModelBarang.ToString().ToLower().Contains(search.ToLower()) ||
                                            p.namaWarna.ToString().ToLower().Contains(search.ToLower()) ||
                                            p.NamaMerk.ToLower().Contains(search.ToLower()) ||
                                            p.HargaOtr.ToString().ToLower().Contains(search.ToLower()) ||
                                            p.Year.ToString().ToLower().Contains(search.ToLower())
                                            );
                    }
                    int recFilter = query.Count();
                    response.ListModel = await (from model in query
                                                where model.RowStatus == true
                                                select new BarangViewModel
                                                {
                                                    Id = model.Id,
                                                    Name = model.Name,
                                                    HargaOtr = model.HargaOtr,
                                                    NamaTypeBarang = model.namaType,
                                                    TypeBarangId = model.TypeBarangId,
                                                    NamaModelBarang = model.namaModelBarang,
                                                    ModelBarangID = model.ModelBarangId,
                                                    NamaMerk = model.NamaMerk,
                                                    MerkBarangID = model.MerkId,
                                                    NamaWarna = model.namaWarna,
                                                    PhotoPath = model.PhotoPath,
                                                    WarnaId = model.WarnaId,
                                                    Created = model.Created,
                                                    CreatedBy = model.CreatedBy,
                                                    Modified = model.Modified,
                                                    ModifiedBy = model.ModifiedBy,
                                                    RowStatus = model.RowStatus,
                                                    KotaID = model.kotaID,
                                                    Year = model.Year
                                                }).Skip(startRec).Take(pageSize).ToListAsync();
                    response.draw = Convert.ToInt32(draw);
                    response.recordsTotal = totalRecords;
                    response.recordsFiltered = recFilter;
                    response.Message = "Load Success";
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "Opps, Something Error with System Righ Now !";
                    response.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {

                response.Message = ex.ToString();
                response.IsSuccess = false;
            }



            return response;
        }

        public async Task<Barang> GetBarang(long ID)
        {
            if (db!=null)
            {
                try
                {
                    return await db.Barang.Where(x => x.RowStatus == true).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        public async Task<bool> UpdatePost(Barang model)
        {
            try
            {
                if (db != null)
                {
                    Barang barang = await db.Barang.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                    if (barang != null)
                    {
                        barang.Name = model.Name;
                        barang.JumlahKlik = model.JumlahKlik;
                        barang.Year = model.Year;
                        barang.HargaOtr = model.HargaOtr;
                        barang.WarnaId = model.WarnaId;
                        barang.TypeBarangId = model.TypeBarangId;
                        barang.PhotoPath = model.PhotoPath;
                        barang.Modified = model.Modified;
                        barang.ModifiedBy = model.ModifiedBy;

                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }

        public async Task<bool> UpdateMany(List<Barang> ListBarang)
        {
            try
            {
                if (db != null)
                {
                    db.Barang.UpdateRange(ListBarang);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public async Task<Barang> GetHargaOTR(Barang request)
        {
            Barang response = new Barang();
            try
            {
                //response = await db.Barang.Where(x => x.RowStatus == true
                //                                 && x.TypeBarangId == request.TypeBarangId
                //                                 && x.WarnaId == request.WarnaId).FirstOrDefaultAsync();
                response = await db.Barang.Where(x => x.RowStatus == true
                                                 && x.TypeBarangId == request.TypeBarangId
                                                 ).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public async Task<List<SP_GetBarangByHomeParameter>> GetBarangByHomeParameter(BarangRequest request)
        {

            try
            {
                return await db.Set<SP_GetBarangByHomeParameter>().FromSqlRaw("EXEC sp_GetBarangByHomeParameter {0},{1},{2},{3},{4},{5},{6}", request.KotaID, request.MerkID, request.ModelBarangID, request.TypeID, request.Year, request.start, request.limit).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<SP_GetBarangByHomeParameterCount> GetBarangByHomeParameterCount(BarangRequest request)
        {
            try
            {
                return await db.Set<SP_GetBarangByHomeParameterCount>().FromSqlRaw("EXEC SP_GetBarangByHomeParameterCount {0},{1},{2},{3},{4},{5},{6}", request.KotaID, request.MerkID, request.ModelBarangID, request.TypeID, request.Year, request.start, request.limit).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Barang>> GetAllBarangSameTypeAndKota(Barang barang)
        {
            List<Barang> ListBarang = new List<Barang>();
            try
            {
                Barang b = await db.Barang.FirstOrDefaultAsync(x => x.RowStatus == true && x.Id == barang.Id);
                ListBarang = await (from brg in db.Barang
                                    where brg.RowStatus == true && brg.TypeBarangId == b.TypeBarangId
                                    && brg.WarnaId == b.WarnaId && brg.Name.ToLower() == b.Name.ToLower()
                                    select brg
                                ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListBarang;
        }
        
        public async Task<List<SP_GetPhotoAndWarnaByBarangID>> GetPhotoAndWarnaByID(BarangRequest request)
        {
            try
            {
                return await db.Set<SP_GetPhotoAndWarnaByBarangID>().FromSqlRaw("EXEC SP_GetPhotoAndWarnaByBarangID {0}", request.ID).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<SP_GetTypeBarangByBarangID>> GetTypeBarangByBarangID(BarangRequest request)
        {
            try
            {
                return await db.Set<SP_GetTypeBarangByBarangID>().FromSqlRaw("EXEC sp_GetTypeBarangByBarangID {0}", request.ID).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CarBids> GetAllBidsById(BarangRequest req)
        {
            if (db != null)
            {
                try
                {
                    int result = 0;
                    return db.Set<CarBids>().FromSqlRaw("EXEC sp_GetAllBidsByID_With_Paging {0},{1},{2},{3},{4},{5},{6}",
                        req.ID, req.start, req.limit, null, null, req.max, result).AsNoTracking().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return null;
        }

        public async Task<Barang> GetIDBarangByTypeAndCOlour(Barang request)
        {
            try
            {
                return await db.Barang.Where(x => x.RowStatus == true && x.TypeBarangId == request.TypeBarangId && x.WarnaId == request.WarnaId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
               
        public async Task<Barang> GetHargaOTRTypeBarangID(long TypeBarangID)
        {
            try
            {
                return await db.Barang.Where(x => x.RowStatus == true && x.TypeBarangId == TypeBarangID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Barang>> GetIDBarangByTypeAndColourIDS(long typeBarangID, List<long> listWarnaID)
        {
            try
            {
                return await db.Barang.Where(x => x.RowStatus == true && x.TypeBarangId == typeBarangID && listWarnaID.Contains(x.WarnaId)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

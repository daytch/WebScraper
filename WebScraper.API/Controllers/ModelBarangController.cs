using WebScraper.BusinessLogic;
using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebScraper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelBarangController : ControllerBase
    {
        private ModelBarangFacade facade = new ModelBarangFacade();
        private Security sec = new Security();
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string search = HttpContext.Request.Query["search[value]"].ToString();
                int draw = Convert.ToInt32(HttpContext.Request.Query["draw"]);
                string order = HttpContext.Request.Query["order[0][column]"];
                string orderDir = HttpContext.Request.Query["order[0][dir]"];
                int startRec = Convert.ToInt32(HttpContext.Request.Query["start"]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Query["length"]);
                var models = await facade.GetAll(search, order, orderDir, startRec, pageSize, draw);
                if (models == null)
                {
                    return NotFound();
                }

                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                string search = HttpContext.Request.Query["search[value]"].ToString();
                int draw = Convert.ToInt32(HttpContext.Request.Query["draw"]);
                string order = HttpContext.Request.Query["order[0][column]"];
                string orderDir = HttpContext.Request.Query["order[0][dir]"];
                int startRec = Convert.ToInt32(HttpContext.Request.Query["start"]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Query["length"]);
                var models = await facade.GetAllCategory(search, order, orderDir, startRec, pageSize, draw);
                if (models == null)
                {
                    return NotFound();
                }

                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAllCategoryAsks")]
        public async Task<IActionResult> GetAllCategoryAsks()
        {
            try
            {
                string search = HttpContext.Request.Query["search[value]"].ToString();
                int draw = Convert.ToInt32(HttpContext.Request.Query["draw"]);
                string order = HttpContext.Request.Query["order[0][column]"];
                string orderDir = HttpContext.Request.Query["order[0][dir]"];
                int startRec = Convert.ToInt32(HttpContext.Request.Query["start"]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Query["length"]);
                var models = await facade.GetAllCategoryAsks(search, order, orderDir, startRec, pageSize, draw);
                if (models == null)
                {
                    return NotFound();
                }

                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAllCategoryBids")]
        public async Task<IActionResult> GetAllCategoryBids()
        {
            try
            {
                string search = HttpContext.Request.Query["search[value]"].ToString();
                int draw = Convert.ToInt32(HttpContext.Request.Query["draw"]);
                string order = HttpContext.Request.Query["order[0][column]"];
                string orderDir = HttpContext.Request.Query["order[0][dir]"];
                int startRec = Convert.ToInt32(HttpContext.Request.Query["start"]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Query["length"]);
                var models = await facade.GetAllCategoryBids(search, order, orderDir, startRec, pageSize, draw);
                if (models == null)
                {
                    return NotFound();
                }

                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost]
        //[Route("GetModelWithID")]
        //public async Task<IActionResult> GetPost(long postId)
        //{
        //    if (postId < 1)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var post = await facade.GetPost(postId);

        //        if (post == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(post);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost]
        [Route("GetModelWithMerkID")]
        public async Task<IActionResult> GetModelWithMerkID([FromBody]ModelBarangRequest request)
        {

            try
            {
                var post = await facade.GetModelWithMerkID(request);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<ModelBarangResponse> AddPost([FromBody]ModelBarangRequest model)
        {
            ModelBarangResponse resp = new ModelBarangResponse();
            try
            {
                string bearer = Request.HttpContext.Request.Headers["Authorization"];
                string token = bearer.Substring("Bearer ".Length).Trim();
                string username = string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    resp.IsSuccess = false;
                    resp.Message = "You don't have access.";
                    return resp;
                }

                username = sec.ValidateToken(token);
                if (username == null)
                {
                    Response.HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    });
                    resp.IsSuccess = false;
                    resp.Message = "Your session was expired, please re-login.";
                    return resp;
                }
                model.UserName = username;
                if (model.ID > 0)
                {
                    resp = await facade.UpdatePost(model);
                }
                else
                {
                    resp = await facade.AddPost(model);
                }
                return resp;
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }
            //ModelBarangResponse result = new ModelBarangResponse();
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        if (model.ID>0)
            //        {
            //            result = await facade.UpdatePost(model);
            //        }
            //        else
            //        {
            //            result = await facade.AddPost(model);
            //        }


            //        return Ok(result);

            //    }
            //    catch (Exception)
            //    {
            //        return BadRequest();
            //    }

            //}

            //return BadRequest();
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<ModelBarangResponse> AddCategory([FromBody]ModelBarangRequest model)
        {
            ModelBarangResponse resp = new ModelBarangResponse();
            try
            {
                string bearer = Request.HttpContext.Request.Headers["Authorization"];
                string token = bearer.Substring("Bearer ".Length).Trim();
                string username = string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    resp.IsSuccess = false;
                    resp.Message = "You don't have access.";
                    return resp;
                }

                username = sec.ValidateToken(token);
                if (username == null)
                {
                    Response.HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    });
                    resp.IsSuccess = false;
                    resp.Message = "Your session was expired, please re-login.";
                    return resp;
                }
                model.UserName = username;
                if (model.ID > 0)
                {
                    resp = await facade.UpdateCategory(model);
                }
                //else
                //{
                //    resp = await facade.AddPost(model);
                //}
                return resp;
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }
        }

        [HttpPost]
        [Route("DeleteCategory")]
        public async Task<ModelBarangResponse> DeleteCategory([FromBody]ModelBarangRequest model)
        {
            ModelBarangResponse resp = new ModelBarangResponse();
            try
            {
                string bearer = Request.HttpContext.Request.Headers["Authorization"];
                string token = bearer.Substring("Bearer ".Length).Trim();
                string username = string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    resp.IsSuccess = false;
                    resp.Message = "You don't have access.";
                    return resp;
                }

                username = sec.ValidateToken(token);
                if (username == null)
                {
                    Response.HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    });
                    resp.IsSuccess = false;
                    resp.Message = "Your session was expired, please re-login.";
                    return resp;
                }
                model.UserName = username;

                return resp = await facade.DeleteCategory(model); ;
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }
        }

        [HttpPost]
        [Route("DeleteCategoryBid")]
        public async Task<ModelBarangResponse> DeleteCategoryBid([FromBody]ModelBarangRequest model)
        {
            ModelBarangResponse resp = new ModelBarangResponse();
            try
            {
                string bearer = Request.HttpContext.Request.Headers["Authorization"];
                string token = bearer.Substring("Bearer ".Length).Trim();
                string username = string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    resp.IsSuccess = false;
                    resp.Message = "You don't have access.";
                    return resp;
                }

                username = sec.ValidateToken(token);
                if (username == null)
                {
                    Response.HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    });
                    resp.IsSuccess = false;
                    resp.Message = "Your session was expired, please re-login.";
                    return resp;
                }
                model.UserName = username;

                return resp = await facade.DeleteCategoryBid(model); ;
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }
        }

        [HttpPost]
        [Route("DeleteCategoryAsk")]
        public async Task<ModelBarangResponse> DeleteCategoryAsk([FromBody]ModelBarangRequest model)
        {
            ModelBarangResponse resp = new ModelBarangResponse();
            try
            {
                string bearer = Request.HttpContext.Request.Headers["Authorization"];
                string token = bearer.Substring("Bearer ".Length).Trim();
                string username = string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    resp.IsSuccess = false;
                    resp.Message = "You don't have access.";
                    return resp;
                }

                username = sec.ValidateToken(token);
                if (username == null)
                {
                    Response.HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    });
                    resp.IsSuccess = false;
                    resp.Message = "Your session was expired, please re-login.";
                    return resp;
                }
                model.UserName = username;

                return resp = await facade.DeleteCategoryAsk(model); ;
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }
        }

        [HttpPost]
        [Route("DeletePost")]
        public async Task<ModelBarangResponse> DeletePost([FromBody]ModelBarangRequest model)
        {
            ModelBarangResponse resp = new ModelBarangResponse();
            try
            {
                string bearer = Request.HttpContext.Request.Headers["Authorization"];
                string token = bearer.Substring("Bearer ".Length).Trim();
                string username = string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    resp.IsSuccess = false;
                    resp.Message = "You don't have access.";
                    return resp;
                }

                username = sec.ValidateToken(token);
                if (username == null)
                {
                    Response.HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    });
                    resp.IsSuccess = false;
                    resp.Message = "Your session was expired, please re-login.";
                    return resp;
                }
                model.UserName = username;

                return resp = await facade.DeletePost(model); ;
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }
        }


        [HttpPost]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody]ModelBarangRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await facade.UpdatePost(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("GetModelByKotaIDMerkID")]
        public async Task<IActionResult> GetModelByKotaIDMerkID([FromBody]ModelBarangRequest model)
        {


            try
            {
                var post = await facade.GetModelByKotaIDMerkID(model);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

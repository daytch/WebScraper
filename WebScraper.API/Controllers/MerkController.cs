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
    public class MerkController : ControllerBase
    {
        private Security sec = new Security();
        private MerkFacade facade = new MerkFacade();
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
        [Route("GetAllWithoutFilter")]
        public async Task<IActionResult> GetAllWithoutFilter()
        {
            try
            {
              
                var models = await facade.GetAllWithoutFilter();
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

        [HttpPost]
        [Route("GetModelWithID")]
        public async Task<IActionResult> GetPost(long postId)
        {
            if (postId < 1)
            {
                return BadRequest();
            }

            try
            {
                var post = await facade.GetPost(postId);

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
        public async Task<MerkResponse> AddPost([FromBody]MerkRequest model)
        {
            MerkResponse resp = new MerkResponse();
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
            catch (Exception)
            {
                return resp;
            }
            //if (ModelState.IsValid)
            //{
            //    MerkResponse response = new MerkResponse();
            //    try
            //    {
            //        if ( model.ID != 0)
            //        {
            //            response = await facade.UpdatePost(model);
            //        }
            //        else
            //        {
            //            response = await facade.AddPost(model);
            //        }
                   
                    
            //        return Ok(response);
                  
            //    }
            //    catch (Exception)
            //    {
            //        return BadRequest();
            //    }

            //}

            //return BadRequest();
        }

        [HttpPost]
        [Route("DeletePost")]
        public async Task<MerkResponse> DeletePost([FromBody]MerkRequest model)
        {
            MerkResponse resp = new MerkResponse();
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
                return resp = await facade.DeletePost(model.ID, username);
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }

            //long postId = model.ID;
            //if (postId < 1)
            //{
            //    return BadRequest();
            //}

            //try
            //{
            //     var response = await facade.DeletePost(postId);
               
            //    return Ok(response);
            //}
            //catch (Exception)
            //{

            //    return BadRequest();
            //}
        }


        [HttpPost]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody]MerkRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await facade.UpdatePost(model);

                    return Ok(model);
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
        [Route("GetMerkByKotaID")]
        public IActionResult GetMerkByKotaID(long KotaID)
        {
            if (KotaID < 1)
            {
                return BadRequest();
            }

            try
            {
                var post = facade.GetMerkByKotaID(KotaID);

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
        [HttpGet]
        [Route("GetMerkRank")]
        public async Task<MerkResponse> GetMerkRank()
        {
            string search = HttpContext.Request.Query["search[value]"].ToString();
            int draw = Convert.ToInt32(HttpContext.Request.Query["draw"]);
            string order = HttpContext.Request.Query["order[0][column]"];
            string orderDir = HttpContext.Request.Query["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Request.Query["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Request.Query["length"]);
            //var models = await facade.GetAll(search, order, orderDir, startRec, pageSize, draw);
            MerkResponse resp = new MerkResponse();
            try
            {
                resp.draw = draw;
                resp = facade.GetMerkRank(search, draw, startRec, pageSize);

                return resp;
            }
            catch (Exception ex)
            {
                resp.Message = ex.ToString();
                return resp;
            }
        }

        [HttpPost]
        [Route("UpdateMerkRank")]
        public async Task<MerkResponse> UpdateMerkRank([FromBody]MerkRequest model)
        {
            MerkResponse resp = new MerkResponse();
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
                return resp = await facade.UpdateMerkRank(model, username);
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message.ToString();
                return resp;
            }

        }


    }
}

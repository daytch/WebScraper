using System;
using System.Threading.Tasks;
using WebScraper.BusinessLogic;
using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebScraper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private DealerFacade facade = new DealerFacade();
        private Security sec = new Security();
        [HttpGet]
        [Route("GetAllForDropdown")]
        public async Task<IActionResult> GetAllForDropdown(int KotaID)
        {
            DealerResponse response = new DealerResponse();
            try
            {
                response.ListDealer = await facade.GetAllForDropdown(KotaID);
                response.IsSuccess = true;
                response.Message = "Success";

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<DealerResponse> GetAll()
        {
            DealerResponse resp = new DealerResponse();
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
                string search = HttpContext.Request.Query["search[value]"].ToString();
                int draw = Convert.ToInt32(HttpContext.Request.Query["draw"]);
                string order = HttpContext.Request.Query["order[0][column]"];
                string orderDir = HttpContext.Request.Query["order[0][dir]"];
                int startRec = Convert.ToInt32(HttpContext.Request.Query["start"]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Query["length"]);
                resp = await facade.GetAll(search, order, orderDir, startRec, pageSize, draw);


                return resp;
            }
            catch (Exception)
            {
                return resp;
            }
            //try
            //{
               
            //    if (models == null)
            //    {
            //        return NotFound();
            //    }

            //    return Ok(models);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex);
            //}
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
        [Route("AddPost")]
        public async Task<DealerResponse> AddPost([FromBody]DealerRequest model)
        {
            DealerResponse resp = new DealerResponse();
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

            //DealerResponse result = new DealerResponse();
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        if (model.ID > 0)
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
        [Route("DeletePost")]
        public async Task<DealerResponse> DeletePost(long ID)
        {
            DealerResponse resp = new DealerResponse();
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

                resp = await facade.DeletePost(ID, username);


                return resp;
            }
            catch (Exception)
            {
                return resp;
            }
            //DealerResponse response = new DealerResponse();


            //try
            //{
            //    if (ID < 1)
            //    {
            //        return BadRequest();
            //    }

            //    response = await facade.DeletePost(ID);

            //    return Ok(response);
            //}
            //catch (Exception)
            //{

            //    return BadRequest();
            //}
        }


        [HttpPost]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody]DealerRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await facade.UpdatePost(request);

                    return Ok(result);
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
    }
}
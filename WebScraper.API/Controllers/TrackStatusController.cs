using WebScraper.BusinessLogic;
using WebScraper.DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackStatusController : ControllerBase
    {
        //private TrackStatusFacade facade = new TrackStatusFacade();
        //[HttpPost]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var models = await facade.GetAll();
        //        if (models == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(models);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}



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

        //[HttpPost]
        //[Route("AddPost")]
        //public async Task<IActionResult> AddPost([FromBody]TrackStatus model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var postId = await facade.AddPost(model);
        //            if (postId > 0)
        //            {
        //                return Ok(postId);
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest();
        //        }

        //    }

        //    return BadRequest();
        //}

        //[HttpPost]
        //[Route("DeletePost")]
        //public async Task<IActionResult> DeletePost(long postId)
        //{
        //    long result = 0;

        //    if (postId < 1)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        result = await facade.DeletePost(postId);
        //        if (result == 0)
        //        {
        //            return NotFound();
        //        }
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest();
        //    }
        //}


        //[HttpPost]
        //[Route("UpdatePost")]
        //public async Task<IActionResult> UpdatePost([FromBody]TrackStatus model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await facade.UpdatePost(model);

        //            return Ok();
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.GetType().FullName ==
        //                     "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
        //            {
        //                return NotFound();
        //            }

        //            return BadRequest();
        //        }
        //    }

        //    return BadRequest();
        //}
    }
}

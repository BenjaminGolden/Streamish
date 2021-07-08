using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streamish.Models;
using Streamish.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }

        //[HttpGet("GetWithComments")]
        //public IActionResult GetWithComments()
        //{
        //    var videos = _userProfileRepository.GetAllWithComments();
        //    return Ok(videos);
        //}

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _userProfileRepository.GetById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpGet("GetByIdWithVideos/{id}")]
        public IActionResult GetByIdWithVideos(int id)
        {
            var user = _userProfileRepository.GetByIdWithVideos(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //[HttpGet("GetByIdWithComments/{id}")]
        //public IActionResult GetByIdWithComments(int id)
        //{
        //    var video = _userProfileRepository.GetByIdWithComments(id);
        //    if (video == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(video);
        //}

        [HttpPost]
        public IActionResult Post(UserProfile user)
        {
            _userProfileRepository.Add(user);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
    }
}

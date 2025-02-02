﻿using STEM.Models;
using STEM.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using STEM.Data;

namespace STEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private IMapper mapper;
        private STEMContext _context;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            var userId = userService.RegisterUser(user);
            return Ok(userId);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            try
            {
                var returnedUser = userService.UpdateUser(user);
                return Ok(returnedUser);
            }
            catch(Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
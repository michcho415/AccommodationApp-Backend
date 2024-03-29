﻿using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public interface IUserService
    {
        public Task<IActionResult> AuthenticateAsync(LoginDTO login);
        public Task<IActionResult> CreateAsync(UserDTO user);
        public Task<IActionResult> DeleteAsync(int id);
    }
}

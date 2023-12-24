using System;
using Microsoft.Extensions.DependencyInjection;
using music_player.Helpers;
using music_player.Models;
using music_player.Repository;

namespace music_player.Services;

public class LoginService
{
    private readonly IUserRepository userRepository;
    
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginService()
    {
        userRepository = Program.ServiceProvider.GetService<IUserRepository>();
    }
    
    public string LoginAttempt()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            throw new Exception("Email or Password cannot be empty!");
        
        User? user = userRepository.GetByUsername(Username);

        if (user == null)
            throw new Exception("User doesn't exists!");
        
        if (!PasswordHasher.VerifyPassword(Password, user.Password))
            throw new Exception("Password is not valid!");
        
        return "success";
    }
}
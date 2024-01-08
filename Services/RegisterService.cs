using System;
using Microsoft.Extensions.DependencyInjection;
using music_player.Helpers;
using music_player.Models;
using music_player.Repository;

namespace music_player.Services;

public class RegisterService
{
    private readonly IUserRepository userRepository;

    public string Username;
    public string Password;
    public string RetypedPassword; 
    
    public RegisterService()
    { 
        userRepository = Program.ServiceProvider.GetService<IUserRepository>();
    }
    
    public string RegisterAttempt()
    {
        DataValidation();
        
        userRepository.Add(new User()
        {
            Username = Username, 
            Password = PasswordHasher.HashPassword(Password), 
            CreatedAt = DateTime.Now
        });

        return "Your account has been created successfully!";  
    }

    private void DataValidation()
    {
        if (!Password.Equals(RetypedPassword))
            throw new Exception("Passwords doesnt match!");
        
        if (string.IsNullOrEmpty(Password) || Password.Length <= 6)
            throw new Exception("The password is too short!");
        
        User? user = userRepository.GetByUsername(Username);

        if (user != null)
            throw new Exception("User already exists!");
    }
}
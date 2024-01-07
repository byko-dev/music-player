using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using music_player.Models;
using music_player.Repository;

namespace music_player.Services;

public class UserService
{
    private readonly IUserRepository userRepository;
    
    public UserService()
    { 
        userRepository = Program.ServiceProvider.GetService<IUserRepository>();
    }

    public List<User> GetAllUsers()
    {
        return userRepository.All();
    }

    public User? GetById(int Id)
    {
        return userRepository.GetById(Id);
    }
    
    public void Import(User user)
    {
        if (GetById(user.Id) != null)
            throw new Exception("User already exists!");
        
        Add(user);
    }
    
    public void Add(User user)
    {
        userRepository.Add(user);
    }
}
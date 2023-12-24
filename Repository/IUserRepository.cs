using music_player.Models;

namespace music_player.Repository;

public interface IUserRepository
{
    User? GetById(int id);
    void Add(User user);
    User? GetByUsername(string username);
    

}
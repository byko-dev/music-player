using System.Collections.Generic;
using music_player.Models;

namespace music_player.Repository;

public interface ISoundRepository
{
    List<Sound> All();
    void Add(Sound sound);
    Sound? GetById(int id);
    void Update(Sound sound);
    void Delete(Sound? sound);
}
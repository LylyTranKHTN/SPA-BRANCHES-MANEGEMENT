package Spring.service;


import java.util.Optional;

import Spring.model.Room;

public interface IRoomService {
    Iterable<Room> findAll();
    
    Iterable<Room> findByOuletID(int outlet);

    Optional<Room> findOne(int id);

    Room save(Room contact);

    void delete(int id);
}

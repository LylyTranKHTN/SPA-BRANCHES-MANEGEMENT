package Spring.repository;


import org.springframework.data.repository.CrudRepository;

import Spring.model.Room;
import Spring.model.Services;

public interface RoomRepository extends CrudRepository<Room,Integer> {
	Iterable<Room> findByOutlet(int outlet);
}

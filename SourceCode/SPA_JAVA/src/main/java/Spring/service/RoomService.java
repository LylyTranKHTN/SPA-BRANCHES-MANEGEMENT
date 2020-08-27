package Spring.service;


import Spring.model.Room;
import Spring.repository.RoomRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;



import java.util.Optional;

@Service
public class RoomService implements IRoomService {

    @Autowired
    private RoomRepository roomRepository;

    @Override
    public Iterable<Room> findAll() {
        // TODO Auto-generated method stub
        return roomRepository.findAll();
    }

    @Override
    public Optional<Room> findOne(int id) {
        // TODO Auto-generated method stub
        return roomRepository.findById(id);
    }

    @Override
    public Room save(Room contact) {
        // TODO Auto-generated method stub
    	return roomRepository.save(contact);
    }

    @Override
    public void delete(int id) {
        // TODO Auto-generated method stub
    	roomRepository.deleteById(id);
    }

	@Override
	public Iterable<Room> findByOuletID(int outlet) {
		// TODO Auto-generated method stub
		return roomRepository.findByOutlet(outlet);
	}
}


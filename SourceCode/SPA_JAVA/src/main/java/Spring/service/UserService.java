package Spring.service;
import Spring.model.Staff;
import Spring.repository.UserRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;



import java.util.Optional;

@Service
public class UserService implements IUserService {

    @Autowired
    private UserRepository userRepository;

    @Override
    public Iterable<Staff> findAll() {
        // TODO Auto-generated method stub
        return userRepository.findAll();
    }
    
    @Override
    public Iterable<Staff> findName(String name) {
        // TODO Auto-generated method stub
        return userRepository.findByName(name);
    }
    @Override
    public Iterable<Staff> findUsername(String username) {
        // TODO Auto-generated method stub
        return userRepository.findByUsername(username);
    }


    @Override
    public Optional<Staff> findOne(int id) {
        // TODO Auto-generated method stub
        return userRepository.findById(id);
    }

    @Override
    public Staff save(Staff contact) {
        // TODO Auto-generated method stub
    	return userRepository.save(contact);
    }

    @Override
    public void delete(int id) {
        // TODO Auto-generated method stub
    	userRepository.deleteById(id);
    }
}


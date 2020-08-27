package Spring.service;


import java.util.Optional;

import Spring.model.Staff;

public interface IUserService {
    Iterable<Staff> findAll();
    
    Iterable<Staff> findName(String name);
    
    Iterable<Staff> findUsername(String username);

    Optional<Staff> findOne(int id);

    Staff save(Staff contact);

    void delete(int id);
}

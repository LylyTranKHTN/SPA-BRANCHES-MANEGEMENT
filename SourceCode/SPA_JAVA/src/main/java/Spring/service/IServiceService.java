package Spring.service;

import java.util.Optional;

import Spring.model.Services;

public interface IServiceService {
    Iterable<Services> findAll();
    
    Iterable<Services> findName(String name);
    
    Optional<Services> findOne(int id);

    void save(Services contact);

    void delete(int id);
}

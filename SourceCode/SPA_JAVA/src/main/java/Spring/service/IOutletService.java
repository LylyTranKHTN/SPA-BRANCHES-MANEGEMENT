package Spring.service;



import java.util.Optional;

import Spring.model.Outlet;

public interface IOutletService {
    Iterable<Outlet> findAll();
    
    Iterable<Outlet> findName(String name);

    Optional<Outlet> findOne(int id);

    void save(Outlet contact);

    void delete(int id);
}

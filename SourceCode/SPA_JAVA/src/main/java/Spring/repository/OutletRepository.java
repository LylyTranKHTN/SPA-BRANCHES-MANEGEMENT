package Spring.repository;


import org.springframework.data.repository.CrudRepository;
import Spring.model.Outlet;

public interface OutletRepository extends CrudRepository<Outlet,Integer> {
	Iterable<Outlet> findByName(String name);
}

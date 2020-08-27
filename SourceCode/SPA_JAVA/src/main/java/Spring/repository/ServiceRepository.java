package Spring.repository;

import org.springframework.data.repository.CrudRepository;
import Spring.model.Services;

public interface ServiceRepository extends CrudRepository<Services,Integer> {
	Iterable<Services> findByName(String name);
}

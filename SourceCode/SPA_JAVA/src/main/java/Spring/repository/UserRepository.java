package Spring.repository;


import org.springframework.data.repository.CrudRepository;

import Spring.model.Staff;

public interface UserRepository extends CrudRepository<Staff,Integer> {
	Iterable<Staff> findByName(String name);
	Iterable<Staff> findByUsername(String username);
}

package Spring.service;


import Spring.model.Outlet;
import Spring.repository.OutletRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;



import java.util.Optional;

@Service
public class OutletService implements IOutletService {

    @Autowired
    private OutletRepository outletRepository;

    @Override
    public Iterable<Outlet> findAll() {
        // TODO Auto-generated method stub
        return outletRepository.findAll();
    }
    
    @Override
    public Iterable<Outlet> findName(String name) {
        // TODO Auto-generated method stub
        return outletRepository.findByName(name);
    }


    @Override
    public Optional<Outlet> findOne(int id) {
        // TODO Auto-generated method stub
        return outletRepository.findById(id);
    }

    @Override
    public void save(Outlet contact) {
        // TODO Auto-generated method stub
    	outletRepository.save(contact);
    }

    @Override
    public void delete(int id) {
        // TODO Auto-generated method stub
    	outletRepository.deleteById(id);
    }
}


package Spring.service;

import  Spring.model.Services;

import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import Spring.repository.ServiceRepository;

@Service
public class ServiceService implements IServiceService {

    @Autowired
    private ServiceRepository serviceRepository;

    @Override
    public Iterable<Services> findAll() {
        // TODO Auto-generated method stub
        return serviceRepository.findAll();
    }
    
    @Override
    public Iterable<Services> findName(String name) {
        // TODO Auto-generated method stub
        return serviceRepository.findByName(name);
    }


    @Override
    public Optional<Services> findOne(int id) {
        // TODO Auto-generated method stub
        return serviceRepository.findById(id);
    }

    @Override
    public void save(Services contact) {
        // TODO Auto-generated method stub
    	serviceRepository.save(contact);
    }

    @Override
    public void delete(int id) {
        // TODO Auto-generated method stub
    	serviceRepository.deleteById(id);
    }
}


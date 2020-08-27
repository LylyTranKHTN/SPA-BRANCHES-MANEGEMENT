package Spring.service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import Spring.model.Servicetype;
import Spring.repository.ServiceTypeRepository;


@Service
public class ServiceTypeService implements IServiceTypeService {

    @Autowired
    private ServiceTypeRepository serviceTypeRepository;

    @Override
    public Iterable<Servicetype> findAll() {
        // TODO Auto-generated method stub
        return serviceTypeRepository.findAll();
    }
}


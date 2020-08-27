package Spring.service;

import Spring.ModelDTO.BedDTO;
import Spring.ModelDTO.BedRoomDTO;
import Spring.bus.MessageResult;
import Spring.model.Bed;
import Spring.model.ServiceBed;
import antlr.collections.List;

import java.util.Optional;

public interface IBedService {
    Iterable<Bed> findAll();
    
    //get bed by outletID for outlet manager manage bed
    Iterable<BedRoomDTO> getBedRoom(Integer outletID);
    
    Iterable<Integer> getServiceBed(int bed);

    Optional<Bed> findOne(int id);

    void save(Bed contact);

    void delete(int id);

    MessageResult AddBed(BedDTO bedDTO);
	MessageResult EditBed(BedDTO bedDTO);
}

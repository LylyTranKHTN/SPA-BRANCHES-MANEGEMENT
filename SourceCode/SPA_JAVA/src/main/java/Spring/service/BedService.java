package Spring.service;

import Spring.ModelDTO.BedDTO;
import Spring.ModelDTO.BedRoomDTO;
import Spring.bus.MessageResult;
import Spring.model.Bed;
import Spring.model.Room;
import Spring.model.ServiceBed;
import Spring.model.ServiceBedId;
import Spring.repository.BedRepository;
import Spring.repository.RoomRepository;
import Spring.repository.ServiceBedRepository;

import org.apache.commons.collections4.IterableUtils;
import org.hibernate.mapping.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.Optional;

@Service
public class BedService implements IBedService {

    @Autowired
    private BedRepository bedRepository;
    @Autowired
    private RoomRepository roomRepository;
    @Autowired
    private ServiceBedRepository serviceBedRepository;
    
    

    @Override
    public Iterable<Bed> findAll() {
        // TODO Auto-generated method stub
        return bedRepository.findAll();
    }
    
    @Override
    public Iterable<BedRoomDTO> getBedRoom(Integer outletID){
    	Iterable<Room> roomAll = roomRepository.findAll();
    	Collection<Room> roomByoutletID = new ArrayList<Room>();
    	
    	//find room by outletID
    	for (int i = 0; i<IterableUtils.size(roomAll);i++) {
    		Room room = IterableUtils.get(roomAll, i);
    		if (room.getOutlet() == outletID) {
    			roomByoutletID.add(room);
    		}
    	}
    	
    	Iterable<Bed> bedAll = bedRepository.findAll();
    	Collection<BedRoomDTO> Result = new ArrayList<BedRoomDTO>();
    	
    	//find bed by room
    	for (int j = 0; j<IterableUtils.size(bedAll); j++) {
    		Bed bed = IterableUtils.get(bedAll,j);
    		
	    	for (int i = 0; i < IterableUtils.size(roomByoutletID); i++) {
	    		Room room = IterableUtils.get(roomByoutletID,i);
	    		
	    		//mapping bed to BedRoomDTO
	    		if (bed.getRoom() == room.getId()) {
	    			BedRoomDTO dto = new BedRoomDTO();
	    			dto.setbedId(bed.getId());
	    			dto.setRoomID(room.getId());
	    			if (bed.getDeleted() == 1) {
	    				dto.setIsEnable(false);
	    			}
	    			else
	    				dto.setIsEnable(true);
	    			dto.setRoomName(room.getName());
	    			
	    			//add BedRoomDTO is true in Result and break to find with next bed.
	    			Result.add(dto);
	    			
	    			break;
	    		}
	    	}
    	}
    	return Result;
    }

    @Override
    public Optional<Bed> findOne(int id) {
        // TODO Auto-generated method stub
        return bedRepository.findById(id);
    }
    
    @Override
    public MessageResult AddBed(BedDTO bedDto) {
    	try {
	    	//check numOfBed in room
	    	Room room = roomRepository.findById(bedDto.getRoomID()).get();
	    	Iterable<Bed> beds = bedRepository.findAll();
	    	
	    	Integer numOfBedsCurrent = 0;
	    	for (int i = 0; i< IterableUtils.size(beds); i++) {
	    		Bed bed = IterableUtils.get(beds,i);
	    		if(bed.getRoom() == bedDto.getRoomID()){
	    			numOfBedsCurrent++;
	    		}
	    	}
	    	if (numOfBedsCurrent >= room.getNumofbed()) {
	    		return new MessageResult("Room is Full!",false);

	    	}
	    	
	    	//add bed
	    	Bed bed = new Bed();
	    	bed.setRoom(bedDto.getRoomID());
	    	if (bedDto.getIsEnable() == true)
	    		bed.setDeleted(0);
	    	else
	    		bed.setDeleted(1);
	    	bed.setCreatedate(new Date());
	    	bedRepository.save(bed);
	    	
	    	//add service-bed
	    	if (!bedDto.getServiceID().isEmpty()) {
	    		for (int i = 0; i < IterableUtils.size(bedDto.getServiceID()); i++) {
	        		ServiceBedId serviceBedId = new ServiceBedId(bed.getId(),IterableUtils.get(bedDto.getServiceID(),i));
	        		ServiceBed serviceBed = new ServiceBed(serviceBedId);
	        		serviceBed.setDeleted(0);
	        		serviceBed.setCreatedate(new Date());
	        		serviceBedRepository.save(serviceBed);
	    		}
	    	}
	    	return new MessageResult("Succeeded",true);    	}
    	catch(Exception ex) {
    		return new MessageResult("Error!!: ",false);
    	}
    }

    @Override
    public void save(Bed contact) {
        // TODO Auto-generated method stub
        bedRepository.save(contact);
    }

    @Override
    public void delete(int id) {
        // TODO Auto-generated method stub
        bedRepository.deleteById(id);
    }

	@Override
	public MessageResult EditBed(BedDTO bedDto) {
		// TODO Auto-generated method stub
		try {
	    	//check numOfBed in room
	    	Room room = roomRepository.findById(bedDto.getRoomID()).get();
	    	Iterable<Bed> beds = bedRepository.findAll();
	    	
	    	Integer numOfBedsCurrent = 0;
	    	for (int i = 0; i< IterableUtils.size(beds); i++) {
	    		Bed bed = IterableUtils.get(beds,i);
	    		if(bed.getRoom() == bedDto.getRoomID()){
	    			numOfBedsCurrent++;
	    		}
	    	}
		    	if (room.getNumofbed()==null||numOfBedsCurrent >= room.getNumofbed()) {
		    		return new MessageResult("Room is Full!",false);
	    	}
	    	
	    	//add bed
	    	Bed bed = new Bed();
	    	bed.setId(bedDto.getBedID());
	    	bed.setRoom(bedDto.getRoomID());
	    	if (bedDto.getIsEnable() == true)
	    		bed.setDeleted(0);
	    	else
	    		bed.setDeleted(1);
	    	bed.setCreatedate(new Date());
	 	    
	    	bedRepository.save(bed);
	    	
	    	//add service-bed
	    	if (!bedDto.getServiceID().isEmpty()) {
	    		for (int i = 0; i < IterableUtils.size(bedDto.getServiceID()); i++) {
	        		ServiceBedId serviceBedId = new ServiceBedId(bed.getId(),IterableUtils.get(bedDto.getServiceID(),i));
	        		ServiceBed serviceBed = new ServiceBed(serviceBedId);
	        		serviceBed.setDeleted(0);
	        		serviceBed.setCreatedate(new Date());
	        		serviceBedRepository.save(serviceBed);
	    		}
	    	}
	    	return new MessageResult("Succeeded",true);
    	}
    	catch(Exception ex) {
    		return new MessageResult("Error!! " + ex.toString(),false);
    	}
	}

	@Override
	public Iterable <Integer> getServiceBed(int bed) {
		// TODO Auto-generated method stub
		
		Iterable<ServiceBed> serviceBeds = serviceBedRepository.findAll();
		Collection<Integer> result= new ArrayList<Integer>();
    	for (int i = 0; i< IterableUtils.size(serviceBeds); i++) {
    		ServiceBed serviceBed = IterableUtils.get(serviceBeds,i);
    		if(bed == serviceBed.getId().getBed()){
    			result.add(serviceBed.getId().getService());
    		}
    	}
		return result;
	}
}


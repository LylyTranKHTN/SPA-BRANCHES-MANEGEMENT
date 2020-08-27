package Spring.controller;

import Spring.ModelDTO.BedDTO;
import Spring.ModelDTO.BedRoomDTO;
import Spring.bus.MessageResult;
import Spring.bus.PageableData;
import Spring.model.Bed;
import Spring.model.ServiceBed;
import Spring.service.BedService;

import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;


@Controller
public class BedController {
    @Autowired
    private BedService bedService;

    @GetMapping("/bed/{pageSize}/{pageNumber}")
    @ResponseBody
    public Iterable<Bed> GetBedByOutlet(@PathVariable("pageSize") int pageSize ,@PathVariable("pageNumber") int pageNumber) {
        Iterable <Bed> resultBed= bedService.findAll();
        return  PageableData.GetPage(resultBed,pageSize,pageNumber);
    };
    
    @GetMapping("/bed/room/{outletID}/{pageSize}/{pageNumber}")
    @ResponseBody
    public Iterable<BedRoomDTO> GetBedRoomByOutlet(@PathVariable("outletID") int outletID, @PathVariable("pageSize") int pageSize ,@PathVariable("pageNumber") int pageNumber){
    	Iterable<BedRoomDTO> result = bedService.getBedRoom(outletID);
    	return PageableData.GetPage(result, pageSize, pageNumber);
    }
    
    @GetMapping("/bed/{id}")
    @ResponseBody
    public Bed GetBedByID(@PathVariable("id") int id){
    	Optional<Bed> result = bedService.findOne(id);
    	return  result.get();
    }
    
    @GetMapping("/bed/service/{bed}")
    @ResponseBody
    public Iterable<Integer> GetBedByOutlet(@PathVariable("bed") int bed) {
        return  bedService.getServiceBed(bed);
    };
    
    @PostMapping("/bed")
    @ResponseBody
    public MessageResult AddBedByOutlet(@RequestBody BedDTO  bed) {
    	if (bed.getRoomID() < 0) {
    		return new MessageResult("Bad request",false);
    	}
    	return bedService.AddBed(bed);
    };
     
    @PutMapping("/bed")
    @ResponseBody
    public MessageResult EditBedByOutlet(@RequestBody BedDTO  bed) {
    	if (bed.getBedID() != null)
    	{
    		if (bed.getRoomID() < 0) {
        		return new MessageResult("Bad request",false);
        	}
        	return bedService.EditBed(bed);
    	}
    	return new MessageResult("Bad request",false);
    };
    
    
    
//add funtion
//    @DeleteMapping("/bed")
//    @ResponseBody
//    public String delete(@RequestBody Bed  bed) {
//    	
//    	if (bed.getId() != null)
//    	{	
//    		Optional<Bed> temp= bedService.findOne(bed.getId());
//    		bed =temp.get();
//    		bed.setDeleted(1);
//    		bedService.save(bed);
//	    	 return "deleted";
//    	}
//    	return "Bad request";
//    };
}

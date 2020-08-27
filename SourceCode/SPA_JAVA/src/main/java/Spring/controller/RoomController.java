package Spring.controller;

import Spring.bus.MessageResult;
import Spring.bus.PageableData;
import Spring.model.Room;
import Spring.service.RoomService;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;


@Controller
public class RoomController {
    @Autowired
    private RoomService roomService;

    @GetMapping("/room")
    @ResponseBody
    public Iterable<Room> GetRoomAll() {
    	Iterable<Room> resultRoom =roomService.findAll();
    	return resultRoom;
    };
    
    @GetMapping("/room/{outletID}")
    @ResponseBody
    public Iterable<Room> GetRoomByOutletID(@PathVariable("outletID") int outletID ) {
    	Iterable<Room> resultRoom =roomService.findByOuletID(outletID);
    	return resultRoom;
    };
    
    @GetMapping("/room/{pageSize}/{pageNumber}")
    @ResponseBody
    public Iterable<Room> GetRoomByOutlet(@PathVariable("pageSize") int pageSize ,@PathVariable("pageNumber") int pageNumber) {
    	Iterable<Room> resultRoom =roomService.findAll();
    	return PageableData.GetPage(resultRoom,pageSize,pageNumber);
    };
    
    @PostMapping("/room")
    @ResponseBody
    public MessageResult AddRoomByOutlet(@RequestBody Room  room) {
    	if (room.getId() == null)
    	{
    		roomService.save(room);
    		return new MessageResult("Succeeded",true);
    	}
    	return new MessageResult("Bad request ",false);
    };
     
    @PutMapping("/room")
    @ResponseBody
    public MessageResult EditRoomByOutlet(@RequestBody Room  room) {
    	
    	roomService.save(room);
        return new MessageResult("Succeeded",true);
    };
    
    
//    
//    @DeleteMapping("/room")
//    @ResponseBody
//    public String delete(@RequestBody Room  room) {
//    	
//    	if (room.getId() != null)
//    	{	
//    		Optional<Room> temp= roomService.findOne(room.getId());
//    		room =temp.get();
//    		room.setDeleted(1);
//    		roomService.save(room);
//	    	 return "deleted";
//    	}
//    	return "Bad request";
//    };
}

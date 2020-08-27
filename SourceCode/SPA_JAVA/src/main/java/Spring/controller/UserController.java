package Spring.controller;


import java.util.Optional;

import org.apache.commons.collections4.IterableUtils;
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

import Spring.bus.MessageResult;
import Spring.bus.PageableData;
import Spring.model.Staff;
import Spring.service.UserService;


@Controller
public class UserController {
    @Autowired
    private UserService userService;

    @GetMapping("/user/{pageSize}/{pageNumber}")
    @ResponseBody
    public Iterable<Staff> GetUserBySuperOutlet(@PathVariable("pageSize") int pageSize ,@PathVariable("pageNumber") int pageNumber) {
    	Iterable<Staff> resultStaff= userService.findAll();
        return PageableData.GetPage(resultStaff,pageSize,pageNumber);
    };
    
    @GetMapping("/user/{name}/{pageSize}/{pageNumber}")
    @ResponseBody
    public Iterable<Staff> GetUserByName(@PathVariable("name") String name,@PathVariable("pageSize") int pageSize ,@PathVariable("pageNumber") int pageNumber) {
    	Iterable<Staff> resultStaff= userService.findName(name);
        return PageableData.GetPage(resultStaff,pageSize,pageNumber);
        };
    
    @PostMapping("/user")
    @ResponseBody
    public MessageResult AddUserBySuperOutlet(@RequestBody Staff  user) {
    	if (user.getId() == null)
    	{
    		Iterable<Staff> resultStaff= userService.findUsername(user.getUsername());
    		if(IterableUtils.isEmpty(resultStaff))
    		{
	    		try {
	    		  userService.save(user);
	    		  return new MessageResult("Succeeded",true);
	    		}
	    		catch (Exception e) {
	    	         /* This is a generic Exception handler which means it can handle
	    	          * all the exceptions. This will execute if the exception is not
	    	          * handled by previous catch blocks.
	    	          */
	    	         System.out.println(e.getMessage());
	    	      }
    		}
    		else
    		return new MessageResult("User name already exists",false);
    	}
    	return new MessageResult("Bad request",false);
    };
     
    @PutMapping("/user")
    @ResponseBody
    public MessageResult EditServiceBySuperOutlet(@RequestBody Staff  user) {
		
		if (user.getId() != null)
    	{
	    	try {
	    		  userService.save(user);
	    		  return new MessageResult("Succeeded",true);
	    		}
	    		catch (Exception e) {
	    	         /* This is a generic Exception handler which means it can handle
	    	          * all the exceptions. This will execute if the exception is not
	    	          * handled by previous catch blocks.
	    	          */
	    	         System.out.println(e.getMessage());
	    	         return new MessageResult(e.getMessage(),false); 
	    	      }
    	}
    	return new MessageResult("Bad request",false);
    };
    
    
    
//    @DeleteMapping("/user")
//    @ResponseBody
//    public String delete(@RequestBody Staff  user) {
//    	
//    	if (user.getId() != null)
//    	{	
//    		Optional<Staff> temp= userService.findOne(user.getId());
//    		user =temp.get();
//    		user.setDeleted(1);
//    		userService.save(user);
//	    	 return "deleted";
//    	}
//    	return "Bad request";
//    };
}

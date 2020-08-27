package Spring.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseBody;

import Spring.bus.MessageResult;
import Spring.model.Services;
import Spring.service.ServiceService;


@Controller
public class ServiceController {
    @Autowired
    private ServiceService serviceService;


    
    @PostMapping("/service")
    @ResponseBody
    public MessageResult AddServiceBySuperOutlet(@RequestBody Services  service) {
    	if (service.getId() == null)
    	{
	    	try {
	    		serviceService.save(service);
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
    
    
    @PutMapping("/service")
    @ResponseBody
    public MessageResult EditServiceBySuperOutlet(@RequestBody Services  service) {
    	if (service.getId() != null)
    	{
	    	try {
	    		serviceService.save(service);
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
        
    
    @GetMapping("/service")
    @ResponseBody
    public Iterable<Services> index() {
    	Iterable<Services> a= serviceService.findAll();
        return a;
        };
//    @GetMapping("/service/{name}")
//    @ResponseBody
//    public Iterable<Services> indexName(@PathVariable("name") String name) {
//    	Iterable<Services> a= serviceService.findName(name);
//        return a;
//        };
//   
//    @DeleteMapping("/service")
//    @ResponseBody
//    public String delete(@RequestBody Services  service) {
//    	if (service.getId() != null)
//    	{	
//    		Optional<Services> temp= serviceService.findOne(service.getId());
//    		service =temp.get();
//    		service.setDeleted(1);
//    		serviceService.save(service);
//	    	 return "deleted";
//    	}
//    	return "Bad request";
//    };
}

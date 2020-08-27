package Spring.controller;
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

import Spring.bus.MessageResult;
import Spring.bus.PageableData;
import Spring.model.Outlet;
import Spring.service.OutletService;


@Controller
public class OutletController {
    @Autowired
    private OutletService outletService;
   
    @GetMapping("/outlet/all")
    @ResponseBody
    public Iterable<Outlet> GetUserBySuperOutletAll() {
    	Iterable<Outlet> resultOutlet= outletService.findAll();
        return resultOutlet;
        };
    
    @GetMapping("/outlet/{pageSize}/{pageNumber}")
    @ResponseBody
    public Iterable<Outlet> GetUserBySuperOutlet(@PathVariable("pageSize") int pageSize ,@PathVariable("pageNumber") int pageNumber) {
    	Iterable<Outlet> resultOutlet= outletService.findAll();
        return PageableData.GetPage(resultOutlet,pageSize,pageNumber);
        };
        
    @PostMapping("/outlet")
    @ResponseBody
    public MessageResult AddOutletBySuperOutlet(@RequestBody Outlet  outlet) {
    	if (outlet.getId() == null)
    	{
	    	try {
	    		outletService.save(outlet);
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
    
    
    @PutMapping("/outlet")
    @ResponseBody
    public MessageResult EditOutletBySuperOutlet(@RequestBody Outlet  outlet) {
    	if (outlet.getId() != null)
    	{
	    	try {
	    		outletService.save(outlet);
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
    
    
    
//    support funtion
//    @GetMapping("/outlet")
//    @ResponseBody
//    public Iterable<Outlet> GetOutletByOutlet() {
//    	Iterable<Outlet> resultOutlet= outletService.findAll();
//        return resultOutlet;
//        };
//    
//    @GetMapping("/outlet/{name}")
//    @ResponseBody
//    public Iterable<Outlet> indexName(@PathVariable("name") String name) {
//    	Iterable<Outlet> a= outletService.findName(name);
//        return a;
//        };
//   
//    @DeleteMapping("/outlet")
//    @ResponseBody
//    public String delete(@RequestBody Outlet  outlet) {
//    	if (outlet.getId() != null)
//    	{	
//    		Optional<Outlet> temp= outletService.findOne(outlet.getId());
//    		outlet =temp.get();
//    		outlet.setDeleted(1);
//    		outletService.save(outlet);
//	    	 return "deleted";
//    	}
//    	return "Bad request";
//    };
    
    
}

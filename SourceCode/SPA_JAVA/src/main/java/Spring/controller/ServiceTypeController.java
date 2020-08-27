package Spring.controller;

import Spring.model.Servicetype;
import Spring.service.ServiceTypeService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ResponseBody;


@Controller
public class ServiceTypeController {
    @Autowired
    private ServiceTypeService serviceTypeService;

    @GetMapping("/servicetype")
    @ResponseBody
    public Iterable<Servicetype> GetServiceTypeByOutlet() {
    	Iterable<Servicetype> resultServicetype =  serviceTypeService.findAll();
        return resultServicetype;
    };
}

package com.example.demo.controller;

import javax.validation.Valid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.mvc.support.RedirectAttributes;

import com.example.demo.model.Customers;
import com.example.demo.service.CustomersService;

@Controller
public class CustomersController {
	@Autowired
	private CustomersService customersService;

	@GetMapping("/customers")
	public String index(Model model) {
		model.addAttribute("customers", customersService.findAll());
		return "list";
	}

	@GetMapping("/customers/create")
	public String create(Model model) {
		model.addAttribute("customers", new Customers());
		return "form";
	}

	@GetMapping("/customers/{id}/edit")
	public String edit(@PathVariable int id, Model model) {
		model.addAttribute("employee", customersService.findOne(id));
		return "form";
	}

	@PostMapping("/customers/save")
	public String save(@Valid Customers customers, BindingResult result, RedirectAttributes redirect) {
		if (result.hasErrors()) {
			return "form";
		}
		customersService.save(customers);
		redirect.addFlashAttribute("success", "Saved customer successfully!");
		return "redirect:/customers";
	}

	@GetMapping("/customers/{id}/delete")
	public String delete(@PathVariable int id, RedirectAttributes redirect) {
		customersService.delete(id);
		redirect.addFlashAttribute("success", "Deleted customer successfully!");
		return "redirect:/customers";
	}

	@GetMapping("/customers/search")
	public String search(@RequestParam("s") String s, Model model) {
		if (s.equals("")) {
			return "redirect:/customers";
		}

		model.addAttribute("customers", customersService.search(s));
		return "list";
	}
}
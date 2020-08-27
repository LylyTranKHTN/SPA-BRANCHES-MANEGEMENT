package com.example.demo.service;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.demo.model.Customers;
import com.example.demo.repository.CustomersRepository;

@Service
public class CustomersServiceImpl implements CustomersService {
	@Autowired
    private CustomersRepository customersRepository;

    @Override
    public Iterable<Customers> findAll() {
        return customersRepository.findAll();
    }

    @Override
    public List<Customers> search(String q) {
        return customersRepository.findByNameContaining(q);
    }

    @Override
    public Customers  findOne(int id) {
    	Customers a= new Customers();
        return a;
    }

    @Override
    public void save(Customers contact) {
    	customersRepository.save(contact);
    }

    @Override
    public void delete(int id) {
    	customersRepository.deleteById(id);
    }
}
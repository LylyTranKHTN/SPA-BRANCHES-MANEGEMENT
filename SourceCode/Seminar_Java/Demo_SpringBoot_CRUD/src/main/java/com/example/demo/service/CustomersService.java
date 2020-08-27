package com.example.demo.service;

import java.util.List;
import com.example.demo.model.Customers;

public interface CustomersService {
	
	Iterable<Customers> findAll();

    List<Customers> search(String q);

    Customers findOne(int id);

    void save(Customers contact);

    void delete(int id);
}
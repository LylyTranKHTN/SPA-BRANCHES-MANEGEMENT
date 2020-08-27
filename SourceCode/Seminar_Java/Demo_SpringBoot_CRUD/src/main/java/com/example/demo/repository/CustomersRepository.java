package com.example.demo.repository;
import java.util.List;

import org.springframework.data.repository.CrudRepository;

import com.example.demo.model.Customers;

public interface CustomersRepository extends CrudRepository<Customers, Integer> {

    List<Customers> findByNameContaining(String q);
}

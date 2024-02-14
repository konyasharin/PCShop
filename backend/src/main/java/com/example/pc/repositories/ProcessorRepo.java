package com.example.pc.repositories;

import com.example.pc.Models.ProcessorModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ProcessorRepo extends JpaRepository<ProcessorModel, Long> {
}

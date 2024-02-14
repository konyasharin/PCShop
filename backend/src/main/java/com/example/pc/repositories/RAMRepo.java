package com.example.pc.repositories;

import com.example.pc.Models.RAMModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RAMRepo extends JpaRepository<RAMModel, Long> {
}

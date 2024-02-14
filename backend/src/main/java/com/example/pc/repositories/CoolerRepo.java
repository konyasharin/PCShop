package com.example.pc.repositories;

import com.example.pc.Models.CoolerModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface CoolerRepo extends JpaRepository<CoolerModel, Long> {
}

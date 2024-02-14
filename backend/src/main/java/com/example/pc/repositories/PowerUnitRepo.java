package com.example.pc.repositories;

import com.example.pc.Models.PowerUnitModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PowerUnitRepo extends JpaRepository<PowerUnitModel, Long> {
}

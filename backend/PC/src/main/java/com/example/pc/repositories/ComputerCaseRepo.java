package com.example.pc.repositories;

import com.example.pc.Models.ComputerCaseModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ComputerCaseRepo extends JpaRepository<ComputerCaseModel, Long> {
}

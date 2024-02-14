package com.example.pc.repositories;

import com.example.pc.Models.MotherBoardModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface MotherBoardRepo extends JpaRepository<MotherBoardModel, Long> {

}

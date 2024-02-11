package com.example.pc.repositories;

import com.example.pc.Models.SSDModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface SSDRepo extends JpaRepository<SSDModel, Long> {
}
